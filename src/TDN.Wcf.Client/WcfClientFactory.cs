using System;
using System.Collections.Generic;
using TDN.Wcf.Client.Abstractions;
using TDN.Wcf.Client.Internals;

namespace TDN.Wcf.Client
{
    public class WcfClientFactory : IWcfClientFactory
    {
        private readonly Dictionary<string, IWcfBinding> _bindings;

        public WcfClientFactory(IEnumerable<IWcfBinding> bindings)
        {
            _bindings = new Dictionary<string, IWcfBinding>(StringComparer.OrdinalIgnoreCase);
            foreach (var binding in bindings)
            {
                if (!_bindings.ContainsKey(binding.Name))
                {
                    _bindings.Add(binding.Name, binding);
                }
            }
        }

        public WCFServiceContract CreateClient<WCFServiceContract>(string bindingName, string endpointAddressUri)
        {
            if (_bindings.TryGetValue(bindingName, out IWcfBinding binding))
            {
                return new WCFClient<WCFServiceContract>(binding.GetBinding(), endpointAddressUri, binding.MaxItemsInObjectGraph).Proxy;
            }
            else
            {
                throw new ArgumentException($"Binding configuration with name {bindingName} not found.", nameof(bindingName));
            }
        }
    }
}
