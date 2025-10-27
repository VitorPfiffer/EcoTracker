using System.Text;
using TechTalk.SpecFlow;

namespace EcoTracker.Tests.SpecFlow.Steps
{
    [Binding]
    public class PickUpScheduleSteps
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string _baseUrl = "http://localhost:8081/";
        private readonly ScenarioContext _scenarioContext;
        public PickUpScheduleSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [When(@"eu envio uma requisição POST para ""(.*)"" com o payload:")]
        public async Task RequisicaoPostComPayload(string endpoint, string payloadJson)
        {
            string teste = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            payloadJson = payloadJson.Replace("now", teste);

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(new Uri(_baseUrl), endpoint))
            {
                Content = new StringContent(payloadJson, Encoding.UTF8, "application/json")
            };

            // Se tiver token no contexto, adiciona no header
            if (_scenarioContext.ContainsKey("token"))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _scenarioContext["token"] as string);

            var response = await _httpClient.SendAsync(request);
            _scenarioContext["response"] = response;
            _scenarioContext["apiResponse"] = await response.Content.ReadAsStringAsync();
        }
        [When(@"eu envio uma requisição PUT para ""(.*)"" com o payload:")]
        public async Task RequisicaoPutComPayload(string endpoint, string payloadJson)
        {
            string teste = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            payloadJson = payloadJson.Replace("now", teste);

            var request = new HttpRequestMessage(HttpMethod.Put, new Uri(new Uri(_baseUrl), endpoint))
            {
                Content = new StringContent(payloadJson, Encoding.UTF8, "application/json")
            };

            // Se tiver token no contexto, adiciona no header
            if (_scenarioContext.ContainsKey("token"))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _scenarioContext["token"] as string);

            var response = await _httpClient.SendAsync(request);
            _scenarioContext["response"] = response;
            _scenarioContext["apiResponse"] = await response.Content.ReadAsStringAsync();
        }

        [When(@"eu envio uma requisição DELETE para ""(.*)""")]
        public async Task RequisicaoDelete(string endpoint)
        {


            var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(new Uri(_baseUrl), endpoint))
            {
            };

            // Se tiver token no contexto, adiciona no header
            if (_scenarioContext.ContainsKey("token"))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _scenarioContext["token"] as string);

            var response = await _httpClient.SendAsync(request);
            _scenarioContext["response"] = response;
            _scenarioContext["apiResponse"] = await response.Content.ReadAsStringAsync();
        }
    }
}
