using System.Collections.Generic;
using System.Linq;
using SnmpAgent.Constants;
using SnmpAgent.Models;

namespace SnmpAgent.Providers
{
    public class ObjectTypesProvider
    {
        public List<ObjectType> GetAllObjectTypes(string fileName)
        {
            var mib = new MibReader(fileName);
            mib.ReadFile();

            var objectsTypesRunner = new RegexRunner(RegexConstants.ObjectTypesPattern, mib.Text);
            var objectTypes = objectsTypesRunner.GetAllMatches();


            return objectTypes.Select(x => (ObjectType) x).ToList();
        }

        public List<ObjectType> GetAllObjectTypes(string mibName, List<ObjectType> objectTypesList)
        {
            var mib = new MibReader(mibName);
            mib.ReadFile();

            var objectsTypesRunner = new RegexRunner(RegexConstants.ObjectTypesPattern, mib.Text);
            var objectTypes = objectsTypesRunner.GetAllMatches();


            return objectTypes.Select(x => (ObjectType) x).ToList();

            if (false /*any imports were found*/)
                return GetAllObjectTypes(mibName /*found import*/, objectTypesList);

            return objectTypesList;
        }
    }
}