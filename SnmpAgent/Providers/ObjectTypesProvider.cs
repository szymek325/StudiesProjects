using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
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
            mibModel.ObjectIdentifiers = mibModel.ObjectIdentifiers.Concat(GetObjectIdentifiers()); ;
            mibModel.ObjectTypes = mibModel.ObjectTypes.Concat(GetObjectTypes());

            if (!mibModel.Import.Equals(""))
            {
                return GetMibContent(mibModel);
            }

            return mibModel;
        }

        private IEnumerable<ObjectIdentifier> GetObjectIdentifiers()
        {
            var objectsIdentifiersRunner = new RegexRunner(RegexConstants.ObjectIdentifiersPattern, Text);
            var matchCollection = objectsIdentifiersRunner.GetAllMatches();

            var objectIdentifiers = matchCollection.Select(x => (ObjectIdentifier)x);

            return objectIdentifiers;
        }

        private string GetObjectImports()
        {
            var importsRunner = new RegexRunner(RegexConstants.ImportPattern, Text);
            var matchCollection = importsRunner.GetAllMatches();
            if (matchCollection.Count != 0)
            {
                return matchCollection[0]?.Groups[1].Value;
            }
            return "";
        }

        private IEnumerable<ObjectType> GetObjectTypes()
        {
            var objectsTypesRunner = new RegexRunner(RegexConstants.ObjectTypesPattern, Text);
            var matchCollection = objectsTypesRunner.GetAllMatches();
            var objectTypes = matchCollection.Select(x => (ObjectType)x);
            return objectTypes;
        }

    }
}