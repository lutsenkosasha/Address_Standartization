using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AddressStandardization
{
    public class DadataService
    {
        private readonly HttpClient _httpClient;

        public DadataService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DadataClient");

            // Установка заголовков с API и секретным ключами
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Token b306f7d3371438b1d3056e7c00661ceabe4dd785");
            _httpClient.DefaultRequestHeaders.Add("X-Secret", "7bd5bc3a576311d424cb5d483ba81687a7203b6c");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AddressResult> CleanAddress(string rawAddress)
        {
            var content = new StringContent($"[\"{rawAddress}\"]", Encoding.UTF8, "application/json");

            // Выполняем POST-запрос к API Dadata
            var response = await _httpClient.PostAsync("address", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ошибка при обращении к Dadata API: {response.StatusCode}, {errorContent}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var addressResults = JsonConvert.DeserializeObject<List<AddressResult>>(jsonResponse);
            return addressResults?.FirstOrDefault();
        }
    }
}