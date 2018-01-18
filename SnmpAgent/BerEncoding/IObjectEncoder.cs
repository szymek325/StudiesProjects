using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding
{
    public interface IObjectEncoder
    {
        string GetEncodedObject(DependencyTreeNode node);
    }
}