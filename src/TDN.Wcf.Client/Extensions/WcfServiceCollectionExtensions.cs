using System;
using Microsoft.Extensions.DependencyInjection;
using TDN.Wcf.Client.Configuration;
using TDN.Wcf.Client.Enums;
using TDN.Wcf.Client.Factories;
using TDN.Wcf.Client.Interfaces;

namespace TDN.Wcf.Client.Extensions
{
    public static class WcfServiceCollectionExtensions
    {
        public static IServiceCollection AddWCFClientFactory<T>(this IServiceCollection services, HttpBinding binding, Action<WcfServiceConfiguration> configuration) where T : IWCFClientFactory
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var serviceConfig = new WcfServiceConfiguration();
            configuration?.Invoke(serviceConfig);

            if (binding == HttpBinding.BasicHttpBinding)
            {
                services.AddSingleton(typeof(IWCFClientFactory), new BasicHttpBindingFactory(configuration: serviceConfig));
            }
            else
            {
                throw new ArgumentException($"Unknown binding configuration for '{binding}'", nameof(binding));
            }

            return services;
        }
    }
}
