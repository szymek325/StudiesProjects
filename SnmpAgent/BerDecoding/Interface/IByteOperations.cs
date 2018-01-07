using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Interface
{
    public interface IByteOperations
    {
        IdentifierOctet GetType(byte v);
        int GetLenght(byte v);
        string GetValue(byte[] input, string tag);
    }
}