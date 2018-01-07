namespace SnmpAgent.BerDecoding.Interface
{
    public interface IValueOctetsDecoder
    {
        string GetValue(byte[] input, string tag, int length);
    }
}