using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using SnmpAgent.Models;
using SnmpAgent.Models.MibParts;
using SnmpAgent.Providers;

namespace SnmpAgent.Helpers.View
{
    public static class TreeOperations
    {
        public static void ShowDependencyTree(this DependencyTreeNode tree)
        {
            Console.WriteLine();
            Console.WriteLine("---------DEPENDENCY TREE---------");
            Console.WriteLine();
            Console.Write(string.Format("\\ {0} {1}", tree.Oid, tree.Name));
            ShowSubObjects(tree);
        }

        private static void ShowSubObjects(DependencyTreeNode node)
        {
            Console.WriteLine();
            var children = node.ChildrenNodes;
            foreach (var child in children)
            {
                var count = child.Oid.Count(f => f == '.');
                for (var i = 0; i < count; i++)
                    Console.Write(" ");

                var oidNumbers = child.Oid.Split(".");
                var oid = oidNumbers[oidNumbers.Length-1];

                Console.Write(
                    child.ChildrenNodes.Count().Equals(0)
                    ? string.Format("| {0} {1}", oid, child.Name)
                    : string.Format("\\ {0} {1}", oid, child.Name)
                    );
                ShowSubObjects(child);
            }
        }

        public static void FindAndShowElement(this DependencyTreeNode node,string name)
        {
            if (node.Name.Equals(name))
            {
                ShowParentAndChildrens(node);
                return;
            }
            foreach (var child in node.ChildrenNodes)
            {
                FindAndShowElement(child,name);
            }
        }

        private static void ShowParentAndChildrens(DependencyTreeNode node)
        {
            node.ShowNode();
            foreach (var child in node.ChildrenNodes)
            {
                child.ShowNode();
            }
        }
    }
}
