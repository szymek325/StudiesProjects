using System.Linq;
using SnmpAgent.MibParsing.Interface;
using SnmpAgent.MibParsing.Models;

namespace SnmpAgent.MibParsing.Implementation
{
    public class MibModelProvider : IMibModelProvider
    {
        private readonly IMibReader mibReader;
        private readonly IObjectTypesParser objectTypesParser;

        public MibModelProvider(IMibReader mibReader, IObjectTypesParser objectTypesParser)
        {
            this.mibReader = mibReader;
            this.objectTypesParser = objectTypesParser;
        }

        private string Text { get; set; }
        public Mib MibModel { get; set; } = new Mib();

        public Mib GetMibContent(string fileName)
        {
            Text = mibReader.GetTextFromFile(fileName);

            AddParsedDataToModel();

            if (!MibModel.Import.Equals(""))
                return GetMibContent(MibModel.Import);

            return MibModel;
        }

        private void AddParsedDataToModel()
        {
            objectTypesParser.SetText(Text);
            MibModel.Import = objectTypesParser.GetObjectImports();
            MibModel.ObjectIdentifiers = MibModel.ObjectIdentifiers.Concat(objectTypesParser.GetObjectIdentifiers());
            MibModel.ObjectIdentifiers = MibModel.ObjectIdentifiers.Concat(objectTypesParser.GetMainObjectIdentifiersWhichAreNotDefinedInMib());
            MibModel.ObjectTypes = MibModel.ObjectTypes.Concat(objectTypesParser.GetObjectTypes());
            MibModel.Sequences = MibModel.Sequences.Concat(objectTypesParser.GetSequences());
            MibModel.DataTypes = MibModel.DataTypes.Concat(objectTypesParser.GetDataTypes());
        }
    }
}