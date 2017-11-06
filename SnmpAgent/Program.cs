using System;
using System.Text.RegularExpressions;
using SnmpAgent.Providers;

namespace SnmpAgent
{
    class Program
    {
        static void Main(string[] args)
        {

            var objects = new ObjectTypesProvider().GetAllObjectTypes();


            objects[0].ShowObjectType();
            

            Console.ReadKey();
        }
    }
}
