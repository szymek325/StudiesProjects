using System;
using System.Linq;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Implementation
{
    internal class IdentifierOctetDecoder : IIdentifierOctetDecoder
    {
        private readonly IdentifierOctet identifierOctet = new IdentifierOctet();
        public IdentifierOctet GetType(ref byte[] input)
        {
            var v = input[0];
            input = input.Skip(1).ToArray();
            var bits = Convert.ToString(v, 2);
            for (var i = 0; bits.Length < 8; i++)
            {
                bits = "0" + bits;
            }

            var classBits = bits.Substring(0, 2);
            var pcBits = bits.Substring(2, 1);
            var tagBits = bits.Substring(3, 5);

            GetClass(classBits);
            if (!identifierOctet.Class.Equals("unidentified", StringComparison.OrdinalIgnoreCase))
            {
                GetPc(pcBits);
                GetTag(tagBits);
            }
            return identifierOctet;
        }

        private void GetTag(string tagNumber)
        {
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
                        identifierOctet.Tag = "unidentified";
                        Console.WriteLine($"Tag not found: {tagInInt}. Please try again");
                        break;
                }
            }
            else if (identifierOctet.Class.Equals("Application", StringComparison.OrdinalIgnoreCase))
            {
                var tagInInt = Convert.ToInt32(tagNumber);
                identifierOctet.Tag = $"APPLICATION {tagInInt}";
            }
        }

        private void GetPc(string pc)
        {
            switch (pc)
            {
                case "0":
                    identifierOctet.PC = "Primitive";
                    break;
                case "1":
                    identifierOctet.PC = "Constructed";
                    break;
            }
        }

        private void GetClass(string classofTag)
        {
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
                default:
                    identifierOctet.Class = "unidentified";
                    Console.WriteLine($"Class not found: {classofTag}. Please try again");
                    break;

            }
        }
    }
}