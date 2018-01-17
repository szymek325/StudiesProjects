using System;
using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class BerEncoder : IBerEncoder
    {
        private readonly IValueOctetEncoder valueOctetEncoder;

        public BerEncoder(IValueOctetEncoder valueOctetEncoder)
        {
            this.valueOctetEncoder = valueOctetEncoder;
        }

        public string Encode(DependencyTreeNode node, string value)
        {
            if (node.Status == null)
            {
                var tag = "06";
                var codedValue=valueOctetEncoder.EncodeOid(node.Oid);
                var valueLength = codedValue.Length / 2;
                var length= valueLength.ToString("X2");

                return tag + length + codedValue;
            }

            //if object identifier then code OID, dont check value

            //else
            //check if input is okay with limits of object
            //code




            throw new NotImplementedException();
        }
    }
}