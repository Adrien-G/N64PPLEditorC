using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{



    class CSBF1DynamicObject
    {
        private byte[] rawData;
        public CSBF1DynamicObject(byte[] rawData)
        {
            this.rawData = rawData;
        }

        public static int GetHeaderLength(int headerValue)
        {
            switch (headerValue)
            {
                case 0x00024051: return 0x30;
                case 0x00C01461: return 0x30;
                case 0x03000061: return 0x30;
                case 0x03000062: return 0x30;
                case 0x00000071: return 0x34;
                case 0x000000E2: return 0x34;
                case 0x00001471: return 0x34;
                case 0x000014E1: return 0x34;
                case 0x000200D1: return 0x34;
                case 0x00020471: return 0x34;
                case 0x00020472: return 0x34;
                case 0x00024071: return 0x34;
                case 0x000240D1: return 0x34;
                case 0x00201471: return 0x34;
                case 0x00280771: return 0x34;
                case 0x00280772: return 0x34;
                case 0x00C000E2: return 0x34;
                case 0x00C01471: return 0x34;
                case 0x030000E1: return 0x34;
                case 0x03C000E1: return 0x34;
                case 0x000001F1: return 0x38;
                case 0x000104E1: return 0x44;
                case 0x00C100E1: return 0x44;
                case 0x030100E1: return 0x44;
                case 0x030104E1: return 0x4C; //tested

                      //value added for ISO game
                case 0x000204F1: return 0x38;
                case 0x000260D1: return 0x38;
                case 0x00000461: return 0x30;
                case 0x000205F1: return 0x38;
                case 0x000205F2: return 0x38;
                case 0x00000051: return 0x30;
                case 0x000003F1: return 0x38;
                case 0x00000151: return 0x30;
                case 0x002807F1: return 0x38;
                case 0x002807E1: return 0x34;
                case 0x002807F2: return 0x38;
                case 0x002807E2: return 0x34;
                case 0x30D080E1: return 0x34;
                case -204439455: return 0x30;
                case 0x01800061: return 0x30;
                case 0x30D080E2: return 0x34;
                case -1022328734:return 0x30;
                case 0x001080e2: return 0x34;
                case 0x042807f0: return 0x38;
                case 0x042807e0: return 0x34;
                case 0x082807f0: return 0x38;
                case 0x082807e0: return 0x34;
                case 0x30d08061: return 0x30;
                case -1022328607:return 0x34;
                case 0x30d08062: return 0x30;
                case -1022328606: return 0x34;
                case 0x34d08060: return 0x30;
                case -955219744: return 0x34;
                case 0x38d08060: return 0x30;
                case -888110880: return 0x34;
                case 0x30c09471: return 0x34;
                case -1023372191: return 0x30;
                case 0x00000471: return 0x34;
                case 0x003083e1: return 0x34;
                case 0x002007f1: return 0x38;
                case 0x002107F1: return 0x48; //mess
                case 0x001285f1: return 0x38;
                case 0x000284f1: return 0x38;
                case 0x000900F1: return 0x48;
                case 0x30c08461: return 0x30;
                case 0x30c080e1: return 0x34;
                case -205487007: return 0x30;
                case -1023376287: return 0x30;

                default: throw new NotSupportedException();
            }
        }

        public int GetSize()
        {
           return rawData.Length;
        }

        public byte[] GetRawData()
        {

            int flags = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData,0,4));

            //set unknown bit values
            //CGeneric.SetBitInInt(ref flags, 1, true);

            //Array.Copy(CGeneric.ConvertIntToByteArray(flags), 0, rawData, 0, 4);

           

            return rawData;
        }
    }
}
