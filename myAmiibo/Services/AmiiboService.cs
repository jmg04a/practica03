using myAmiibo.Models;
using System.Net.Http.Json;
using System.Text.Json; // <--- ASEGURATE DE TENER ESTE USING

namespace myAmiibo.Services
{
    public class AmiiboService
    {
        private readonly HttpClient _httpClient;

        public AmiiboService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Amiibo>> GetAmiibosAsync()
        {
            var url = "https://www.amiiboapi.com/api/amiibo/";

            // CONFIGURACIÓN DE SEGURIDAD:
            // Esto le dice a C# que no le importen las mayúsculas/minúsculas
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                // Pasamos las 'options' aquí
                var response = await _httpClient.GetFromJsonAsync<AmiiboResponse>(url, options);

                // DEBUG: Esto imprimirá en la consola de Visual Studio cuántos descargó
                var cantidad = response?.Amiibo?.Count ?? 0;
                System.Diagnostics.Debug.WriteLine($"--------> SE DESCARGARON: {cantidad} AMIIBOS <--------");

                return response?.Amiibo ?? new List<Amiibo>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"--------> ERROR FATAL: {ex.Message} <--------");
                return new List<Amiibo>();
            }
        }
    }
}