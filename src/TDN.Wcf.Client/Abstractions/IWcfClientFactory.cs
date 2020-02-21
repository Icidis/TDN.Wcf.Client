namespace TDN.Wcf.Client.Abstractions
{
    public interface IWcfClientFactory
    {
        WCFServiceContract CreateClient<WCFServiceContract>(string bindingName, string endpointAddressUri);
    }
}
