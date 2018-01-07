using System;
using SnmpAgent.MibParsing.Implementation;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.Helpers.View
{
    public class DataViewer
    {
        private readonly DependencyTreeCreator treeCreator;

        public DataViewer(DependencyTreeCreator treeCreator)
        {
            this.treeCreator = treeCreator;
        }

        public static DependencyTreeNode TreeNode { get; set; }

        public void Run()
        {
            do
            {
                var mibToImport = GetMibName();
                UpdateModel(mibToImport);
                ShowDependencies();
            } while (true);
        }

        private void UpdateModel(string mibName)
        {
            treeCreator.CreateDependencyTree(mibName);
            TreeNode = treeCreator.GetDependencyTree();
        }

        private void ShowDependencies()
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

        private void CheckInputValueAndActAccordingly(string objectTypeName)
        {
            if (objectTypeName.Equals("all", StringComparison.OrdinalIgnoreCase))
                TreeNode.ShowDependencyTree();
            else
                TreeNode.FindAndShowElement(objectTypeName);
        }


        private void ShowObjectsMenu()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("What object do you want to see?");
            Console.WriteLine("1.Type 'all' to see all avaiable names");
            Console.WriteLine("2.Type 'exit' to return to file selection");
        }

        private string GetMibName()
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