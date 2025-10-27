//using EcoTracker.Tests.SpecFlow.Services;
//using EcoTracker.Tests.SpecFlow.Validators;
//using TechTalk.SpecFlow;

//[Binding]
//public class LeaderboardSteps
//{
//    private readonly HttpClient _httpClient = new HttpClient();
//    private readonly AuthService _authService;
//    private HttpResponseMessage _response;
//    private string _apiResponse;

//    private string _token;

//    private readonly JsonSchemaValidator _validator = new JsonSchemaValidator();

//    public LeaderboardSteps()
//    {
//        _authService = new AuthService(_httpClient);
//    }

//    [Given(@"que eu tenha os seguintes dados de login:")]
//    public async Task DadoQueEuTenhaOsSeguintesDadosDeLogin(Table table)
//    {
//        _token = await _authService.LoginAsync(table);
//    }

//    [When(@"eu envio a requisição para o endpoint ""(.*)""")]
//    public async Task QuandoEuEnvioARequisicaoParaOEndpoint(string endpoint)
//    {
//        _httpClient.DefaultRequestHeaders.Clear();
//        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

//        _response = await _httpClient.GetAsync($"https://localhost:8081{endpoint}");
//        _apiResponse = await _response.Content.ReadAsStringAsync();
//    }

//    [Then(@"devo receber o status code 200")]
//    public void EntaoDevoReceberOStatusCode200()
//    {
//        Assert.Equals(System.Net.HttpStatusCode.OK, _response.StatusCode);
//    }

//    [Then(@"a resposta deve seguir esse schema ""(.*)""")]
//    public async Task EntaoARespostaDeveSeguirEsseSchema(string schemaPath)
//    {
//        bool isValid = await _validator.ValidateAsync(schemaPath, _apiResponse);
//        Assert.True(isValid, "O JSON retornado não está de acordo com o schema.");
//    }
//}
