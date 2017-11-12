using System;
using System.Collections.Generic;
using System.Text;

namespace SnmpAgent.Models
{
    public class Mib
    {
        public string Import { get; set; }
        public IEnumerable<ObjectType> ObjectTypes { get; set; }= new List<ObjectType>();
        public IEnumerable<ObjectIdentifier> ObjectIdentifiers { get; set; }= new List<ObjectIdentifier>();
        public IEnumerable<DataType> DataTypes { get; set; }= new List<DataType>();
    }
}
