using System.Collections.Generic;
using TDN.Wcf.Client.Abstractions;

namespace TDN.Wcf.Client.Configuration
{
    public class WcfClientFactoryConfiguration
    {
        public List<IWcfBinding> Bindings { get; private set; }

        public WcfClientFactoryConfiguration()
        {
            Bindings = new List<IWcfBinding>();
        }

        public WcfClientFactoryConfiguration AddWcfBinding(IWcfBinding binding)
        {
            Bindings.Add(binding);
            return this;
        }       
    }
}
