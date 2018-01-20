using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IValueOctetEncoder
    {
        string EncodeOid(string oid);
        string EncodeObjectValue(string syntaxName, string valueToEncode);
    }
}