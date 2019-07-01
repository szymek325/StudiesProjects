using System;
using System.Linq;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Implementation
{
    internal class IdentifierOctetDecoder : IIdentifierOctetDecoder
    {
        public IdentifierOctet IdentifierOctet { get; set; }
        public IdentifierOctet GetType(ref byte[] input)
        {
            IdentifierOctet= new IdentifierOctet();
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
            if (!IdentifierOctet.Class.Equals("unidentified", StringComparison.OrdinalIgnoreCase))
            {
                GetPc(pcBits);
                GetTag(tagBits);
            }
                
            return IdentifierOctet;
        }

        private void GetTag(string tagNumber)
        {
            if (IdentifierOctet.Class.Equals("Universal"))
            {
                var tagInInt = Convert.ToInt32(tagNumber, 2);
                switch (tagInInt)
                {
                    case 2:
                        IdentifierOctet.Tag = "INTEGER";
                        break;
                    case 4:
                        IdentifierOctet.Tag = "OCTET STRING";
                        break;
                    case 6:
                        IdentifierOctet.Tag = "OBJECT IDENTIFIER";
                        break;
                    case 16:
                        IdentifierOctet.Tag = "SEQUENCE/ SEQUENCE OF";
                        break;
                    case 26:
                        IdentifierOctet.Tag = "VisibleString";
                        break;
                    default:
                        IdentifierOctet.Tag = "unidentified";
                        Console.WriteLine($"Tag not found: {tagInInt}. Please try again");
                        break;
                }
            }
            else if (IdentifierOctet.Class.Equals("Application", StringComparison.OrdinalIgnoreCase))
            {
                var tagInInt = Convert.ToInt32(tagNumber);
                IdentifierOctet.Tag = $"APPLICATION {tagInInt}";
            }
        }

        private void GetPc(string pc)
        {
            switch (pc)
            {
                case "0":
                    IdentifierOctet.PC = "Primitive";
                    break;
                case "1":
                    IdentifierOctet.PC = "Constructed";
                    break;
            }
        }

        private void GetClass(string classofTag)
        {
            switch (classofTag)
            {
                case "00":
                    IdentifierOctet.Class = "Universal";
                    break;
                case "01":
                    IdentifierOctet.Class = "Application";
                    break;
                case "10":
                    IdentifierOctet.Class = "Context-specific";
                    break;
                case "11":
                    IdentifierOctet.Class = "Private";
                    break;
                default:
                    IdentifierOctet.Class = "unidentified";
                    Console.WriteLine($"Class not found: {classofTag}. Please try again");
                    throw new Exception("Unidentified Type!!! in GetClass(string classofTag)");
                    break;

            }
        }
    }
}