using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DefaultJsonIgnore = System.Text.Json.Serialization.JsonIgnoreAttribute;
using JsonIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition;

namespace EcoTracker.Core.Data
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            this.HasUnhandledException = false;
        }

        public bool Success => !HasUnhandledException && (!Errors?.Any() ?? true);

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore), DefaultJsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object Data { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore), DefaultJsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> Errors { get; set; }

        public DateTime Date => DateTime.UtcNow;

        [JsonIgnore, DefaultJsonIgnore]
        public bool HasUnhandledException { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
