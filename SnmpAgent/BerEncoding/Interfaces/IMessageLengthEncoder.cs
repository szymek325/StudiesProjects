namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IMessageLengthEncoder
    {
        string GetEncodedLentgh(string messageValue);
    }
}