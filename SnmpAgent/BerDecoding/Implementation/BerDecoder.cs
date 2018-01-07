using System;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class BerDecoder : IBerDecoder
    {
        private readonly IIdentifierOctetDecoder identifierOctetDecoder;
        private readonly ILengthDecoder lengthDecoder;
        private readonly IValueOctetsDecoder valueOctetsDecoder;

        public BerDecoder(IIdentifierOctetDecoder identifierOctetDecoder, ILengthDecoder lengthDecoder, IValueOctetsDecoder valueOctetsDecoder)
        {
            this.identifierOctetDecoder = identifierOctetDecoder;
            this.lengthDecoder = lengthDecoder;
            this.valueOctetsDecoder = valueOctetsDecoder;
        }

        public void Decode(byte[] input)
        {
            var identifierOctet = identifierOctetDecoder.GetType(input[0]);
            if (identifierOctet.Tag.Equals("unidentified"))
            {
                return;
            }
            var lenght = lengthDecoder.GetLenght(input[1]);
            var value = valueOctetsDecoder.GetValue(input,identifierOctet.Tag,lenght);
        }
    }
}