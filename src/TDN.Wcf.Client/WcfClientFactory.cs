using System;
using System.Collections.Generic;
using TDN.Wcf.Client.Abstractions;
using TDN.Wcf.Client.Internals;

namespace TDN.Wcf.Client
{
    public class WcfClientFactory : IWcfClientFactory
    {
        private readonly Dictionary<string, IWcfBinding> _bindings;

        public WcfClientFactory(IEnumerable<IWcfBinding> wcfBindings)
        {
            _bindings = new Dictionary<string, IWcfBinding>(StringComparer.OrdinalIgnoreCase);
            foreach (var wcfBinding in wcfBindings)
            {
                var typeName = GetTypeName(wcfBinding: wcfBinding);
                if (!_bindings.ContainsKey(typeName))
                {
                    _bindings.Add(typeName, wcfBinding);
                }
            }
        }

        public TWcfServiceContract CreateClient<TWcfServiceContract, TWcfBinding>(string endpointAddressUri) where TWcfBinding : IWcfBinding
        {
            var typeName = typeof(TWcfBinding).FullName;
            if (_bindings.TryGetValue(typeName, out IWcfBinding binding))
            {
                return new WCFClient<TWcfServiceContract>(binding.GetBinding(), endpointAddressUri, binding.MaxItemsInObjectGraph).Proxy;
            }
            else
            {
                throw new ArgumentException($"Binding configuration with name {typeName} not found.", nameof(TWcfBinding));
            }
        }

        private string GetTypeName(IWcfBinding wcfBinding)
        {
            if (wcfBinding is null)
            {
                throw new ArgumentException(nameof(wcfBinding));
            }
            return wcfBinding.GetType().FullName;
        }
    }
}
