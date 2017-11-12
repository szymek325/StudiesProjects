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

        public static void Run()
        {
            do
            {
                string text = GetTextFromMib();
                ShowWhatUserWants(text);
            } while (true);

        }

        private static void ShowWhatUserWants(string text)
        {
            var objectTypesProvider = new ObjectTypesProvider(text);
            var mibContainer = objectTypesProvider.GetAllObjects();

            ShowDependencies(mibContainer);
        }

        private static void ShowDependencies(Mib mib)
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
                    foreach (var node in mib.ObjectIdentifiers)
                    {
                        Console.WriteLine(node.Name);
                    }

                    foreach (var node in mib.ObjectTypes)
                    {
                        Console.WriteLine(node.Name);
                    }

                }
                else
                {
                    var parentNode = mib.ObjectTypes.FirstOrDefault(x => x.Name.Equals(objectTypeName, StringComparison.OrdinalIgnoreCase));
                    var childrenNode = mib.ObjectTypes.Where(x => x.NameOfNodeAbove.Equals(objectTypeName, StringComparison.OrdinalIgnoreCase)).ToList();
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

        private static string GetTextFromMib()
        {
            do
            {
                string fileName = GetFileNameFromUser();

                var mibReader = new MibReader(fileName);
                mibReader.ReadFile();

                if (!string.IsNullOrEmpty(mibReader.Text))
                {
                    return mibReader.Text;
                }
            } while (true);

        }

        private static string GetFileNameFromUser()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Which MIB file do you want to read ?");
            var fileName = Console.ReadLine();
            return fileName;
        }
    }
}
