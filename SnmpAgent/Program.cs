using System;
using System.Collections.Generic;
using SnmpAgent.Models;
using SnmpAgent.Providers;

namespace SnmpAgent
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var objectTypesProvider= new ObjectTypesProvider();

            var objectList = objectTypesProvider.GetAllObjects("RFC1213-MIB");

            Console.WriteLine(string.Format("Ilość obiektów: {0}", objectList.Count));

            Console.ReadKey();
        }
    }
}