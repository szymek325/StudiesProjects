using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IBerEncoder
    {
        string Encode(DependencyTreeNode node, string input);
    }
}