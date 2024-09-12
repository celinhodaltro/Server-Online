using Blazored.LocalStorage;
using Server.Entities;
using System.Net.Http;
using System.Text;
using System.Text.Json;


namespace Front.Services
{
    public class CharacterService : AuthService
    {
        public CharacterService(ApiService ApiService, ILocalStorageService LocalStorage, IHttpClientFactory HttpClientFactory) : base(ApiService, LocalStorage, HttpClientFactory)
        {
        }


        public async Task CreateCharacter(Player? Character)
        {
            try
            {
                var result = await ApiService.SendRequestAsync<Player, object>("api/Player", HttpMethod.Post, Character);
                Console.WriteLine("Player created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        

        public async Task<List<Player>> GetCharacters(int UserId)
        {
            try
            {

                var httpClient = HttpClientFactory.CreateClient("API");
                var UserIdAsJson = JsonSerializer.Serialize(UserId);
                var requestContent = new StringContent(UserIdAsJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/Character/GetById", requestContent);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception(responseContent);

                var Characters = JsonSerializer.Deserialize<List<Player>>(responseContent,
                     new JsonSerializerOptions
                     {
                         PropertyNameCaseInsensitive = true
                     });

                return Characters;

            }
            catch
            {
                throw;
            }

        }

        public async Task<List<Player>> GetCharacters(Guid UserUniqueId)
        {
            try
            {
                var player = await ApiService.SendRequestAsync<Guid, List<Player>>("api/Character/GetByUserUniqueId", HttpMethod.Post, UserUniqueId);
                return player;
            }
            catch
            {
                throw;
            }

        }

    }
}
