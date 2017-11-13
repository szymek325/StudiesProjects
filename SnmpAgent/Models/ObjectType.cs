using System;
using System.Linq;
using System.Text.RegularExpressions;
using SnmpAgent.Constants;
using RegexRunner = SnmpAgent.Providers.RegexRunner;

namespace SnmpAgent.Models
{
    public class ObjectType : ObjectIdentifier
    {
        public string Syntax { get; set; }
        public string Access { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Index { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public override void ShowObjectType()
        {
            Console.WriteLine();
            Console.WriteLine(nameof(Name) + ": " + Name);
            Console.WriteLine(nameof(Syntax) + ": " + Syntax);
            if (!Max.Equals(0))
            {
                Console.WriteLine(string.Format("       MIN: {0}",Min));
                Console.WriteLine(string.Format("       MAX: {0}", Max));
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
            var runner = new RegexRunner(RegexConstants.SyntaxLimitationsPattern, match.Groups[2].Value);
            var matches=runner.GetAllMatches();
            var syntax = match.Groups[2].Value.Split("(")[0];
            return new ObjectType
            {
                Name = match.Groups[1].Value.Replace(" ", string.Empty),
                Syntax = syntax,
                Access = match.Groups[3].Value,
                Status = match.Groups[4].Value,
                Description = Regex.Replace(match.Groups[5].Value, @"\r\n?|\n\s*", " "),
                Index = match.Groups[6].Value,
                NameOfNodeAbove = match.Groups[7].Value.Replace(" ", string.Empty),
                LeafNumber = int.Parse(match.Groups[8].Value),
                Min= !matches.Count.Equals(0)?int.Parse(matches.First().Groups[1].Value):0,
                Max = !matches.Count.Equals(0) ? int.Parse(matches.First().Groups[2].Value):0,
            };
        }
    }
}