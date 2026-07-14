namespace N64PPLEditorC
{
    /// <summary>
    /// Classe contenant les informations de base d'un texte (position X et Y initiales et Identifiant)
    /// </summary>
    public class CSBF1TextBase
    {
        public int X;
        public int Y;
        public int Id;

        public CSBF1TextBase(int baseX, int baseY, int Id)
        {
            this.X = baseX;
            this.Y = baseY;
            this.Id = Id;
        }

        public CSBF1TextBase(ref int index, byte[] rawData)
        {
            this.X = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
            this.Y = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index+4, 4));
            this.Id   = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index+8, 4));
            index += 12;
        }
    }
}