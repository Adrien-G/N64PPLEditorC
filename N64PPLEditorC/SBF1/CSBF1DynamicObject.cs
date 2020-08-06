using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{



    class CSBF1DynamicObject
    {
        public CSBF1DynamicObject(byte[] rawData)
        {

        }

        public static int GetHeaderLength(int headerValue)
        {
            switch (headerValue)
            {
                case 0x00024051: return 48;
                case 0x00C01461: return 48;
                case 0x03000061: return 48;
                case 0x03000062: return 48;
                case 0x00000071: return 52;
                case 0x000000E2: return 52;
                case 0x00001471: return 52;
                case 0x000014E1: return 52;
                case 0x000200D1: return 52;
                case 0x00020471: return 52;
                case 0x00020472: return 52;
                case 0x00024071: return 52;
                case 0x000240D1: return 52;
                case 0x00201471: return 52;
                case 0x00280771: return 52;
                case 0x00280772: return 52;
                case 0x00C000E2: return 52;
                case 0x00C01471: return 52;
                case 0x030000E1: return 52;
                case 0x03C000E1: return 52;
                case 0x000001F1: return 56;
                case 0x000104E1: return 68;
                case 0x00C100E1: return 68;
                case 0x030100E1: return 68;
                case 0x030104E1: return 76; //tested
                default: throw new NotSupportedException();
            }
        }
    }
}
