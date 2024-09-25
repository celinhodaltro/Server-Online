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


        public async Task CreateCharacter(Character? Character)
        {
            try
            {
                var result = await ApiService.SendRequestAsync<Character, object>("api/Player", HttpMethod.Post, Character);
                Console.WriteLine("Player created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        

        public async Task<List<Character>> GetCharacters(int UserId)
        {
            var Characters = await ApiService.SendRequestAsync<int, List<Character>>("api/Character/GetByUserId", HttpMethod.Post, UserId);
            return Characters;
        }

        public async Task<List<Character>> GetCharacters(Guid UserUniqueId)
        {
            var Characters = await ApiService.SendRequestAsync<Guid, List<Character>>("api/Character/GetByUserUniqueId", HttpMethod.Post, UserUniqueId);
            return Characters;
        }

    }
}
