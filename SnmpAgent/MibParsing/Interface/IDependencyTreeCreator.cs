using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IDependencyTreeCreator
    {
        DependencyTreeNode GetDependencyTree();
        void CreateDependencyTree(string mibName);
    }
}