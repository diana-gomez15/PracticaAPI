using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using RawPostres.Model;

namespace RawPostres.Services
{
    public class PostresApiService
    {
        private readonly HttpClient _httpClient;

        // URL de TU API (local)
        private const string BaseUrl = "http://localhost:3000/api";

        private readonly JsonSerializerOptions _jsonOptions;

        public PostresApiService()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // Obtener todos los postres (equivalente a GetGamesAsync)
        public async Task<PostresResponse?> GetPostresAsync()
        {
            try
            {
                var url = $"{BaseUrl}/postres";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PostresResponse>(json, _jsonOptions);
                }
                else
                {
                    Console.WriteLine($"Error en GetPostresAsync: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching postres: {ex.Message}");
                return null;
            }
        }

        // Buscar postres por nombre (equivalente a SearchGamesAsync)
        public async Task<PostresResponse?> SearchPostresAsync(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                    return await GetPostresAsync();

                var url = $"{BaseUrl}/postres/search?nombre={Uri.EscapeDataString(query)}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PostresResponse>(json, _jsonOptions);
                }
                else
                {
                    Console.WriteLine($"Error en SearchPostresAsync: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching postres: {ex.Message}");
                return null;
            }
        }
    }
}