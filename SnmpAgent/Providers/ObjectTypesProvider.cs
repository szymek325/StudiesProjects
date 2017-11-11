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
        public List<ObjectType> GetAllObjects(string fileName, List<ObjectType> collection = null)
        {
            if (collection == null)
            {
                collection= new List<ObjectType>();
            }

            var mib = new MibReader(fileName);
            mib.ReadFile();

            var import = GetObjectImports(mib.Text);
            var objectIdentifiers = GetObjectIdentifiers(mib.Text);
            var objectTypes = GetObjectTypes(mib.Text);

            collection.AddRange(objectIdentifiers);
            collection.AddRange(objectTypes);

            if (import != "")
            {
                return GetAllObjects(import, collection);
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
            if (matchCollection.Count!=0)
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