using System;
using System.Text.RegularExpressions;
using SnmpAgent.Providers;

namespace SnmpAgent
{
    class Program
    {
        static void Main(string[] args)
        {

            var objectTypesProvider = new ObjectTypesProvider();
            objectTypesProvider.GetAllObjectTypes();

            Console.WriteLine(objectTypesProvider.ObjectTypesList);
            

            

            //Console.WriteLine(Constants.MyPattern);

            Console.ReadKey();
        }
    }
}
