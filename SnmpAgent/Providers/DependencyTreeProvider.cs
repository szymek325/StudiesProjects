using System;
using System.Collections.Generic;
using System.Text;
using SnmpAgent.Helpers;
using SnmpAgent.Helpers.MibProcessing;
using SnmpAgent.Models;

namespace SnmpAgent.Providers
{
    public class DependencyTreeProvider
    {
        public static Mib MibModel { get; set; } = new Mib();

        private static MibParser mibParser;

        public DependencyTreeProvider()
        {
            mibParser= new MibParser();
        }

        public void GetDependencyTree(string mibName)
        {
            MibModel = mibParser.GetMibContent(mibName);
        }
    }
}
