using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.Abstractions
{
    public interface IDynamicDnsService
    {
        Task<string> RetrieveSourceIpAddress();

        Task<string> RetrieveDestinationIpAddress();

        Task UpdateIpAddress();
    }
}
