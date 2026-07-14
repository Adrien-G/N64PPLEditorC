using System;

namespace N64PPLEditorC
{
    public class CSBF1TextAnimation
    {
        public int PackedPagination { get; set; }
        /// <summary>
        /// indique si il y a une liaison avec une texture
        /// </summary>
        public ushort LinkedTextureId { get; set; }
        /// <summary>
        /// indique si un son est joué pendant l'affichage progressive du texte
        /// </summary>
        public ushort ProgressiveSoundStyle { get; set; }

        /// <summary>
        /// permet d'indiquer si le texte est affiché de manière progressive ou non
        /// </summary>
        public bool ProgressiveDisplay { get; set; }

        public CSBF1TextAnimation(ref int index,int flags, byte[] rawData)
        {
            ProgressiveSoundStyle = CGeneric.ReadUInt16BigEndian(rawData, index);
            LinkedTextureId = CGeneric.ReadUInt16BigEndian(rawData, index + 2);
            ProgressiveDisplay = (flags & 0x00001000) != 0;
            index += 4;
        }

        public void AddPackedMetadata(ref int index, byte[] RawData)
        {
            PackedPagination = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(RawData, index, 4));
            index += 4;
        }
    }
}