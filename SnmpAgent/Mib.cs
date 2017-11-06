using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SnmpAgent
{
    public class Mib
    {
        public string Path { get; set; } 
        public string Text { get; set; }

        public void ReadFile()
        {
            var streamReader = new StreamReader(Constants.Path);
            this.Text = streamReader.ReadToEnd();
        }
    }
}
