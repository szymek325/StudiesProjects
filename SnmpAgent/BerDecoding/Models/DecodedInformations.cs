using System.Collections;
using System.Collections.Generic;

namespace SnmpAgent.BerDecoding.Models
{
    public class DecodedInformations
    {
        public IdentifierOctet IdentifierOctet { get; set; }
        public int Length { get; set; }
        public string Value { get; set; }
        public List<DecodedInformations> Sequences { get; set; }= new List<DecodedInformations>();
    }

    public class IdentifierOctet
    {
        public string Class { get; set; }
        public string PC { get; set; }
        public string Tag { get; set; }
    }
}