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
        public ObjectTypesProvider(string text)
        {
            Text = text;
        }

        private string Text { get; set; }

        public Mib GetAllObjects()
        {
            var mibModel = new Mib();
            mibModel.Import = GetObjectImports(Text);
            mibModel.ObjectIdentifiers = GetObjectIdentifiers(Text);
            mibModel.ObjectTypes = GetObjectTypes(Text);



            if (!mibModel.Import.Equals(""))
            {
                return GetAllObjectsHelper(mibModel);
            }

            return mibModel;
        }

        private Mib GetAllObjectsHelper(Mib mibModel)
        {
            var mibReader = new MibReader(mibModel.Import);
            mibReader.ReadFile();

            mibModel.Import = GetObjectImports(mibReader.Text);
            mibModel.ObjectIdentifiers = mibModel.ObjectIdentifiers.Concat(GetObjectIdentifiers(mibReader.Text)); ;
            mibModel.ObjectTypes = mibModel.ObjectTypes.Concat(GetObjectTypes(mibReader.Text));



            if (!mibModel.Import.Equals(""))
            {
                return GetAllObjectsHelper(mibModel);
            }

            return mibModel;
        }

        private IEnumerable<ObjectIdentifier> GetObjectIdentifiers(string mibText)
        {
            var objectsIdentifiersRunner = new RegexRunner(RegexConstants.ObjectIdentifiersPattern, mibText);
            var matchCollection = objectsIdentifiersRunner.GetAllMatches();

            var objectIdentifiers = matchCollection.Select(x => (ObjectIdentifier)x);

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

        private IEnumerable<ObjectType> GetObjectTypes(string mibText)
        {
            var objectsTypesRunner = new RegexRunner(RegexConstants.ObjectTypesPattern, mibText);
            var matchCollection = objectsTypesRunner.GetAllMatches();
            var objectTypes = matchCollection.Select(x => (ObjectType)x);
            return objectTypes;
        }

    }
}