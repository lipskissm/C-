using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CryptoLibrary
{
    public class CryptoApi
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string _baseUrl = "https://api.coinlayer.com";

        public async Task<Dictionary<string, decimal>> FetchCryptoPricesByDate(List<string> symbols, DateTime? date)
        {
            string dateString = date?.ToString("yyyy-MM-dd") ?? "live";
            string endpoint = $"{_baseUrl}/{dateString}?access_key=058e86578b8879aa9cbe3ac580f93e8b";

            if (symbols != null && symbols.Count > 0)
            {
                string symbolString = string.Join(",", symbols);
                endpoint += $"&symbols={symbolString}";
            }

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var cryptoResponse = JsonConvert.DeserializeObject<CryptoResponse>(jsonResponse);
                return cryptoResponse?.Rates ?? new Dictionary<string, decimal>();
            }

            return new Dictionary<string, decimal>();
        }

        public async Task<Dictionary<string, decimal>> FetchCurrentCryptoPrices()
        {
            string endpoint = $"{_baseUrl}/live?access_key=058e86578b8879aa9cbe3ac580f93e8b";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var cryptoResponse = JsonConvert.DeserializeObject<CryptoResponse>(jsonResponse);
                return cryptoResponse?.Rates ?? new Dictionary<string, decimal>();
            }

            return new Dictionary<string, decimal>();
        }
    }
}
