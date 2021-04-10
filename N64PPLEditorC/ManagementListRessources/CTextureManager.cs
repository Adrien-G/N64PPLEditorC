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

        #region Specific usage -> Make texture to good format ("compress" texture to good format)
        public static (byte[] palette,byte[] data) ConvertPixelsToGoodFormat(byte[] texture, CGeneric.Compression textureType, bool isCompressedPalette = false)
        {

            byte[] finalArray = texture;
            byte[] palette = new byte[0];

            switch (textureType)
            {
                case CGeneric.Compression.greyscale:
                    finalArray = ConvertRGBAtoGreyscale(texture);
                    break;
                case CGeneric.Compression.max16Colors:
                    (palette, finalArray) = ConvertRGBAtoMax16Colors(texture);
                    break;
                case CGeneric.Compression.max256Colors:
                    (palette, finalArray) = ConvertRGBAtoMax256Colors(texture,isCompressedPalette);
                    break;
                case CGeneric.Compression.trueColor16Bits :
                    finalArray = ConvertRGBAtoTrueColor16Bits(texture);
                    break;
            }
            return (palette,finalArray);
        }

        private static (byte[] palette, byte[] data) ConvertRGBAtoMax16Colors(byte[] texture)
        {
            //construct palette and use it.
            List<Color> colors = ExtractPaletteFromByteArray(texture);

            //first array for palette color
            byte[] palette = new byte[colors.Count * 4];

            //second array for palette data
            byte[] finalArray = new byte[texture.Length / 8];

            // write palette 
            for (int i = 0; i < colors.Count(); i++)
            {
                palette[i * 4] = colors[i].R;
                palette[i * 4 + 1] = colors[i].G;
                palette[i * 4 + 2] = colors[i].B;
                palette[i * 4 + 3] = colors[i].A;
            }

            int index = 0;
            byte nibble1;
            byte nibble2;

            // write data with palette information
            for (int i = 0; i < texture.Length; i += 8)
            {
                nibble1 = GetIndexFromPalette(colors, Color.FromArgb(texture[i + 3], texture[i], texture[i + 1], texture[i + 2]));
                nibble2 = GetIndexFromPalette(colors, Color.FromArgb(texture[i + 7], texture[i + 4], texture[i + 5], texture[i + 6]));
                finalArray[index] = CGeneric.NibbleToByte(nibble1, nibble2);
                index++;
            }
            return (palette, finalArray);
        }
        private static (byte[] palette, byte[] data) ConvertRGBAtoMax256Colors(byte[] texture,bool isCompressedPalette)
        {
            //construct palette and use it.
            List<Color> colors = ExtractPaletteFromByteArray(texture);

            //first array for palette color
            byte[] palette;

            //second array for palette data
            byte[] finalArray = new byte[texture.Length / 4];

            // write palette, if compressed palette, compress the palette else just write it
            if (isCompressedPalette)
            {
                palette = new byte[colors.Count * 2];
                byte tmpR, tmpG,tmpG2, tmpB, tmpA;
                byte res1, res2;
                for (int i = 0; i < colors.Count(); i++)
                {
                    //red (5 bits),green (5 bits),blue (5 bits),Alpha (1bit)
                    tmpR = (byte)Math.Floor((double)colors[i].R/8);
                    tmpG = (byte)Math.Floor((double)colors[i].G/8);
                    tmpG2 =(byte)Math.Floor((double)colors[i].G/8);
                    tmpB = (byte)Math.Floor((double)colors[i].B/8);
                    tmpA = (byte)Math.Floor((double)colors[i].A/255);
                    

                    //set first byte (5 red, 3 green)
                    res1 =  tmpR;
                    res1 <<= 3;
                    tmpG >>= 2;
                    res1 += tmpG;

                    //set second byte (2 green, 5 blue, 1 alpha)
                    res2 = tmpG2;
                    res2 <<= 6;
                    tmpB <<= 1;
                    res2 += tmpB;
                    res2 += tmpA;
                    

                    palette[i * 2] = (byte)res1;
                    palette[i * 2 + 1] = (byte)res2;
                }

                int index = 0;
                // write data with palette information
                for (int i = 0; i < texture.Length; i += 4)
                {
                    finalArray[index] = GetIndexFromPalette(colors, Color.FromArgb(texture[i + 3], texture[i], texture[i + 1], texture[i + 2]));
                    index++;
                }
            }
            else
            { 
                palette = new byte[colors.Count * 4];
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
                    index++;
                }
            }

            
            return (palette, finalArray);
        }
        private static byte[] ConvertRGBAtoTrueColor16Bits(byte[] texture)
        {
            byte[] finalArray = new byte[texture.Length / 2];

            byte G1,G2, B, A;
            byte byte1, byte2;
            int index = 0;

            for (int i = 0; i < texture.Length; i += 4)
            {

                //add red color
                byte1 = (byte)Math.Floor((double)(texture[i] / 8));
                byte1 <<= 3;

                //add green component to the first byte
                G1 = (byte)Math.Floor((double)(texture[i + 1] / 8));
                G1 <<= 3;
                G1 >>= 5;
                byte1 += G1;

                //add green component to the second byte
                G2 = (byte)Math.Floor((double)(texture[i + 1] / 8));
                G2 <<= 6;
                byte2 = G2;

                //add the blue component
                B = (byte)Math.Floor((double)(texture[i + 2] / 8));
                B <<= 1;
                byte2 += B;

                //add alpha component
                A = (byte)Math.Ceiling((double)(texture[i + 3] / 255));
                byte2 += A;

                finalArray[index] = byte1;
                finalArray[index + 1] = byte2;

                index += 2;
            }

            return finalArray;
        }

        private static byte[] ConvertRGBAtoGreyscale(byte[] texture)
        {
            byte[] finalArray = new byte[texture.Length / 2];
            int index = 0;

            for (int i = 0; i < texture.Length; i += 4)
            {
                finalArray[index] = texture[i];
                finalArray[index + 1] = texture[i + 3];
                index += 2;
            }
            return finalArray;
        }
        private static List<Color> ExtractPaletteFromByteArray(byte[] texture)
        {
            HashSet<Color> colors = new HashSet<Color>();

            for (int i = 0; i < texture.Length; i += 4)
            {
                colors.Add(Color.FromArgb(texture[i + 3], texture[i], texture[i + 1], texture[i + 2]));
            }
            return colors.ToList<Color>();
        }

        private static byte GetIndexFromPalette(List<Color> palette, Color color)
        {
            byte index = 0;
            for (int i = 0; i < palette.Count; i++)
                if (palette[i] == color)
                    break;
                else
                    index++;
            return index;
        }
        #endregion

        #region Specific usage -> Decompress texture part

        public static byte[] ConvertByteArrayToRGBA(byte[] texture, CGeneric.Compression compressionType, byte[] palette = null,bool isCompressedPalette=false)
        {
            byte[] arrayRGBA = new byte[0];

            switch (compressionType)
            {
                case CGeneric.Compression.unknow22:
                    arrayRGBA = texture; //unknow
                    break;
                case CGeneric.Compression.unknow23:
                    arrayRGBA = texture; //unknow
                    break;
                case CGeneric.Compression.greyscale:
                    arrayRGBA = ConvertGreyscaleToRGBA(texture);
                    break;
                case CGeneric.Compression.max16Colors:
                    arrayRGBA = ConvertMax16ColorsToRGBA(texture, palette);
                    break;
                case CGeneric.Compression.max256Colors:
                    arrayRGBA = ConvertMax256ColorsToRGBA(texture, palette,isCompressedPalette);
                    break;
                case CGeneric.Compression.trueColor16Bits:
                    arrayRGBA = ConvertTrueColor16BitsToRGBA(texture);
                    break;
                case CGeneric.Compression.trueColor32Bits:
                    arrayRGBA = texture;
                    break;
            }
            return arrayRGBA;
        }

        private static byte[] ConvertGreyscaleToRGBA(byte[] texture)
        {
            byte[] rgbaArray = new byte[texture.Length * 2];
            for (int i = 0; i < texture.Length - 1; i += 2)
            {
                rgbaArray[i * 2] = texture[i];
                rgbaArray[i * 2 + 1] = texture[i];
                rgbaArray[i * 2 + 2] = texture[i];
                rgbaArray[i * 2 + 3] = texture[i + 1];
            }
            return rgbaArray;
        }

        private static byte[] ConvertMax256ColorsToRGBA(byte[] texture, byte[] palette,bool isCompressedPalette)
        {
            //safety check...
            if (palette == null)
                return new byte[0];

            //return rgbaArray;
            byte[] rgbaArray = new byte[texture.Length * 4];
            byte[] palette32bitColor;
            if (isCompressedPalette)
            {
                //convert palette to 32bpp
                palette32bitColor = new byte[palette.Length * 2];
                byte tmpR, tmpG, tmpG2, tmpB, tmpA;
                int R, G, B, A;

                for (int i = 0; i < palette.Length; i += 2)
                {
                    //red (5 bits)
                    tmpR = palette[i];
                    tmpR >>= 3;

                    //green (5 bits -> 3bits for nibble 1, 2 bits for nibble 2)
                    tmpG = palette[i];
                    tmpG <<= 5;
                    tmpG >>= 3;

                    tmpG2 = palette[i + 1];
                    tmpG2 >>= 6;

                    tmpG += tmpG2;


                    //blue (5 bits)
                    tmpB = palette[i + 1];
                    tmpB <<= 2;
                    tmpB >>= 3;

                    //alpha 1 bit
                    tmpA = palette[i + 1];
                    tmpA <<= 7;
                    tmpA >>= 7;

                    R = tmpR;
                    G = tmpG;
                    B = tmpB;
                    A = tmpA;
                    R *= 8;
                    G *= 8;
                    B *= 8;
                    A *= 255;

                    palette32bitColor[i * 2] = (byte)R;
                    palette32bitColor[i * 2 + 1] = (byte)G;
                    palette32bitColor[i * 2 + 2] = (byte)B;
                    palette32bitColor[i * 2 + 3] = (byte)A;
                }
            }
            else
                palette32bitColor = palette;
               
            //with the god palette create the good texture
            for (int i = 0; i < texture.Length - 1; i++)
                Array.Copy(palette32bitColor, texture[i]*4, rgbaArray, i * 4, 4);

            return rgbaArray;
        }

        private static byte[] ConvertMax16ColorsToRGBA(byte[] texture, byte[] palette)
        {
            byte[] rgbaArray = new byte[texture.Length * 8];
            byte tmpB1;
            byte tmpB2;

            for (int i = 0; i < texture.Length - 1; i++)
            {
                tmpB1 = texture[i];
                tmpB1 >>= 4;

                tmpB2 = texture[i];
                tmpB2 <<= 4;
                tmpB2 >>= 4;
                Array.Copy(palette, tmpB1 * 4, rgbaArray, 8 * i, 4);
                Array.Copy(palette, tmpB2 * 4, rgbaArray, 8 * i + 4, 4);
            }
            return rgbaArray;
        }

        private static byte[] ConvertTrueColor16BitsToRGBA(byte[] texture)
        {
            byte[] rgbaArray = new byte[texture.Length * 2];

            byte red, green, green2, blue, alpha;
            int R, G, B, A;

            for (int i = 0; i < texture.Length - 1; i += 2)
            {
                //red (5 bits)
                red = texture[i];
                red >>= 3;

                //green (5 bits -> 3bits for nibble 1, 2 bits for nibble 2)
                green = texture[i];
                green <<= 5;
                green >>= 3;
                green2 = texture[i + 1];
                green2 >>= 6;
                green += green2;

                //blue (5 bits)
                blue = texture[i + 1];
                blue <<= 2;
                blue >>= 3;

                //alpha 1 bit
                alpha = texture[i + 1];
                alpha <<= 7;
                alpha >>= 7;

                R = red;
                G = green;
                B = blue;
                A = alpha;
                R *= 8;
                G *= 8;
                B *= 8;
                A *= 255;

                rgbaArray[i * 2] = (byte)R;
                rgbaArray[i * 2 + 1] = (byte)G;
                rgbaArray[i * 2 + 2] = (byte)B;
                rgbaArray[i * 2 + 3] = (byte)A;
            }

            return rgbaArray;
        }
        #endregion


        #region AutoSelect best available compression type
        public static CGeneric.Compression TestBestCompression(Bitmap bmp)
        {
            if (CTextureManager.IsGreyscaleTexture(bmp))
                return CGeneric.Compression.greyscale;
            else if (!CTextureManager.IsMoreColorThanExpected(bmp, 16))
                return CGeneric.Compression.max16Colors;
            else if (!CTextureManager.IsMoreColorThanExpected(bmp, 256))
                return CGeneric.Compression.max256Colors;
            else if (!CTextureManager.IsAlphaVariation(bmp))
                return CGeneric.Compression.trueColor16Bits;
            else
                return CGeneric.Compression.trueColor32Bits;
        }

        private static bool IsGreyscaleTexture(Bitmap bmp)
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
        private static bool IsMoreColorThanExpected(Bitmap bmp, int maxColor)
        {
            //deny alpha variation for indexed color (not well interpreted by the rom)
            if (IsAlphaVariation(bmp))
                return true;

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
        private static bool IsAlphaVariation(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    if (color.A != 0xFF)
                        return true;
                }
            }
            return false;
        }
        #endregion

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
                    finalValue = 4;
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
        public static byte GetGreenAlphaValueIndex(byte[] palette, CGeneric.Compression compressionMethod)
        {
            Color greenAlpha = Color.FromArgb(255, 0, 255, 0);
            Color tmpColor;
            byte index = 0;

            for (int i = 0; i < palette.Length; i += 4)
            {
                tmpColor = Color.FromArgb(palette[i+3], palette[i], palette[i+1], palette[i+2]);
                if (tmpColor == greenAlpha)
                    return index;
                else
                    index++;
            }
            
            return index;
        }
        public static byte[] ConvertRGBABitmapToByteArrayRGBA(Bitmap bmp)
        {
            byte[] textureArray = new Byte[bmp.Width * bmp.Height * 4];
            Color tmpColor;
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

        public static Bitmap ConvertRGBAByteArrayToBitmap(Byte[] array, int sizeX, int sizeY)
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
