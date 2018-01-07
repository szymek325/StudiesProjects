using System;
using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Helpers.Implementation
{
    public class MibViewMenu : IMibViewMenu
    {
        private readonly IDependencyTreeViewer dependencyTreeViewer;
        private readonly INodeFinder nodeFinder;

        public MibViewMenu(IDependencyTreeViewer dependencyTreeViewer, INodeFinder nodeFinder)
        {
            this.dependencyTreeViewer = dependencyTreeViewer;
            this.nodeFinder = nodeFinder;
        }

        public static DependencyTreeNode TreeNode { get; set; }

        public void StartViewMode(DependencyTreeNode node)
        {
            TreeNode = node;
            Console.Clear();
            do
            {
                ShowObjectsMenu();
                var objectTypeName = Console.ReadLine();
                CheckInputValueAndActAccordingly(objectTypeName);
                if (objectTypeName.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;
            } while (true);
        }

        private void ShowObjectsMenu()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("What object do you want to see?");
            Console.WriteLine("1.Type 'all' to see all avaiable names");
            Console.WriteLine("2.Type 'exit' to return to file selection");
        }

        private void CheckInputValueAndActAccordingly(string objectTypeName)
        {
            if (objectTypeName.Equals("all", StringComparison.OrdinalIgnoreCase))
                dependencyTreeViewer.ShowDependencyTree(TreeNode);
            else if (objectTypeName.Contains("1"))
                nodeFinder.FindAndShowElement(TreeNode,objectTypeName);
            else
                nodeFinder.FindAndShowElement(TreeNode, objectTypeName);
        }
    }
}