using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Helpers.Implementation
{
    public class NodeFinder : INodeFinder
    {
        public DependencyTreeNode FoundNode { get; set; }

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

        public void SetNeededElement(DependencyTreeNode node, string oid)
        {
            if (node.Oid.Equals(oid))
            {
                FoundNode = node;
                return;
            }

            foreach (var child in node.ChildrenNodes)
            {
                SetNeededElement(child, oid);
            }
        }

        public DependencyTreeNode GetFoundNode()
        {
            if (FoundNode == null)
            {
                FoundNode = new DependencyTreeNode
                {
                    Name = "not found"
                };
            }
            return FoundNode;
        }



        private void ShowParentAndChildrens(DependencyTreeNode node)
        {
            node.ShowNode();
            foreach (var child in node.ChildrenNodes)
            {
                child.ShowNode();
            }
        }
    }
}