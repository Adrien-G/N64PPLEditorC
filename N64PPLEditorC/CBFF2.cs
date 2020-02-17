using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{ 
    class CBFF2
    {
        private Byte[] rawData;
        private BFFHeader headerBFF2;

        //create the BFF2 struct, represent information of the BFF header and also calculated fields.
        public struct BFFHeader
        {
            public int sizeX, sizeY;
            public byte bytePerPixel;
            public byte textureType;
            public byte transparencyPixelIndex;
            public byte nameLength;
            public byte[] name;
            public byte[] textureWidth;
            public byte[] textureHeight;
            public bool isCompressedTexture;
        }

        public CBFF2(Byte[] rawData)
        {
            this.rawData = rawData;
            this.headerBFF2 = new BFFHeader();
        }

        public void Init()
        {
            //init header...

            //grab transparency index (when indexed color)
            // take 2 byte : for the fist byte keep only 4 last bit and for the second keep only the 4 first..
            byte b1 = rawData[17];
            byte b2 = rawData[18];
            b1 %= 16;
            b1 <<= 4;
            b2 /= 16;
            b1 += b2;
            headerBFF2.transparencyPixelIndex = b1;

            // check if the data is compressed
            headerBFF2.isCompressedTexture = (b2 > 7) ? true : false;

            //copy basic informations..
            headerBFF2.textureType = rawData[19];
            headerBFF2.nameLength = rawData[35];

            headerBFF2.textureWidth = new byte[2];
            headerBFF2.textureHeight = new byte[2];
            headerBFF2.name = new byte[headerBFF2.nameLength];
            Array.Copy(rawData,22, headerBFF2.textureWidth, 0,2);
            Array.Copy(rawData, 26, headerBFF2.textureHeight, 0, 2);
            Array.Copy(rawData,36, headerBFF2.name, 0, headerBFF2.nameLength);

            headerBFF2.sizeX = CGeneric.ConvertByteArrayToInt(headerBFF2.textureWidth);
            headerBFF2.sizeY = CGeneric.ConvertByteArrayToInt(headerBFF2.textureHeight);
        }

        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString(headerBFF2.name);
        }
    }
}
