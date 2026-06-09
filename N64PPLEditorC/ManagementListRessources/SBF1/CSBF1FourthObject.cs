using System;
using System.Collections.Generic;

namespace N64PPLEditorC
{
    public class CSBF1FourthObject
    {
        private byte[] rawData;

        public CSBF1FourthObject(byte[] rawData)
        {
            this.rawData = rawData;
        }

        internal static int GetHeaderLength(int headerValue)
        {
            var result = 20;

            if (CGeneric.GetBitStateFromInt(headerValue, 27))
                result += 4;

            if (CGeneric.GetBitStateFromInt(headerValue, 28))
                result += 4;

            if (CGeneric.GetBitStateFromInt(headerValue, 30))
                result += 8;

            if (CGeneric.GetBitStateFromInt(headerValue, 32))
                result += 4;


            return result;
            ////add 4 because 0x00000000 at the end of each scene..
            ////add specific value for ISO file (the 4th hidden thing in sbf...
            //var hidden4thData = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(data, generalIndex, 4));
            //generalIndex += 4;

            //byte[] fourthData = new byte[4];
            //if (hidden4thData != 0)
            //{
            //    for (int i = 0; i < hidden4thData; i++)
            //    {
            //        var code4thData = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(data, generalIndex, 4));
            //        switch (code4thData)
            //        {
            //            case 0x1E:
            //            case 0x9E:
            //                fourthData = CGeneric.GiveMeArray(data, generalIndex, 0x20);
            //                generalIndex += 0x20;
            //                break;
            //            case 0x1F:
            //                fourthData = CGeneric.GiveMeArray(data, generalIndex, 0x24);
            //                generalIndex += 0x24;
            //                break;
            //            default:
            //                throw new NotImplementedException();
            //        }

            //    }
            //}
        }

        public byte[] GetRawData()
        {
            return rawData;
        }
    }
}