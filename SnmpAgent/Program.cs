using System;
using SnmpAgent.Providers;

namespace SnmpAgent
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var objects = new ObjectTypesProvider().GetAllObjectTypes("RFC1213-MIB");


            objects[0].ShowObjectType();
            objects[1].ShowObjectType();
            objects[3].ShowObjectType();

            Console.ReadKey();
        }
    }
}