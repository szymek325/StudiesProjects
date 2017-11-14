using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

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
        }

        private void AddParentsAndChildrens()
        {
            foreach (var node in DependencyTree)
            {
                node.ParentNode = DependencyTree.FirstOrDefault(x => x.Name.Equals(node.NameOfNodeAbove, StringComparison.OrdinalIgnoreCase));
                node.ChildrenNodes = DependencyTree.Where(x => x.NameOfNodeAbove.Equals(node.Name, StringComparison.OrdinalIgnoreCase));
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