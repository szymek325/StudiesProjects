using System;
using System.Collections.Generic;
using System.Linq;
using SnmpAgent.Models.MibParts;

namespace SnmpAgent.Models
{
    public class Mib
    {
        public string Import { get; set; }
        public IEnumerable<ObjectType> ObjectTypes { get; set; } = new List<ObjectType>();
        public IEnumerable<ObjectIdentifier> ObjectIdentifiers { get; set; } = new List<ObjectIdentifier>();
        public IEnumerable<DataType> DataTypes { get; set; } = new List<DataType>();
        public IEnumerable<Sequence> Sequences { get; set; } = new List<Sequence>();
        public IEnumerable<ObjectIdentifier> ListOfAllObjects { get; set; } = new List<ObjectIdentifier>();
        public ObjectIdentifier Tree { get; set; }

        public void CreateDependencyTree()
        {
            ListOfAllObjects = ObjectIdentifiers.Concat(ObjectTypes).ToList();
            AddParentsAndChildrens();

            Tree = ListOfAllObjects.FirstOrDefault(x => x.ParentNode == null);

            CreteOids(Tree);
            
        }

        public void FindElementInTree(string name, ObjectIdentifier node=null)
        {
            if (node == null)
                node = Tree;

            foreach (var child in node.ChildrenNodes)
            {
                if (child.Name.Equals(name))
                    child.ShowObjectType();
                FindElementInTree(name, child);
            }
        }

        public void ShowDependencyTree()
        {
            
            Console.Write(string.Format("\\ {0} {1}",Tree.Oid,Tree.Name));
            ShowSubObjects(Tree);
        }

        private void ShowSubObjects(ObjectIdentifier node)
        {
            Console.WriteLine();
            var children = node.ChildrenNodes;
            foreach (var child in children)
            {
                var count = child.Oid.Count(f => f == '.');
                for (var i = 0; i < count; i++)
                    Console.Write(" ");

                Console.Write(
                    child.ChildrenNodes.Count().Equals(0)
                    ? string.Format("| {0} {1}", child.LeafNumber, child.Name)
                    : string.Format("\\ {0} {1}", child.LeafNumber, child.Name)
                    );
                ShowSubObjects(child);
            }
        }

        private void AddParentsAndChildrens()
        {
            foreach (var node in ListOfAllObjects)
            {
                node.ParentNode = ListOfAllObjects.FirstOrDefault(x =>
                    x.Name.Equals(node.NameOfNodeAbove, StringComparison.OrdinalIgnoreCase));
                node.ChildrenNodes = ListOfAllObjects.Where(x =>
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