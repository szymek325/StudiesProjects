namespace SnmpAgent.BerDecoding.Interface
{
    public interface IByteOperations
    {
        int get_length(ref byte[] input);
        byte[] code_length(int size);
        int get_int(ref byte[] input);
        string get_octet_string(ref byte[] input);
        string get_object_id(ref byte[] input, ref byte[] raw_obj_id);
        void strip_sequence(ref byte[] input);
    }
}