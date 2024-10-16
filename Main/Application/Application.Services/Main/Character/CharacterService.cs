﻿using Blazored.LocalStorage;
using Server.Entities;
using System.Net.Http;
using System.Text;
using System.Text.Json;


namespace Application.Services
{
    public class CharacterService : AuthService
    {
        public CharacterService(ApiService ApiService, ILocalStorageService LocalStorage, IHttpClientFactory HttpClientFactory) : base(ApiService, LocalStorage, HttpClientFactory)
        {
        }


        public async Task<bool> CreateCharacter(Character? Character)
        {
            return await ApiService.SendRequestAsync<Character, bool>("api/Character/Register", HttpMethod.Post, Character);
        }


        

        public async Task<List<Character>?> GetCharacters(int UserId)
        {
            var Characters = await ApiService.SendRequestAsync<int, List<Character>>("api/Character/GetByUserId", HttpMethod.Get, UserId);
            return Characters;
        }

        public async Task<List<Character>?> GetCharacters(Guid UserUniqueId)
        {
            var Characters = await ApiService.SendRequestAsync<Guid, List<Character>>($"api/Character/User/{UserUniqueId}", HttpMethod.Get);
            return Characters;
        }

    }
}
