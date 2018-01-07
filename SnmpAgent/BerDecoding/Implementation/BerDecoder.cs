using System;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class BerDecoder : IBerDecoder
    {
        private IByteOperations byteOperations;

        public BerDecoder(IByteOperations byteOperations)
        {
            this.byteOperations = byteOperations;
        }

        public void Decode(byte[] input)
        {
            byteOperations.GetType(input[0]);
        }
    }
}