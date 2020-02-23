using System.ServiceModel.Channels;

namespace TDN.Wcf.Client.Abstractions
{
    public interface IWcfBinding
    {
        int MaxItemsInObjectGraph { get; }
        Binding GetBinding();
    }
}
