using System.Runtime.Serialization;

namespace WCFServiceContracts
{
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
