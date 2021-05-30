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
        public byte textureIndex;
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

        public void SetTextureIndex(int textureIndex)
        {
           this.textureIndex = (byte)textureIndex;
        }

        public int GetSize()
        {
            return rawData.Length;
        }

        public byte[] GetRawData()
        {
            Array.Copy(CGeneric.ConvertIntToByteArray(posX), 0, rawData, 4, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(posY), 0, rawData, 8, 4);
            rawData[27] = this.textureIndex ;
            return this.rawData;
        }

        public static int GetHeaderLength(int headerValue)
        {
            switch (headerValue)
            {
                case 0x00000000: return 0x1C;
                case 0x00000002: return 0x1C; //tested
                case 0x00000004: return 0x1C; //tested
                case 0x00000006: return 0x1C;
                case 0x00000008: return 0x1C; //tested
                case 0x0000000C: return 0x1C;
                case 0x00000040: return 0x24;
                case 0x00000044: return 0x24; //tested
                case 0x00000080: return 0x1C; //tested
                case 0x00000082: return 0x1C;
                case 0x00000084: return 0x1C; //tested
                case 0x00000200: return 0x1C;
                case 0x00000400: return 0x20; //tested
                case 0x00000402: return 0x20;
                case 0x00000404: return 0x20;
                case 0x00000406: return 0x20; //tested
                case 0x00000482: return 0x20;
                case 0x00002000: return 0x1C;
                case 0x00002006: return 0x1C; //tested
                case 0x00002040: return 0x24;
                case 0x00002406: return 0x20; //tested

                //test cas ISO only
                case 0x00000664: return 0x1C;
                case 0x000000D2: return 0x1C;
                case 0x000000D3: return 0x1C;
                case 0x00000091: return 0x1C;
                case 0x0000008C: return 0x1C;
                case 0x00000010: return 0x1C;
                case 0x00000210: return 0x1C;
                case 0x00000204: return 0x1C;
                case 0x00000104: return 0x1C;
                
                case 0x00001000: return 0x1C;
                case 0x00000001: return 0x1C;
                case 0x00000502: return 0x20;
                case 0x00000202: return 0x1C;
                case 0x00000020: return 0x18;

            }
            throw new NotImplementedException();
        }
    }
}
