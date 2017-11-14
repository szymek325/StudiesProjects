using System;
using System.Collections.Generic;
using System.Linq;

namespace SnmpAgent.Models
{
    public class Mib
    {
        public string Import { get; set; }
        public IEnumerable<ObjectType> ObjectTypes { get; set; } = new List<ObjectType>();
        public IEnumerable<ObjectIdentifier> ObjectIdentifiers { get; set; } = new List<ObjectIdentifier>();
        public IEnumerable<DataType> DataTypes { get; set; } = new List<DataType>();
        public IEnumerable<Sequence> Sequences { get; set; } = new List<Sequence>();
        public IEnumerable<ObjectIdentifier> DependencyTree { get; set; } = new List<ObjectIdentifier>();

        public void CreateDependencyTree()
        {
            DependencyTree = ObjectIdentifiers.Concat(ObjectTypes).ToList();
            AddParentsAndChildrens();

            CreteOids(DependencyTree.FirstOrDefault(x => x.ParentNode == null));

            DependencyTree = DependencyTree.OrderBy(x => x.Oid).ToList();
        }

        public void ShowDependencyTree()
        {
            var oidOfFirstElementInStructure = "1";
            var mainObject = DependencyTree.FirstOrDefault(x => x.Oid.Equals(oidOfFirstElementInStructure));
            Console.Write(string.Format("\\ {0} {1}",mainObject.Oid,mainObject.Name));
            ShowSubObjects(mainObject.Name);
        }

        private void ShowSubObjects(string name)
        {
            Console.WriteLine();
            var children =
                DependencyTree.Where(x => x.NameOfNodeAbove.Equals(name, StringComparison.OrdinalIgnoreCase));
            foreach (var node in children)
            {
                var count = node.Oid.Count(f => f == '.');
                for (var i = 0; i < count; i++)
                    Console.Write(" ");

                Console.Write(
                    node.ChildrenNodes.Count().Equals(0)
                    ? string.Format("| {0} {1}", node.LeafNumber, node.Name)
                    : string.Format("\\ {0} {1}", node.LeafNumber, node.Name)
                    );
                ShowSubObjects(node.Name);
            }
        }

        private void AddParentsAndChildrens()
        {
            foreach (var node in DependencyTree)
            {
                node.ParentNode = DependencyTree.FirstOrDefault(x =>
                    x.Name.Equals(node.NameOfNodeAbove, StringComparison.OrdinalIgnoreCase));
                node.ChildrenNodes = DependencyTree.Where(x =>
                    x.NameOfNodeAbove.Equals(node.Name, StringComparison.OrdinalIgnoreCase));
            }
        }

        private void CreteOids(ObjectIdentifier node)
        {
            var childrenNodes = node.ChildrenNodes.ToList();
            foreach (var children in childrenNodes)
            {
                children.Oid = string.Format("{0}.{1}", children.ParentNode.Oid, node.LeafNumber);
                CreteOids(children);
            }
        }
    }
}