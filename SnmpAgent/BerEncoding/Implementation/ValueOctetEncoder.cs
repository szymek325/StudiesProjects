using SnmpAgent.BerEncoding.Interfaces;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class ValueOctetEncoder : IValueOctetEncoder
    {
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
    }
}