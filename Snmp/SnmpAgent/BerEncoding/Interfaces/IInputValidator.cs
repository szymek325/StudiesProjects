using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.BerEncoding.Interfaces
{
    public interface IInputValidator
    {
        bool CheckIfValueCompliesWithObjectSyntax(Syntax nodeSyntax, string value);
    }
}