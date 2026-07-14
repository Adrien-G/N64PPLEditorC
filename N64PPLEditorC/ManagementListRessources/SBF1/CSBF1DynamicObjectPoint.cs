using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CSBF1DynamicObjectPoint
    {
        ushort Column;
        ushort Row;
        ushort ScreenX;
        ushort ScreenY;
        public CSBF1DynamicObjectPoint(byte[] rawData)
        {
            this.Column  = CGeneric.ReadUInt16BigEndian(rawData, 0);
            this.Row     = CGeneric.ReadUInt16BigEndian(rawData, 2);
            this.ScreenX = CGeneric.ReadUInt16BigEndian(rawData, 4);
            this.ScreenY = CGeneric.ReadUInt16BigEndian(rawData, 6);
        }
    }
}
