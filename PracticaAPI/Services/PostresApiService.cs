using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RawPostres.Model;

namespace RawPostres.Services
{
    public class PostresApiService
    {
        public async Task<PostresResponse?> GetPostresAsync()
        {
            await Task.Delay(300);

            var postres = new List<Postre>
            {
                new Postre
                {
                    Id = 1,
                    Nombre = "Tiramisú",
                    Imagen = "tiramisu.jpg",
                    Precio = 85.50m
                },
                new Postre
                {
                    Id = 2,
                    Nombre = "Cheesecake de Fresa",
                    Imagen = "cheesecake_fresa.jpg",
                    Precio = 95.00m
                },
                new Postre
                {
                    Id = 3,
                    Nombre = "Brownie con Helado",
                    Imagen = "brownie_helado.jpg",
                    Precio = 75.00m
                },
                new Postre
                {
                    Id = 4,
                    Nombre = "Tarta de Limón",
                    Imagen = "tarta_limon.jpg",
                    Precio = 80.00m
                },
                new Postre
                {
                    Id = 5,
                    Nombre = "Flan Napolitano",
                    Imagen = "flan_napolitano.jpg",
                    Precio = 65.00m
                },
                new Postre
                {
                    Id = 6,
                    Nombre = "Mousse de Chocolate",
                    Imagen = "mousse_chocolate.jpg",
                    Precio = 70.00m
                },
                new Postre
                {
                    Id = 7,
                    Nombre = "Tres Leches",
                    Imagen = "tres_leches.jpg",
                    Precio = 90.00m
                },
                new Postre
                {
                    Id = 8,
                    Nombre = "Macarons Franceses",
                    Imagen = "macarons.jpg",
                    Precio = 120.00m
                }
            };

            return new PostresResponse
            {
                Count = postres.Count,
                Results = postres
            };
        }

        public async Task<PostresResponse?> SearchPostresAsync(string query)
        {
            var response = await GetPostresAsync();

            if (string.IsNullOrWhiteSpace(query))
                return response;

            var filtered = response.Results
                .Where(p => p.Nombre.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return new PostresResponse
            {
                Count = filtered.Count,
                Results = filtered
            };
        }
    }
}