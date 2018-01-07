using System.Collections.Generic;
using System.Text.RegularExpressions;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IMainObjectIdentifiersCreator
    {
        List<ObjectIdentifier> CreateMainObjectIdentifiersNotDefinedInMib(Match match);
    }
}