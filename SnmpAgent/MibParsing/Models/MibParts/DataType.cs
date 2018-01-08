using System.Linq;
using System.Text.RegularExpressions;
using SnmpAgent.MibParsing.Constants;
using SnmpAgent.MibParsing.Implementation;

namespace SnmpAgent.MibParsing.Models.MibParts
{
    public class DataType
    {
        public string Name { get; set; }
        public string Application { get; set; }
        public string Mode { get; set; }
        public string Type { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }

        public static explicit operator DataType(Match v)
        {
            var regexRunner = new CustomRegexRunner();
            var matches = regexRunner.GetAllMatches(RegexConstants.SyntaxLimitationsPattern, v.Groups[5].Value);
            
            return new DataType
            {
                Name = v.Groups[1].Value,
                Application = Regex.Match(v.Groups[2].Value, @"\d+").Value,
                Mode = v.Groups[4].Value,
                Type = v.Groups[5].Value,
                Min = !matches.Count.Equals(0) ? matches.First().Groups[1].Value : null,
                Max = !matches.Count.Equals(0) ? matches.First().Groups[2].Value : null
            };
        }
    }
}