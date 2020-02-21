using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TDN.Wcf.Client.Abstractions;
using TDN.Wcf.Client.Configuration;

namespace TDN.Wcf.Client.Bindings
{
    public class BindingBasicHttp : IWcfBinding
    {
        private readonly BasicHttpBinding _binding;
        public readonly int _maxItemsInObjectGraph = 65536;

        string IWcfBinding.Name => nameof(BindingBasicHttp);

        public int MaxItemsInObjectGraph => _maxItemsInObjectGraph;

        public BindingBasicHttp(BasicHttpBinding binding)
        {
            _binding = binding;
        }

        public BindingBasicHttp(BindingBasicHttpConfiguration configuration)
        {
            _binding = new BasicHttpBinding
            {
                MaxBufferPoolSize = configuration.MaxBufferPoolSize.GetValueOrDefault(524288),
                MaxReceivedMessageSize = configuration.MaxReceivedMessageSize.GetValueOrDefault(65536),
                SendTimeout = configuration.SendTimeout.GetValueOrDefault(new TimeSpan(0, 1, 0)),
                ReceiveTimeout = configuration.ReceiveTimeout.GetValueOrDefault(new TimeSpan(0, 1, 0))
            };

            _binding.ReaderQuotas.MaxArrayLength = configuration.ReaderQuotasMaxArrayLength.GetValueOrDefault(16384);
            _binding.ReaderQuotas.MaxStringContentLength = configuration.ReaderQuotasMaxStringContentLength.GetValueOrDefault(8192);

            _binding.Security.Mode = configuration.BasicHttpSecurityMode;
            _binding.Security.Transport.ClientCredentialType = configuration.HttpClientCredentialType;

            //Max Items in Object Graph
            _maxItemsInObjectGraph = configuration.MaxItemsInObjectGraph.GetValueOrDefault(65536);
        }

        public BindingBasicHttp(Action<BindingBasicHttpConfiguration> options)
        {
            var configuration = new BindingBasicHttpConfiguration();
            options?.Invoke(configuration);

            _binding = new BasicHttpBinding
            {
                MaxBufferPoolSize = configuration.MaxBufferPoolSize.GetValueOrDefault(524288),
                MaxReceivedMessageSize = configuration.MaxReceivedMessageSize.GetValueOrDefault(65536),
                SendTimeout = configuration.SendTimeout.GetValueOrDefault(new TimeSpan(0, 1, 0)),
                ReceiveTimeout = configuration.ReceiveTimeout.GetValueOrDefault(new TimeSpan(0, 1, 0))
            };

            _binding.ReaderQuotas.MaxArrayLength = configuration.ReaderQuotasMaxArrayLength.GetValueOrDefault(16384);
            _binding.ReaderQuotas.MaxStringContentLength = configuration.ReaderQuotasMaxStringContentLength.GetValueOrDefault(8192);

            _binding.Security.Mode = configuration.BasicHttpSecurityMode;
            _binding.Security.Transport.ClientCredentialType = configuration.HttpClientCredentialType;

            //Max Items in Object Graph
            _maxItemsInObjectGraph = configuration.MaxItemsInObjectGraph.GetValueOrDefault(65536);
        }

        Binding IWcfBinding.GetBinding()
        {
            return _binding;
        }
    }
}
