using System;
using System.Collections.Generic;
using System.Linq;
using SnmpAgent.MibParsing.Interface;
using SnmpAgent.MibParsing.Models;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.MibParsing.Implementation
{
    public class DependencyTreeCreator : IDependencyTreeCreator
    {
        private readonly MibModelProvider mibModelProvider;

        public DependencyTreeCreator(MibModelProvider mibModelProvider)
        {
            this.mibModelProvider = mibModelProvider;
        }

        public static Mib MibModel { get; set; }
        public IEnumerable<ObjectIdentifier> ListOfAllObjects { get; set; } = new List<ObjectIdentifier>();
        public IEnumerable<DependencyTreeNode> TreeNodes { get; set; }
        public DependencyTreeNode Tree { get; set; }

        public void AddConnectionToDataTypes()
        {
            MibModel.ObjectTypes = MibModel.ObjectTypes.ToList();
            foreach (var node in MibModel.ObjectTypes)
            {
                foreach (var dataType in MibModel.DataTypes)
                {
                    if (node.Syntax.Name.Equals(dataType.Name))
                    {
                        node.Syntax = new Syntax
                        {
                            Name = dataType.Name,
                            Min = dataType.Min,
                            Max = dataType.Max,
                            Mode = dataType.Mode
                        };
                    }
                }
            }
        }

        public void CreateTreeNodes()
        {
            ListOfAllObjects = MibModel.ObjectTypes.Concat(
                MibModel.ObjectIdentifiers.Select(x => new ObjectType
                {
                    Name = x.Name,
                    NameOfNodeAbove = x.NameOfNodeAbove,
                    LeafNumber = x.LeafNumber
                })
            ).ToList();

            TreeNodes = ListOfAllObjects.Select(x => (DependencyTreeNode) x);
        }

        public DependencyTreeNode GetDependencyTree(string mibName)
        {
            MibModel = mibModelProvider.GetMibContent(mibName);

            AddConnectionToDataTypes();

            CreateTreeNodes();
            AddParentsAndChildrens();
            Tree = TreeNodes.FirstOrDefault(x => x.ParentNode == null);
            CreteOids(Tree);

            return Tree;
        }

        private void AddParentsAndChildrens()
        {
            TreeNodes = TreeNodes.ToList();
            foreach (var node in TreeNodes)
            {
                node.ParentNode = TreeNodes.FirstOrDefault(x =>
                    x.Name.Equals(node.ParentNode.Name, StringComparison.OrdinalIgnoreCase));
                node.ChildrenNodes = TreeNodes.Where(x =>
                    x.ParentNode != null
                        ? x.ParentNode.Name.Equals(node.Name, StringComparison.OrdinalIgnoreCase)
                        : false
                );
            }
        }

        private void CreteOids(DependencyTreeNode node)
        {
            var childrenNodes = node.ChildrenNodes.ToList();
            foreach (var children in childrenNodes)
            {
                children.Oid = string.Format("{0}.{1}", children.ParentNode.Oid, children.Oid);
                CreteOids(children);
            }
        }
    }
}