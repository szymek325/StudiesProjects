using System;
using System.Collections;
using System.Collections.Generic;

namespace SnmpAgent.BerDecoding.Models
{
    public class DecodedInformations
    {
        public IdentifierOctet IdentifierOctet { get; set; }
        //public string Class { get; set; }
        //public string PC { get; set; }
        //public string Tag { get; set; }
        public int Length { get; set; }
        public string Value { get; set; }
        public List<DecodedInformations> Sequences { get; set; } = new List<DecodedInformations>();

        public void ShowNode(int len = 0)
        {
            for (var i = 0; i < len; i++)
                Console.Write("         ");
            Console.WriteLine("-----------------------------------");
            //Console.WriteLine($"    {nameof(Class)}: {Class}");
            //Console.WriteLine($"    {nameof(PC)}: {PC}");
            //Console.WriteLine($"    {nameof(Tag)}: {Tag}");
            for (var i = 0; i < len; i++)
                Console.Write("         ");
            Console.WriteLine(nameof(IdentifierOctet));
            for (var i = 0; i < len; i++)
                Console.Write("         ");
            Console.WriteLine($"    {nameof(IdentifierOctet.Class)}: {IdentifierOctet.Class}");
            for (var i = 0; i < len; i++)
                Console.Write("         ");
            Console.WriteLine($"    {nameof(IdentifierOctet.PC)}: {IdentifierOctet.PC}");
            for (var i = 0; i < len; i++)
                Console.Write("         ");
            Console.WriteLine($"    {nameof(IdentifierOctet.Tag)}: {IdentifierOctet.Tag}");
            for (var i = 0; i < len; i++)
                Console.Write("         ");
            Console.WriteLine($"{nameof(Length)}: {Length}");
            for (var i = 0; i < len; i++)
                Console.Write("         ");
            Console.WriteLine($"{nameof(Value)}: {Value}");

            foreach (var node in Sequences)
            {
                node.ShowNode(len + 1);
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