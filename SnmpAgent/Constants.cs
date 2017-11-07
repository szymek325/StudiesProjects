using System;
using System.Collections.Generic;
using System.Text;

namespace SnmpAgent
{
    public static class Constants
    {
        public static string ObjectTypesPattern = @"(\w*\s*)OBJECT-TYPE\s*SYNTAX\s*(.*?)\s*ACCESS\s*(\w*-\w*)\s*STATUS\s*(.*?)\s*DESCRIPTION\s*""(.*?)""\s*::=\s*{\s*(\S*)\s(\d*)\s*}";
        public static string Path = ".\\RFC1213-MIB.txt";
        public static string ImportPattern = @"\s*FROM\s(\S*)\s*";
        public static string ObjectIdentifiersPattern = @"\s(\w*)\s*OBJECT IDENTIFIER ::= { (\w*-\d*) (\d*) }";
    }
}
