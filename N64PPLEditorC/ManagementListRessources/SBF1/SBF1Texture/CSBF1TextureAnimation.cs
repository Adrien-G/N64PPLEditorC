namespace N64PPLEditorC
{
    public class CSBF1TextureAnimation
    {
        public int InitialFrameIndex;
        public int LinkedTextureId;

        public CSBF1TextureAnimation()
        {
        }

        public CSBF1TextureAnimation(byte[] rawData, ref int index)
        {
            this.InitialFrameIndex = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
            index += 4;
        }

        public void AddLinkedtextureId(byte[] rawData, ref int index)
        {
            this.LinkedTextureId = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
            index += 4;
        }
    }
}