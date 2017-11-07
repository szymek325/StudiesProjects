using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SnmpAgent
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
            var streamReader = new StreamReader(string.Format("{0}{1}.txt",Constants.Path,MibFileName));
            Text = streamReader.ReadToEnd();
        }
    }
}
