using SnmpAgent.MibParsing.Interface;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Implementation
{
    public class MibParsingRunner : IMibParsingRunner
    {
        private readonly IMibPicker mibPicker;
        private readonly IDependencyTreeCreator dependencyTreeCreator;

        public MibParsingRunner(IMibPicker mibPicker, IDependencyTreeCreator dependencyTreeCreator)
        {
            this.mibPicker = mibPicker;
            this.dependencyTreeCreator = dependencyTreeCreator;
        }

        public DependencyTreeNode ParseMib()
        {
            var mibName = mibPicker.GetMibName();
            dependencyTreeCreator.CreateDependencyTree(mibName);
            return dependencyTreeCreator.GetDependencyTree();
        }
    }
}