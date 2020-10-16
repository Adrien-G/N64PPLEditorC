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

        //size of the elements present in the table of ressources
        public static int sizeOfElementTable = 24;

        //some different pattern in the rom
        public static readonly Byte[] patternPuzzleLeagueN64 = { 0x50, 0x55, 0x5A, 0x5A, 0x4C, 0x45, 0x20, 0x4C, 0x45, 0x41, 0x47, 0x55, 0x45, 0x20, 0x4E, 0x36, 0x34 };
        public static readonly Byte[] patternN64WaveTable = { 0x4E, 0x36, 0x34, 0x20, 0x57, 0x61, 0x76, 0x65, 0x54, 0x61, 0x62, 0x6C, 0x65, 0x73, 0x20, 0x00 };
        public static readonly Byte[] patternN64PtrTableV2= { 0x4E, 0x36, 0x34, 0x20, 0x50, 0x74, 0x72, 0x54, 0x61, 0x62, 0x6C, 0x65, 0x73, 0x56, 0x32, 0x00 };
        public static readonly Byte[] patternMidiSoundBank1 = { 0x00, 0x00, 0x00, 0xCE, 0x00, 0x00, 0x00, 0xBA, 0x00, 0x00, 0x00, 0xA9, 0x00, 0x00, 0x00, 0x00 };
        public static readonly Byte[] patternAbraBif = { 65, 66, 82, 65, 46, 66, 73, 70 };
        public static readonly Byte[] patternBFF2 = { 66, 70, 70, 50 };
        
        public static readonly Byte[] endOfRom = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};
        public static readonly int romSize = 0x2000000;

        public enum TextType : int
        {
            Unused = 0x36, // unused, think only used by dev..
            Dialog = 0x44, //dialog, show text caracter by caracter
            Unknown52 = 0x52,
            Unknown60 = 0x60,
        }

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

        public enum romLang : byte
        {
            French = 0x46,
            European = 0x50,
            German = 0x44,
            USA = 0x45
        }

        public enum TextureDisplayStyle : byte
        {
            Fixed = 0x08,
            Animated = 0x2E,
            AnimatedScroll = 0x09
        }

        public static byte[] GiveMeArray(byte[] rawData, int startingData, int sizeData)
        {
            var data = new byte[sizeData];
            Array.Copy(rawData, startingData, data, 0, data.Length);
            return data;
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

        public static int ConvertByteArrayToInt(Byte[] byteArray)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            if (byteArray.Length == 2)
                return BitConverter.ToInt16(byteArray, 0);
            else
                return BitConverter.ToInt32(byteArray, 0);
        }

        public static uint ConvertByteArrayToUInt(Byte[] byteArray)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            if (byteArray.Length == 2)
                return BitConverter.ToUInt16(byteArray, 0);
            else
                return BitConverter.ToUInt32(byteArray, 0);
        }

        public static Byte[] ConvertUIntToByteArray(UInt32 nb)
        {
            byte[] res = BitConverter.GetBytes(nb);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(res);

            return res;
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

        public static string ConvertByteArrayToString(Byte[] text)
        {
            return Encoding.UTF8.GetString(text);
        }

        public static int SearchBytesInArray(Byte[] arraySource, Byte[] dataSearched,int nbOccurence=0,int startingAddress=0,int step=1)
        {
            //allow to search the second, third (and so on) value of the occurence
            int nbOcc = 0;


            // loop on the first array for find occurence
            for (int i = startingAddress; i <= arraySource.Length - dataSearched.Length; i +=step)
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
                Directory.CreateDirectory(pathExtractedTexture);
                Directory.CreateDirectory(pathOtherContent);
        }
    }
}
