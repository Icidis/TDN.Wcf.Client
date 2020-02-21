using System;
using Microsoft.Extensions.DependencyInjection;
using TDN.Wcf.Client.Abstractions;
using TDN.Wcf.Client.Configuration;

namespace TDN.Wcf.Client.Extensions
{
    public static class WcfServiceCollectionExtensions
    {
        public static IServiceCollection AddWcfClientFactory(this IServiceCollection services, Action<WcfClientFactoryConfiguration> configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var serviceConfig = new WcfClientFactoryConfiguration();
            configuration?.Invoke(serviceConfig);

            services.AddSingleton(typeof(IWcfClientFactory), new WcfClientFactory(serviceConfig.Bindings));

            return services;
        }
    }
}
