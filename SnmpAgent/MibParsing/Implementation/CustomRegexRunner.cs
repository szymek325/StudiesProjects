using System.Text.RegularExpressions;
using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MibParsing.Implementation
{
    public class CustomRegexRunner : ICustomRegexRunner
    {
        public MatchCollection GetAllMatches(string pattern, string text)
        {
            var myRegex = new Regex(pattern, RegexOptions.Singleline);

            return myRegex.Matches(text);
        }

        public MatchCollection GetAllMatchesWithoutSingleLine(string pattern, string text)
        {
            var myRegex = new Regex(pattern);

            return myRegex.Matches(text);
        }
    }
}