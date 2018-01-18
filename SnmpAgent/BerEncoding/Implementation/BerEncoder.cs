using System;
using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class BerEncoder : IBerEncoder
    {
        private readonly IObjectIdentifierEncoder objectIdentifierEncoder;
        private readonly IObjectEncoder objectEncoder;

        public BerEncoder(IObjectIdentifierEncoder objectIdentifierEncoder, IObjectEncoder objectEncoder)
        {
            this.objectIdentifierEncoder = objectIdentifierEncoder;
            this.objectEncoder = objectEncoder;
        }

        public string Encode(DependencyTreeNode node, string value)
        {
            //if object identifier then code OID, dont check value
            if (node.Status == null)
            {
                return objectIdentifierEncoder.GetEncodedMessage(node);
            }
            else
            {
                CheckIfValueCompliesWithObjectSyntax(); //TODO
                return objectEncoder.GetEncodedObject(node);
            }
        }

        private void CheckIfValueCompliesWithObjectSyntax()
        {

        }
    }
}