using System;
using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class Encoding : IEncoding
    {
        private DependencyTreeNode node;
        private string value;

        public void Encode(DependencyTreeNode node, string value)
        {
            this.node = node;
            this.value = value;

            string encodedData;

            encodedData = EncodeTag();
            Console.WriteLine(encodedData);

        }

        private string EncodeTag()
        {
            if (node.Syntax!=null)
            {
                switch (node.Syntax.Name)
                {
                    case "INTEGER":
                        return "2";
                    case "OCTET STRING":
                        return "4";
                    case "NULL":
                        return "2";
                    case "SEQUENCE":
                        return "16";
                    case "SEQUENCE OF":
                        return "16";
                    case "DisplayString":
                        return "4";
                    case "PhysAddress":
                        return "4";
                    default:
                        return "6";
                }
            }
            else
                return "6";
        }
    }
}