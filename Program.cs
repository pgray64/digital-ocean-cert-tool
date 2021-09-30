using System;
using System.Linq;
using DigitalOceanCertTool.Helpers;

namespace DigitalOceanCertTool {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Enter environment [dev, prod]: ");
            string environment = Console.ReadLine()?.Trim();
            Console.WriteLine("Enter cert name: ");
            string certName = Console.ReadLine()?.Trim();
            Console.WriteLine("Enter base domain: ");
            string domain = Console.ReadLine()?.Trim();
            Console.WriteLine("Enter starting index: ");
            string startIndexStr = Console.ReadLine()?.Trim();
            int startIndex = int.Parse(startIndexStr ?? "1");
            Console.WriteLine("Enter count: ");
            string countStr = Console.ReadLine()?.Trim();
            int count = int.Parse(countStr ?? "100");
            string prefix;
            switch (environment) {
                case "dev":
                    prefix = "dev-scan";
                    break;
                case "prod":
                    prefix = "scan";
                    break;
                default: 
                    Console.WriteLine("Invalid environment");
                    return;
            }

            var dnsNames = CertHelper.GenerateDomains(domain, startIndex, count, prefix);
            var sql = CertHelper.GenerateSql(dnsNames);
            Console.WriteLine("SQL Output:\n" + sql);
            
            // Include base domain for JSON so its in the cert, but not the sql rows
            dnsNames = dnsNames.Prepend(domain).ToList();
            var json = CertHelper.GenerateJson(dnsNames, certName);

            Console.WriteLine("JSON Output:\n" + json);
        }
    }
}