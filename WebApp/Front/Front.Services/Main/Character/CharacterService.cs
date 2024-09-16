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
                var Characters = await ApiService.SendRequestAsync<Guid, List<Player>>("api/Character/GetByUserId", HttpMethod.Post, UserUniqueId);
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
                var Characters = await ApiService.SendRequestAsync<Guid, List<Player>>("api/Character/GetByUserUniqueId", HttpMethod.Post, UserUniqueId);
                return Characters;
            }
            catch
            {
                throw;
            }
        }

    }
}
