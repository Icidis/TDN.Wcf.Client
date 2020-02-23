namespace TDN.Wcf.Client.Abstractions
{
    public interface IWcfClientFactory
    {
        TWcfServiceContract CreateClient<TWcfServiceContract, TWcfBinding>(string endpointAddressUri) where TWcfBinding : IWcfBinding;
    }
}
