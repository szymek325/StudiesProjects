using System.IO;
using System.Text;
using SnmpAgent.MibParsing.Constants;
using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MibParsing.Implementation
{
    public class MibReader : IMibReader
    {
        public string GetTextFromFile(string fileName)
        {
            var fileWithPath = string.Format("{0}{1}.txt", OtherConstants.Path, fileName);
            var commentLessFile= Remove(fileWithPath);
            //var streamReader = new StreamReader(fileWithPath);
            //var mibText = streamReader.ReadToEnd();
            return commentLessFile;
        }

        private string Remove(string filePath)
        {
            string filewithout = "";
            string temporaryfile = string.Format("{0}{1}.txt", OtherConstants.Path, "temp");

            if (File.Exists(filePath))
            {
                File.Delete(temporaryfile);
                TextWriter tsw = new StreamWriter(temporaryfile, true);
                using (StreamReader r = new StreamReader(filePath))
                {
                    string line;


                    bool komentarz = false;
                    while ((line = r.ReadLine()) != null)
                    {
                        StringBuilder sb = new StringBuilder(line);
                        for (int i = 0; i < line.Length - 1; i++)
                        {
                            if (sb[i] == '-' && sb[i + 1] == '-') komentarz = true;
                            if (komentarz) { sb[i] = ' '; sb[line.Length - 1] = ' '; }
                        }
                        komentarz = false;

                        tsw.WriteLine(sb);
                        sb.Clear();
                    }
                    // 
                    tsw.Close();
                    filewithout = File.ReadAllText(temporaryfile);
                    File.Delete(temporaryfile);
                }

            }
            return (filewithout);
        } 
    }
}