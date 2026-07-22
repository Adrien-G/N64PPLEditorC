namespace N64PPLEditorC
{
    public class C3FIBHeader
    {
        //TODO reste a comprendre l'utilité, toujours a 0 coté N64, définit dans la version GC
        public int FrameX { get; private set; }

        //TODO reste a comprendre l'utilité, toujours a 0 coté N64, définit dans la version GC
        public int FrameY { get; private set; }

        //Durée d'affichage du BFF2 avant passage du suivant
        public uint DisplayTime { get; set; }
        public C3FIBHeader(byte[] rawData, ref int globalIndex)
        {
            var frameX = CGeneric.SwapBigAndLittleEndian(CGeneric.GiveMeArray(rawData, globalIndex, 4));
            FrameX = CGeneric.ConvertByteArrayToInt(frameX);

            var frameY = CGeneric.SwapBigAndLittleEndian(CGeneric.GiveMeArray(rawData, globalIndex+4, 4));
            FrameY = CGeneric.ConvertByteArrayToInt(frameY);

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