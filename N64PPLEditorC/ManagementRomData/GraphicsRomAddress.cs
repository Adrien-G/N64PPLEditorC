using System;
using System.Drawing;
using System.Windows.Forms;

namespace N64PPLEditorC
{
    public class GraphicsRomAddress 
    {
        private byte[] dataRaw;
        private int location;
        private int sizeX, sizeY;
        private int bytePerPixel;
        private string name;
        private byte[] palette;


        public GraphicsRomAddress(byte[] dataRaw, int location, int sizeX, int sizeY, int bytePerPixel, string name,byte[] palette = null)
        {
            this.dataRaw = dataRaw;
            this.location = location;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.bytePerPixel = bytePerPixel;
            this.name = name;
            this.palette = palette;
        }

        public Bitmap ShowTexture()
        {
            byte[] tmpArray = null;
            switch (bytePerPixel)
            {
                case 32:
                    tmpArray = dataRaw;
                    break;
                case 16:
                    tmpArray = CTextureManager.ConvertByteArrayToRGBA(dataRaw, CGeneric.Compression.trueColor16Bits);
                    break;
                case 8:
                    tmpArray = CTextureManager.ConvertByteArrayToRGBA(dataRaw, CGeneric.Compression.max256Colors,palette);
                    break;

            }
            
            return CTextureManager.ConvertRGBAByteArrayToBitmap(tmpArray, sizeX, sizeY);
        }

        public void SetTexture(byte[] rawData)
        {
            this.dataRaw = rawData;
        }

        public string GetName()
        {
            return name;
        }
    }
}