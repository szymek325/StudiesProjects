using System;
using System.Collections.Generic;
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

            var regexRunner= new RegexRunner(Constants.MyPattern, mib.Text);
            var matchList = regexRunner.GetAllMatches();

            return CreateObjects(matchList);
        }

        private List<ObjectType> CreateObjects(MatchCollection collection)
        {
            
            if (collection.Count > 0)
            {
                var list = new List<ObjectType>();
                foreach (Match match in collection)
                {
                    var objectType= new ObjectType();

                    objectType.Name = match.Groups[1].Value;
                    objectType.Syntax = match.Groups[2].Value;
                    objectType.Access = match.Groups[3].Value;
                    objectType.Status = match.Groups[4].Value;
                    objectType.Description = match.Groups[5].Value;

                    list.Add(objectType);
                }
                return list;
            }
            return new List<ObjectType>();
        }
    }
}
