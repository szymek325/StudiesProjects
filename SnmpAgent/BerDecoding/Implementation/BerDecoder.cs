using System;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class BerDecoder : IBerDecoder
    {
        private readonly IByteOperations byteOperations;

        public BerDecoder(IByteOperations byteOperations)
        {
            this.byteOperations = byteOperations;
        }

        public void Decode(byte[] input)
        {
            var identifierOctet = byteOperations.GetType(input[0]);
            var lenght = byteOperations.GetLenght(input[1]);
            var value = byteOperations.GetValue(input,identifierOctet.Tag);
        }
    }
}