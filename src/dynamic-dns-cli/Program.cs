using Resistr.DynamicDnsClient.Abstractions;
using Resistr.DynamicDnsClient.AzCli;
using Resistr.DynamicDnsClient.IpIfy;
using Resistr.DynamicDnsClient.Service;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Resistr.DynamicDnsClient.DynamicDnsCli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IRetrieveIpAddress, AzCliIpV4AddressRetriever>();
            serviceCollection.AddHttpClient<IRetrieveIpAddress, IpIfyIpV4AddressRetriever>();
            serviceCollection.AddScoped<IUpdateIpAddress, AzCliIpV4AddressUpdater>();
            serviceCollection.AddScoped<IDynamicDnsService, DefaultDynamicDnsService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            await serviceProvider.GetRequiredService<IDynamicDnsService>().UpdateIpAddress();
        }
    }
}
