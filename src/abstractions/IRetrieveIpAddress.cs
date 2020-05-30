using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.Abstractions
{
    public interface IRetrieveIpAddress : IHaveName
    {
        Task<string> RetrieveIpAddress();
    }
}
