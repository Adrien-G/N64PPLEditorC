using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{

    class CSBF1TextObject
    {
        private int nbItems;

        public CSBF1TextObject(byte[] rawData)
        {

        }

        public static int GetHeaderLength(int headerValue,int indexDebug)
        {
            switch (headerValue)
            {
                case 0x0008C403: return 52; // tested
                case 0x20080006: return 36;
                case 0x20880442: return 36;
                case 0x20000003: return 44;
                case 0x20080003: return 44;
                case 0x20880403: return 44;
                case 0x20000403: return 44;
                case 0x20080403: return 44;
                case 0x20080443: return 44;
                case 0x2000C013: return 52; //tested
                case 0x20080C03: return 52; //tested
                case 0x20804003: return 52;
                case 0x20884003: return 52;
                case 0x20804403: return 52;
                case 0x20884403: return 52;
                case 0x2000C003: return 52;
                case 0x2008C003: return 52;
                case 0x2000C403: return 52;
                case 0x2008C403: return 52;
                case 0x2008C443: return 52; //tested *48 ?
                case 0x20884043: return 52;
                case 0x2000C443: return 52; //tested
                case 0x20084C43: return 60;
                case 0x21884003: return 52;
                case 0x21884007: return 52;
                case 0x22000003: return 44; //tested
                case 0x22101443: return 44;
                case 0x22804003: return 52;
                case 0x22804403: return 52;
                case 0x2208D403: return 52;
                case 0x22804443: return 52;
                case 0x2200C443: return 52;
                case 0x23000403: return 44;
                case 0x33804407: return 52;
                case 0x62804403: return 60; //tested
                default: throw new NotSupportedException();
            }
        }

    }
}
