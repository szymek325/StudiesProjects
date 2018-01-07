using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Helpers.Implementation
{
    public class NodeFinder : INodeFinder
    {
        private readonly IBerEncoder berEncoder;

        public NodeFinder(IBerEncoder berEncoder)
        {
            this.berEncoder = berEncoder;
        }

        public void FindAndShowElement(DependencyTreeNode node, string name)
        {
            if (node.Name.Equals(name))
            {
                ShowParentAndChildrens(node);
                return;
            }

            foreach (var child in node.ChildrenNodes)
            {
                FindAndShowElement(child, name);
            }
        }

        public void FindAndShowElementByOid(DependencyTreeNode node, string oid)
        {
            if (node.Oid.Equals(oid))
            {
                ShowParentAndChildrens(node);
                return;
            }

            foreach (var child in node.ChildrenNodes)
            {
                FindAndShowElementByOid(child, oid);
            }
        }

        private void ShowParentAndChildrens(DependencyTreeNode node)
        {
            node.ShowNode();
            berEncoder.Encode(node, "5");
            foreach (var child in node.ChildrenNodes)
            {
                child.ShowNode();
            }
        }
    }
}