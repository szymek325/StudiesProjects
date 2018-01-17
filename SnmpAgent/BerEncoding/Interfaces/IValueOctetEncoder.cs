namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IValueOctetEncoder
    {
        string EncodeOid(string oid);
    }
}