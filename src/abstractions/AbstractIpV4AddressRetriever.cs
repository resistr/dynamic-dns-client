using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.Abstractions
{
    public abstract class AbstractIpV4AddressRetriever : AbstractNamedItem, IRetrieveIpAddress
    {
        protected AbstractIpV4AddressRetriever() : base() { }

        public virtual async Task<string> RetrieveIpAddress()
            => AsIp4Address(await RetrieveIpV4Address());

        protected abstract Task<string> RetrieveIpV4Address();

        protected virtual string AsIp4Address(string possibleIp4Address)
            => IsIp4Address(possibleIp4Address?.Trim()) == true ? possibleIp4Address.Trim() : string.Empty;

        protected virtual bool IsIp4Address(string possibleIp4Address)
        {
            if (possibleIp4Address?.Contains(".") != true)
            {
                return false;
            }
            var ipAddressParts = possibleIp4Address.Split('.');
            if (ipAddressParts.Length != 4)
            {
                return false;
            }
            return IsIp4Octet(ipAddressParts[0]) && IsIp4Octet(ipAddressParts[1]) && IsIp4Octet(ipAddressParts[2]) && IsIp4Octet(ipAddressParts[3]);
        }
        
        protected virtual bool IsIp4Octet(string possibleIp4Octet)
            => IsByte(possibleIp4Octet);

        protected virtual bool IsByte(string possibleByte)
            => byte.TryParse(possibleByte, out byte discardedByte); 
    }
}
