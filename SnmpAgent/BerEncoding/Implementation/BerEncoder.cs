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
            if (node.Status == null)
            {
                return objectIdentifierEncoder.GetEncodedMessage(node);
            }
            else
            {
                if (CheckIfValueCompliesWithObjectSyntax())//TODO
                {
                    return objectEncoder.GetEncodedObject(node, value);
                }

                return "Input doesn't comply with syntax of object";
            }
        }

        private bool CheckIfValueCompliesWithObjectSyntax()
        {
            return true;
        }
    }
}