using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    static class CTextureDecompress
    {

        private static (byte quantity ,byte multiplicator) getMultiplicatorAndPacketQuantity(byte value)
        {
            (byte multiplicator,byte quantity) tupleNibble = (0,0);
            switch (value)
            {
                case byte val when (val >= 128 && val <= 135):
                    tupleNibble = (1, Convert.ToByte(value - 126));
                    break;
                case byte val when (val >= 192 && val <= 199):
                    tupleNibble = (2, Convert.ToByte(value - 190));
                    break;
                case byte val when (val >= 200 && val <= 207):
                    tupleNibble = (3, Convert.ToByte(value - 198));
                    break;
                case byte val when (val >= 208 && val <= 215):
                    tupleNibble = (4, Convert.ToByte(value - 206));
                    break;
                case byte val when (val >= 216 && val <= 223):
                    tupleNibble = (5, Convert.ToByte(value - 214));
                    break;
                case byte val when (val >= 224 && val <= 231):
                    tupleNibble = (6, Convert.ToByte(value - 222));
                    break;
                case byte val when (val >= 232 && val <= 239):
                    tupleNibble = (7, Convert.ToByte(value - 230));
                    break;
                case byte val when (val >= 240 && val <= 247):
                    tupleNibble = (8, Convert.ToByte(value - 238));
                    break;
                case byte val when (val >= 248 && val <= 255):
                    tupleNibble = (9, Convert.ToByte(value - 246));
                    break;
            }
            return tupleNibble;
        }

        public static byte[] DecompressTexture(CBFF2.BFFHeader headerBFF2)
        {
            byte[] decompressedTexture = new Byte[headerBFF2.sizeX*headerBFF2.sizeY*headerBFF2.bytePerPixel];
            byte[] compressedTexture = headerBFF2.dataCompressed;
            int cursorCompressed = 0;
            int cursorDecompressed = 0;
            do
            {
                //surround with try catch because few of texture exceed the size of the texture size...
                try
                {
                    if (compressedTexture[cursorCompressed] < 128)
                        PerformSimpleReading(ref compressedTexture,ref cursorCompressed,ref decompressedTexture,ref cursorDecompressed);
                    else
                    {
                        var tupleNibbleMtAndQt = getMultiplicatorAndPacketQuantity(compressedTexture[cursorCompressed]);
                        PerformDecompressReading(tupleNibbleMtAndQt, ref compressedTexture, ref cursorCompressed, ref decompressedTexture, ref cursorDecompressed);
                    }
                }
                catch { }
            } while (cursorCompressed < compressedTexture.Length);
            return decompressedTexture;
        }

        private static void PerformDecompressReading((byte quantity, byte multiplicator) multiplicatorAndQuantity, ref byte[] compressedTexture, ref int cursorCompressed, ref byte[] decompressedTexture, ref int cursorDecompressed)
        {
            cursorCompressed += 1;
            //scilly copy same data 'quantity' times...
            for (byte mult = 0; mult < multiplicatorAndQuantity.multiplicator; mult++)
            {
                Array.Copy(compressedTexture, cursorCompressed, decompressedTexture, cursorDecompressed, multiplicatorAndQuantity.quantity);
                cursorDecompressed += multiplicatorAndQuantity.quantity;
            }

            //update cursors..
            cursorCompressed += multiplicatorAndQuantity.quantity;
        }
        private static void PerformSimpleReading(ref byte[] compressedTexture,ref int cursorCompressed,ref byte[] decompressedTexture,ref int cursorDecompressed)
        {
            int dataLength = compressedTexture[cursorCompressed]+1;
            cursorCompressed += 1;

            //paste array in the decompressed data
            Array.Copy(compressedTexture, cursorCompressed, decompressedTexture, cursorDecompressed, dataLength);

            //update the cursors
            cursorCompressed += dataLength;
            cursorDecompressed += dataLength;
        }

        public static byte[] ConvertByteArrayToRGBA(byte[] texture, CGeneric.Compression compressionType,byte[] palette = null)
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
                    arrayRGBA = ConvertMax256ColorsToRGBA(texture,palette);
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
            byte[] rgbaArray = new byte[texture.Length*2];
            for (int i = 0; i < texture.Length - 1; i += 2)
            {
                rgbaArray[i * 2] = texture[i];
                rgbaArray[i * 2 + 1] = texture[i];
                rgbaArray[i * 2 + 2] = texture[i];
                rgbaArray[i * 2 + 3] = texture[i + 1];
            }
            return rgbaArray;
        }

        private static byte[] ConvertMax256ColorsToRGBA(byte[] texture, byte[] palette)
        {
            byte[] rgbaArray = new byte[texture.Length *4];
            for (int i = 0; i < texture.Length; i++)
                Array.Copy(palette, texture[i] * 4, rgbaArray, 4 * i, 4);

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
    }
}
