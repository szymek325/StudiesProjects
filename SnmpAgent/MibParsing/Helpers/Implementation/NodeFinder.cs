﻿using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Helpers.Implementation
{
    public class NodeFinder : INodeFinder
    {
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
            if (node.Name.Equals(oid))
            {
                ShowParentAndChildrens(node);
                return;
            }

            foreach (var child in node.ChildrenNodes)
            {
                FindAndShowElement(child, oid);
            }
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