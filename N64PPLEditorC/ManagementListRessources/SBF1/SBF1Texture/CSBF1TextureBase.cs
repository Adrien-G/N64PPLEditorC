namespace N64PPLEditorC
{
    public class CSBF1TextureBase
    {
        public int X;
        public int Y;
        public int Id;

        public CSBF1TextureBase()
        {
        }

        public CSBF1TextureBase(byte[] rawData, ref int index)
        {
            this.X = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
            this.Y = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index+4, 4));
            index += 8;
        }

        public void AddId(byte[] rawData, ref int index)
        {
            this.Id = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
            index += 4;
        }
    }
}