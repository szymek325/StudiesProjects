using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Helpers.Interface
{
    public interface IDependencyTreeViewer
    {
        void ShowDependencyTree(DependencyTreeNode tree);
    }
}