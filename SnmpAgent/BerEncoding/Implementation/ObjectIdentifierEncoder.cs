using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Models;

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

        public string GetEncodedMessage(DependencyTreeNode node)
        {
            var tag = "06";
            var codedValue = valueOctetEncoder.EncodeOid(node.Oid);
            var length = messageLengthEncoder.GetEncodedLentgh(codedValue);
            return tag + length + codedValue;
        }
    }
}