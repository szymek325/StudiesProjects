using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SnmpAgent
{
    public class MibReader
    {
        public MibReader(string path)
        {
            Path = path;
        }

        public string Path { get; set; }
        public string Text { get; private set; }

        //public MibReader(string text)
        //{
        //    var streamReader = new StreamReader(Constants.Path);
        //    Text = streamReader.ReadToEnd();
        //}

        public void ReadFile()
        {
            var streamReader = new StreamReader(Constants.Path);
            Text = streamReader.ReadToEnd();
        }
    }
}
