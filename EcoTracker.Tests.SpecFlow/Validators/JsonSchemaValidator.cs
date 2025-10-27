using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace EcoTracker.Tests.SpecFlow.Validators
{
    public class JsonSchemaValidator
    {
        public async Task<bool> ValidateAsync(string jsonSchemaPath, string apiResponse)
        {
            if (!File.Exists(jsonSchemaPath))
                throw new FileNotFoundException($"Schema file not found at path: {jsonSchemaPath}");

            // Ler o schema do arquivo
            string schemaJson = await File.ReadAllTextAsync(jsonSchemaPath);
            JSchema schema = JSchema.Parse(schemaJson);

            // Ler o JSON da resposta
            JObject jsonObject;
            try
            {
                jsonObject = JObject.Parse(apiResponse);
            }
            catch (JsonReaderException)
            {
                return false; // JSON inválido
            }

            bool isValid = jsonObject.IsValid(schema, out IList<string> errors);

            return isValid;
        }
    }
}
