using System;
using System.Collections.Generic;
using System.Text;

namespace SnmpAgent.Models
{
    public class Mib
    {
        public string Import { get; set; }
        public IEnumerable<ObjectType> ObjectTypes { get; set; }
        public IEnumerable<ObjectIdentifier> ObjectIdentifiers { get; set; }
        public IEnumerable<DataType> DataTypes { get; set; }
    }
}
