using System;

using SnmpAgent.MibParsing.Implementation;
using SnmpAgent.Models;
using SnmpAgent.Providers;

namespace SnmpAgent.Helpers.View
{
    public static class DataViewer
    {
        public static DependencyTreeNode TreeNode { get; set; }

        public static void Run()
        {
            do
            {
                var mibToImport = GetMibName();
                UpdateModel(mibToImport);
                ShowDependencies();
            } while (true);
        }

        private static void UpdateModel(string mibName)
        {
            var treeProvider = new DependencyTreeProvider();

            TreeNode = treeProvider.GetDependencyTree(mibName);
        }

        private static void ShowDependencies()
        {
            Console.Clear();
            do
            {
                ShowObjectsMenu();
                var objectTypeName = Console.ReadLine();
                CheckInputValueAndActAccordingly(objectTypeName);
                if (objectTypeName.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;
            } while (true);
        }

        private static void CheckInputValueAndActAccordingly(string objectTypeName)
        {
            if (objectTypeName.Equals("all", StringComparison.OrdinalIgnoreCase))
                TreeNode.ShowDependencyTree();
            else
                TreeNode.FindAndShowElement(objectTypeName);
        }


        private static void ShowObjectsMenu()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("What object do you want to see?");
            Console.WriteLine("1.Type 'all' to see all avaiable names");
            Console.WriteLine("2.Type 'exit' to return to file selection");
        }

        private static string GetMibName()
        {
            Console.Clear();
            do
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Which MIB file do you want to read ?");
                Console.WriteLine("1.Type 'all' if you want to see all avaiable files");
                var fileName = "rfc1213-mib";
                //var fileName = Console.ReadLine();
                //if (fileName.Equals("all", StringComparison.OrdinalIgnoreCase))
                //    MibReader.ListAllAvaiableFiles();
                //else if (MibReader.CheckIfFileExists(fileName))
                    return fileName;
            } while (true);
        }
    }
}