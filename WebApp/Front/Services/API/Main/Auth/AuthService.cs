using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Server.Entities;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace WebApp.Services.API.Main
{
    public class AuthService : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly IHttpClientFactory httpClientFactory;
        public AuthService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
        {
            this.localStorage = localStorage;
            this.httpClientFactory = httpClientFactory;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await localStorage.GetItemAsync<string>("authToken");
            var expirationToken = await localStorage.GetItemAsync<string>("tokenExpiration");

            if(string.IsNullOrEmpty(savedToken)|| TokenExpired(expirationToken))
            {
                MarkUserAsLoggedOut();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
        }

        public async Task<bool> IsAuthenticated()
        {
            var savedToken = await localStorage.GetItemAsync<string>("authToken");
            var expirationToken = await localStorage.GetItemAsync<string>("tokenExpiration");

            if (string.IsNullOrEmpty(savedToken) || TokenExpired(expirationToken))
            {
                return false;
            }

            return true;
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

        public async Task<UserToken> Login(User User)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("API");
                var loginAsJson = JsonSerializer.Serialize(User);
                var requestContent = new StringContent(loginAsJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/User/Login", requestContent);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception(responseContent);


                var UserToken = JsonSerializer.Deserialize<UserToken>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                await AuthUserByToken(UserToken, User?.Email);


                return UserToken;
                
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        public async Task<UserToken> Register(User User)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("API");
                var loginAsJson = JsonSerializer.Serialize(User);
                var requestContent = new StringContent(loginAsJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/User/Register", requestContent);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception(responseContent);

                UserToken? UserToken = JsonSerializer.Deserialize<UserToken>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                await AuthUserByToken(UserToken, User?.Email);
                return UserToken;

            }
            catch 
            {
                throw;
            }

        }
        public async Task AuthUserByToken(UserToken? UserToken, [Optional] string Email)
        {
            await localStorage.SetItemAsync("authToken", UserToken.Token);
            await localStorage.SetItemAsync("tokenExpiration", UserToken.Expiration);

            if (!String.IsNullOrEmpty(Email))
                MarkUserAsAuthenticated(Email);
        }

        public async Task Loggout()
        {
            var httpClient = httpClientFactory.CreateClient("API");
            await localStorage.RemoveItemAsync("authToken");

            this.MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> GetUserId()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                var userIdClaim = user.FindFirst("UserId");
                return userIdClaim?.Value;
            }

            return null;
        }


    }
}
