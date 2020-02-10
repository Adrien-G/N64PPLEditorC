using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace N64PPLEditorC
{
    public class CGenericFunctions
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
