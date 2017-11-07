using System;
using System.Text.RegularExpressions;

namespace SnmpAgent.Models
{
    public class ObjectType
    {
        public string Name { get; set; }
        public string Syntax { get; set; }
        public string Access { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string NameOfNodeAbove { get; set; }
        public int LeafNumber { get; set; }

        public void ShowObjectType()
        {
            Console.WriteLine();
            Console.WriteLine(nameof(Name) + ": " + Name);
            Console.WriteLine(nameof(Syntax) + ": " + Syntax);
            Console.WriteLine(nameof(Access) + ": " + Access);
            Console.WriteLine(nameof(Status) + ": " + Status);
            Console.WriteLine(nameof(Description) + ":");
            Console.WriteLine(Description);
            Console.WriteLine(nameof(NameOfNodeAbove) + ": " + NameOfNodeAbove);
            Console.WriteLine(nameof(LeafNumber) + ": " + LeafNumber);
            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
        }


        public static explicit operator ObjectType(Match match)
        {
            return new ObjectType
            {
                Name = match.Groups[1].Value,
                Syntax = match.Groups[2].Value,
                Access = match.Groups[3].Value,
                Status = match.Groups[4].Value,
                Description = Regex.Replace(match.Groups[5].Value, @"\r\n?|\n\s*", " "),
                NameOfNodeAbove = match.Groups[6].Value,
                LeafNumber = int.Parse(match.Groups[7].Value)
            };
        }
    }
}