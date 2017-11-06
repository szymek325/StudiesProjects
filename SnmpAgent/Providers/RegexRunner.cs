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

        public MatchCollection MatchCollection { get; set; }
        public string Pattern { get; set; }
        public string Text { get; set; }

        public void MatchAll()
        {
            var myRegex = new Regex(Pattern, RegexOptions.Singleline);

            this.MatchCollection = myRegex.Matches(Text);
        }
    }
}
