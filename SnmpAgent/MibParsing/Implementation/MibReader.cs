using System.IO;
using SnmpAgent.MibParsing.Constants;
using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MibParsing.Implementation
{
    public class MibReader : IMibReader
    {
        public string GetTextFromFile(string fileName)
        {
            var streamReader = new StreamReader(string.Format("{0}{1}.txt", OtherConstants.Path, fileName));
            var mibText = streamReader.ReadToEnd();
            return mibText;
        }
    }
}