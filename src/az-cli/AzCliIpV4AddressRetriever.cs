using Resistr.DynamicDnsClient.Abstractions;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.AzCli
{
    public class AzCliIpV4AddressRetriever : AbstractIpV4AddressRetriever
    {
        public AzCliIpV4AddressRetriever() : base() { }

        protected override async Task<string> RetrieveIpV4Address()
        {
            var processStartInfo = new ProcessStartInfo
            {
                WorkingDirectory = @"C:\Program Files (x86)\Microsoft SDKs\Azure\CLI2\wbin",
                Arguments = "network dns record-set a show --resource-group personal --zone-name jamesbromley.com --name vpn",
                FileName = "az.cmd",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            var processResult = await processStartInfo.RunProcess();
            var dnsIp = string.Empty;
            using (JsonDocument document = JsonDocument.Parse(processResult.Item1))
            {
                foreach (JsonElement element in document.RootElement.GetProperty("arecords").EnumerateArray())
                {
                    dnsIp = element.GetProperty("ipv4Address").GetString();
                    break;
                }
            }
            return dnsIp;
        }
    }
}
