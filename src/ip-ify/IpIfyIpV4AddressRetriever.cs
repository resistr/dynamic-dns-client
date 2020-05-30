using Resistr.DynamicDnsClient.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.IpIfy
{
    public class IpIfyIpV4AddressRetriever : AbstractIpV4AddressRetriever
    {
        protected virtual HttpClient HttpClient { get; }

        public IpIfyIpV4AddressRetriever(HttpClient httpClient) : base() 
        {
            HttpClient = httpClient;
        }

        protected override async Task<string> RetrieveIpV4Address()
            => await HttpClient.GetStringAsync("https://api.ipify.org");
    }
}
