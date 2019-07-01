using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IObjectIdentifierEncoder
    {
        string GetEncodedMessage(DependencyTreeNode node);
    }
}