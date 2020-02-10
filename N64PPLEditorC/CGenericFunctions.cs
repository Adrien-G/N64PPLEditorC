using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace N64PPLEditorC
{
    public static class CGenericFunctions
    {

        public static String pathExtractedTexture = Application.StartupPath + @"\extractedTexture\";
        public static String pathOtherContent = Application.StartupPath + @"\OtherContent\";
        public static String pathCompressedTexture = Application.StartupPath + @"\compressedTexture\";
        public static String pathReplacedTexture = Application.StartupPath + @"\replacedTexture\";


        public static Int32 ConvertByteArrayToInt(Byte[] byteArray)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            return BitConverter.ToInt32(byteArray, 0);
        }

        public static int SearchBytesInArray(Byte[] arraySource, Byte[] dataSearched)
        {
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
                            return i;
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
