using System;
using System.Linq;
using System.Text;
using SnmpAgent.BerDecoding.Interface;

namespace SnmpAgent.BerDecoding.Implementation
{
    public class DecoderRunner : IDecoderRunner
    {
        private readonly IBerDecoder berDecoder;

        public DecoderRunner(IBerDecoder berDecoder)
        {
            this.berDecoder = berDecoder;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowDecoderMenu();
                var encodedData = ReadEncodedData();
                Console.WriteLine(encodedData);
                var formattedData = PrepareString(encodedData);
                if (formattedData.Equals("exit"))
                {
                    return;
                }
                else if (formattedData.Length != default(int) && !formattedData.Equals(""))
                {
                    var bytes = StringToByteArrayFastest(formattedData);
                    var node = berDecoder.Decode(ref bytes);
                    node.ShowNode();
                    Console.ReadKey();
                }

            }
        }

        private string PrepareString(string encodedData)
        {
            encodedData=encodedData.Replace(" ", "");
            encodedData = encodedData.Replace("-", "");
            encodedData = encodedData.Replace(",", "");
            return encodedData;
        }

        private string ReadEncodedData()
        {
            var text = Console.ReadLine();
            return text;
        }

        private void ShowDecoderMenu()
        {
            Console.WriteLine("-------------------------DECODER MENU-------------------------");
            Console.WriteLine(" Type data to decode"); 
            Console.WriteLine(" Type exit to exit decoder");
        }

        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
            {
                throw new Exception("The binary key cannot have an odd number of digits");
                Console.WriteLine("The binary key cannot have an odd number of digits");
                
            }
                
                

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            //return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

    }
}