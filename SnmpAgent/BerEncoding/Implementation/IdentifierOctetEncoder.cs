using System;
using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class IdentifierOctetEncoder : IIdentifierOctetEncoder
    {
        public string GetEncodedMessageTag(Syntax syntax)
        {
            var classType = "";
            var pc = "";
            var tag = "";

            if (syntax.Application != null)
            {
                classType = "00";
            }
            else
            {
                classType = "01";
            }

            if (syntax.Name.Contains("Sequence"))
            {
                pc = "01";
            }
            else
            {
                pc = "00";
            }

            if (classType == "00")
            {
            }
            else if (classType == "01")
            {
            }


            throw new NotImplementedException();
        }

        private string GetTag(string name)
        {
            var tag=0;
            switch (name)
            {
                case "INTEGER":
                    tag = 2;
                    break;
                case "OCTET STRING":
                    tag = 4;
                    break;
                //case 6:
                //    .Tag = "OBJECT IDENTIFIER";
                //    break;
                case "SEQUENCE":
                    tag = 16;
                    break;
                case "DisplayString":
                    tag = 26;
                    break;
                default:
                    Console.WriteLine($"CLassName not found: {name}. Please try again");
                    Console.ReadKey();
                    break;
            }

            var tagString = Convert.ToString(tag, 2); //Convert to binary in a string

            for (var i = 0; tagString.Length < 5; i++)
            {
                tagString = "0" + tagString;
            }


            return tagString;
        }
    }
}