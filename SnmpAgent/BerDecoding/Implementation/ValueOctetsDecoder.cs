using System;
using System.Linq;
using SnmpAgent.BerDecoding.Interface;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class ValueOctetsDecoder: IValueOctetsDecoder
    {
        private byte[] inputBits;
        private int valueLenght;
        public string GetValue(ref byte[] input, string tag, int length)
        {
            
            inputBits = input;
            valueLenght = length;
            input = input.Skip(length).ToArray();

            if (tag.Equals("INTEGER", StringComparison.OrdinalIgnoreCase))
            {
                return GetInteger();
            }
            else if (tag.Equals("OCTET STRING", StringComparison.OrdinalIgnoreCase))
            {
                return GetOctetString();
            }
            else if (tag.Equals("VisibleString", StringComparison.OrdinalIgnoreCase))
            {
                return GetVisibleString();
            }
            else if (tag.Equals("OBJECT IDENTIFIER", StringComparison.OrdinalIgnoreCase))
            {
                return GetObjectIdentifier();
            }

            return "not implemented Tag";
        }

        private string GetObjectIdentifier()
        {
            var oid = "";
            for (var i = 0; i < valueLenght; i++)
            {
                if (i == 0)
                {
                    var text = Convert.ToString(inputBits[i], 16);
                    var number = Convert.ToInt16(text, 16);
                    var y = number % 40;
                    var x = (number - y) / 40;
                    oid = $"{x}.{y}";
                }
                else
                {
                    var inputInString = Convert.ToString(inputBits[i], 16);
                    var number = Convert.ToInt16(inputInString, 16).ToString();
                    oid = oid + "." + number;
                }
            }

            return oid;
        }

        private  string GetVisibleString()
        {
            var message = "";
            for (var i = 0; i < valueLenght; i++)
            {
                var inputInString = Convert.ToString(inputBits[i], 16);
                var asciiNumber = Convert.ToInt32(inputInString, 16);
                message = message + Convert.ToChar(asciiNumber);
            }

            return message;
        }

        private  string GetOctetString()
        {
            var hex = "";
            for (var i = 0; i < valueLenght; i++)
            {
                var inputInString = Convert.ToString(inputBits[i], 16);
                if (inputInString.Length.Equals(1))
                {
                    inputInString = "0" + inputInString;
                }

                hex = hex + inputInString;
            }

            return hex;
        }

        private  string GetInteger()
        {
            var hex = "";
            for (var i = 0; i < valueLenght; i++)
            {
                var inputInString = Convert.ToString(inputBits[i], 16);
                hex = hex + inputInString;
            }

            return Convert.ToInt16(hex, 16).ToString();
        }
    }
}