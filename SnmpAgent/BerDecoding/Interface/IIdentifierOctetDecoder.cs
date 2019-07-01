using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Interface
{
    public interface IIdentifierOctetDecoder
    {
        IdentifierOctet GetType(ref byte[] input);
    }
}