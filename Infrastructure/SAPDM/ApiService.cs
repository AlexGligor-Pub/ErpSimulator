using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Infrastructure.SAPDM
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public ApiService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<HttpResponseMessage> PostDataAsync(string url, string data)
        {
            
            var token = await _authService.GetAccessTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }

}
