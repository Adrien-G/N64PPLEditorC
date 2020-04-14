using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace N64PPLEditorC
{
    public static class CGeneric
    {
        //different path used in the application
        public static readonly String pathExtractedTexture = Application.StartupPath + @"\extractedTexture\";
        public static readonly String pathOtherContent = Application.StartupPath + @"\OtherContent\";
        public static readonly String pathCompressedTexture = Application.StartupPath + @"\compressedTexture\";
        public static readonly String pathReplacedTexture = Application.StartupPath + @"\replacedTexture\";

        //size of the elements present in the table of ressources
        public static int sizeOfElementTable = 24;

        //some different pattern in the rom
        public static readonly Byte[] patternN64WaveTable = { 78, 54, 52, 32, 80, 116, 114, 84, 97, 98, 108, 101, 115, 86, 50 };
        public static readonly Byte[] patternAbraBif = { 65, 66, 82, 65, 46, 66, 73, 70 };
        public static readonly Byte[] patternBFF2 = { 66, 70, 70, 50 };
        
        //check the decimal value of ressources type
        public enum RessourceType : int
        {
            FIB = 860244290,
            HVQM = 1213616461,
            SBF = 1396852273,
            BFF = 1111901746
        }

        public enum Compression : byte
        {
            unknow22 = 0x22, 
            unknow23 = 0x23,
            greyscale = 0x24,       // 8 bits for color (R,G,B shared) and 8 bits for alpha
            max16Colors = 0x32,     // indexed color, palette reference a 16 bit color
            max256Colors = 0x33,    // indexed color, palette reference a 32 bit color
            trueColor16Bits = 0x54, // 5 bits for R, G and B, one bit for Alpha
            trueColor32Bits = 0x55  // 8 bits for each R,G,B,A
        }
        public static List<byte> ByteToNibble(byte byteToDecompose)
        {
            List<byte> byteList = new List<byte>();
            byte nibble1 = byteToDecompose;
            byte nibble2 = byteToDecompose;

            nibble1 /= 16;
            nibble2 %= 16;

            byteList.Add(nibble1);
            byteList.Add(nibble2);

            return byteList;
        }

        public static byte NibbleToByte(byte nibble1, byte nibble2)
        {
            if(nibble1 > 15 || nibble2 > 15)
                throw new OverflowException();

            byte res;
            res = nibble1;
            res <<= 4;
            res += nibble2;

            return res;
        }

        public static Int32 ConvertByteArrayToInt(Byte[] byteArray)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            if (byteArray.Length == 2)
                return BitConverter.ToInt16(byteArray, 0);
            else
                return BitConverter.ToInt32(byteArray, 0);
        }

        public static Byte[] ConvertIntToByteArray(Int32 nb)
        {
            byte[] res = BitConverter.GetBytes(nb);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(res);

            return res;
        }

        public static Byte[] ConvertIntToByteArray16bits(Int32 nb)
        {
            byte[] res = BitConverter.GetBytes(nb);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(res);

            return new byte[] { res[2], res[3] }; ;
        }

        public static byte[] ConvertStringToByteArray(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static int SearchBytesInArray(Byte[] arraySource, Byte[] dataSearched,int nbOccurence=0)
        {
            //allow to search the second, third (and so on) value of the occurence
            int nbOcc = 0;

            // loop on the first array for find occurence
            for (int i = 0; i < arraySource.Length - dataSearched.Length; i++)
            {
                if (arraySource[i] == dataSearched[0])
                {
                    //when occurence founded, check the rest of the array to compare
                    for (int j = 0; j < dataSearched.Length; j++)
                    {
                        if (arraySource[i + j] != dataSearched[j])
                            break;
                        if (j == dataSearched.Length - 1)
                            if (nbOcc == nbOccurence)
                                return i;
                            else
                                nbOcc += 1;
                    }
                }
            }
            return -1;
        }

        public static void VerifyExistingPath()
        {
            if (!File.Exists(pathExtractedTexture))
            {
                Directory.CreateDirectory(pathExtractedTexture);
            }
            if (!File.Exists(pathOtherContent))
            {
                Directory.CreateDirectory(pathOtherContent);
            }
            if (!File.Exists(pathCompressedTexture))
            {
                Directory.CreateDirectory(pathCompressedTexture);
            }
            if (!File.Exists(pathReplacedTexture))
            {
                Directory.CreateDirectory(pathReplacedTexture);
            }
        }
    }
}
