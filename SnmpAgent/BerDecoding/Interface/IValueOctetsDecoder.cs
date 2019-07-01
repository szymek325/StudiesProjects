namespace SnmpAgent.BerDecoding.Interface
{
    public interface IValueOctetsDecoder
    {
        string GetValue(ref byte[] input, string tag, int length);
    }
}