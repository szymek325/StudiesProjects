using System;
using System.Text;
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
        private readonly IDecoderRunner decoderRunner;

        public SnmpRunner(IMibParsingRunner mibParsingRunner, IMibViewMenu mibViewMenu, IDecoderRunner decoderRunner)
        {
            this.mibParsingRunner = mibParsingRunner;
            this.mibViewMenu = mibViewMenu;
            this.decoderRunner = decoderRunner;
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
                var pick=Console.ReadLine();
                if (pick.Contains("1"))
                {
                    mibViewMenu.StartViewMode(mibTree);
                }
                else if (pick.Contains("2"))
                {
                    decoderRunner.Run();
                }
                
                
            }

        }
    }
}