using System.Collections.Generic;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.MibParsing.Interface
{
    public interface IObjectTypesParser
    {
        void SetText(string textToBeParsed);
        IEnumerable<DataType> GetDataTypes();
        IEnumerable<Sequence> GetSequences();
        IEnumerable<ObjectIdentifier> GetObjectIdentifiers();
        string GetObjectImports();
        IEnumerable<ObjectType> GetObjectTypes();
        IEnumerable<ObjectIdentifier> GetMainOid();
    }
}