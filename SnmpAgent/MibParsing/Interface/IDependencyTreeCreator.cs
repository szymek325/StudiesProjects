using System;
using System.Collections.Generic;
using System.Text;
using SnmpAgent.Models;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IDependencyTreeCreator
    {
        DependencyTreeNode GetDependencyTree(string mibName);
        void AddConnectionToDataTypes();
        void CreateTreeNodes();
    }
}
