using System;
using SnmpAgent.BerEncoding.Interfaces;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class ValueOctetEncoder : IValueOctetEncoder
    {
        private string messageValue;
        public string EncodeObjectValue(string syntaxName, string valueToEncode)
        {
            messageValue = valueToEncode;
            var trimmedSyntaxName = syntaxName.Replace(" ", "").ToLower();

            if (trimmedSyntaxName.Contains("integer"))
            {
                return EncodeInteger();
            }
            else if (trimmedSyntaxName.Contains("octetstring"))
            {
                return EncodeOctetString();
            }
            else if (trimmedSyntaxName.Contains("visiblestring"))
            {
                return EncodeVisibleString();
            }
            else if (trimmedSyntaxName.Contains("displaystring"))
            {
                return EncodeVisibleString();
            }
            throw new NotImplementedException();
        }

        public string EncodeOid(string oid)
        {
            var delimiter = '.';
            var numbersAsString = oid.Split(delimiter);
            var output = "";

            var firstNumber = int.Parse(numbersAsString[0]);

            for (var i = 1; i < numbersAsString.Length; i++)
            {
                var nextNumber = int.Parse(numbersAsString[i]);
                if (i == 1)
                {
                    var z = 40 * firstNumber + nextNumber;
                    var hexValue = z.ToString("X2");
                    output += hexValue;
                }
                else
                {
                    var hexValue = nextNumber.ToString("X2");
                    output += hexValue;
                }
            }

            return output;
        }

        private string EncodeVisibleString()
        {
            var message = "";
            foreach (var character in messageValue)
            {
                var asciiValue = (int) character;
                string hexValue = asciiValue.ToString("X2");
                if (hexValue.Length % 2 != 0)
                {
                    hexValue = "0" + hexValue;
                }
                message += hexValue;
            }
            return message;
        }

        private string EncodeOctetString()
        {
           throw new NotImplementedException();
        }

        private string EncodeInteger()
        {
            var inputInInt = int.Parse(messageValue);
            string hexValue = inputInInt.ToString("X2");
            if (hexValue.Length % 2 != 0)
            {
                hexValue = "0" + hexValue;
            }
            return hexValue;
        }


    }
}