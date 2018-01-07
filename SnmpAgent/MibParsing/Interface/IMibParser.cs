using SnmpAgent.Models;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IMibParser
    {
        Mib GetMibContent(string fileName);
    }
}