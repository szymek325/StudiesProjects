using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Helpers.Interface
{
    public interface INodeFinder
    {
        void FindAndShowElement(DependencyTreeNode node, string name);
        void FindAndShowElementByOid(DependencyTreeNode node, string oid);
        DependencyTreeNode GetFoundNode();
        void SetNeededElement(DependencyTreeNode node, string oid);
    }
}