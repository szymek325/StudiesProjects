using System;
using System.Linq;
using System.Text.RegularExpressions;
using SnmpAgent.Constants;
using SnmpAgent.Helpers.MibProcessing;

namespace SnmpAgent.Models.MibParts
{
    public class ObjectType : ObjectIdentifier
    {
        public string Access { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Index { get; set; }
        public Syntax Syntax { get; set; }

        public override void ShowObjectType()
        {
            Console.WriteLine();
            Console.WriteLine(nameof(Name) + ": " + Name);
            Console.WriteLine(nameof(Syntax.Name) + ": " + Syntax.Name);
            if (!string.IsNullOrEmpty(Syntax.Max))
            {
                Console.WriteLine(string.Format("       MIN: {0}", Syntax.Min));
                Console.WriteLine(string.Format("       MAX: {0}", Syntax.Max));
            }
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
            var runner = new CustomRegexRunner(RegexConstants.SyntaxLimitationsPattern, match.Groups[2].Value);
            var matches = runner.GetAllMatches();
            var syntax = match.Groups[2].Value.Split("(")[0];
            return new ObjectType
            {
                Name = match.Groups[1].Value.Replace(" ", string.Empty),
                Access = match.Groups[3].Value,
                Status = match.Groups[4].Value,
                Description = Regex.Replace(match.Groups[5].Value, @"\r\n?|\n\s*", " "),
                Index = match.Groups[6].Value,
                NameOfNodeAbove = match.Groups[7].Value.Replace(" ", string.Empty),
                LeafNumber = int.Parse(match.Groups[8].Value),
                Syntax = new Syntax()
                {
                    Name = syntax,
                    Min = !matches.Count.Equals(0) ? matches.First().Groups[1].Value : null,
                    Max = !matches.Count.Equals(0) ? matches.First().Groups[2].Value : null
                },
            };
        }
    }
}