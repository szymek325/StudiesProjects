using System.Text.RegularExpressions;

namespace SnmpAgent.Models
{
    public class Sequence
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static explicit operator Sequence(Match v)
        {
            return new Sequence
            {
                Name = v.Groups[1].Value,
                Value = v.Groups[2].Value
            };
        }
    }
}