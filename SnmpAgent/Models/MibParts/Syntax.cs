namespace SnmpAgent.Models.MibParts
{
    public class Syntax
    {
        public string Name { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Mode { get; set; } // Explicit/Implicit
    }
}