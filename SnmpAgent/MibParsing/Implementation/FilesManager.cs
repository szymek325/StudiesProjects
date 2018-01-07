using System;
using System.IO;
using SnmpAgent.Constants;
using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MibParsing.Implementation
{
    public class FilesManager: IFilesManager
    {
        public void ListAllAvaiableFiles()
        {
            var fileEntries = Directory.GetFiles(OtherConstants.Path);
            Console.WriteLine("-------------List of all Files----------------");
            foreach (var fileName in fileEntries)
                Console.WriteLine(fileName);
        }

        public bool CheckIfFileExists(string fileName)
        {
            var doesItExist = File.Exists(string.Format("{0}{1}.txt", OtherConstants.Path, fileName));
            if (!doesItExist)
                Console.WriteLine("File wasn't recognized. Please try again.");
            return doesItExist;
        }
    }
}