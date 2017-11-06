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
        public string Address { get; set; }

        public void ShowObjectType()
        {
            Console.WriteLine();
            Console.WriteLine(nameof(Name)+": "+ Name);
            Console.WriteLine(nameof(Syntax) + ": " + Syntax);
            Console.WriteLine(nameof(Access) + ": " + Access);
            Console.WriteLine(nameof(Status) + ": " + Status);
            Console.WriteLine(nameof(Description)+ ":");
            Console.WriteLine(Description);
            Console.WriteLine(nameof(Address) + ": " + Address);
            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
        }
    }

}
