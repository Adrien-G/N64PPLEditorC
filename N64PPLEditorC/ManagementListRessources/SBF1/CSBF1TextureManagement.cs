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
        public byte transparency;

        //flags
        public bool bit19;
        public bool bit20;
        public bool bit21;
        public bool bit23;
        public bool bit24;
        public bool bit25;
        public bool bit27;
        public bool bit28;
        public bool bit29;
        public bool bit30;
        public bool bit31;
        public bool bit32;
        public bool transparencybit;
        public bool extra2;


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
            //data grabbed from 4 first bytes (converted to bits)
            int dataInitial = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 0, 4));

            //set flags
            transparencybit = CGeneric.GetBitStateFromInt(dataInitial, 22);
            extra2 = CGeneric.GetBitStateFromInt(dataInitial, 26);

            //set unknown flags
            bit19 = CGeneric.GetBitStateFromInt(dataInitial, 19);
            bit20 = CGeneric.GetBitStateFromInt(dataInitial, 20);
            bit21 = CGeneric.GetBitStateFromInt(dataInitial, 21);
            bit23 = CGeneric.GetBitStateFromInt(dataInitial, 23);
            bit24 = CGeneric.GetBitStateFromInt(dataInitial, 24);
            bit25 = CGeneric.GetBitStateFromInt(dataInitial, 25);
            bit27 = CGeneric.GetBitStateFromInt(dataInitial, 27);
            bit28 = CGeneric.GetBitStateFromInt(dataInitial, 28);
            bit29 = CGeneric.GetBitStateFromInt(dataInitial, 29);
            bit30 = CGeneric.GetBitStateFromInt(dataInitial, 30);
            bit31 = CGeneric.GetBitStateFromInt(dataInitial, 31);
            bit32 = CGeneric.GetBitStateFromInt(dataInitial, 32);

            if (transparencybit)
                this.transparency = rawData[31];

            if (rawData[3] != 8)
                this.isCompressedTexture = true;
            this.textureIndex = rawData[27];
            this.posX = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 4, 4));
            this.posY = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 8, 4));
        }

        public int getTextureIndex()
        {
            return (int)textureIndex;
        }

        public void SetTransparencyValue(bool activated,byte value)
        {
            transparencybit = activated; 
            if (activated)
                this.transparency = value;
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
            int newHeaderDataSize = 28;
            if (this.transparencybit)
                newHeaderDataSize += 4;
            if (this.extra2)
                newHeaderDataSize += 8;

            byte[] newHeaderData = new byte[newHeaderDataSize];

            int flags = 0;

            //set extra bit value
            CGeneric.SetBitInInt(ref flags, 22, this.transparencybit);
            CGeneric.SetBitInInt(ref flags, 26, this.extra2);

            //set unknown bit values
            CGeneric.SetBitInInt(ref flags, 19, this.bit19);
            CGeneric.SetBitInInt(ref flags, 20, this.bit20);
            CGeneric.SetBitInInt(ref flags, 21, this.bit21);
            CGeneric.SetBitInInt(ref flags, 23, this.bit23);
            CGeneric.SetBitInInt(ref flags, 24, this.bit24);
            CGeneric.SetBitInInt(ref flags, 25, this.bit25);
            CGeneric.SetBitInInt(ref flags, 27, this.bit27);
            CGeneric.SetBitInInt(ref flags, 28, this.bit28);
            CGeneric.SetBitInInt(ref flags, 29, this.bit29);
            CGeneric.SetBitInInt(ref flags, 30, this.bit30);
            CGeneric.SetBitInInt(ref flags, 31, this.bit31);
            CGeneric.SetBitInInt(ref flags, 32, this.bit32);

            Array.Copy(CGeneric.ConvertIntToByteArray(flags), 0, newHeaderData, 0, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(posX), 0, newHeaderData, 4, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(posY), 0, newHeaderData, 8, 4);

            //copy unknown data
            Array.Copy(rawData,12, newHeaderData, 12,16);

            newHeaderData[27] = this.textureIndex ;
            if (transparencybit)
                newHeaderData[31] = transparency;
            return newHeaderData;
        }

        public static int GetHeaderLength(int headerValue)
        {
            var result = 28;

            if (CGeneric.GetBitStateFromInt(headerValue, 22))
                result += 4;
            if (CGeneric.GetBitStateFromInt(headerValue, 26))
                result += 8;

            return result;

            //unknown data for now
            //switch (headerValue)
            //{
            //    case 0x00000000: return 0x1C;
            //    case 0x00000002: return 0x1C; //tested
            //    case 0x00000004: return 0x1C; //tested
            //    case 0x00000006: return 0x1C;
            //    case 0x00000008: return 0x1C; //tested
            //    case 0x0000000C: return 0x1C;
            //    case 0x00000040: return 0x24; //tested
            //    case 0x00000044: return 0x24; //tested
            //    case 0x00000080: return 0x1C; //tested
            //    case 0x00000082: return 0x1C;
            //    case 0x00000084: return 0x1C; //tested
            //    case 0x00000200: return 0x1C;
            //    case 0x00000400: return 0x20; //tested
            //    case 0x00000402: return 0x20;
            //    case 0x00000404: return 0x20;
            //    case 0x00000406: return 0x20; //tested
            //    case 0x00000482: return 0x20;
            //    case 0x00002000: return 0x1C;
            //    case 0x00002006: return 0x1C; //tested
            //    case 0x00002040: return 0x24;
            //    case 0x00002406: return 0x20; //tested

            //    //test cas ISO only
            //    case 0x00000664: return 0x28;//old vavlue 1C
            //    case 0x00000764: return 0x28;
            //    case 0x00000C02: return 0x20;
            //    //case 0x000000D2: return 0x1C; //not exists
            //    //case 0x000000D3: return 0x1C; //not exists
            //    case 0x00000091: return 0x1C;
            //    case 0x0000008C: return 0x1C;
            //    case 0x00000010: return 0x1C;
            //    case 0x00000210: return 0x1C;
            //    case 0x00000204: return 0x1C;
            //    case 0x00000104: return 0x1C;

            //    case 0x00001000: return 0x1C;
            //    case 0x00000001: return 0x1C;//tested
            //    case 0x00000502: return 0x20;
            //    case 0x00000202: return 0x1C;
            //    //case 0x00000020: return 0x18;//not exist
            //    default: throw new NotImplementedException();
            //}
        }
    }
}
