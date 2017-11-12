using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var objectList = objectTypesProvider.GetAllObjects();

            ShowDependencies(objectList);
        }

        private static void ShowDependencies(List<Models.ObjectType> objectList)
        {
            do
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("What object do you want to see?");
                var objectTypeName = Console.ReadLine();

                var parentNode = objectList.FirstOrDefault(x => x.Name.Contains(objectTypeName));
                var childrenNode = objectList.Where(x => x.NameOfNodeAbove.Contains(objectTypeName)).ToList();
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
                if (objectTypeName == "exit") return;
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
