using System;
using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MibParsing.Implementation
{
    public class MibPicker : IMibPicker
    {
        private readonly IMibReader mibReader;
        private readonly IFilesManager filesManager;

        public MibPicker(IMibReader mibReader, IFilesManager filesManager)
        {
            this.mibReader = mibReader;
            this.filesManager = filesManager;
        }

        public string GetMibName()
        {
            Console.Clear();
            do
            {
                Console.WriteLine("-------------------------MIB PICKER-------------------------");
                Console.WriteLine("Which MIB file do you want to read ?");
                Console.WriteLine("1.Type 'all' if you want to see all avaiable files");
                Console.WriteLine("2.Type name to load MIB");
                var fileName = "rfc1213-mib";
                //var fileName = Console.ReadLine();
                if (fileName.Equals("all", StringComparison.OrdinalIgnoreCase))
                    filesManager.ListAllAvaiableFiles();
                else if (filesManager.CheckIfFileExists(fileName))
                    return fileName;
            } while (true);
        }
    }
}