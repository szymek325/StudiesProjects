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
            var number = Convert.ToInt32(bits, 2);
            if (number> input.Length)
            {
                Console.WriteLine("Defined Length isn't equal number of value bytes");
                throw new Exception("Defined Length isn't equal number of value bytes");
            }
            return number;
        }
    }
}