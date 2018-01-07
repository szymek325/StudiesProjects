using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Interface
{
    public interface IBerDecoder
    {
        SnmpMessage.SNMP_message decode(byte[] input);
    }
}