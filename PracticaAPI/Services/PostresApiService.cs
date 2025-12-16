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
        private readonly JsonSerializerOptions _jsonOptions;

        // URL de TU API según la plataforma
        private const string AndroidLocalUrl = "http://10.0.2.2:3000/api";  // Emulador Android
        private const string iOSLocalUrl = "http://localhost:3000/api";      // Simulador iOS
        private const string WindowsLocalUrl = "http://localhost:3000/api";  // Windows
        
        // Cambia esta URL a tu API desplegada o usa la IP local
        private const string RemoteUrl = "https://tu-api-deployada.com/api";

        private string BaseUrl
        {
            get
            {
#if ANDROID
                return AndroidLocalUrl;
#elif IOS
                return iOSLocalUrl;
#else
                return WindowsLocalUrl;
#endif
            }
        }

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

        // Obtener todos los postres
        public async Task<PostresResponse?> GetPostresAsync()
        {
            try
            {
                var url = $"{BaseUrl}/postres";
                Console.WriteLine($"Fetching from: {url}");
                
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {json}");
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
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return null;
            }
        }

        // Buscar postres por nombre
        public async Task<PostresResponse?> SearchPostresAsync(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                    return await GetPostresAsync();

                var url = $"{BaseUrl}/postres/search?nombre={Uri.EscapeDataString(query)}";
                Console.WriteLine($"Searching at: {url}");
                
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

        // Obtener detalle de un postre por ID
        public async Task<PostreDetail?> GetPostreDetailAsync(int id)
        {
            try
            {
                var url = $"{BaseUrl}/postres/{id}";
                Console.WriteLine($"Fetching detail from: {url}");
                
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PostreDetail>(json, _jsonOptions);
                }
                else
                {
                    Console.WriteLine($"Error en GetPostreDetailAsync: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching postre detail: {ex.Message}");
                return null;
            }
        }
    }
}
