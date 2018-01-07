using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class ByteOperations : IByteOperations
    {

        public IdentifierOctet GetType(byte v)
        {
            var identifierOctet = new IdentifierOctet();

            var bits=Convert.ToString(v, 2);
            for (int i = 0; bits.Length<8; i++)
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
                var tagInInt = Convert.ToInt32(tagNumber,2);
                switch (tagInInt)
                {
                    case 2:
                        identifierOctet.TagNumber = "INTEGER";
                        break;
                    case 4:
                        identifierOctet.TagNumber = "OCTET STRING";
                        break;
                    case 6:
                        identifierOctet.TagNumber = "OBJECT IDENTIFIER";
                        break;
                    case 16:
                        identifierOctet.TagNumber = "SEQUENCE/ SEQUENCE OF";
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
                identifierOctet.TagNumber = $"APPLICATION {tagInInt.ToString()}";
            }

            return identifierOctet;
        }
    }
}