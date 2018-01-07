using System;
using System.Linq;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class ByteOperations : IByteOperations
    {
        public int GetLenght(byte v)
        {
            var bits = Convert.ToString(v, 2);
            return Convert.ToInt32(bits, 2);
        }

        public IdentifierOctet GetType(byte v)
        {
            var identifierOctet = new IdentifierOctet();

            var bits = Convert.ToString(v, 2);
            for (var i = 0; bits.Length < 8; i++)
            {
                bits = "0" + bits;
            }

            var classofTag = bits.Substring(0, 2);
            var pc = bits.Substring(2, 1);
            var tagNumber = bits.Substring(3, 5);

            switch (classofTag)
            {
                case "00":
                    identifierOctet.Class = "Universal";
                    break;
                case "01":
                    identifierOctet.Class = "Application";
                    break;
                case "10":
                    identifierOctet.Class = "Context-specific";
                    break;
                case "11":
                    identifierOctet.Class = "Private";
                    break;
            }

            switch (pc)
            {
                case "0":
                    identifierOctet.PC = "Primitive";
                    break;
                case "1":
                    identifierOctet.PC = "Constructed";
                    break;
            }

            if (identifierOctet.Class.Equals("Universal"))
            {
                var tagInInt = Convert.ToInt32(tagNumber, 2);
                switch (tagInInt)
                {
                    case 2:
                        identifierOctet.Tag = "INTEGER";
                        break;
                    case 4:
                        identifierOctet.Tag = "OCTET STRING";
                        break;
                    case 6:
                        identifierOctet.Tag = "OBJECT IDENTIFIER";
                        break;
                    case 16:
                        identifierOctet.Tag = "SEQUENCE/ SEQUENCE OF";
                        break;
                    case 26:
                        identifierOctet.Tag = "VisibleString";
                        break;
                    default:
                        Console.WriteLine($"Tag nie został rozpoznany, wartość pola Tag: {tagInInt}");
                        throw new NotImplementedException();
                        break;
                }
            }
            else if (identifierOctet.Class.Equals("Application"))
            {
                var tagInInt = Convert.ToInt32(tagNumber);
                identifierOctet.Tag = $"APPLICATION {tagInInt}";
            }

            return identifierOctet;
        }

        public string GetValue(byte[] input, string tag)
        {
            input = input.Skip(2).ToArray();
            string hex = "";

            if (tag.Equals("INTEGER"))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    var inputInString = Convert.ToString(input[i], 16);
                    hex = hex + inputInString;
                }
                return Convert.ToInt16(hex, 16).ToString();
            }
            else if(tag.Equals("OCTET STRING"))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    var inputInString = Convert.ToString(input[i], 16);
                    if (inputInString.Length.Equals(1))
                    {
                        inputInString = "0" + inputInString;
                    }
                    hex = hex + inputInString;
                }

                return hex;
            }
            else if (tag.Equals("VisibleString"))
            {
                string message = "";
                for (int i = 0; i < input.Length; i++)
                {
                    var inputInString = Convert.ToString(input[i], 16);
                    var asciiNumber=Convert.ToInt32(inputInString, 16);
                    message = message + Convert.ToChar(asciiNumber);
                }

                return message;

            }

            return "vbalue";
        }
    }
}