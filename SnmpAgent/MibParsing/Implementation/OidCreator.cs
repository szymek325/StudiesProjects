using System.Collections.Generic;
using System.Text.RegularExpressions;
using SnmpAgent.MibParsing.Interface;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.MibParsing.Implementation
{
    public class OidCreator : IOidCreator
    {
        public List<ObjectIdentifier> CreateMainOids(Match match)
        {
            return new List<ObjectIdentifier>
            {
                new ObjectIdentifier
                {
                    Name = match.Groups[2].Value,
                    NameOfNodeAbove = "",
                    LeafNumber = 1
                },
                new ObjectIdentifier
                {
                    Name = match.Groups[3].Value,
                    NameOfNodeAbove = match.Groups[2].Value,
                    LeafNumber = int.Parse(match.Groups[4].Value)
                },
                new ObjectIdentifier
                {
                    Name = match.Groups[5].Value,
                    NameOfNodeAbove = match.Groups[3].Value,
                    LeafNumber = int.Parse(match.Groups[6].Value)
                },
                new ObjectIdentifier
                {
                    Name = match.Groups[1].Value,
                    NameOfNodeAbove = match.Groups[5].Value,
                    LeafNumber = int.Parse(match.Groups[7].Value)
                }
            };
        }
    }
}