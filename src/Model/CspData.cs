using System.Text.Json.Serialization;

namespace csplogger.Controllers
{
    public class CspData
    {
        [JsonPropertyName("csp-report")]
        public CspReport CspReport { get; set; }
    }
}