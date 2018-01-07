namespace SnmpAgent.BerDecoding.Models
{
    public class SnmpMessage
    {
        public enum SNMP_message_types
        {
            GetRequest,
            GetResponse,
            SetRequest
        }

        public struct SNMP_message
        {
            public SNMP_message_types SNMP_message_type;
            public int req_id;
            public string object_id;
            public byte[] raw_object_id;
            public int int_value;
            public string string_value;
            public string community_string;
            public int error;
            public byte aplication_spec_id;
        }
    }
}