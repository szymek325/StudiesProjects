using System.Text.RegularExpressions;

namespace SnmpAgent.MibParsing.Models.MibParts
{
    public class ObjectIdentifier
    {
        public string Name { get; set; }
        public string NameOfNodeAbove { get; set; }
        public int LeafNumber { get; set; }

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