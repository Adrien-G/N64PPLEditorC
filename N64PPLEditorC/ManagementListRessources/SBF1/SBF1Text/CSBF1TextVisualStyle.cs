using System.Collections.Generic;
using System.Drawing;

namespace N64PPLEditorC
{
    public class CSBF1TextVisualStyle
    {
        public enum FontMode
        {
            Big=0,
            Normal=1,
            Small=2,
        }
        public FontMode FontSize { get; set; }
        public List<Color> PrimaryColor { get; set; }
        public List<Color> SecondaryColor { get; set; }
        public int RenderLayer { get;set; }

        public CSBF1TextVisualStyle()
        {
            
        }

        public CSBF1TextVisualStyle(ref int index,int flags, byte[] rawData)
        {
            if ((flags & 0x02000000) != 0)
            {
                FontSize = FontMode.Small;
            }
            else if ((flags & 0x00080000) != 0)
            {
                FontSize = FontMode.Normal;
            }
            else
                FontSize = FontMode.Big;

            int nbColors = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
            index += 4;

            PrimaryColor = new List<Color>();
            SecondaryColor = new List<Color>();
            

            //primary
            for (int i = 0; i < nbColors; i++)
            {
                PrimaryColor.Add(Color.FromArgb(rawData[index + 3], rawData[index], rawData[index + 1], rawData[index + 2]));
                index += 4;
            }

            //secondary
            for (int i = 0; i < nbColors; i++)
            {
                SecondaryColor.Add(Color.FromArgb(rawData[index + 3], rawData[index], rawData[index + 1], rawData[index + 2]));
                index += 4;
            }
        }
    }
}