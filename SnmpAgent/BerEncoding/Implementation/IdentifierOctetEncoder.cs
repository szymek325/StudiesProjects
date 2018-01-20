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
                classType = "01";
            }
            else
            {
                classType = "00";
            }

            if (syntax.Name.Contains("Sequence"))
            {
                pc = "1";
            }
            else
            {
                pc = "0";
            }

            if (classType == "00")
            {
                var str = syntax.Name.Replace(" ", "").ToLower();
                tag = GetTag(str);
            }
            else if (classType == "01")
            {
                var applicaiton = syntax.Application;
                var applicaitonNumber = int.Parse(applicaiton);

                var tagString = Convert.ToString(applicaitonNumber, 2); //Convert to binary in a string

                for (var i = 0; tagString.Length < 5; i++)
                {
                    tagString = "0" + tagString;
                }

            }

            var bitsMessageTag = classType + pc + tag;
            var messageTag = Convert.ToInt32(bitsMessageTag, 2).ToString("X2");
            return messageTag;


        }

        private string GetTag(string name)
        {
            var tag=0;
            switch (name)
            {
                case "integer":
                    tag = 2;
                    break;
                case "octetstring":
                    tag = 4;
                    break;
                //case 6:
                //    .Tag = "OBJECT IDENTIFIER";
                //    break;
                case "sequence":
                    tag = 16;
                    break;
                case "sequenceof":
                    tag = 16;
                    break;
                case "displaystring":
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