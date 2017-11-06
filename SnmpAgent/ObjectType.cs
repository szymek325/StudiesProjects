using System;
using System.Collections.Generic;
using System.Text;

namespace SnmpAgent
{
    public class ObjectType
    {
        public string Name { get; set; }
        public string Syntax { get; set; }
        public string Access { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
