using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Interface
{
    public interface IByteOperations
    {
        IdentifierOctet GetType(byte v);
    }
}