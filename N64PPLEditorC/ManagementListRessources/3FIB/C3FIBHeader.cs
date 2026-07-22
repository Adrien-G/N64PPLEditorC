namespace N64PPLEditorC
{
    public class C3FIBHeader
    {
        public uint Unk04 { get; private set; }
        public uint Unk08 { get; private set; }
        public uint DisplayTime { get; set; }
        public C3FIBHeader(byte[] rawData, ref int globalIndex)
        {
            var unk04 = CGeneric.SwapBigAndLittleEndian(CGeneric.GiveMeArray(rawData, globalIndex, 4));
            Unk04 = (uint)CGeneric.ConvertByteArrayToInt(unk04);

            var unk08 = CGeneric.SwapBigAndLittleEndian(CGeneric.GiveMeArray(rawData, globalIndex+4, 4));
            Unk08 = (uint)CGeneric.ConvertByteArrayToInt(unk08);

            var displayTime = CGeneric.SwapBigAndLittleEndian(CGeneric.GiveMeArray(rawData, globalIndex + 8, 4));
            DisplayTime = (uint)CGeneric.ConvertByteArrayToInt(displayTime);

            globalIndex += 12;
        }

        public C3FIBHeader()
        {
            DisplayTime = 1;
        }


    }
}