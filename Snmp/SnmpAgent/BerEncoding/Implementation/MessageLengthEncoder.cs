using SnmpAgent.BerEncoding.Interfaces;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class MessageLengthEncoder : IMessageLengthEncoder
    {
        public string GetEncodedLentgh(string messageValue)
        {
            var valueLength = messageValue.Length / 2;
            return valueLength.ToString("X2");
        }
    }
}