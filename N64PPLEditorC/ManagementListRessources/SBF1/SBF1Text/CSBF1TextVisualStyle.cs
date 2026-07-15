using System.Collections.Generic;
using System.Drawing;

namespace N64PPLEditorC
{
    public class CSBF1TextVisualStyle
    {
        public List<Color> PrimaryColor { get; set; }
        public List<Color> SecondaryColor { get; set; }


        public CSBF1TextVisualStyle(ref int index,CSBF1TextFlags flags, byte[] rawData)
        {
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