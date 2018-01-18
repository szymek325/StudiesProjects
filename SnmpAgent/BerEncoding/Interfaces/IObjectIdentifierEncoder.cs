namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IObjectIdentifierEncoder
    {
        string GetEncodedMessage(string oid);
    }
}