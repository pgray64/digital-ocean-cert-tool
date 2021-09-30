using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DigitalOceanCertTool.Entities;

namespace DigitalOceanCertTool.Helpers {
    public static class CertHelper {
        public static List<string> GenerateDomains(string baseDomain, int startIndex, int count, string prefix) {
            var domains = new List<string>() { baseDomain };
            for (var i = startIndex; i < startIndex + count; i++) {
                var randSuffix = RandomHelper.GetSecureRandomString(25).ToLowerInvariant();
                domains.Add(prefix + "-" + i + "-" + randSuffix + "." + baseDomain);
            }

            return domains;
        }

        public static string GenerateSql(List<string> dnsNames) {
            var sb = new StringBuilder("INSERT INTO digital_ocean_available_scanner_domains (domain, digital_ocean_certificate_id) VALUES ");
            for (var i = 0; i < dnsNames.Count; i++) {
                var name = dnsNames[i];
                sb.Append($"('{name}', null)");
                if (i < dnsNames.Count - 1) {
                    sb.Append(",\n");
                }
            }

            sb.Append(";");
            return sb.ToString();
        }

        public static string GenerateJson(List<string> dnsNames, string certName) {
            var certRequest = new DigitalOceanCertificate() {
                Name = certName,
                Type = "lets_encrypt",
                DnsNames = dnsNames
            };
            return JsonSerializer.Serialize(certRequest);
        }
    }
}