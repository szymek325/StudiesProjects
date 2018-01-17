using System;
using System.Text;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerEncoding.Interfaces;
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
        private readonly IDecoderRunner decoderRunner;
        private readonly IEncoderRunner encoderRunner;

        public SnmpRunner(IMibParsingRunner mibParsingRunner, IMibViewMenu mibViewMenu, IDecoderRunner decoderRunner, IEncoderRunner encoderRunner)
        {
            this.mibParsingRunner = mibParsingRunner;
            this.mibViewMenu = mibViewMenu;
            this.decoderRunner = decoderRunner;
            this.encoderRunner = encoderRunner;
        }

        public void Run()
        {
            var mibTree=mibParsingRunner.ParseMib();
            var dataTypes = mibParsingRunner.GetDataTypes();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("-------------------------MENU-------------------------");
                Console.WriteLine("1.Browse MIB Tree");
                Console.WriteLine("2.Decode bytes");
                Console.WriteLine("3.Encode element of mib tree");
                var pick=Console.ReadLine();
                if (pick.Contains("1"))
                {
                    mibViewMenu.StartViewMode(mibTree);
                }
                else if (pick.Contains("2"))
                {
                    decoderRunner.Run();
                }
                else if(pick.Contains("3"))
                {
                    encoderRunner.Run(mibTree);
                }
                
                
            }

        }
    }
}