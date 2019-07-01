using System;
using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class BerEncoder : IBerEncoder
    {
        private readonly IObjectIdentifierEncoder objectIdentifierEncoder;
        private readonly IObjectEncoder objectEncoder;
        private readonly IInputValidator inputValidator;

        public BerEncoder(IObjectIdentifierEncoder objectIdentifierEncoder, IObjectEncoder objectEncoder, IInputValidator inputValidator)
        {
            this.objectIdentifierEncoder = objectIdentifierEncoder;
            this.objectEncoder = objectEncoder;
            this.inputValidator = inputValidator;
        }

        public string Encode(DependencyTreeNode node, string input)
        {
            if (node.Status == null)
            {
                return objectIdentifierEncoder.GetEncodedMessage(node);
            }
            else
            {
                if (inputValidator.CheckIfValueCompliesWithObjectSyntax(node.Syntax,input))//TODO
                {
                    return objectEncoder.GetEncodedObject(node, input);
                }

                return "Input doesn't comply with syntax of object";
            }
        }


    }
}