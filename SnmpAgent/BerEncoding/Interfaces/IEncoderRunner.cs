using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IEncoderRunner
    {
        void Run(DependencyTreeNode mibTree);
    }
}