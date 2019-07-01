using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IIdentifierOctetEncoder
    {
        string GetEncodedMessageTag(Syntax syntax);
    }
}