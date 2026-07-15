using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace N64PPLEditorC
{
    public class CSBF1TextFlags
    {
        // byte array des différents Flags
        public byte[] Flags { get; private set; }

        //représentations sur un int
        public int FlagsInt { get { return CGeneric.ConvertByteArrayToInt(Flags); } }
        public bool IsRenderMiddlePass { get; set; } // permet de déterminer un ordre d'affichage (Z)
        public bool IsRenderLatePass { get; set; } // permet de déterminer un ordre d'affichage (Z)

        //détermine si la petite font est utilisé
        public bool IsSmallFont { get; set; }
        //détermine si la font de taille moyenne est utilisé
        public bool IsNormalFont { get; set; }


        //détermine si le text est masqué ou non
        public bool IsTextHidden { get; set; } // OK
        public bool IsFixedGlyphAdvance { get; set; } //OK

        //détermine si le texte s'affiche caractère par caractère
        public bool IsProgressiveDisplay { get; set; }
        public bool IsProgressiveSound { get; set; }

        //permet de faire des retour a la ligne "propre" en cas de texte qui dépasserait la boite bounding box définit par WrapWidth et Layout height
        public bool IsBoundedLayout { get; set; }

        //centrage du texte en vertical
        public bool IsCenterVertically { get; set; }
        //centrage du texte en horizontal
        public bool IsCenterHorizontally { get; set; }
        public bool IsUseFixedColors { get; set; }
        
        
        
      
        public bool IsAdditionalGlyphAdvance { get; set; }

        //flags To Understand / not sure
        public bool IsRuntimeGlyphState1 { get; set; }
        public bool IsUnknown00000002 { get; set; }
        public bool IsAlternateGlyphRendering { get; set; }
        public bool IsDynamicLayoutRuntimeState { get; set; }
        public bool IsDynamicLayoutText { get; set; }
        public bool IsAlternateRevealControl { get; set; }
        public bool IsAlternateTextMode { get; set; }

        public CSBF1TextFlags(ref int index, byte[] rawData)
        {
            this.Flags = CGeneric.GiveMeArray(rawData, index, 4);
            index += 4;

            //flags déterminés
            this.IsRenderMiddlePass =          CGeneric.GetBitInMask(FlagsInt, 0x00000040);
            this.IsTextHidden =                CGeneric.GetBitInMask(FlagsInt, 0x00000400);
            this.IsFixedGlyphAdvance =         CGeneric.GetBitInMask(FlagsInt, 0x00000800);
            this.IsProgressiveDisplay =        CGeneric.GetBitInMask(FlagsInt, 0x00001000);
            this.IsRenderLatePass =            CGeneric.GetBitInMask(FlagsInt, 0x00002000);
            this.IsBoundedLayout =             CGeneric.GetBitInMask(FlagsInt, 0x00004000);
            this.IsCenterVertically =          CGeneric.GetBitInMask(FlagsInt, 0x00008000);
            this.IsUseFixedColors =            CGeneric.GetBitInMask(FlagsInt, 0x00020000);
            this.IsNormalFont =                CGeneric.GetBitInMask(FlagsInt, 0x00080000);
            this.IsProgressiveSound =          CGeneric.GetBitInMask(FlagsInt, 0x00100000);
            this.IsCenterHorizontally =        CGeneric.GetBitInMask(FlagsInt, 0x00800000);
            this.IsSmallFont =                 CGeneric.GetBitInMask(FlagsInt, 0x02000000);
            this.IsAdditionalGlyphAdvance =    CGeneric.GetBitInMask(FlagsInt, 0x40000000);

            //Flags incertains
            this.IsRuntimeGlyphState1 =        CGeneric.GetBitInMask(FlagsInt, 0x00000001);
            this.IsUnknown00000002 =           CGeneric.GetBitInMask(FlagsInt, 0x00000002);
            this.IsAlternateGlyphRendering =   CGeneric.GetBitInMask(FlagsInt, 0x00000010);
            this.IsDynamicLayoutRuntimeState = CGeneric.GetBitInMask(FlagsInt, 0x00010000);
            this.IsDynamicLayoutText =         CGeneric.GetBitInMask(FlagsInt, 0x01000000);
            this.IsAlternateRevealControl =    CGeneric.GetBitInMask(FlagsInt, 0x08000000);
            this.IsAlternateTextMode =         CGeneric.GetBitInMask(FlagsInt, 0x20000000);

            
         
        }

        public void UpdateFlags()
        {
            var newFlags = this.FlagsInt;
            CGeneric.SetBitInMask(ref newFlags, 0x00000040, this.IsRenderMiddlePass);
            CGeneric.SetBitInMask(ref newFlags, 0x00000400, this.IsTextHidden);
            CGeneric.SetBitInMask(ref newFlags, 0x00000800, this.IsFixedGlyphAdvance);
            CGeneric.SetBitInMask(ref newFlags, 0x00001000, this.IsProgressiveDisplay);
            CGeneric.SetBitInMask(ref newFlags, 0x00002000, this.IsRenderLatePass);
            CGeneric.SetBitInMask(ref newFlags, 0x00004000, this.IsBoundedLayout);
            CGeneric.SetBitInMask(ref newFlags, 0x00008000, this.IsCenterVertically);
            CGeneric.SetBitInMask(ref newFlags, 0x00020000, this.IsUseFixedColors);
            CGeneric.SetBitInMask(ref newFlags, 0x00080000, this.IsNormalFont);
            CGeneric.SetBitInMask(ref newFlags, 0x00100000, this.IsProgressiveSound);
            CGeneric.SetBitInMask(ref newFlags, 0x00800000, this.IsCenterHorizontally);
            CGeneric.SetBitInMask(ref newFlags, 0x02000000, this.IsSmallFont);
            CGeneric.SetBitInMask(ref newFlags, 0x40000000, this.IsAdditionalGlyphAdvance);

            CGeneric.SetBitInMask(ref newFlags, 0x00000001, this.IsRuntimeGlyphState1);
            CGeneric.SetBitInMask(ref newFlags, 0x00000002, this.IsUnknown00000002);
            CGeneric.SetBitInMask(ref newFlags, 0x00000010, this.IsAlternateGlyphRendering);
            CGeneric.SetBitInMask(ref newFlags, 0x00010000, this.IsDynamicLayoutRuntimeState);
            CGeneric.SetBitInMask(ref newFlags, 0x01000000, this.IsDynamicLayoutText);
            CGeneric.SetBitInMask(ref newFlags, 0x08000000, this.IsAlternateRevealControl);
            CGeneric.SetBitInMask(ref newFlags, 0x20000000, this.IsAlternateTextMode);

            this.Flags = CGeneric.ConvertIntToByteArray(newFlags);
        }
    }
}