using System;
using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class EncoderRunner : IEncoderRunner
    {
        private readonly INodeFinder nodeFinder;

        public EncoderRunner(INodeFinder nodeFinder)
        {
            this.nodeFinder = nodeFinder;
        }

        public DependencyTreeNode snmpTree { get; set; }

        public void Run(DependencyTreeNode mibTree)
        {
            snmpTree = mibTree;

            while (true)
            {
                Console.Clear();
                ShowEncoderMenu();
                var inputData = ReadDataToEncode();
                
                if (inputData.Equals("exit"))
                {
                    return;
                }

                var formattedData = PrepareString(inputData);
                if (formattedData.Length == 2)
                {
                    Console.WriteLine($"OID: {formattedData[0]}");
                    Console.WriteLine($"Value: {formattedData[1]}");

                    nodeFinder.SetNeededElement(mibTree,formattedData[0]);
                    var node = nodeFinder.GetFoundNode();



                }
                else
                {
                    Console.WriteLine("Input in not correct format");
                }

                Console.ReadKey();
            }
        }

        private string[] PrepareString(string encodedData)
        {
            Char delimiter = ' ';
            var substrings = encodedData.Split(delimiter);
            return substrings;
        }

        private string ReadDataToEncode()
        {
            var text = Console.ReadLine();
            return text;
        }

        private void ShowEncoderMenu()
        {
            Console.WriteLine("-------------------------ENCODER MENU-------------------------");
            Console.WriteLine(" Type data to encode");
            Console.WriteLine(" Type exit to exit encoder");
            Console.WriteLine(" Used input format: '<OID> <value>' ");
        }



    }
}