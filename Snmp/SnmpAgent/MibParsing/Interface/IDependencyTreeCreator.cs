using System.Collections.Generic;
using SnmpAgent.MibParsing.Models;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IDependencyTreeCreator
    {
        DependencyTreeNode GetDependencyTree();
        void CreateDependencyTree(string mibName);
        IEnumerable<DataType> GetDataTypes();
    }
}