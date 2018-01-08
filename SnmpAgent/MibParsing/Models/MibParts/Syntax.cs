namespace SnmpAgent.MibParsing.Models.MibParts
{
    public class Syntax
    {
        public string Name { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Mode { get; set; } // Explicit/Implicit
        public string Application { get; set; } // 1/2/3/4/5 etc
        public string SpecialTypeName { get; set; }//Opaque etc
    }
}