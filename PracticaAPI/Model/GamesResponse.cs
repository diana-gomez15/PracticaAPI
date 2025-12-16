using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RawPostres.Model
{
    // Clase base (equivalente a Game)
    public class Postre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("imagen")]
        public string Imagen { get; set; } = string.Empty;

        [JsonPropertyName("precio")]
        public decimal? Precio { get; set; }

        // Propiedad calculada (igual que MetacriticFormatted)
        public string PrecioFormateado =>
            Precio.HasValue ? $"Precio: ${Precio}" : "Sin precio";
    }

    // Detalle (equivalente a GameDetail)
    public class PostreDetail : Postre
    {
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
    }

    // Response (equivalente a GamesResponse)
    public class PostresResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("results")]
        public List<Postre> Results { get; set; } = new();
    }
}