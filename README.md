[![Build Status](https://dev.azure.com/icidisvs/GitHub/_apis/build/status/Icidis.TDN.Wcf.Client?branchName=master)](https://dev.azure.com/icidisvs/GitHub/_build/latest?definitionId=1&branchName=master)

# TDN.Wcf.Client
.NET Standard WCF Client library

## Usage

Exposing a WCF service (NET4/4.5 etc) with the following service contract

```
[ServiceContract]
public interface IService1
{
    [OperationContract]
    CompositeType GetDataUsingDataContract(CompositeType composite);
}
```

### Update your .NET Core application with the following

Note, the BindingBasicHttp implements IWcfBinding which has a binding name of "BindingBasicHttp"

#### Startup.cs

```
using TDN.Wcf.Client.Bindings;
using TDN.Wcf.Client.Extensions;

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
```

#### Controller.cs

```
using WCFServiceContracts;
using TDN.Wcf.Client.Abstractions;

[Route("[controller]")]
public class ValuesController : ControllerBase
{
    private readonly IWcfClientFactory _wcfClientFactory;

    public ValuesController(IWcfClientFactory wcfClientFactory)
    {
        _wcfClientFactory = wcfClientFactory;
    }

    [HttpGet]
    public CompositeType Get()
    {
        var client = _wcfClientFactory.CreateClient<IService1>("BindingBasicHttp", "http://localhost:51677/Service1.svc");

        return client.GetDataUsingDataContract(new CompositeType()
        {
            BoolValue = true,
            StringValue = "Test from WCF"
        });
    }
}
```

### Create your own binding

Create a class and implement the **IWcfBinding** interface such as below with the configuration and binding you require. This class will then be registered on Startup by using the **AddWcfBinding** method when configuring the WcfClientFactory using **AddWcfClientFactory**.

```
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TDN.Wcf.Client.Abstractions;
using TDN.Wcf.Client.Configuration;

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
```
