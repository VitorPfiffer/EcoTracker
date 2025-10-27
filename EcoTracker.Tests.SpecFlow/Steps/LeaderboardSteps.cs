using EcoTracker.Tests.SpecFlow.Validators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

[Binding]
public class LeaderboardSteps
{
    private readonly HttpClient _httpClient = new HttpClient();

    private readonly ScenarioContext _scenarioContext;
    public LeaderboardSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }



    private HttpResponseMessage _response;
    private string _apiResponse;

    private readonly JsonSchemaValidator _validator = new JsonSchemaValidator();


    [When(@"eu envio a requisição para o endpoint ""(.*)""")]
    public async Task QuandoEuEnvioARequisicaoParaOEndpoint(string endpoint)
    {
        string token = _scenarioContext["token"] as string;
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var _response = await _httpClient.GetAsync($"http://localhost:8081/{endpoint}");
        _scenarioContext["response"] = _response;
        _scenarioContext["apiResponse"] = await _response.Content.ReadAsStringAsync();
    }

    [Then(@"a resposta deve seguir esse schema ""(.*)""")]
    public async Task EntaoARespostaDeveSeguirEsseSchema(string schemaPath)
    {

        var apiResponse = _scenarioContext["apiResponse"] as string;
        bool isValid = await _validator.ValidateAsync(schemaPath, apiResponse);
        Assert.True(isValid, "O JSON retornado não está de acordo com o schema.");
    }

    [Then(@"a resposta JSON deve conter os campos obrigatórios")]
    public void ValidarCamposObrigatorios(Table table)
    {
        string responseContent = _scenarioContext["ApiResponse"] as string;
        dynamic json = JsonConvert.DeserializeObject<dynamic>(responseContent);

        foreach (var row in table.Rows)
        {
            string campo = row["campo"];
            string tipoEsperado = row["tipo"];

            // Verifica se o campo existe
            Assert.True(json[campo] != null, $"O campo '{campo}' não existe na resposta.");

            // Verifica o tipo esperado
            switch (tipoEsperado.ToLower())
            {
                case "string":
                    Assert.IsInstanceOf<string>((string)json[campo]);
                    break;
                case "boolean":
                    Assert.IsInstanceOf<bool>((bool)json[campo]);
                    break;
                case "integer":
                case "int":
                    Assert.IsInstanceOf<int>((int)json[campo]);
                    break;
                case "array":
                    Assert.IsInstanceOf<JArray>(json[campo]);
                    break;
                case "object":
                    Assert.IsInstanceOf<JObject>(json[campo]);
                    break;
            }
        }
    }
    [Then(@"os valores esperados da resposta JSON devem ser")]
    public void ValidarValoresEsperados(Table table)
    {
        string responseContent = _scenarioContext["apiResponse"] as string;
        dynamic json = JsonConvert.DeserializeObject<dynamic>(responseContent);

        foreach (var row in table.Rows)
        {
            string campo = row["campo"];
            string valorEsperado = row["valor"];

            string valorReal = json[campo]?.ToString();
            Assert.AreEqual(valorEsperado, valorReal, $"Campo '{campo}' não tem o valor esperado.");
        }
    }
}
