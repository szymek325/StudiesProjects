using SnmpAgent.MibParsing.Models.MibParts;
using System;
using System.Collections.Generic;

namespace SnmpAgent.MibParsing.Models
{
    public class DependencyTreeNode
    {
        public string Name { get; set; }
        public string Oid { get; set; }
        public DependencyTreeNode ParentNode { get; set; }
        public IEnumerable<DependencyTreeNode> ChildrenNodes { get; set; }

        //not included in all nodes
        public Syntax Syntax { get; set; }
        public string Access { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Index { get; set; }

        public static explicit operator DependencyTreeNode(ObjectType model)
        {
            return new DependencyTreeNode
            {
                Name = model.Name,
                Oid = model.LeafNumber.ToString(),
                Syntax = model.Syntax,
                Access = model.Access,
                Status = model.Status,
                Description = model.Description,
                Index = model.Index,
                ParentNode = new DependencyTreeNode
                {
                    Name = model.NameOfNodeAbove
                }
            };
        }

        public void ShowNode()
        {
            Console.WriteLine();
            Console.WriteLine(nameof(Name) + ": " + Name);
            Console.WriteLine(nameof(Oid) + ": " + Oid);
            if (ParentNode != null && !string.IsNullOrEmpty(ParentNode.Name))
                Console.WriteLine("Parent node: " + ParentNode.Name);
            if (Syntax != null
                && !string.IsNullOrEmpty(Access)
                && !string.IsNullOrEmpty(Status)
                && !string.IsNullOrEmpty(Description))
            {
                Console.WriteLine(" Syntax: " + Syntax.Name);
                if (!string.IsNullOrEmpty(Syntax.Max))
                {
                    Console.WriteLine(string.Format("       MIN: {0}", Syntax.Min));
                    Console.WriteLine(string.Format("       MAX: {0}", Syntax.Max));
                }

                Console.WriteLine(nameof(Access) + ": " + Access);
                Console.WriteLine(nameof(Status) + ": " + Status);
                Console.WriteLine(nameof(Description) + ":");
                Console.WriteLine(Description);
                if (Index != "" && !Index.Equals("\n ")) //sometimes "\n" is added to Index beacuse of bad parsing
                    Console.WriteLine(nameof(Index) + ": " + Index);
            }

            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
        }

    }
}