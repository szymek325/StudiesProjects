using System.Text.RegularExpressions;

namespace SnmpAgent.Models
{
    public class DataType
    {
        public string Name { get; set; }
        public string Application { get; set; }
        public string Mode { get; set; }
        public string Type { get; set; }

        public static explicit operator DataType(Match v)
        {
            return new DataType
            {
                Name = v.Groups[1].Value,
                Application = v.Groups[2].Value,
                Mode = v.Groups[4].Value,
                Type = v.Groups[5].Value
            };
        }
    }
}