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

        //to be deleted

        //to be deleted
        //public void FindElementInTree(string name, ObjectIdentifier node=null)
        //{
        //    if (node == null)
        //        node = Tree;

        //    foreach (var child in node.ChildrenNodes)
        //    {
        //        if (child.Name.Equals(name))
        //            child.ShowObjectType();
        //        FindElementInTree(name, child);
        //    }
        //}
        ////to be deleted
        //public void ShowDependencyTree()
        //{
            
        //    Console.Write(string.Format("\\ {0} {1}",Tree.Oid,Tree.Name));
        //    ShowSubObjects(Tree);
        //}

        //private void ShowSubObjects(ObjectIdentifier node)
        //{
        //    Console.WriteLine();
        //    var children = node.ChildrenNodes;
        //    foreach (var child in children)
        //    {
        //        var count = child.Oid.Count(f => f == '.');
        //        for (var i = 0; i < count; i++)
        //            Console.Write(" ");

        //        Console.Write(
        //            child.ChildrenNodes.Count().Equals(0)
        //            ? string.Format("| {0} {1}", child.LeafNumber, child.Name)
        //            : string.Format("\\ {0} {1}", child.LeafNumber, child.Name)
        //            );
        //        ShowSubObjects(child);
        //    }
        //}


    }
}