using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.ServiceModel;
using TDN.Wcf.Client.Bindings;
using TDN.Wcf.Client.Extensions;

namespace AspNetCoreWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWcfClientFactory(options =>
            {
                options.AddWcfBinding(new BindingBasicHttp(c =>
                {
                    c.SetBasicHttpBindingSecurity(basicHttpSecurityMode: BasicHttpSecurityMode.None, httpClientCredentialType: HttpClientCredentialType.None);
                    c.SetMaxSizes(maxBufferPoolSize: 524288, maxReceivedMessageSize: 65536);
                    c.SetReaderQuotas(maxArrayLength: 16384, maxStringContentLength: 8192);
                    c.SetTimeouts(sendTimeout: new TimeSpan(0, 1, 0), receiveTimeout: new TimeSpan(0, 1, 0));
                }));
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
