using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            public byte[] palette;
            public byte[] paletteSize;
            public byte[] dataCompressed;
            public byte[] dataUncompressed;
            public bool isCompressedTexture;
            public bool isIndexedColor;
        }
        
        public CBFF2(Byte[] rawData)
        {
            this.rawData = rawData;
            this.headerBFF2 = new BFFHeader();
        }

        public void Init()
        {
            //init header...

            // take 2 byte : for the fist byte keep only 4 last bit and for the second keep only the 4 first..
            byte b1 = rawData[17];
            byte b2 = rawData[18];
            b1 %= 16;
            b1 <<= 4;
            b2 /= 16;
            b1 += b2;
            headerBFF2.transparencyPixelIndex = b1;

            // check if the data is compressed and if data had indexed color
            headerBFF2.isCompressedTexture = (rawData[18] % 16 > 7) ? true : false;

            //copy basic informations..
            headerBFF2.textureType = rawData[19];
            headerBFF2.nameLength = rawData[35];

            headerBFF2.isIndexedColor = (headerBFF2.textureType == 51 || headerBFF2.textureType == 50) ? true : false;
            headerBFF2.textureWidth = new byte[2];
            headerBFF2.textureHeight = new byte[2];
            headerBFF2.name = new byte[headerBFF2.nameLength];
            
            switch (headerBFF2.textureType)
            {
                case 0x22: headerBFF2.bytePerPixel = 4; break;
                case 0x23: headerBFF2.bytePerPixel = 1; break;
                case 0x24: headerBFF2.bytePerPixel = 2; break;
                case 0x32: headerBFF2.bytePerPixel = 4; break;
                case 0x33: headerBFF2.bytePerPixel = 4; break;
                case 0x54: headerBFF2.bytePerPixel = 2; break;
                case 0x55: headerBFF2.bytePerPixel = 4; break;
            }

            Array.Copy(rawData, 22, headerBFF2.textureWidth, 0, 2);
            Array.Copy(rawData, 26, headerBFF2.textureHeight, 0, 2);
            Array.Copy(rawData, 36, headerBFF2.name, 0, headerBFF2.nameLength);

            //remove last not necessary character
            if (headerBFF2.name[headerBFF2.nameLength-1] == 0)
            {
                headerBFF2.name = new byte[headerBFF2.nameLength-1];
                Array.Copy(rawData, 36, headerBFF2.name, 0, headerBFF2.nameLength-1);

            }

                headerBFF2.sizeX = CGeneric.ConvertByteArrayToInt(headerBFF2.textureWidth);
            headerBFF2.sizeY = CGeneric.ConvertByteArrayToInt(headerBFF2.textureHeight);

            //extract palette
            int startingData = 0x24 + headerBFF2.nameLength;

            if (headerBFF2.isIndexedColor) { 
                ExtractPalette(startingData);
                startingData +=  headerBFF2.palette.Length + headerBFF2.paletteSize.Length;
            }

            headerBFF2.dataCompressed = new Byte[rawData.Length - startingData];
            Array.Copy(rawData, startingData, headerBFF2.dataCompressed, 0, headerBFF2.dataCompressed.Length); 
        }

        private void ExtractPalette(int indexPalette)
        {
            //get palette size
            headerBFF2.paletteSize = new Byte[4];
            Array.Copy(rawData, indexPalette, headerBFF2.paletteSize, 0, headerBFF2.paletteSize.Length);

            //fill palette data
            int paletteSize1 = CGeneric.ConvertByteArrayToInt(headerBFF2.paletteSize);
            headerBFF2.palette = new Byte[paletteSize1*headerBFF2.bytePerPixel];
            Array.Copy(rawData, indexPalette + headerBFF2.paletteSize.Length, headerBFF2.palette, 0, headerBFF2.palette.Length);
        }

        //TODO
        public static byte[] GenerateBFF2Packet(CGeneric.Compression compressionMethod,byte[] dataBFF2)
        {
            //needed for bffheader
            byte greenAlphaIndex = 0;

            //if indexedColor generate palette first (need greenAlpha index for header)
            if (compressionMethod == CGeneric.Compression.max16Colors || compressionMethod == CGeneric.Compression.max256Colors)
            {
                //generate palette

                //extract green alpha index

                
            }

            //generate header

            //generate palette (when texture is indexed)
            if (compressionMethod == CGeneric.Compression.max16Colors || compressionMethod == CGeneric.Compression.max256Colors)
            {

            }

            //generate data

            // compress the data
            byte[] compressedData = CTextureCompress.MakeCompression(dataBFF2);





            //palette = SetPalette();
            // greenAlphaColor = GetGreenAlphaColorIndex();

            //Byte[] headerBFF2 = SetHeader();

            // compression part..
            //CTextureCompress compress = new CTextureCompress();
            //byte[] dataBFF2 = compress.MakeCompression();

            return new byte[0];
        }

        //specific format for BFF header
        private byte[] SetHeader(byte displayTime, byte textureType, byte compressedValue, byte[] displayedWidth, byte[] pixelWidth, byte[] displayHeight, byte[] instructLength, byte[] bffName)
        {
            //size = 35 + bffName + 1 if bffName size is not pair
            byte[] headerBFF2 = new Byte[35 + bffName.Length + bffName.Length % 2];

            headerBFF2[9] = displayTime;

            //write "BFF2"
            for (int i = 0; i < CGeneric.patternBFF2.Length; i++)
                headerBFF2[13 + i] = CGeneric.patternBFF2[i];

            //decompose the next two bytes in nibbles
            //grab the nibbles of alpha color
            List<byte> alphaColor = CGeneric.ByteToNibble(GetGreenAlphaColorIndex());

            //write two nibbles for the both bytes..
            headerBFF2[18] = CGeneric.NibbleToByte(2, alphaColor[0]);
            headerBFF2[19] = CGeneric.NibbleToByte(alphaColor[1], compressedValue);

            //write texture type
            headerBFF2[20] = textureType;

            //write displayed width
            for (int i = 0; i < displayedWidth.Length; i++)
                headerBFF2[21 + i] = displayedWidth[i];

            //write pixel width
            for (int i = 0; i < pixelWidth.Length; i++)
                headerBFF2[23 + i] = pixelWidth[i];

            //write display height
            for (int i = 0; i < displayHeight.Length; i++)
                headerBFF2[27 + i] = displayHeight[i];

            //write instruction length
            for (int i = 0; i < instructLength.Length; i++)
                headerBFF2[29 + i] = instructLength[i];

            //length of file name 
            if (bffName.Length % 2 == 0)
                headerBFF2[34] = (byte)bffName.Length;
            else
                headerBFF2[34] = (byte)(bffName.Length + 1);

            //write bff name
            for (int i = 0; i < bffName.Length; i++)
                headerBFF2[35 + i] = bffName[i];

            return headerBFF2;
        }






        public int GetSize()
        {
            return rawData.Length;
        }

        public Byte[] GetRawData()
        {
            return rawData;
        }

        public void DecompressTexture()
        {
            Byte[] compressedTex;
            compressedTex = new Byte[rawData.Length - 35 + headerBFF2.nameLength];

            Byte[] decompressedTex;

            if (headerBFF2.isCompressedTexture)
                decompressedTex = CTextureDecompress.DecompressTexture(headerBFF2);
            else
                decompressedTex = compressedTex;

            if (headerBFF2.isIndexedColor)
                decompressedTex = CTextureDecompress.ConvertIndexedToRGB(headerBFF2, decompressedTex);
            else
                decompressedTex = CTextureDecompress.ConvertByteArrayToRGBA(headerBFF2, decompressedTex);

            headerBFF2.dataUncompressed = decompressedTex;

        }

        public Bitmap GetBmpTexture(){
            return CTextureManager.ConvertByteArrayToBitmap(headerBFF2.dataUncompressed, headerBFF2.sizeX, headerBFF2.sizeY);
        }
      
        public string GetName()
        {
            return System.Text.Encoding.UTF8.GetString(headerBFF2.name);
        }
    }
}
