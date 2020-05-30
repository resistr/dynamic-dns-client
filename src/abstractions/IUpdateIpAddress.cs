using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.Abstractions
{
    public interface IUpdateIpAddress : IHaveName
    {
        Task UpdateIpAddress(string oldIpV4Address, string newIpV4Address);
    }
}
