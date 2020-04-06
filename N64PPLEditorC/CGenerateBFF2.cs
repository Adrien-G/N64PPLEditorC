using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CGenerateBFF2
    {

        public CGenerateBFF2()
        {

        }

        public void Init()
        {

        }

        public byte[] GenerateBFF2(byte textureType)
        {
            //grab information part..
            byte greenAlphaColor = 0;
            byte[] palette = null;

            if (textureType == 0x32 || textureType == 0x33)
            {
                palette = SetPalette();
                greenAlphaColor = GetGreenAlphaColorIndex();
            }

            //Byte[] headerBFF2 = SetHeader();

            // compression part..
            //CTextureCompress compress = new CTextureCompress();
            //byte[] dataBFF2 = compress.MakeCompression();


            

            //writing information part
            if (palette != null)
            {

            }

            return new byte[0];

        }

        private byte[] SetPalette()
        {
            //set length, set data.
            return new byte[0];
        }

        private byte GetGreenAlphaColorIndex()
        {
            return new byte();
        }

        //specific format for BFF header
        private byte[] SetHeader(byte displayTime, byte textureType, byte compressedValue, byte[] displayedWidth, byte[] pixelWidth, byte[] displayHeight, byte[] instructLength, byte[] bffName)
        {
            //size = 35 + bffName + 1 if bffName size is not pair
            byte[] headerBFF2 = new Byte[35+bffName.Length + bffName.Length %2];

            headerBFF2[9] = displayTime;

            //write "BFF2"
            for (int i = 0; i < CGeneric.patternBFF2.Length; i++)
                headerBFF2[13+i] = CGeneric.patternBFF2[i];

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

    }
}
