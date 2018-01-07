using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MainProgramLoop
{
    internal class SnmpRunner : ISnmpRunner
    {
        private readonly IMibPicker mibPicker;
        private readonly IMibReader mibReader;

        public SnmpRunner(IMibPicker mibPicker, IMibReader mibReader)
        {
            this.mibPicker = mibPicker;
            this.mibReader = mibReader;
        }

        public void Run()
        {
            var mibName = mibPicker.GetMibName();
        }
    }
}