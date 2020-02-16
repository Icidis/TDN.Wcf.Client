using System;
using System.ServiceModel;
using TDN.Wcf.Client.Interfaces;
using TDN.Wcf.Client.Configuration;

namespace TDN.Wcf.Client.Factories
{
    public class BasicHttpBindingFactory : IWCFClientFactory
    {
        private readonly BasicHttpBinding _binding;
        private readonly int _maxItemsInObjectGraph;

        public BasicHttpBindingFactory(WcfServiceConfiguration configuration)
        {
            //Set default configuration
            _binding = new BasicHttpBinding
            {
                MaxBufferPoolSize = configuration.MaxBufferPoolSize.GetValueOrDefault(524288),
                MaxReceivedMessageSize = configuration.MaxReceivedMessageSize.GetValueOrDefault(65536),
                SendTimeout = configuration.SendTimeout.GetValueOrDefault(new TimeSpan(0, 1, 0)),
                ReceiveTimeout = configuration.ReceiveTimeout.GetValueOrDefault(new TimeSpan(0, 1, 0))
            };

            //Set reader quotas configuration
            _binding.ReaderQuotas.MaxArrayLength = configuration.ReaderQuotasMaxArrayLength.GetValueOrDefault(16384);
            _binding.ReaderQuotas.MaxStringContentLength = configuration.ReaderQuotasMaxStringContentLength.GetValueOrDefault(8192);

            //Set schema specific configuration
            _binding.Security.Mode = configuration.BasicHttpSecurityMode;
            _binding.Security.Transport.ClientCredentialType = configuration.HttpClientCredentialType;

            //Max Items in Object Graph
            _maxItemsInObjectGraph = configuration.MaxItemsInObjectGraph.GetValueOrDefault(65536);
        }

        public WCFServiceContract CreateClient<WCFServiceContract>(string endpointAddressUri)
        {
            return new WCFClient<WCFServiceContract>(_binding, endpointAddressUri, _maxItemsInObjectGraph).Proxy;
        }
    }
}
