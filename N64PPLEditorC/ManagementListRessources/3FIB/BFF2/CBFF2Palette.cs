using System.Collections.Generic;
using System.Drawing;

namespace N64PPLEditorC
{
    public class CBFF2Palette
    {

        public uint ColorCount { get; set; }
        public byte[] RawData { get; set; }

        public CBFF2Palette(byte[] palette)
        {
            RawData = palette;
            ColorCount = (uint)(palette.Length / 4);
        }
        public CBFF2Palette(byte[] rawData,ref int globalIndex)
        {
           
            this.ColorCount = (uint)CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex, 4));
            globalIndex += 4;
            var indexBeforeRead = globalIndex;
            globalIndex += 4 * (int)this.ColorCount;
            RawData = CGeneric.GiveMeArray(rawData, indexBeforeRead, globalIndex - indexBeforeRead);
        }
    }
}