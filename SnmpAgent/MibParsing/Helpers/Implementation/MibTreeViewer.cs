using System;
using SnmpAgent.MibParsing.Helpers;
using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.Helpers.View
{
    public class MibTreeViewer: IMibTreeViewer
    {
        public static DependencyTreeNode TreeNode { get; set; }

        public void PassTreeToViewer(DependencyTreeNode tree)
        {
            TreeNode = tree;
        }
        public void StartViewMode()
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
    }
}