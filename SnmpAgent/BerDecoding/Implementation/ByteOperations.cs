using System;
using System.Linq;
using System.Text;
using SnmpAgent.BerDecoding.Interface;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class ByteOperations: IByteOperations
    {
        public void strip_sequence(ref byte[] input)
        {
            get_length(ref input);
        }

        public int get_length(ref byte[] input)
        {
            var size = 0;
            if (input[1] >= 128)
            {
                for (var i = 0; i < Convert.ToInt32(input[1] & 0x7F); i++)
                {
                    if (i == 0) size = Convert.ToInt32(input[2]);
                    else size = size * 256 + Convert.ToInt32(input[2 + i]);
                }

                input = input.Skip(2 + Convert.ToInt32(input[1] & 0x7F)).ToArray();
            }
            else
            {
                size = Convert.ToInt32(input[1]);
                input = input.Skip(2).ToArray();
            }

            return size;
        }

        public byte[] code_length(int size)
        {
            byte[] array;
            if (size < 128)
            {
                array = new byte[1];
                array[0] = Convert.ToByte(size);
            }
            else
            {
                var count = 0;
                var temp = size;
                while (temp >= 1)
                {
                    count++;
                    temp = temp / 256;
                }

                array = new byte[count + 1];
                array[0] = Convert.ToByte(count + 128);

                for (var i = count; i >= 1; i--)
                {
                    array[i] = Convert.ToByte(size % 256);
                    size = size / 256;
                }
            }

            return array;
        }

        public int get_int(ref byte[] input)
        {
            if (input[0] != 0x02)
            {
                Console.WriteLine("Zły typ, spodziwano się inta");
                return -1;
            }

            var value = 0;
            var length = get_length(ref input);
            for (var i = 0; i < length; i++)
            {
                if (i == 0) value = Convert.ToInt32(input[i] & 0x7F);
                else value = value * 256 + Convert.ToInt32(input[i]);
            }

            value = value - Convert.ToInt32((input[0] & 0x80) << (8 * (length - 1)));
            Console.WriteLine("Int o długości: " + length + " i wartości: " + value);
            input = input.Skip(length).ToArray();
            return value;
        }

        public string get_octet_string(ref byte[] input)
        {
            if (input[0] != 0x04)
            {
                Console.WriteLine("Zły typ, spodziwano się stringa");
                return "error";
            }

            var value = "";
            var length = get_length(ref input);
            var my_string = new byte[length];
            Array.Copy(input, 0, my_string, 0, length);
            Console.WriteLine("Octet string: " + Encoding.ASCII.GetString(my_string) + " o długości:" + length);
            value = Encoding.ASCII.GetString(my_string);
            input = input.Skip(length).ToArray();
            return value;
        }

        public string get_object_id(ref byte[] input, ref byte[] raw_obj_id)
        {
            var obj_id = "";

            if (input[0] != 0x06)
            {
                Console.WriteLine("Zły typ, spodziwano się object_id");
                return "error";
            }

            var temp = new byte[input.Length];
            Buffer.BlockCopy(input, 0, temp, 0, input.Length);

            var size = get_length(ref input);
            raw_obj_id = new byte[size + (temp.Length - input.Length)];
            Buffer.BlockCopy(temp, 0, raw_obj_id, 0, size + (temp.Length - input.Length));

            for (var i = 0; i < size; i++)
            {
                if (input[i] == 0x2B) obj_id += "1.3";
                else obj_id += "." + input[i];
            }

            Console.WriteLine("Object id: " + obj_id + " o długości:" + size);

            input = input.Skip(size).ToArray();

            return obj_id;
        }
    }
}