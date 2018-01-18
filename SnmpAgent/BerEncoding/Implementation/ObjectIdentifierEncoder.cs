using SnmpAgent.BerEncoding.Interfaces;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class ObjectIdentifierEncoder : IObjectIdentifierEncoder
    {
        private readonly IValueOctetEncoder valueOctetEncoder;
        private readonly IMessageLengthEncoder messageLengthEncoder;

        public ObjectIdentifierEncoder(IValueOctetEncoder valueOctetEncoder, IMessageLengthEncoder messageLengthEncoder)
        {
            this.valueOctetEncoder = valueOctetEncoder;
            this.messageLengthEncoder = messageLengthEncoder;
        }

        public string GetEncodedMessage(string oid)
        {
            var tag = "06";
            var codedValue = valueOctetEncoder.EncodeOid(oid);
            var length = messageLengthEncoder.GetEncodedLentgh(codedValue);
            return tag + length + codedValue;
        }
    }
}