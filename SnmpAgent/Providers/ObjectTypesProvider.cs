using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SnmpAgent.Providers
{
    public class ObjectTypesProvider
    {
        public List<ObjectType> GetAllObjectTypes()
        {
            var mib = new MibReader(Constants.Path);
            mib.ReadFile();

            var objectsTypesRunner = new RegexRunner(Constants.ObjectTypesPattern, mib.Text);
            var objectTypes = objectsTypesRunner.GetAllMatches();


            return objectTypes.Select(x => (ObjectType)x).ToList();
        }
    }
}
