using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CSBF1TextureManagement
    {
        public CSBF1TextureManagement(byte[] rawData)
        {

        }

        public static int GetHeaderLength(int headerValue,int indexDebug)
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
