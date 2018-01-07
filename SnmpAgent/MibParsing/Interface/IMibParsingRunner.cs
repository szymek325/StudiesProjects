using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IMibParsingRunner
    {
        DependencyTreeNode ParseMib();
    }
}