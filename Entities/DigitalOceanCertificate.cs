using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DigitalOceanCertTool.Entities {
    public class DigitalOceanCertificate {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("dns_names")]
        public List<string> DnsNames { get; set; }
    }
}