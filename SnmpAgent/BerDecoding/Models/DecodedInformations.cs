namespace SnmpAgent.BerDecoding.Models
{
    public class DecodedInformations
    {
        public IdentifierOctet IdentifierOctet { get; set; }
    }

    public class IdentifierOctet
    {
        public string Class { get; set; }
        public string PC { get; set; }
        public string Tag { get; set; }
    }
}