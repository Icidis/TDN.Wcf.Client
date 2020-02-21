using System.ServiceModel.Channels;

namespace TDN.Wcf.Client.Abstractions
{
    public interface IWcfBinding
    {
        string Name { get; }
        int MaxItemsInObjectGraph { get; }
        Binding GetBinding();
    }
}
