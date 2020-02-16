# TDN.Wcf.Client
.NET Standard WCF Client


## Usage

If you exposing a WCF service with the following service contract

```
[ServiceContract]
public interface IService1
{
    [OperationContract]
    CompositeType GetDataUsingDataContract(CompositeType composite);
}
```

### Update you .NET Core application with the following

#### Startup.cs

```
services.AddWCFClientFactory<BasicHttpBindingFactory>(TDN.Wcf.Client.Enums.HttpBinding.BasicHttpBinding, options =>
{
    options.SetBasicHttpBindingSecurity(System.ServiceModel.BasicHttpSecurityMode.None, System.ServiceModel.HttpClientCredentialType.None);
});
```

#### Controller.cs

```
[Route("[controller]")]
public class ValuesController : ControllerBase
{
    private readonly IWCFClientFactory _wcfClientFactory;

    public ValuesController(IWCFClientFactory wcfClientFactory)
    {
        _wcfClientFactory = wcfClientFactory;
    }

    [HttpGet]
    public CompositeType Get()
    {
        var client = _wcfClientFactory.CreateClient<IService1>("http://localhost/Service1.svc");

        return client.GetDataUsingDataContract(new CompositeType()
        {
            BoolValue = true,
            StringValue = "Test from WCF"
        });
    }
}
```
