using System;
using System.Collections.Generic;
using SnmpAgent.Models.MibParts;

namespace SnmpAgent.Models
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
            return new DependencyTreeNode()
            {
                Name = model.Name,
                Oid = model.LeafNumber.ToString(),
                Syntax= model.Syntax,
                Access = model.Access,
                Status = model.Status,
                Description = model.Description,
                Index = model.Index,
                ParentNode = new DependencyTreeNode()
                {
                    Name = model.NameOfNodeAbove
                }
            };
        }
    }
}