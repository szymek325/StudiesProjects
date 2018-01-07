using System.Text.RegularExpressions;

namespace SnmpAgent.MibParsing.Interface
{
    public interface ICustomRegexRunner
    {
        MatchCollection GetAllMatches(string pattern, string text);
        MatchCollection GetAllMatchesWithoutSingleLine(string pattern, string text);
    }
}