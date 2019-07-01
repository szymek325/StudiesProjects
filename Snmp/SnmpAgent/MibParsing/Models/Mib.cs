using System.Collections.Generic;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.MibParsing.Models
{
    public class Mib
    {
        public string Import { get; set; }
        public IEnumerable<ObjectType> ObjectTypes { get; set; } = new List<ObjectType>();
        public IEnumerable<ObjectIdentifier> ObjectIdentifiers { get; set; } = new List<ObjectIdentifier>();
        public IEnumerable<DataType> DataTypes { get; set; } = new List<DataType>();
        public IEnumerable<Sequence> Sequences { get; set; } = new List<Sequence>();
    }
}