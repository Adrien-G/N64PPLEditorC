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
        public static (byte[] palette,byte[] data) ConvertPixelsToCompressedFormat(byte[] texture, CGeneric.Compression textureType)
        {

            byte[] finalArray = texture;
            byte[] palette = new byte[0];

            //compress result
            switch (textureType)
            {
                //greyscale with alpha
                case CGeneric.Compression.greyscale:
                    finalArray = ConvertFormatGreyscale(texture);
                    break;

                case CGeneric.Compression.max16Colors:
                    break;

                case CGeneric.Compression.max256Colors:
                    (palette, finalArray) = ConvertFormatMax256Colors(texture);
                    break;

                case CGeneric.Compression.trueColor16Bits :
                    break;
            }
            return (palette,finalArray);
        }

        public static int GetBytePerPixel(CGeneric.Compression compressionType)
        {
            int finalValue = 4;
            switch (compressionType)
            {
                case CGeneric.Compression.unknow22:
                    finalValue = 4;
                    break;
                case CGeneric.Compression.unknow23:
                    finalValue = 1;
                    break;
                case CGeneric.Compression.greyscale:
                    finalValue = 2;
                    break;
                case CGeneric.Compression.max16Colors:
                    finalValue = 2; //TODO to recheck
                    break;
                case CGeneric.Compression.max256Colors:
                    finalValue = 4;
                    break;
                case CGeneric.Compression.trueColor16Bits:
                    finalValue = 2;
                    break;
                case CGeneric.Compression.trueColor32Bits:
                    finalValue = 4;
                    break;
            }
            return finalValue;
        }

        private static byte[] ConvertFormatGreyscale(byte[] texture)
        {
            byte[] finalArray = new byte[texture.Length / 2];
            int index = 0;

            for (int i = 0; i < texture.Length; i += 4)
            {
                double nb = (texture[i] + texture[i + 1] + texture[i + 2]) / 3;
                finalArray[index] = (byte)Math.Floor(nb);
                finalArray[index + 1] = texture[i + 3];
                index += 2;
            }
            return finalArray;
        }
        private static (byte[] palette,byte[] data) ConvertFormatMax256Colors(byte[] texture)
        {
            //construct palette and use it.
            List<Color> colors = ExtractPaletteFromByteArray(texture);

            //first array for palette color
            byte[] palette = new byte[colors.Count * 4];

            //second array for palette data
            byte[] finalArray = new byte[texture.Length / 4];

            // write palette 
            for (int i = 0; i < colors.Count(); i++)
            {
                palette[i * 4] = colors[i].R;
                palette[i * 4 + 1] = colors[i].G;
                palette[i * 4 + 2] = colors[i].B;
                palette[i * 4 + 3] = colors[i].A;
            }

            int index = 0;
            // write data with palette information
            for (int i = 0; i < texture.Length; i += 4)
            {
                finalArray[index] = GetIndexFromPalette(colors, Color.FromArgb(texture[i + 3], texture[i], texture[i + 1], texture[i + 2]));
                index ++;
            }
            return (palette,finalArray);
        }
        private static List<Color> ExtractPaletteFromByteArray(byte[] texture)
        {
            HashSet<Color> colors = new HashSet<Color>();

            for (int i = 0; i < texture.Length; i += 4)
            {
                colors.Add(Color.FromArgb(texture[i+3], texture[i], texture[i+1], texture[i+2]));
            }
            return colors.ToList<Color>();
        }

        private static byte GetIndexFromPalette(List<Color> palette,Color color)
        {
            byte index = 0;
            for (int i = 0; i < palette.Count; i++)
                if(palette[i] == color)
                    break;
                else
                    index++;
            return index;
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

        public static byte GetGreenAlphaValueIndex(byte[] palette, CGeneric.Compression compressionMethod)
        {
            Color greenAlpha = Color.FromArgb(255, 0, 255, 0);
            Color tmpColor;
            byte index = 0;
            if(compressionMethod == CGeneric.Compression.max16Colors)
            {
                throw new NotImplementedException();
            }
            else 
            {
                for (int i = 0; i < palette.Length; i += 4)
                {
                    tmpColor = Color.FromArgb(palette[i+3], palette[i], palette[i+1], palette[i+2]);
                    if (tmpColor == greenAlpha)
                        return index;
                    else
                        index++;
                }
            }
            
            return index;
        }

        public static bool isMoreColorThanExpected(Bitmap bmp, int maxColor)
        {
            HashSet<Color> colors = new HashSet<Color>();

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    colors.Add(bmp.GetPixel(x, y));
                    if (colors.Count() > maxColor)
                        return true;
                }
            }
            return false;
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
        public static Bitmap ConvertByteArrayToBitmap(Byte[] array, int sizeX, int sizeY, CGeneric.Compression compressionMode)
        {
            Bitmap bmp = new Bitmap(sizeX, sizeY);
            int index = 0;

            switch (compressionMode)
            {
                case CGeneric.Compression.greyscale:
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(array[index + 1], array[index], array[index], array[index]));
                            index += 2;
                        }
                    }
                    break;
                case CGeneric.Compression.max16Colors:
                    break;
                case CGeneric.Compression.max256Colors:
                    break;
                case CGeneric.Compression.trueColor16Bits:
                    break;
                case CGeneric.Compression.trueColor32Bits:
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(array[index + 3], array[index], array[index + 1], array[index + 2]));
                            index += 4;
                        }
                    }
                    break;
            }
            return bmp;
        }
    }
}
