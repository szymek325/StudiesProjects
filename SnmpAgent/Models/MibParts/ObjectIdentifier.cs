using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SnmpAgent.Models.MibParts
{
    public class ObjectIdentifier
    {
        public string Name { get; set; }
        public string NameOfNodeAbove { get; set; }
        public int LeafNumber { get; set; }

        //to be deleted
        public string Oid { get; set; }
        public ObjectIdentifier ParentNode { get; set; }
        public IEnumerable<ObjectIdentifier> ChildrenNodes { get; set; }

        public virtual void ShowObjectType()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(nameof(Name) + ": " + Name);
            Console.WriteLine(nameof(NameOfNodeAbove) + ": " + NameOfNodeAbove);
            Console.WriteLine(nameof(LeafNumber) + ": " + LeafNumber);
            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
        }


        public static explicit operator ObjectIdentifier(Match match)
        {
            return new ObjectIdentifier
            {
                Name = match.Groups[1].Value.Replace(" ", string.Empty),
                NameOfNodeAbove = match.Groups[2].Value.Replace(" ", string.Empty),
                LeafNumber = int.Parse(match.Groups[3].Value)
            };
        }
    }
}