using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Helpers.Interface
{
    public interface IMibTreeViewer
    {
        void StartViewMode();
        void PassTreeToViewer(DependencyTreeNode tree);
    }
}