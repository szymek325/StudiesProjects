using System;
using System.Collections.Generic;
using System.Linq;
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
            var toSHow=objectList.Where(x=>x.NameOfNodeAbove== "ipRouteEntry").ToList();

            Console.WriteLine(string.Format("Ilość obiektów: {0}", objectList.Count));

            foreach (var obj in toSHow)
            {
                obj.ShowObjectType();
            }

            Console.ReadKey();
        }
    }
}