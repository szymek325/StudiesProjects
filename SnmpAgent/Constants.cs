using System;
using System.Collections.Generic;
using System.Text;

namespace SnmpAgent
{
    public static class Constants
    {
        public static string MyPattern = @"(\w*\s*)OBJECT-TYPE\s*SYNTAX\s*(.*?)\s*ACCESS\s*(\w*-\w*)\s*STATUS\s*(.*?)\s*DESCRIPTION\s*""(.*?)""\s*::=\s*{\s*(.*?)\s*}";
        public static string Path = ".\\RFC1213-MIB.txt";
    }
}
