using Newtonsoft.Json;
using System.Text;
using TechTalk.SpecFlow;

namespace MyApp.Tests.SpecFlow.Steps
{

    [Binding]
    public class LoginBemSucedidoSteps
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private HttpResponseMessage _response;
        private string _token;
        private string _email;
        private string _senha;

        [Given(@"que eu informei o email ""(.*)""")]
        public void DadoQueEuInformeiOEmail(string email)
        {
            _email = email;
        }

        [Given(@"a senha ""(.*)""")]
        public void DadoASenha(string senha)
        {
            _senha = senha;
        }

        [When(@"eu envio a requisição para ""(.*)""")]
        public async Task QuandoEuEnvioARequisicaoPara(string endpoint)
        {
            var payload = new { email = _email, password = _senha };
            string json = JsonConvert.SerializeObject(payload);

            string url = $"http://localhost:8081/{endpoint}";
            Console.WriteLine(url);
            _response = await _httpClient.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json-patch+json")
            );

            if (_response.IsSuccessStatusCode)
            {
                var content = await _response.Content.ReadAsStringAsync();
                _token = JsonConvert.DeserializeObject<dynamic>(content).data;
            }
        }

        [Then(@"devo receber o status code (.*)")]
        public void EntaoDevoReceberOStatusCode(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)_response.StatusCode);
        }

        [Then(@"o token de autenticação deve ser retornado")]
        public void EntaoOTokenDeAutenticacaoDeveSerRetornado()
        {
            Assert.False(string.IsNullOrEmpty(_token), "Token não foi retornado.");
        }


        [Then(@"o erro deve ser retornado")]
        public async Task EntaoOErroDeveSerRetornado()
        {
            var content = await _response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject<dynamic>(content);

            Assert.True(json.errors != null && json.errors.Count > 0, "O array 'errors' está vazio ou não existe.");
            Assert.False((bool)json.success, "O campo 'success' deveria ser false.");
        }

    }


}
