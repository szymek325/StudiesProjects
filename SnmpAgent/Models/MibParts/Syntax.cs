using System;
using System.Collections.Generic;
using System.Text;

namespace SnmpAgent.Models.MibParts
{
    public class Syntax
    {
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string Mode { get; set; } // Explicit/Implicit
    }
}
