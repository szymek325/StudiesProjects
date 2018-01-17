﻿using System;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class BerDecoder : IBerDecoder
    {
        private readonly IIdentifierOctetDecoder identifierOctetDecoder;
        private readonly ILengthDecoder lengthDecoder;
        private readonly IValueOctetsDecoder valueOctetsDecoder;

        public BerDecoder(IIdentifierOctetDecoder identifierOctetDecoder, ILengthDecoder lengthDecoder,
            IValueOctetsDecoder valueOctetsDecoder)
        {
            this.identifierOctetDecoder = identifierOctetDecoder;
            this.lengthDecoder = lengthDecoder;
            this.valueOctetsDecoder = valueOctetsDecoder;
        }

        public DecodedInformations Decode(ref byte[] input)
        {
            var receivedData = new DecodedInformations();

            receivedData.IdentifierOctet= identifierOctetDecoder.GetType(ref input);
            receivedData.Length = lengthDecoder.GetLenght(ref input);



            if (!receivedData.IdentifierOctet.Tag.Contains("SEQUENCE"))
            {
                receivedData.Value =
                    valueOctetsDecoder.GetValue(ref input, receivedData.IdentifierOctet.Tag, receivedData.Length);
                return receivedData;
            }
            else
            {
                receivedData.Value = "Sequences";
                while (input.Length != default(int))
                {
                    var newElement = Decode(ref input);
                    receivedData.Sequences.Add(newElement);
                }

                return receivedData;
            }


        }
    }
}