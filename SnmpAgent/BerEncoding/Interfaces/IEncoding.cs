using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IEncoding
    {
        void Encode(DependencyTreeNode node, string value);
    }
}