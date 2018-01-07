using System;
using System.Linq;
using SnmpAgent.BerDecoding.Interface;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class LengthDecoder : ILengthDecoder
    {
        public int GetLenght(ref byte[] input)
        {
            var v = input[0];
            input = input.Skip(1).ToArray();
            var bits = Convert.ToString(v, 2);
            return Convert.ToInt32(bits, 2);
        }
    }
}