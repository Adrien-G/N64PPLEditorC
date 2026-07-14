namespace N64PPLEditorC
{
    public class CSBF1TextRendering
    {
        public enum TextRenderPassFlags
        {
            Default = 0,
            MiddlePass = 0x00000040,
            LatePass = 0x00002000
        }
        /// permet de déterminer le moment du rendu du texte par rapport aux autres textures et objets.
        public TextRenderPassFlags RenderPasses;
    

        public CSBF1TextRendering(int flags)
        {
            RenderPasses = (TextRenderPassFlags)(flags & 0x00002040); ;
        }

    }
}