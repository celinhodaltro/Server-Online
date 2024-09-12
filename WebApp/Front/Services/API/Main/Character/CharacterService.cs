using Blazored.LocalStorage;
using Server.Entities;
using System.Text;
using System.Text.Json;


namespace WebApp.Services.API.Main
{
    public class CharacterService(IHttpClientFactory httpClientFactory)
    {

        public async Task<Player?> Create(Player? Character)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("API");
                var characterAsJson = JsonSerializer.Serialize(Character);
                var requestContent = new StringContent(characterAsJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/Player/Create", requestContent);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception(responseContent);

               Character = JsonSerializer.Deserialize<Player>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return Character;

            }
            catch
            {
                throw;
            }

        }

        public async Task<List<Player>> GetCharacters(int UserId)
        {
            try
            {

                var httpClient = httpClientFactory.CreateClient("API");
                var UserIdAsJson = JsonSerializer.Serialize(UserId);
                var requestContent = new StringContent(UserIdAsJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/Player/Create", requestContent);

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

    }
}
