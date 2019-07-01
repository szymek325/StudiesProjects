using System;
using System.Linq;
using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.Helpers.View
{
    public class DependencyTreeViewer : IDependencyTreeViewer
    {
        public void ShowDependencyTree(DependencyTreeNode tree)
        {
            Console.WriteLine();
            Console.WriteLine("---------DEPENDENCY TREE---------");
            Console.WriteLine();
            Console.Write(string.Format("\\ {0} {1}", tree.Oid, tree.Name));
            ShowSubObjects(tree);
        }

        private void ShowSubObjects(DependencyTreeNode node)
        {
            Console.WriteLine();
            var children = node.ChildrenNodes;
            foreach (var child in children)
            {
                var count = child.Oid.Count(f => f == '.');
                for (var i = 0; i < count; i++)
                    Console.Write(" ");

                var oidNumbers = child.Oid.Split(".");
                var oid = oidNumbers[oidNumbers.Length - 1];

                Console.Write(
                    child.ChildrenNodes.Count().Equals(0)
                        ? string.Format("| {0} {1}", oid, child.Name)
                        : string.Format("\\ {0} {1}", oid, child.Name)
                );
                ShowSubObjects(child);
            }
        }
    }
}