using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SnmpAgent.Providers
{
    public class RegexRunner
    {
        public RegexRunner(string pattern, string text)
        {
            Pattern = pattern;
            Text = text;
        }

        public string Pattern { get; set; }
        public string Text { get; set; }

        public MatchCollection GetAllMatches()
        {
            var myRegex = new Regex(Pattern, RegexOptions.Singleline);

            return myRegex.Matches(Text);
        }

        public MatchCollection GetAllMatchesWithoutSingleLine()
        {
            var myRegex = new Regex(Pattern);

            return myRegex.Matches(Text);
        }
    }
}
