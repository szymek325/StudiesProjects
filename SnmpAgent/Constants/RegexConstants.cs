﻿namespace SnmpAgent.Constants
{
    public static class RegexConstants
    {
        public static string ObjectTypesPattern =
                @"(\w*\s*)OBJECT-TYPE\s*SYNTAX\s*(.*?)\s*ACCESS\s*(\w*-\w*)\s*STATUS\s*(.*?)\s*DESCRIPTION\s*""(.*?)""\s*::=\s*{\s*(\S*)\s(\d*)\s*}";
        public static string ImportPattern = @"\s*FROM\s(\S*)\s*";
        public static string ObjectIdentifiersPattern = @"(\w*)\s*OBJECT IDENTIFIER ::= {\s(\w*-\d*)\s(\d*)\s}";
        public static string ObjectIdentifiersPattern2 = @"(\w*)\s*OBJECT IDENTIFIER ::= {\s(\w*) (\d*) }";
        //public static string ObjectIdentifiersPattern = @"\s(\w*)\s*OBJECT IDENTIFIER ::= { (\w*-\d*) (\d*) }";
        //public static string ObjectIdentifiersPattern = @"(?<name>.*)OBJECT IDENTIFIER ::= {(?<parent>.*)}"; //od Kamila
    }
}