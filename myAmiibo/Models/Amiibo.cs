using System;
using System.Collections.Generic;
using System.Text.Json.Serialization; // <--- ESTO ES IMPORTANTE

namespace myAmiibo.Models
{
    public class AmiiboResponse
    {
        // El JSON dice "amiibo", nosotros queremos "Amiibo"
        [JsonPropertyName("amiibo")]
        public List<Amiibo> Amiibo { get; set; }
    }

    public class Amiibo
    {
        // Mapeamos "name" (JSON) a "Name" (C#)
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }

        [JsonPropertyName("gameSeries")]
        public string GameSeries { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; } // URL de la imagen

        [JsonPropertyName("head")]
        public string Head { get; set; }

        [JsonPropertyName("tail")]
        public string Tail { get; set; }
    }
}