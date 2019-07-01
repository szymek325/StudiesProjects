using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IMibModelProvider
    {
        Mib GetMibContent(string fileName);
    }
}