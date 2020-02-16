using System;
using System.ServiceModel;

namespace TDN.Wcf.Client.Configuration
{
    public class WcfServiceConfiguration
    {
        public long? MaxBufferPoolSize { get; set; }
        public long? MaxReceivedMessageSize { get; set; }
        public int? ReaderQuotasMaxArrayLength { get; set; }
        public int? ReaderQuotasMaxStringContentLength { get; set; }
        public int? MaxItemsInObjectGraph { get; set; }

        public TimeSpan? SendTimeout { get; set; }
        public TimeSpan? ReceiveTimeout { get; set; }

        public BasicHttpSecurityMode BasicHttpSecurityMode { get; set; }
        public HttpClientCredentialType HttpClientCredentialType { get; set; }

        public WcfServiceConfiguration SetMaxSizes(long maxBufferPoolSize, long maxReceivedMessageSize)
        {
            this.MaxBufferPoolSize = maxBufferPoolSize;
            this.MaxReceivedMessageSize = maxReceivedMessageSize;
            return this;
        }

        public WcfServiceConfiguration SetReaderQuotas(int maxArrayLength, int maxStringContentLength)
        {
            this.ReaderQuotasMaxArrayLength = maxArrayLength;
            this.ReaderQuotasMaxStringContentLength = maxStringContentLength;
            return this;
        }

        public WcfServiceConfiguration SetTimeouts(TimeSpan sendTimeout, TimeSpan receiveTimeout)
        {
            this.SendTimeout = sendTimeout;
            this.ReceiveTimeout = receiveTimeout;
            return this;
        }

        public WcfServiceConfiguration SetBasicHttpBindingSecurity(BasicHttpSecurityMode basicHttpSecurityMode, HttpClientCredentialType httpClientCredentialType)
        {
            this.BasicHttpSecurityMode = basicHttpSecurityMode;
            this.HttpClientCredentialType = httpClientCredentialType;
            return this;
        }
    }
}
