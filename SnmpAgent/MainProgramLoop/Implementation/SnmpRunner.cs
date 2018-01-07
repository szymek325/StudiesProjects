using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.MainProgramLoop.Interface;
using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Interface;
using IDependencyTreeViewer = SnmpAgent.MibParsing.Helpers.Interface.IDependencyTreeViewer;

namespace SnmpAgent.MainProgramLoop.Implementation
{
    internal class SnmpRunner : ISnmpRunner
    {
        private readonly IMibParsingRunner mibParsingRunner;
        private readonly IMibViewMenu mibViewMenu;
        private readonly IBerDecoder berDecoder;

        public SnmpRunner(IMibParsingRunner mibParsingRunner, IMibViewMenu mibViewMenu, IBerDecoder berDecoder)
        {
            this.mibParsingRunner = mibParsingRunner;
            this.mibViewMenu = mibViewMenu;
            this.berDecoder = berDecoder;
        }

        public void Run()
        {
            var mibTree=mibParsingRunner.ParseMib();
            //mibViewMenu.StartViewMode(mibTree);

            byte[] input= new byte[7];
            //input[0] = 0x02;
            //input[1] = 0x02;
            //input[2] = 0xFF;
            //input[3] = 0x7f;
            input[0] = 0x06;
            input[1] = 0x05;
            input[2] = 0x2b;
            input[3] = 0x06;
            input[4] = 0x01;
            input[5] = 0x04;
            input[6] = 0x01;

            berDecoder.Decode(input);
        }
    }
}