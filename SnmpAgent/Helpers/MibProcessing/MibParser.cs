using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SnmpAgent.Constants;
using SnmpAgent.Models;
using SnmpAgent.Models.MibParts;

namespace SnmpAgent.Helpers.MibProcessing
{
    public class MibParser
    {
        private string Text { get; set; }
        public Mib MibModel { get; set; } = new Mib();

        public Mib GetMibContent(string fileName)
        {
            Text = MibReader.GetTextFromFile(fileName);

            AddParsedDataToModel();

            if (!MibModel.Import.Equals(""))
                return GetMibContent(MibModel.Import);

            return MibModel;
        }

        private void AddParsedDataToModel()
        {
            MibModel.Import = GetObjectImports();
            MibModel.ObjectIdentifiers = MibModel.ObjectIdentifiers.Concat(GetObjectIdentifiers());
            MibModel.ObjectIdentifiers = MibModel.ObjectIdentifiers.Concat(GetMainOid());
            MibModel.ObjectTypes = MibModel.ObjectTypes.Concat(GetObjectTypes());
            MibModel.Sequences = MibModel.Sequences.Concat(GetSequences());
            MibModel.DataTypes = MibModel.DataTypes.Concat(GetDataTypes());
        }

        private IEnumerable<DataType> GetDataTypes()
        {
            var sequencesRunner = new CustomRegexRunner(RegexConstants.DataTypePattern, Text);
            var matchCollection = sequencesRunner.GetAllMatches();

            var dataTypes = matchCollection.Select(x => (DataType)x);

            return dataTypes;
        }

        private IEnumerable<Sequence> GetSequences()
        {
            var sequencesRunner = new CustomRegexRunner(RegexConstants.SequencePattern, Text);
            var matchCollection = sequencesRunner.GetAllMatchesWithoutSingleLine();

            var sequences = matchCollection.Select(x => (Sequence)x);

            return sequences;
        }

        private IEnumerable<ObjectIdentifier> GetObjectIdentifiers()
        {
            var objectsIdentifiersRunner = new CustomRegexRunner(RegexConstants.ObjectIdentifiersPattern, Text);
            var matchCollection = objectsIdentifiersRunner.GetAllMatches();

            var objectIdentifiers = matchCollection.Select(x => (ObjectIdentifier)x);

            return objectIdentifiers;
        }

        private string GetObjectImports()
        {
            var importsRunner = new CustomRegexRunner(RegexConstants.ImportPattern, Text);
            var matchCollection = importsRunner.GetAllMatches();
            if (matchCollection.Count != 0)
                return matchCollection[0]?.Groups[1].Value;
            return "";
        }

        private IEnumerable<ObjectType> GetObjectTypes()
        {
            var objectsTypesRunner = new CustomRegexRunner(RegexConstants.ObjectTypesPattern, Text);
            var matchCollection = objectsTypesRunner.GetAllMatches();
            var objectTypes = matchCollection.Select(x => (ObjectType)x);
            return objectTypes;
        }

        private IEnumerable<ObjectIdentifier> GetMainOid()
        {
            var objectsIdentifiersRunner = new CustomRegexRunner(RegexConstants.MainOidPattern, Text);
            var matchCollection = objectsIdentifiersRunner.GetAllMatches();

            if (matchCollection.Count != default(int))
                return CreateMainOids(matchCollection[0]);

            return new List<ObjectIdentifier>();
        }

        private static List<ObjectIdentifier> CreateMainOids(Match match)
        {
            return new List<ObjectIdentifier>
            {
                new ObjectIdentifier
                {
                    Name = match.Groups[2].Value,
                    NameOfNodeAbove = "",
                    LeafNumber = 1,
                    Oid = "1"
                },
                new ObjectIdentifier
                {
                    Name = match.Groups[3].Value,
                    NameOfNodeAbove = match.Groups[2].Value,
                    LeafNumber = int.Parse(match.Groups[4].Value)
                },
                new ObjectIdentifier
                {
                    Name = match.Groups[5].Value,
                    NameOfNodeAbove = match.Groups[3].Value,
                    LeafNumber = int.Parse(match.Groups[6].Value)
                },
                new ObjectIdentifier
                {
                    Name = match.Groups[1].Value,
                    NameOfNodeAbove = match.Groups[5].Value,
                    LeafNumber = int.Parse(match.Groups[7].Value)
                }
            };
        }
    }
}