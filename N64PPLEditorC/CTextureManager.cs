using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    static class CTextureManager
    {
        public static byte[] ConvertPixelsToCompressedFormat(byte[] texture, CGeneric.Compression textureType)
        {

            byte[] finalArray = new byte[0];
            int index = 0;

            //compress result
            switch (textureType)
            {
                //greyscale with alpha
                case CGeneric.Compression.greyscale:
                    finalArray = new byte[texture.Length / 2];
                    for (int i = 0; i < texture.Length; i += 4)
                    {
                        double nb = (texture[i] + texture[i + 1] + texture[i + 2]) / 3;
                        finalArray[index] = (byte)Math.Floor(nb);
                        finalArray[index + 1] = texture[i + 3];
                        index += 2;
                    }
                    break;
                case CGeneric.Compression.max16Colors :

                    break;
                case CGeneric.Compression.max256Colors :
                    break;
                //true color 16 bit, with alpha
                case CGeneric.Compression.trueColor16Bits :
                    break;
                //true color 32 bit, with alpha
                case CGeneric.Compression.trueColor32Bits:
                    break;
            }
            return finalArray;
        }

        public static CGeneric.Compression TestBestCompression(Bitmap bmp)
        {
            if (CTextureManager.isGreyscaleTexture(bmp))
                return CGeneric.Compression.greyscale;
            else if (!CTextureManager.isMoreColorThanExpected(bmp, 16))
                return CGeneric.Compression.max16Colors;
            else if (!CTextureManager.isMoreColorThanExpected(bmp, 256))
                return CGeneric.Compression.max256Colors;
            else if (!CTextureManager.isAlphaVariation(bmp))
                return CGeneric.Compression.trueColor16Bits;
            else
                return CGeneric.Compression.trueColor32Bits;
        }

        public static bool isMoreColorThanExpected(Bitmap bmp, int maxColor)
        {
            HashSet<Color> colors = new HashSet<Color>();

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    colors.Add(bmp.GetPixel(x, y));
                    if(colors.Count() > maxColor)
                        return true;
                }
            }
            return false;

        }

        public static List<Color> extractPaletteFromBitmap()
        {
            return new List<Color>();
        }

        public static bool isGreyscaleTexture(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    if (color.R != color.G || color.R != color.B)
                        return false;
                }
            }
            return true;
        }

        public static bool isAlphaVariation(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    if (color.A != 0 || color.A != 255)
                        return true;
                }
            }
            return false;
        }

        public static byte[] ConvertTextureToByteArray(Bitmap bmp)
        {
            byte[] textureArray = new Byte[bmp.Width * bmp.Height * 4];
            Color tmpColor = new Color();
            int index = 0;

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    tmpColor = bmp.GetPixel(x, y);
                    textureArray[index] = tmpColor.R;
                    textureArray[index + 1] = tmpColor.G;
                    textureArray[index + 2] = tmpColor.B;
                    textureArray[index + 3] = tmpColor.A;
                    index += 4;
                }
            }
            return textureArray;
        }
        public static Bitmap ConvertByteArrayToBitmap(Byte[] array, int sizeX, int sizeY)
        {
            Bitmap bmp = new Bitmap(sizeX, sizeY);
            int index = 0;

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    bmp.SetPixel(x, y, Color.FromArgb(array[index + 3], array[index], array[index + 1], array[index + 2]));
                    index += 4;
                }
            }
            return bmp;
        }
    }
}
