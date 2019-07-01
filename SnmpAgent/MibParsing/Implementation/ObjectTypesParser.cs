using System.Collections.Generic;
using System.Linq;
using SnmpAgent.MibParsing.Constants;
using SnmpAgent.MibParsing.Interface;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.MibParsing.Implementation
{
    public class ObjectTypesParser : IObjectTypesParser
    {
        private readonly IMainObjectIdentifiersCreator mainObjectIdentifiersCreator;
        private readonly ICustomRegexRunner regexRunner;

        public ObjectTypesParser(ICustomRegexRunner regexRunner,
            IMainObjectIdentifiersCreator mainObjectIdentifiersCreator)
        {
            this.regexRunner = regexRunner;
            this.mainObjectIdentifiersCreator = mainObjectIdentifiersCreator;
        }

        private string Text { get; set; }

        public void SetText(string textToBeParsed)
        {
            Text = textToBeParsed;
        }

        public string GetObjectImports()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.ImportPattern, Text);
            if (matchCollection.Count != 0)
                return matchCollection[0]?.Groups[1].Value;
            return "";
        }

        public IEnumerable<DataType> GetDataTypes()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.DataTypePattern, Text);

            var dataTypes = matchCollection.Select(x => (DataType) x);
            var types = dataTypes.ToList();

            foreach (var type in types)
            {
                if (type.Type.Contains("SIZE"))
                {
                    var size = regexRunner.GetAllMatches(RegexConstants.SingleSize, type.Type);
                    if (size.Count != default(int))
                    {
                        type.Min = size.First().Groups[1].Value;
                        type.Max = size.First().Groups[1].Value;
                    }
                }
                var split= type.Type.Split('(');;
                type.Type = split[0];
            }

            return types;
        }

        public IEnumerable<Sequence> GetSequences()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.SequencePattern, Text);

            var sequences = matchCollection.Select(x => (Sequence) x);

            return sequences;
        }

        public IEnumerable<ObjectIdentifier> GetObjectIdentifiers()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.ObjectIdentifiersPattern, Text);

            var objectIdentifiers = matchCollection.Select(x => (ObjectIdentifier) x);

            return objectIdentifiers;
        }

        public IEnumerable<ObjectType> GetObjectTypes()
        {
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.ObjectTypesPattern, Text);
            var objectTypes = matchCollection.Select(x => (ObjectType) x);
            return objectTypes;
        }

        public IEnumerable<ObjectIdentifier> GetMainObjectIdentifiersWhichAreNotDefinedInMib()
        {
            //ISO etc
            var matchCollection = regexRunner.GetAllMatches(RegexConstants.MainOidPattern, Text);

            if (matchCollection.Count != default(int))
                return mainObjectIdentifiersCreator.CreateMainObjectIdentifiersNotDefinedInMib(matchCollection[0]);

            return new List<ObjectIdentifier>();
        }
    }
}