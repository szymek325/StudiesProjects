using System;
using SnmpAgent.Helpers;

namespace SnmpAgent
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            UserInterface.Run();

            Console.ReadKey();
        }
    }
}