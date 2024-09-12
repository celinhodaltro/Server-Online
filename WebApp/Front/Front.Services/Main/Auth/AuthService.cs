using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Server.Entities;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace Front.Services
{
    public class AuthService : AuthenticationStateProvider
    {
        protected readonly ILocalStorageService LocalStorage;
        protected readonly IHttpClientFactory HttpClientFactory;
        protected readonly ApiService ApiService;
        public AuthService(ApiService ApiService, ILocalStorageService LocalStorage, IHttpClientFactory HttpClientFactory)
        {
            this.LocalStorage = LocalStorage;
            this.HttpClientFactory = HttpClientFactory;
            this.ApiService = ApiService;
        }


        #region Auth

        public async Task<UserToken> Login(User user)
        {
            var userToken = await ApiService.SendRequestAsync<User, UserToken>("api/User/Login", HttpMethod.Post, user);

            await AuthUserByToken(userToken, user?.Email);

            return userToken;
        }

        public async Task<UserToken> Register(User user)
        {
            var userToken = await ApiService.SendRequestAsync<User, UserToken>("api/User/Register", HttpMethod.Post, user);
            await AuthUserByToken(userToken, user?.Email);

            return userToken;
        }


        public async Task<int> GetUserId()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                var userIdClaim = user.FindFirst("UserId");
                return Convert.ToInt32(userIdClaim?.Value);
            }

            return 0;
        }

        public async Task<bool> IsAuthenticated()
        {
            var savedToken = await LocalStorage.GetItemAsync<string>("authToken");
            var expirationToken = await LocalStorage.GetItemAsync<string>("tokenExpiration");

            if (string.IsNullOrEmpty(savedToken) || TokenExpired(expirationToken))
            {
                return false;
            }

            return true;
        }

        #endregion


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await LocalStorage.GetItemAsync<string>("authToken");
            var expirationToken = await LocalStorage.GetItemAsync<string>("tokenExpiration");

            if(string.IsNullOrEmpty(savedToken)|| TokenExpired(expirationToken))
            {
                MarkUserAsLoggedOut();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
        }




        public void MarkUserAsAuthenticated(String email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, email)
            }, "apiauth"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }
        
        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }

        private bool TokenExpired(String dateToken)
        {
            DateTime dataNowUtc = DateTime.UtcNow;
            DateTime dateTokenExpiration = Convert.ToDateTime(dateToken).ToUniversalTime();

            if(dateTokenExpiration < dataNowUtc)
            {
                return true;
            }
            return false;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonBytes);

            if (keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles))
            {
                if (roles.ValueKind == JsonValueKind.Array)
                {
                    foreach (var role in roles.EnumerateArray())
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.GetString()));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.GetString()));
                }
                keyValuePairs.Remove(ClaimTypes.Role);
            }

            foreach (var kvp in keyValuePairs)
            {
                if (kvp.Value.ValueKind == JsonValueKind.String)
                {
                    claims.Add(new Claim(kvp.Key, kvp.Value.GetString()));
                }
                else
                {

                }
            }

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }







        public async Task AuthUserByToken(UserToken? UserToken, [Optional] string Email)
        {
            if (UserToken?.Token == null || UserToken?.Expiration == null)
                MarkUserAsLoggedOut();
            else
            {
                await LocalStorage.SetItemAsync("authToken", UserToken.Token);
                await LocalStorage.SetItemAsync("tokenExpiration", UserToken.Expiration);

                if (!String.IsNullOrEmpty(Email))
                    MarkUserAsAuthenticated(Email);
            }
        }

        public async Task Loggout()
        {
            var httpClient = HttpClientFactory.CreateClient("API");
            await LocalStorage.RemoveItemAsync("authToken");

            this.MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }




    }
}
