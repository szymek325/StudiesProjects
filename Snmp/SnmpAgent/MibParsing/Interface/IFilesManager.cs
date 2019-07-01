namespace SnmpAgent.MibParsing.Interface
{
    public interface IFilesManager
    {
        void ListAllAvaiableFiles();
        bool CheckIfFileExists(string fileName);
    }
}