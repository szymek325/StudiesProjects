using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Interface
{
    public interface IBerDecoder
    {
        void Decode(byte[] input);
    }
}