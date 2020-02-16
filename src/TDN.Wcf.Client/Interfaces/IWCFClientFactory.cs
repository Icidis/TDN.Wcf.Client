namespace TDN.Wcf.Client.Interfaces
{
    public interface IWCFClientFactory
    {
        WCFServiceContract CreateClient<WCFServiceContract>(string endpointAddressUri);
    }
}
