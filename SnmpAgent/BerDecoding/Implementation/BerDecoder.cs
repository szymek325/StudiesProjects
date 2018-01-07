﻿using System;
using SnmpAgent.BerDecoding.Interface;
using SnmpAgent.BerDecoding.Models;

namespace SnmpAgent.BerDecoding.Implementation
{
    public  class BerDecoder : IBerDecoder
    {
        private IByteOperations byteOperations;

        public BerDecoder(IByteOperations byteOperations)
        {
            this.byteOperations = byteOperations;
        }


        public SnmpMessage.SNMP_message decode(byte[] input) //tu się pewnie typ zmieni
        {
            Console.WriteLine(BitConverter.ToString(input));
            SnmpMessage.SNMP_message snmp_message = new SnmpMessage.SNMP_message();
            int snmp_version = 0;
            string community_string = "";

            int request_id = 0;
            int error = 0;
            int error_index = 0;

            string object_id = "";
            byte[] raw_obj_id = new byte[1];

            int value_int = 0;
            string value_string = "";

            switch (input[0])
            {
                case 0x30:  //snmp
                    Console.WriteLine("Typ wiadomości SNMP o długości: " + byteOperations.get_length(ref input));

                    snmp_version = byteOperations.get_int(ref input);
                    community_string = byteOperations.get_octet_string(ref input);

                    snmp_message.community_string = community_string;

                    switch (input[0])
                    {
                        case 0xA0: //SNMP get
                            byteOperations.get_length(ref input);

                            snmp_message.SNMP_message_type = SnmpMessage.SNMP_message_types.GetRequest;

                            request_id = byteOperations.get_int(ref input);
                            snmp_message.req_id = request_id;

                            error = byteOperations.get_int(ref input);
                            error_index = byteOperations.get_int(ref input);

                            byteOperations.strip_sequence(ref input);
                            byteOperations.strip_sequence(ref input);

                            object_id = byteOperations.get_object_id(ref input, ref raw_obj_id);
                            snmp_message.object_id = object_id;
                            snmp_message.raw_object_id = raw_obj_id;

                            Console.WriteLine("Wiadmość typu SNMP GetRequest dla object_id: " + object_id);

                            break;

                        case 0xA3: //SNMP set
                            byteOperations.get_length(ref input);

                            snmp_message.SNMP_message_type = SnmpMessage.SNMP_message_types.SetRequest;

                            request_id = byteOperations.get_int(ref input);
                            snmp_message.req_id = request_id;

                            error = byteOperations.get_int(ref input);
                            error_index = byteOperations.get_int(ref input);

                            byteOperations.strip_sequence(ref input);
                            byteOperations.strip_sequence(ref input);

                            // TODO: może zrób wiele var bindsów

                            object_id = byteOperations.get_object_id(ref input, ref raw_obj_id);
                            snmp_message.object_id = object_id;
                            snmp_message.raw_object_id = raw_obj_id;

                            switch (input[0])
                            {
                                case 0x02:
                                    value_int = byteOperations.get_int(ref input);
                                    snmp_message.int_value = value_int;
                                    Console.WriteLine("Wiadmość typu SNMP SetRequest dla object_id: " + object_id + " i wartości: " + value_int);
                                    break;
                                case 0x04:
                                    value_string = byteOperations.get_octet_string(ref input);
                                    snmp_message.string_value = value_string;
                                    Console.WriteLine("Wiadmość typu SNMP SetRequest dla object_id: " + object_id + " i wartości: " + value_string);
                                    break;
                                default:
                                    Console.WriteLine("Typ wartości nie jest obsługiwany");
                                    break;
                            }

                            break;

                        default:
                            Console.WriteLine("Typ zapytania SNMP nie jest obsługiwany");
                            break;
                    }

                    //Console.WriteLine(BitConverter.ToString(input));
                    break;

                default:
                    Console.WriteLine("Typ wiadomości nie jest obsługiwany");
                    break;
            }
            return snmp_message;
        }

    }
}