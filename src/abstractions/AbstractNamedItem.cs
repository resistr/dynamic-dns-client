using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.Abstractions
{
    public abstract class AbstractNamedItem : IHaveName
    {
        protected AbstractNamedItem()
        {
            Name = this.GetType().Name;
        }

        public virtual string Name { get; }
    }
}
