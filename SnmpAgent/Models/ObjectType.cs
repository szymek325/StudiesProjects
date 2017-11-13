using System;
using System.Text.RegularExpressions;

namespace SnmpAgent.Models
{
    public class ObjectType : ObjectIdentifier
    {
        public string Syntax { get; set; }
        public string Access { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Index { get; set; }

        public override void ShowObjectType()
        {
            Console.WriteLine();
            Console.WriteLine(nameof(Name) + ": " + Name);
            Console.WriteLine(nameof(Syntax) + ": " + Syntax);
            Console.WriteLine(nameof(Access) + ": " + Access);
            Console.WriteLine(nameof(Status) + ": " + Status);
            Console.WriteLine(nameof(Description) + ":");
            Console.WriteLine(Description);
            if (Index != "")
                Console.WriteLine(nameof(Index) + ": " + Index);
            Console.WriteLine(nameof(NameOfNodeAbove) + ": " + NameOfNodeAbove);
            Console.WriteLine(nameof(LeafNumber) + ": " + LeafNumber);
            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
        }

        public static explicit operator ObjectType(Match match)
        {
            return new ObjectType
            {
                Name = match.Groups[1].Value.Replace(" ", string.Empty),
                Syntax = match.Groups[2].Value,
                Access = match.Groups[3].Value,
                Status = match.Groups[4].Value,
                Description = Regex.Replace(match.Groups[5].Value, @"\r\n?|\n\s*", " "),
                Index = match.Groups[6].Value,
                NameOfNodeAbove = match.Groups[7].Value.Replace(" ", string.Empty),
                LeafNumber = int.Parse(match.Groups[8].Value)
            };
        }
    }
}