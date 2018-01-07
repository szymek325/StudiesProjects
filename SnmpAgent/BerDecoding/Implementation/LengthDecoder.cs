using System;
using SnmpAgent.BerDecoding.Interface;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class LengthDecoder : ILengthDecoder
    {
        public int GetLenght(byte v)
        {
            var bits = Convert.ToString(v, 2);
            return Convert.ToInt32(bits, 2);
        }
    }
}