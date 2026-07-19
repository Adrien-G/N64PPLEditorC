namespace N64PPLEditorC
{
    public class CBFF2Base
    {
        public uint DisplayWidth { get; set; }
        public uint PixelWidth { get; set; }

        public uint DisplayHeight { get; set; }

        public CBFF2Base(byte[] rawData,ref int globalIndex,bool packedWidth)
        {
            var width = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex, 4));
            if (packedWidth)
            {
                DisplayWidth = (ushort)(width >> 16);
                PixelWidth = (ushort)(width & 0xFFFF);
            }
            else
            {
                DisplayWidth = (uint)width;
                PixelWidth = (uint)width;
            }
            this.DisplayHeight = (uint)CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex + 4, 4));
            globalIndex += 8;
        }
    }
}