using System.IO;
using SnmpAgent.Constants;

namespace SnmpAgent.Helpers
{
    public class MibReader
    {
        public MibReader(string mibFileName)
        {
            MibFileName = mibFileName;
        }

        public string MibFileName { get; set; }
        public string Text { get; private set; }

        public void ReadFile()
        {
            var streamReader = new StreamReader(string.Format("{0}{1}.txt", OtherConstants.Path, MibFileName));
            Text = streamReader.ReadToEnd();
        }
    }
}