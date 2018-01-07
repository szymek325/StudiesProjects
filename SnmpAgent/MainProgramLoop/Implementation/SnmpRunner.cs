using SnmpAgent.MainProgramLoop.Interface;
using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MainProgramLoop.Implementation
{
    internal class SnmpRunner : ISnmpRunner
    {
        private readonly IMibParsingRunner mibParsingRunner;
        private readonly IMibTreeViewer mibTreeViewer;

        public SnmpRunner(IMibParsingRunner mibParsingRunner, IMibTreeViewer mibTreeViewer)
        {
            this.mibParsingRunner = mibParsingRunner;
            this.mibTreeViewer = mibTreeViewer;
        }

        public void Run()
        {
            var mibTree=mibParsingRunner.ParseMib();
            mibTreeViewer.PassTreeToViewer(mibTree);
            mibTreeViewer.StartViewMode();
        }
    }
}