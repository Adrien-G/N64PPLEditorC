using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CSBF1TextureManagement
    {
        private byte[] rawData;
        public bool isCompressedTexture;
        private byte textureIndex;
        public int posX;
        public int posY;


        public CSBF1TextureManagement(byte[] rawData)
        {
            this.rawData = rawData;
            decomposeHeader();
        }

        public CSBF1TextureManagement(int id, int index)
        {
            byte[] newTexture = new byte[28];
            Array.Copy(CGeneric.ConvertIntToByteArray(id), 0, newTexture, 16,4);
            Array.Copy(CGeneric.ConvertIntToByteArray(-1), 0, newTexture, 20, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(index), 0, newTexture, 24, 4);
            
            rawData = newTexture;
            decomposeHeader();
        }

        private void decomposeHeader()
        {
            if(rawData[3] != 8)
                this.isCompressedTexture = true;
            this.textureIndex = rawData[27];
            this.posX = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 4, 4));
            this.posY = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 8, 4));
        }

        public int getTextureIndex()
        {
            return (int)textureIndex;
        }

        public int GetSize()
        {
            return rawData.Length;
        }

        public byte[] GetRawData()
        {
            Array.Copy(CGeneric.ConvertIntToByteArray(posX), 0, rawData, 4, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(posY), 0, rawData, 8, 4);
            return this.rawData;
        }

        public static int GetHeaderLength(int headerValue)
        {
            switch (headerValue)
            {
                case 0x00000000: return 28;
                case 0x00000002: return 28; //tested
                case 0x00000004: return 28; //tested
                case 0x00000006: return 28;
                case 0x00000008: return 28; //tested
                case 0x0000000C: return 28;
                case 0x00000040: return 36;
                case 0x00000044: return 36; //tested
                case 0x00000080: return 28; //tested
                case 0x00000082: return 28;
                case 0x00000084: return 28; //tested
                case 0x00000200: return 28;
                case 0x00000400: return 32;
                case 0x00000402: return 32;
                case 0x00000404: return 32;
                case 0x00000406: return 32; //tested
                case 0x00000482: return 32;
                case 0x00002000: return 28;
                case 0x00002006: return 28; //tested
                case 0x00002040: return 36;
                case 0x00002406: return 32; //tested
            }
            throw new NotImplementedException();
        }
    }
}
