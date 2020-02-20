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
                if (compressedTexture[cursorCompressed] < 128)
                    PerformSimpleReading();
                else
                {
                    var tupleNibbleMtAndQt = getMultiplicatorAndPacketQuantity(compressedTexture[cursorCompressed]);
                    PerformDecompressReading(tupleNibbleMtAndQt);
                }
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
            Byte[] newData = new byte[decompressedTex.Length * headerBFF2.bytePerPixel];

            for (int i = 0; i < decompressedTex.Length; i++)
                Array.Copy(headerBFF2.palette, decompressedTex[i],newData, headerBFF2.bytePerPixel*i,headerBFF2.bytePerPixel);
            return newData;
        }
    }
}
