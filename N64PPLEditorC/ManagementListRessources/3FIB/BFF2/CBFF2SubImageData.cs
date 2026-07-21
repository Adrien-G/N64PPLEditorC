using System;

namespace N64PPLEditorC
{
    public class CBFF2SubImageData
    {
        public byte[] Header { get; }
        public uint HeaderInt { get { return (uint)CGeneric.ConvertByteArrayToInt(this.Header); } }

        public uint Width { get; }
        public uint Height { get; }
        public uint DataOffset { get; }


        public int BytesPerRow { get; internal set; }
        public int PixelDataLength { get; internal set; }
        public int StoredSpanLength { get; internal set; }

        public byte[] PixelData { get; internal set; }
        public byte[] PaddingData { get; internal set; }

        public CBFF2SubImageData(byte[] rawData,ref int globalIndex)
        {
            this.Header = CGeneric.GiveMeArray(rawData, globalIndex,4);
            this.Width = (uint)(HeaderInt & 0x3F);
            this.Height = (uint)(HeaderInt >> 6) & 0x3F;
            this.DataOffset = (uint)(HeaderInt >> 12);
   
            globalIndex += 4;
        }
    }
}