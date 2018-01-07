namespace SnmpAgent.BerDecoding.Interface
{
    public interface ILengthDecoder
    {
        int GetLenght(ref byte[] input);
    }
}