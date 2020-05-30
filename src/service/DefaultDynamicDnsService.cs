using Resistr.DynamicDnsClient.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.Service
{
    public class DefaultDynamicDnsService : IDynamicDnsService
    {
        protected virtual IRetrieveIpAddress SourceIpAddressRetriever { get; }

        protected virtual IRetrieveIpAddress DestinationIpAddressRetriever { get; }

        protected virtual IUpdateIpAddress IpAddressUpdater { get; }

        public DefaultDynamicDnsService(IEnumerable<IRetrieveIpAddress> ipAddressRetrievers
                                      , IEnumerable<IUpdateIpAddress> ipAddressUpdaters)
        {
            SourceIpAddressRetriever = ipAddressRetrievers.FirstOrDefault(ipAddressRetriever => ipAddressRetriever.Name == "IpIfyIpV4AddressRetriever");
            DestinationIpAddressRetriever = ipAddressRetrievers.FirstOrDefault(ipAddressRetriever => ipAddressRetriever.Name == "AzCliIpV4AddressRetriever");
            IpAddressUpdater = ipAddressUpdaters.FirstOrDefault(ipAddressUpdater => ipAddressUpdater.Name == "AzCliIpV4AddressUpdater");
        }

        public async Task<string> RetrieveSourceIpAddress()
            => await SourceIpAddressRetriever.RetrieveIpAddress();

        public async Task<string> RetrieveDestinationIpAddress()
            => await DestinationIpAddressRetriever.RetrieveIpAddress();

        public async Task UpdateIpAddress()
        {
            var sourceIpAddressRetrieveTask = RetrieveSourceIpAddress();
            var DestinationIpAddressRetrieveTask = RetrieveDestinationIpAddress();

            Task.WaitAll(new[] { sourceIpAddressRetrieveTask, DestinationIpAddressRetrieveTask });

            var sourceIp = sourceIpAddressRetrieveTask.Result;
            var destinationIp = DestinationIpAddressRetrieveTask.Result;
            var ipUpdateRequired = sourceIp != destinationIp;

            if (ipUpdateRequired)
            {
                await IpAddressUpdater.UpdateIpAddress(destinationIp, sourceIp);
            }
        }
    }
}
