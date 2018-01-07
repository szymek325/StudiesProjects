using System.Collections.Generic;
using System.Text.RegularExpressions;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IOidCreator
    {
        List<ObjectIdentifier> CreateMainOids(Match match);
    }
}