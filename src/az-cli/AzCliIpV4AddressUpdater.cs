using Resistr.DynamicDnsClient.Abstractions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.AzCli
{
    public class AzCliIpV4AddressUpdater : AbstractNamedItem, IUpdateIpAddress
    {
        public AzCliIpV4AddressUpdater() : base() { }

        public async Task UpdateIpAddress(string oldIpV4Address, string newIpV4Address)
        {
            if (!string.IsNullOrWhiteSpace(newIpV4Address))
            {
                var addRecordProcessStartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = @"C:\Program Files (x86)\Microsoft SDKs\Azure\CLI2\wbin",
                    Arguments = $"network dns record-set a add-record --resource-group personal --zone-name jamesbromley.com --record-set-name vpn --ipv4-address {newIpV4Address}",
                    FileName = "az.cmd",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                var addRecordProcessResult = await addRecordProcessStartInfo.RunProcess();
            }

            if (!string.IsNullOrWhiteSpace(oldIpV4Address))
            {
                var removeRecordProcessStartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = @"C:\Program Files (x86)\Microsoft SDKs\Azure\CLI2\wbin",
                    Arguments = $"network dns record-set a remove-record --resource-group personal --zone-name jamesbromley.com --record-set-name vpn --ipv4-address {oldIpV4Address}",
                    FileName = "az.cmd",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                var removeRecordProcessResult = await removeRecordProcessStartInfo.RunProcess();
            }
        }
    }
}
