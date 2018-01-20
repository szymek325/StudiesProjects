using System;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class ObjectEncoder : IObjectEncoder
    {
        private readonly IIdentifierOctetEncoder identifierOctetEncoder;
        private readonly IMessageLengthEncoder messageLengthEncoder;
        private readonly IValueOctetEncoder valueOctetEncoder;

        public ObjectEncoder(IIdentifierOctetEncoder identifierOctetEncoder, IMessageLengthEncoder messageLengthEncoder, IValueOctetEncoder valueOctetEncoder)
        {
            this.identifierOctetEncoder = identifierOctetEncoder;
            this.messageLengthEncoder = messageLengthEncoder;
            this.valueOctetEncoder = valueOctetEncoder;
        }

        public string GetEncodedObject(DependencyTreeNode node, string inputValue)
        {
            var tag = identifierOctetEncoder.GetEncodedMessageTag(node.Syntax);

            var encodedValue = valueOctetEncoder.EncodeObjectValue(node.Syntax.Name, inputValue);

            var length = messageLengthEncoder.GetEncodedLentgh(encodedValue);

            var result= tag + length + encodedValue;
            return result;
        }
    }
}