using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IDependencyTreeCreator
    {
        DependencyTreeNode GetDependencyTree(string mibName);
        void AddConnectionToDataTypes();
        void CreateTreeNodes();
    }
}