using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;
using SnmpAgent.Constants;
using SnmpAgent.Helpers;
using SnmpAgent.Models;

namespace SnmpAgent.Providers
{
    public class ObjectTypesProvider
    {
        public ObjectTypesProvider(string text)
        {
            Text = text;
        }

        private string Text { get; set; }

        public List<ObjectType> GetAllObjects()
        {
            var import = GetObjectImports(Text);
            var objectIdentifiers = GetObjectIdentifiers(Text);
            var objectTypes = GetObjectTypes(Text);

            objectTypes.AddRange(objectIdentifiers);

            if (import != "")
            {
                return GetAllObjectsHelper(import, objectTypes);
            }

            return objectTypes;
        }

        private List<ObjectType> GetAllObjectsHelper(string fileName, List<ObjectType> collection)
        {
            var mibReader = new MibReader(fileName);
            mibReader.ReadFile();

            var import = GetObjectImports(mibReader.Text);
            var objectIdentifiers = GetObjectIdentifiers(mibReader.Text);
            var objectTypes = GetObjectTypes(mibReader.Text);

            collection.AddRange(objectIdentifiers);
            collection.AddRange(objectTypes);

            if (import != "")
            {
                return GetAllObjectsHelper(import, collection);
            }

            return collection;
        }

        private List<ObjectType> GetObjectIdentifiers(string mibText)
        {
            var objectsIdentifiersRunner = new RegexRunner(RegexConstants.ObjectIdentifiersPattern, mibText);
            var matchCollection = objectsIdentifiersRunner.GetAllMatches();

            var objectIdentifiers = matchCollection.Select(x => new ObjectType()
            {
                Name = x.Groups[1].Value,
                NameOfNodeAbove = x.Groups[2].Value,
                LeafNumber = int.Parse(x.Groups[3].Value)
            }).ToList();

            return objectIdentifiers;
        }

        private string GetObjectImports(string mibText)
        {
            var importsRunner = new RegexRunner(RegexConstants.ImportPattern, mibText);
            var matchCollection = importsRunner.GetAllMatches();
            if (matchCollection.Count != 0)
            {
                return matchCollection[0]?.Groups[1].Value;
            }
            return "";
        }

        private List<ObjectType> GetObjectTypes(string mibText)
        {
            var objectsTypesRunner = new RegexRunner(RegexConstants.ObjectTypesPattern, mibText);
            var matchCollection = objectsTypesRunner.GetAllMatches();
            var objectTypes = matchCollection.Select(x => (ObjectType)x).ToList();
            return objectTypes;
        }

    }
}