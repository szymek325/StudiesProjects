using System;
using System.Collections.Generic;
using System.Linq;
using SnmpAgent.Helpers;
using SnmpAgent.Models;
using SnmpAgent.Providers;

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