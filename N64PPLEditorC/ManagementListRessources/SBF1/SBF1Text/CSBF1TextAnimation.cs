using System;

namespace N64PPLEditorC
{
    public class CSBF1TextAnimation
    {
        public int PackedRevealMetadata { get; set; }
        /// <summary>
        /// indique si il y a une liaison avec une texture
        /// </summary>
        public ushort LinkedTextureId { get; set; }
        /// <summary>
        /// indique si un son est joué pendant l'affichage progressive du texte
        /// </summary>
        public ushort ProgressiveSound { get; set; }

        public CSBF1TextAnimation(ref int index,CSBF1TextFlags flags, byte[] rawData)
        {
            ProgressiveSound = CGeneric.ReadUInt16BigEndian(rawData, index);
            LinkedTextureId = CGeneric.ReadUInt16BigEndian(rawData, index + 2);
            index += 4;
        }

        public void AddPackedRevealMetadata(ref int index, byte[] RawData)
        {
            PackedRevealMetadata = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(RawData, index, 4));
            index += 4;
        }
    }
}