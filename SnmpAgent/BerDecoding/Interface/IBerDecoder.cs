using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Interface
{
    public interface IBerDecoder
    {
        DecodedInformations Decode(ref byte[] input);
    }
}