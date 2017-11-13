using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SnmpAgent.Constants;
using SnmpAgent.Helpers;
using SnmpAgent.Models;

namespace SnmpAgent.Providers
{
    public class ObjectTypesProvider
    {
        private string Text { get; set; }

        public Mib GetMibContent(Mib mibModel)
        {
            Text = MibReader.GetTextFromFile(mibModel.Import);

            mibModel.Import = GetObjectImports();
            mibModel.ObjectIdentifiers = mibModel.ObjectIdentifiers.Concat(GetObjectIdentifiers());
            mibModel.ObjectTypes = mibModel.ObjectTypes.Concat(GetObjectTypes());
            mibModel.ObjectIdentifiers = mibModel.ObjectIdentifiers.Concat(GetMainOid());
            mibModel.Sequences = mibModel.Sequences.Concat(GetSequences());
            mibModel.DataTypes = mibModel.DataTypes.Concat(GetDataTypes());

            if (!mibModel.Import.Equals(""))
                return GetMibContent(mibModel);

            return mibModel;
        }

        private IEnumerable<DataType> GetDataTypes()
        {
            var sequencesRunner = new RegexRunner(RegexConstants.DataTypePattern, Text);
            var matchCollection = sequencesRunner.GetAllMatches();

            var dataTypes = matchCollection.Select(x => (DataType) x);

            return dataTypes;
        }

        private IEnumerable<Sequence> GetSequences()
        {
            var sequencesRunner = new RegexRunner(RegexConstants.SequencePattern, Text);
            var matchCollection = sequencesRunner.GetAllMatchesWithoutSingleLine();

            var sequences = matchCollection.Select(x => (Sequence) x);

            return sequences;
        }

        private IEnumerable<ObjectIdentifier> GetObjectIdentifiers()
        {
            var objectsIdentifiersRunner = new RegexRunner(RegexConstants.ObjectIdentifiersPattern, Text);
            var matchCollection = objectsIdentifiersRunner.GetAllMatches();

            var objectIdentifiers = matchCollection.Select(x => (ObjectIdentifier) x);

            return objectIdentifiers;
        }

        private string GetObjectImports()
        {
            var importsRunner = new RegexRunner(RegexConstants.ImportPattern, Text);
            var matchCollection = importsRunner.GetAllMatches();
            if (matchCollection.Count != 0)
                return matchCollection[0]?.Groups[1].Value;
            return "";
        }

        private IEnumerable<ObjectType> GetObjectTypes()
        {
            var objectsTypesRunner = new RegexRunner(RegexConstants.ObjectTypesPattern, Text);
            var matchCollection = objectsTypesRunner.GetAllMatches();
            var objectTypes = matchCollection.Select(x => (ObjectType) x);
            return objectTypes;
        }

        private IEnumerable<ObjectIdentifier> GetMainOid()
        {
            var objectsIdentifiersRunner = new RegexRunner(RegexConstants.MainOidPattern, Text);
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
                    LeafNumber = 1
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