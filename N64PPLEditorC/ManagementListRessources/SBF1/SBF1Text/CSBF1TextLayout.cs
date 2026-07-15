using System;
using System.Collections.Generic;
using System.Drawing;

namespace N64PPLEditorC
{
    /// <summary>
    /// classe permettant de déterminer entre autre si la police doit être a chasse fixe ou non
    /// </summary>
    public class CSBF1TextLayout
    {
        //permet de déterminer si nous sommes en chasse fixe
        public int FixedGlyphAdvance { get; set; }

        //espacement Horizontal utilisé lors du passage a la ligne suivante
        //en fonction du flag il remplace ou complète la hauteur normale de la police
        public int LineAdvance { get; set; }

        //définit la largeur disponible pour construire les lignes.
        public int WrapWidth { get; set; }

        //définit la hauteur de la zone dans laquelle les lignes doivent être disposées.
        public int LayoutHeight { get; set; }
        public List<int> XPositions {get;set;}

        public CSBF1TextLayout()
        {
            XPositions = new List<int>();
        }
        public CSBF1TextLayout(ref int index, CSBF1TextFlags flags, byte[] rawData) : this()
        {
            if (flags.IsFixedGlyphAdvance || flags.IsAdditionalGlyphAdvance)
            {
                FixedGlyphAdvance = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
                LineAdvance = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index + 4, 4));

                index += 8;
            }
            if (flags.IsBoundedLayout)
            {
                WrapWidth = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
                LayoutHeight = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index + 4, 4));
                index += 8;
            }

        }

        public void AddXPositions(ref int index, byte[] rawData)
        {
            int xPositionCount = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
            index += 4;

            if (xPositionCount > 0)
            {
                for (int i = 0; i < xPositionCount; i++)
                {
                    XPositions.Add(CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4)));
                    index += 4;
                }
            }
        }
    }
}