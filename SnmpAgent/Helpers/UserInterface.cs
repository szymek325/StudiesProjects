using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using SnmpAgent.Models;
using SnmpAgent.Providers;

namespace SnmpAgent.Helpers
{
    public static class UserInterface
    {
        public static Mib MibModel { get; set; }
        public static void Run()
        {
            do
            {
                string mibInput = GetMibName();
                UpdateModel();


                ShowDependencies();
            } while (true);

        }

        private static void UpdateModel()
        {
            var objectTypesProvider = new ObjectTypesProvider();

            MibModel = objectTypesProvider.GetMibContent(MibModel);
        }

        private static void ShowDependencies()
        {
            do
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("What object do you want to see?");
                Console.WriteLine("1.Type 'all' to see all avaiable names");
                Console.WriteLine("2.Type exit to return to file selection");
                var objectTypeName = Console.ReadLine();

                if (objectTypeName.Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("----------LIST OF ALL NODES----------");
                    foreach (var node in MibModel.ObjectIdentifiers)
                    {
                        Console.WriteLine(node.Name);
                    }

                    foreach (var node in MibModel.ObjectTypes)
                    {
                        Console.WriteLine(node.Name);
                    }

                }
                else
                {
                    var parentNode = MibModel.ObjectTypes.FirstOrDefault(x => x.Name.Equals(objectTypeName, StringComparison.OrdinalIgnoreCase));
                    var childrenNode = MibModel.ObjectTypes.Where(x => x.NameOfNodeAbove.Equals(objectTypeName, StringComparison.OrdinalIgnoreCase)).ToList();
                    if (parentNode != null)
                    {
                        Console.WriteLine("----------PARENT NODE----------");
                        parentNode.ShowObjectType();
                        Console.WriteLine("----------CHILDREN NODES----------");
                        foreach (var node in childrenNode)
                        {
                            node.ShowObjectType();
                        }
                    }
                    if (objectTypeName.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;
                }


            } while (true);
        }

        private static string GetMibName()
        {
            do
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Which MIB file do you want to read ?");
                Console.WriteLine("1.Type 'all' if you want to see all avaiable files");
                var fileName = Console.ReadLine();
                if (fileName.Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    MibReader.ListAllAvaiableFiles();
                }
                else if (MibReader.CheckIfFileExists(fileName))
                {
                    return fileName;
                }
            } while (true);

        }
    }
}
