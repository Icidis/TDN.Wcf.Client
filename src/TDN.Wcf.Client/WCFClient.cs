using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace TDN.Wcf.Client
{
    public class WCFClient<WCFServiceContract> : IDisposable
    {
        public readonly WCFServiceContract Proxy;
        private readonly ChannelFactory<WCFServiceContract> _channelFactory;

        public WCFClient(Binding binding, string endpointAddressUri, int maxItemsInObjectGraph)
        {
            var endpointAddress = new EndpointAddress(endpointAddressUri);
            _channelFactory = new ChannelFactory<WCFServiceContract>(binding, endpointAddress);
            foreach (var operationDescription in _channelFactory.Endpoint.Contract.Operations)
            {
                if (operationDescription.OperationBehaviors[typeof(DataContractSerializerOperationBehavior)] is DataContractSerializerOperationBehavior dataContractBehavior)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = maxItemsInObjectGraph;
                }
            }
            Proxy = _channelFactory.CreateChannel();
            ((IClientChannel)Proxy).Open();
        }

        public void Dispose()
        {
            _channelFactory?.Close();
        }
    }
}
