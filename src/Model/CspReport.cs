using System.Text.Json.Serialization;

namespace csplogger.Controllers
{
    public class CspReport  
    {
        [JsonPropertyName("document-uri")]
        public string DocumentUri { get; set; }

        [JsonPropertyName("referrer")]
        public string Referrer { get; set; }
        
        [JsonPropertyName("blocked-uri")]
        public string BlockedUri { get; set; }
        [JsonPropertyName("violated-directive")]
        public string ViolatedDirective { get; set; }
        [JsonPropertyName("original-policy")]
        public string OriginalPolicy { get; set; }
        [JsonPropertyName("disposition")]
        public string Disposition { get; set; }

    }
}
