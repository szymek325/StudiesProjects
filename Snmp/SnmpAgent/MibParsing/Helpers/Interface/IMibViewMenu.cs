using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Helpers.Interface
{
    public interface IMibViewMenu
    {
        void StartViewMode(DependencyTreeNode node);
    }
}