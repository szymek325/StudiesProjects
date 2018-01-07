using SnmpAgent.MibParsing.Interface;

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

        public void StartParsing()
        {
            var mibName = mibPicker.GetMibName();
            dependencyTreeCreator.CreateDependencyTree(mibName);
            var tree = dependencyTreeCreator.GetDependencyTree();
        }
    }
}