
using ExternalClients.Options;
using ExternalClients.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;

namespace ExternalClients
{
    public class BallDontLieClient(HttpClient httpClient, IOptions<BallDontLieClientOptions> configOptions)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly BallDontLieClientOptions _options = configOptions.Value;

        private void SetHeaders() 
        {
            if(_httpClient.BaseAddress is null)
                _httpClient.BaseAddress = new Uri(_options.BaseUrl);

            _httpClient.DefaultRequestHeaders.Clear();

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", _options.ApiKey);
        }

        public async Task GetTodaysGame() 
        {
            SetHeaders();
            var today = DateTime.Today.ToString("yyyy-MM-dd");
            var res = await _httpClient.GetAsync($"/nba/v1/games?dates[]={today}");

            if (!res.IsSuccessStatusCode) 
            {
                Console.WriteLine("Bad Request");
            }
            var content = await res.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        public async Task<List<PlayerInfoResponse>> GetAllPlayers()
        {
            SetHeaders();
            var res = await _httpClient.GetAsync("/v1/players?first_name=michael&cursor=200");

            if (!res.IsSuccessStatusCode) 
            {
            
            }
            var content = await res.Content.ReadFromJsonAsync<GetAllPlayersResponse>();
            return content.data;
        }

    }
}
