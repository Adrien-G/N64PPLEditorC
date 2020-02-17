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

        public static Int32 ConvertByteArrayToInt(Byte[] byteArray)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            return BitConverter.ToInt32(byteArray, 0);
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
