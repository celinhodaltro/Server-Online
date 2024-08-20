using Blazored.LocalStorage;
using Intersoft.Crosslight;
using Microsoft.AspNetCore.Components.Authorization;
using Server.Entities;
using System.Net.Http;
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

        private bool TokenExpired(String dataToken)
        {
            DateTime dataNowUtc = DateTime.UtcNow;
            DateTime dataExpiration =
                DateTime.ParseExact(dataToken, "yyyy-MM-dd'T'HH>mm:ss.fffffff'Z'", null,System.Globalization.DateTimeStyles.RoundtripKind);

            if( dataExpiration < dataNowUtc)
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
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);

            if( roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());
                    foreach(var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }
                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(x => new Claim(x.Key, x.Value)));

            return claims;

        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch( base64.Length % 4)
            {
                case 2: base64 += "=="; break; 
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task<LoginResult> Login(User User)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("API");
                var loginAsJson = JsonSerializer.Serialize(User);
                var requestContent = new StringContent(loginAsJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/User/Login", requestContent);

                var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return loginResult;
                
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
                UserToken? loginResult = JsonSerializer.Deserialize<UserToken>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return loginResult;

            }
            catch 
            {
                throw;
            }

        }

        public async Task Loggout()
        {
            var httpClient = httpClientFactory.CreateClient("API");
            await localStorage.RemoveItemAsync("authToken");

            this.MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

    }
}
