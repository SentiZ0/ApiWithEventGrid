using System.Text;
using System.Text.Json;
using ApiWithEventGrid.Models;

namespace ApiWithEventGrid.Services
{
    public class EventGridService
    {
        private readonly HttpClient _httpClient;

        public EventGridService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("eventgrid");
        }

        public async Task PublishUserEventAsync(UserDto user)
        {
            var eventData = new[]
            {
                new
                {
                    id = new Guid().ToString(),
                    eventType = "UserRegistered",
                    subject = $"NewUser/{user.Email}",
                    eventTime = DateTime.UtcNow,
                    data = user,
                    dataVersion = "1.0"
                }
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(eventData), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress)
            {
                Content = jsonContent
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}