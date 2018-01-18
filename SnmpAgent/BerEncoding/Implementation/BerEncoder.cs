using System;
using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class BerEncoder : IBerEncoder
    {
        private readonly IObjectIdentifierEncoder objectIdentifierEncoder;

        public BerEncoder(IObjectIdentifierEncoder objectIdentifierEncoder)
        {
            this.objectIdentifierEncoder = objectIdentifierEncoder;
        }

        public string Encode(DependencyTreeNode node, string value)
        {
            //if object identifier then code OID, dont check value
            if (node.Status == null)
            {
                return objectIdentifierEncoder.GetEncodedMessage(node.Oid);
            }

            CheckIfValueCompliesWithObjectSyntax(); //TODO





            

            throw new NotImplementedException();
        }

        private void CheckIfValueCompliesWithObjectSyntax()
        {

        }
    }
}