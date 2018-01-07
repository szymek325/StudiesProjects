using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MainProgramLoop
{
    internal class SnmpRunner : ISnmpRunner
    {
        private readonly IMibParsingRunner mibParsingRunner;

        public SnmpRunner(IMibParsingRunner mibParsingRunner)
        {
            this.mibParsingRunner = mibParsingRunner;
        }

        public void Run()
        {
            mibParsingRunner.StartParsing();

        }
    }
}