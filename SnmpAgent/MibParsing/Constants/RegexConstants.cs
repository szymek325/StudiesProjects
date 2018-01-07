namespace SnmpAgent.MibParsing.Constants
{
    public static class RegexConstants
    {
        public static string ObjectTypesPattern =
            @"(\w*\s*)OBJECT-TYPE\s* SYNTAX\s* (.*?)\s* ACCESS\s* (\w*-\w*)\s* STATUS\s* (.*?)\s* DESCRIPTION\s*""(.*?)""(.*?)::= { (\w*) (\d*) }";


        public static string ImportPattern = @"\s*FROM\s(\S*)\s*";
        public static string ObjectIdentifiersPattern = @"(\w*|\w*-\w*)\s*OBJECT IDENTIFIER ::= {\s*(\S*)\s*(\d*)\s*}";

        public static string MainOidPattern =
            @"(\w*|\w*-\w*)\s*OBJECT IDENTIFIER ::= {\s(\S*)\s(\w*)[(](\d*)[)]\s(\w*)[(](\d*)[)]\s(\d*)\s}";

        public static string SequencePattern = @"(\w*)\s*::=\s*SEQUENCE\s*{\s*(.*?)\s*}";

        public static string DataTypePattern =
            @"\s*(\S*)\s::=\s*[[](\S* \d*)[]]\s*(.*?)\s*(IMPLICIT|EXPLICIT)\s*(.*?)\n";

        public static string SyntaxLimitationsPattern = @"\s[(](\d*)..(\d*)[)]";

        #region unusedRegex
        //public static string SequencePattern = @"\s*(\w*)\s::=\s*SEQUENCE\s{.*\n(?s)(.*?)(?>)}";
        //public static string ObjectTypesPattern =
        //        @"(\w*\s*)OBJECT-TYPE\s*SYNTAX\s*(.*?)\s*ACCESS\s*(\w*-\w*)\s*STATUS\s*(.*?)\s*DESCRIPTION\s*""(.*?)""\s*\w*\s*\W (\w*) \W\s*::= { (\w*) (\d*) }";
        //public static string DataTypePattern = @"\s*(\S*)\s::=\s*[[](\S* \d*)[]]\s*(IMPLICIT|EXPLICIT)\s*(\S*)\s(\S*)";
        //public static string ObjectIdentifiersPattern = @"(\w*)\s*OBJECT IDENTIFIER ::= {\s(\w*-\d*)\s(\d*)\s}";
        //public static string ObjectIdentifiersPattern2 = @"(\w*)\s*OBJECT IDENTIFIER ::= {\s(\w*) (\d*) }";
        //public static string ObjectIdentifiersPattern = @"(\S*)\s*OBJECT IDENTIFIER ::= {\s(.*?)\s}";
        //public static string ObjectIdentifiersPattern = @"(\w*|\w*-\w*)\s*OBJECT IDENTIFIER ::= {\s(\w*|\w*-\w*) (\d*)\s}";
        //public static string ObjectIdentifiersPattern = @"\s(\w*)\s*OBJECT IDENTIFIER ::= { (\w*-\d*) (\d*) }";
        //public static string ObjectIdentifiersPattern = @"(?<name>.*)OBJECT IDENTIFIER ::= {(?<parent>.*)}"; 
        //public static string OidNodeNameAndNumber = @"(\S*)\s(\d*)";

        #endregion
    }
}