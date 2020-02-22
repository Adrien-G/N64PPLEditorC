using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CTextureDecompress
    {

        private Byte[] compressedTexture;
        private Byte[] decompressedTexture;
        int cursorDecompressed;
        int cursorCompressed;

        public CTextureDecompress(Byte[] compressedTexture)
        {
            this.compressedTexture = compressedTexture;
        }

        private (byte quantity ,byte multiplicator) getMultiplicatorAndPacketQuantity(byte value)
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

        public Byte[] DecompressTexture(CBFF2.BFFHeader headerBFF2)
        {
            decompressedTexture = new Byte[headerBFF2.sizeX*headerBFF2.sizeY*headerBFF2.bytePerPixel];
            compressedTexture = headerBFF2.dataCompressed;
            cursorCompressed = 0;
            cursorDecompressed = 0;
            do
            {
                //surround with try catch because few of texture exceed the size of the texture size...
                try
                {
                    if (compressedTexture[cursorCompressed] < 128)
                        PerformSimpleReading();
                    else
                    {
                        var tupleNibbleMtAndQt = getMultiplicatorAndPacketQuantity(compressedTexture[cursorCompressed]);
                        PerformDecompressReading(tupleNibbleMtAndQt);
                    }
                }
                catch { }
            } while (cursorCompressed < compressedTexture.Length);
            return decompressedTexture;
        }

        private void PerformDecompressReading((byte quantity, byte multiplicator) multiplicatorAndQuantity)
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

        private void PerformSimpleReading()
        {
            int dataLength = compressedTexture[cursorCompressed]+1;
            cursorCompressed += 1;

            //paste array in the decompressed data
            Array.Copy(compressedTexture, cursorCompressed, decompressedTexture, cursorDecompressed, dataLength);

            //update the cursors
            cursorCompressed += dataLength;
            cursorDecompressed += dataLength;
        }

        public byte[] ConvertIndexedToRGB(CBFF2.BFFHeader headerBFF2,Byte[] decompressedTex)
        {
            Byte[] newData;
            if (headerBFF2.bytePerPixel != 8)
            {
                newData = new byte[decompressedTex.Length * headerBFF2.bytePerPixel];
                for (int i = 0; i < decompressedTex.Length; i++)
                    Array.Copy(headerBFF2.palette, decompressedTex[i] * headerBFF2.bytePerPixel, newData, headerBFF2.bytePerPixel * i, headerBFF2.bytePerPixel);
            }
            else
            {
                newData = new byte[decompressedTex.Length * 8 ];
                byte tmpB1;
                byte tmpB2;
                for (int i = 0; i < decompressedTex.Length-1; i++)
                {
                    tmpB1 = decompressedTex[i];
                    tmpB1 >>= 4;

                    tmpB2 = decompressedTex[i];
                    tmpB2 <<= 4;
                    tmpB2 >>= 4;
                    Array.Copy(headerBFF2.palette, tmpB1, decompressedTex, 8 * i, 4);
                    Array.Copy(headerBFF2.palette, tmpB2, decompressedTex, 8 * i+4, 4);

                }

            }
            return newData;
        }

        public Byte[] ConvertByteArrayToRGBA(CBFF2.BFFHeader headerBFF2, byte[] arrayTexture)
        {

            if (arrayTexture.Length == headerBFF2.sizeX * headerBFF2.sizeY * 4)
                return arrayTexture;

            Byte[] arrayTextureRGBA = new Byte[headerBFF2.sizeX * headerBFF2.sizeY * 4];

            switch (headerBFF2.textureType)
            {
                case 0x22:
                    //actually unknown color repartition scheme..
                    arrayTextureRGBA = arrayTexture;
                    break;
                case 0x23:
                    //actually unknown color repartition scheme..
                    break;
                case 0x24:
                    //8 bits for color (greyscale) and 8 bit for transparency
                    for (int i = 0; i < arrayTexture.Length-1; i+=2)
                    {
                        arrayTextureRGBA[i * 2] = arrayTexture[i];
                        arrayTextureRGBA[i * 2 + 1] = arrayTexture[i];
                        arrayTextureRGBA[i * 2 + 2] = arrayTexture[i];
                        arrayTextureRGBA[i * 2 + 3] = arrayTexture[i + 1];
                    }
                    break;
                case 0x54:
                    //16 bits
                    byte red, green,green2, blue, alpha;
                    int R, G, B, A;

                    for (int i = 0; i < arrayTexture.Length-1; i+=2)
                    {
                        //red (5 bits)
                        red = arrayTexture[i];
                        red >>= 3;

                        //green (5 bits)
                        green = arrayTexture[i];
                        green <<= 5;
                        green >>= 3;
                        green2 = arrayTexture[i+1];
                        green2 >>= 6;
                        green += green2;
                        
                        //blue (5 bits)
                        blue = arrayTexture[i+ 1];
                        blue <<= 2;
                        blue >>= 3;
                        
                        //alpha 1 bit
                        alpha = arrayTexture[i + 1];
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

                        arrayTextureRGBA[i * 2] = (byte)R;
                        arrayTextureRGBA[i * 2 + 1] = (byte)G;
                        arrayTextureRGBA[i * 2 + 2] = (byte)B;
                        arrayTextureRGBA[i * 2 + 3] = (byte)A;

                    }
                    break;
            }

            return arrayTextureRGBA;
        }
    }
}
