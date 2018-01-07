using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IBerEncoder
    {
        void Encode(DependencyTreeNode node, string value);
    }
}