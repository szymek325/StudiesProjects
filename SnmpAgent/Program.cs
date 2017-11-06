using System;
using System.Text.RegularExpressions;

namespace SnmpAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            string myPattern = @"(\w*\s* )OBJECT-TYPE\s* SYNTAX(.*? )ACCESS(.*? )STATUS(.*? )DESCRIPTION\s * ""(.*?)""\s *::=\s *{.*?}";
            var myRegex= new Regex(myPattern,RegexOptions.Singleline);



            var mib= new Mib();
            mib.ReadFile();
            //Console.WriteLine(mib.Path);
            //Console.WriteLine(mib.Text);
            Console.WriteLine(myPattern);

            Console.WriteLine("\n" + "*** First Match ***");
            var oneMatch = myRegex.Match(mib.Text);
            if (oneMatch.Success)
            {
                Console.WriteLine("Overall Match: " + oneMatch.Groups[0].Value);
                Console.WriteLine("Group 1: " + oneMatch.Groups[1].Value);
                Console.WriteLine("Group 2: " + oneMatch.Groups[2].Value);
                Console.WriteLine("Group 3: " + oneMatch.Groups[3].Value);
                Console.WriteLine("Group 4: " + oneMatch.Groups[4].Value);
                Console.WriteLine("Group 5: " + oneMatch.Groups[5].Value);
            }


            //Console.WriteLine("\n" + "*** Matches ***");
            //var allMatches = myRegex.Matches(mib.Text);
            //if (allMatches.Count > 0)
            //{
            //    foreach (Match SomeMatch in allMatches)
            //    {
            //        Console.WriteLine("Overall Match: " + SomeMatch.Groups[0].Value);
            //        Console.WriteLine("Group 1: " + SomeMatch.Groups[1].Value);
            //        Console.WriteLine("Group 2: " + SomeMatch.Groups[2].Value);
            //        Console.WriteLine("Group 3: " + SomeMatch.Groups[3].Value);
            //    }
            //}


            Console.ReadKey();
        }
    }
}
