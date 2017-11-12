using System;
using System.IO;
using SnmpAgent.Constants;

namespace SnmpAgent.Helpers
{
    public static class MibReader
    {
        public static string MibFileName { get; set; }
        public static string Text { get; private set; }

        public static void ListAllAvaiableFiles()
        {
            string[] fileEntries = Directory.GetFiles(OtherConstants.Path);
            Console.WriteLine("-------------List of all Files----------------");
            foreach (var fileName in fileEntries)
            {
                Console.WriteLine(fileName);
            }
        }

        public static bool CheckIfFileExists(string fileName)
        {
            var doesItExist = File.Exists(string.Format("{0}{1}.txt", OtherConstants.Path, fileName));
            if (!doesItExist)
            {
                Console.WriteLine("File wasn't recognized. Please try again.");
            }
            return doesItExist;
        }

        public static string GetTextFromFile(string fileName)
        {
            var streamReader = new StreamReader(string.Format("{0}{1}.txt", OtherConstants.Path, MibFileName));\
            MibFileName = fileName;
            Text = streamReader.ReadToEnd();
            return Text;
        }
    }
}