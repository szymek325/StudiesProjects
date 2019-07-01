using System.Collections.Generic;
using SnmpAgent.MibParsing.Models;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IMibParsingRunner
    {
        DependencyTreeNode ParseMib();
        IEnumerable<DataType> GetDataTypes();

    }
}