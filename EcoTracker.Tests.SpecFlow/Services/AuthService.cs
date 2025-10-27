using Newtonsoft.Json;
using System.Text;
using TechTalk.SpecFlow;

namespace EcoTracker.Tests.SpecFlow.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient? httpClient = null)
        {
            // Permite injetar HttpClient ou usar um novo
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<string> LoginAsync(Table table)
        {
            // Converte a tabela de SpecFlow em um dicionário
            var loginData = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                loginData[row["campo"]] = row["valor"];
            }

            var loginPayload = new
            {
                email = loginData["email"],
                password = loginData["senha"]
            };

            string json = JsonConvert.SerializeObject(loginPayload);

            // Envia requisição de login
            var response = await _httpClient.PostAsync(
                "https://localhost:8081/api/login",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();

            var resultJson = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<dynamic>(resultJson).token;

            return token;
        }
    }
}
