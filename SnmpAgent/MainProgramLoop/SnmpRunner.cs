using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MainProgramLoop
{
    internal class SnmpRunner : ISnmpRunner
    {
        private readonly IMibPicker mibPicker;
        private readonly IMibReader mibReader;
        private readonly IMibModelProvider mibModelProvider;

        public SnmpRunner(IMibPicker mibPicker, IMibReader mibReader, IMibModelProvider mibModelProvider)
        {
            this.mibPicker = mibPicker;
            this.mibReader = mibReader;
            this.mibModelProvider = mibModelProvider;
        }

        public void Run()
        {
            var mibName = mibPicker.GetMibName();
            var mib = mibModelProvider.GetMibContent(mibName); //get different object types
            // creates tree
            //returns tree

        }
    }
}