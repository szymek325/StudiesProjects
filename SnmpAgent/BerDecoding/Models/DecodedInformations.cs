using System;
using System.Collections;
using System.Collections.Generic;

namespace SnmpAgent.BerDecoding.Models
{
    public class DecodedInformations
    {
        public IdentifierOctet IdentifierOctet { get; set; }
        public int Length { get; set; }
        public string Value { get; set; }
        public List<DecodedInformations> Sequences { get; set; }= new List<DecodedInformations>();

        public void ShowNode()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(nameof(IdentifierOctet));
            Console.WriteLine($"    {nameof(IdentifierOctet.Class)}: {IdentifierOctet.Class}");
            Console.WriteLine($"    {nameof(IdentifierOctet.PC)}: {IdentifierOctet.PC}");
            Console.WriteLine($"    {nameof(IdentifierOctet.Tag)}: {IdentifierOctet.Tag}");
            Console.WriteLine($"{nameof(Length)}: {Length}");
            Console.WriteLine($"{nameof(Value)}: {Value}");
            foreach (var node in Sequences)
            {
                node.ShowNode();
            }
        }
    }

    public class IdentifierOctet
    {
        public string Class { get; set; }
        public string PC { get; set; }
        public string Tag { get; set; }
    }
}