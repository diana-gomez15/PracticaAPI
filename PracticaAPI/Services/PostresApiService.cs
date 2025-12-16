using System;
using System.Text.Json;
using System.Threading.Tasks;
using RawPostres.Model;

namespace RawPostres.Services
{
    public class PostresApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

#if ANDROID
        private const string BaseUrl = "http://10.0.2.2:3000/api";
#else
        private const string BaseUrl = "http://localhost:3000/api";
#endif

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

        public async Task<PostresResponse?> GetPostresAsync()
        {
            try
            {
                var url = $"{BaseUrl}/postres";
                Console.WriteLine($"🔍 Fetching from: {url}");

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"✅ Response: {json}");
                    return JsonSerializer.Deserialize<PostresResponse>(json, _jsonOptions);
                }
                else
                {
                    Console.WriteLine($"❌ Error: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception: {ex.Message}");
                return null;
            }
        }

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
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching: {ex.Message}");
                return null;
            }
        }
    }
}

