using System.Collections.Generic;
using System.Drawing;

namespace N64PPLEditorC
{
    public class CBFF2Palette
    {
        public uint ColorCount { get; set; }
        public List<Color> PixelColor { get; set; }

        public CBFF2Palette(byte[] rawData,ref int globalIndex)
        {
            this.ColorCount = (uint)CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex, 4));
            globalIndex += 4;
            PixelColor = new List<Color>();
            for(int i = 0; i < this.ColorCount; i++)
            {
                var palettei = CGeneric.GiveMeArray(rawData, globalIndex, 4);
                globalIndex += 4;
                PixelColor.Add(Color.FromArgb(palettei[3], palettei[0], palettei[1], palettei[2]));
            }
        }
    }
}