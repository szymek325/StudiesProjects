using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SnmpAgent.Constants;
using SnmpAgent.MibParsing.Interface;
using SnmpAgent.Models;
using SnmpAgent.Models.MibParts;

namespace SnmpAgent.MibParsing.Implementation
{
    public class MibParser: IMibParser
    {
        private readonly IMibReader mibReader;
        private readonly ICustomRegexRunner regexRunner;

        public MibParser(IMibReader mibReader, ICustomRegexRunner regexRunner)
        {
            this.mibReader = mibReader;
            this.regexRunner = regexRunner;
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
            MibModel.Import = GetObjectImports();
            MibModel.ObjectIdentifiers = MibModel.ObjectIdentifiers.Concat(GetObjectIdentifiers());
            MibModel.ObjectIdentifiers = MibModel.ObjectIdentifiers.Concat(GetMainOid());
            MibModel.ObjectTypes = MibModel.ObjectTypes.Concat(GetObjectTypes());
            MibModel.Sequences = MibModel.Sequences.Concat(GetSequences());
            MibModel.DataTypes = MibModel.DataTypes.Concat(GetDataTypes());
        }

        private IEnumerable<DataType> GetDataTypes()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.DataTypePattern, Text);

            var dataTypes = matchCollection.Select(x => (DataType) x);

            return dataTypes;
        }

        private IEnumerable<Sequence> GetSequences()
        {
            var matchCollection = regexRunner.GetAllMatchesWithoutSingleLine(RegexConstants.SequencePattern, Text);

            var sequences = matchCollection.Select(x => (Sequence) x);

            return sequences;
        }

        private IEnumerable<ObjectIdentifier> GetObjectIdentifiers()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.ObjectIdentifiersPattern, Text);

            var objectIdentifiers = matchCollection.Select(x => (ObjectIdentifier) x);

            return objectIdentifiers;
        }

        private string GetObjectImports()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.ImportPattern, Text);
            if (matchCollection.Count != 0)
                return matchCollection[0]?.Groups[1].Value;
            return "";
        }

        private IEnumerable<ObjectType> GetObjectTypes()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.ObjectTypesPattern, Text);
            var objectTypes = matchCollection.Select(x => (ObjectType) x);
            return objectTypes;
        }

        private IEnumerable<ObjectIdentifier> GetMainOid()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.MainOidPattern, Text);

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