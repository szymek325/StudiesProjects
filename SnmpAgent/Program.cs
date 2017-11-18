using System;
using SnmpAgent.Helpers;
using SnmpAgent.Helpers.View;

namespace SnmpAgent
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DataViewer.Run();

            Console.ReadKey();
        }
    }
}