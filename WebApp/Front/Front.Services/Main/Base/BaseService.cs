﻿using System.Text.Json;
using System.Text;
using System.Runtime.InteropServices;

public class ApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(string apiPath, HttpMethod method, [Optional] TRequest requestContent)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("API");

            // Serializando o objeto de request, se existir
            StringContent requestBody = null;
            if (requestContent != null)
            {
                var contentAsJson = JsonSerializer.Serialize(requestContent);
                requestBody = new StringContent(contentAsJson, Encoding.UTF8, "application/json");
            }

            // Preparando a requisição
            var requestMessage = new HttpRequestMessage(method, apiPath);
            if (method != HttpMethod.Get && requestBody != null)
            {
                requestMessage.Content = requestBody;
            }

            // Enviando a requisição
            var response = await httpClient.SendAsync(requestMessage);

            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {responseContent}");
            }

            // Deserializando a resposta
            return JsonSerializer.Deserialize<TResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            // Logar ou lidar com a exceção
            throw;
        }
    }
}