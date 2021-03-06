﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace N64PPLEditorC
{
    public class UncompressedRomTextureData 
    {
        public byte[] rawData { get; private set; }
        public int location { get; private set; }
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }
        public int bytePerPixel { get; private set; }
        private string name;
        public byte[] palette;
        public int paletteLocation { get; private set; }


        public UncompressedRomTextureData(byte[] rawData, int location, int sizeX, int sizeY, int bytePerPixel, string name,byte[] palette = null,int paletteLocation = 0)
        {
            this.rawData = rawData;
            this.location = location;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.bytePerPixel = bytePerPixel;
            this.name = name;
            this.palette = palette;
            this.paletteLocation = paletteLocation;
        }

        public Bitmap ShowTexture()
        {
            byte[] tmpArray = null;
            switch (bytePerPixel)
            {
                case 32:
                    tmpArray = rawData;
                    break;
                case 16:
                    tmpArray = CTextureManager.ConvertByteArrayToRGBA(rawData, CGeneric.Compression.trueColor16Bits);
                    break;
                case 8:
                    tmpArray = CTextureManager.ConvertByteArrayToRGBA(rawData, CGeneric.Compression.max256Colors,palette,true);
                    break;

            }
            
            return CTextureManager.ConvertRGBAByteArrayToBitmap(tmpArray, sizeX, sizeY);
        }

        public void SetTexture(byte[] rawData,byte[] palette)
        {
            this.rawData = rawData;
            //be sure to overwrite all the palette
            this.palette = new byte[this.palette.Length];
            Array.Copy(palette, 0, this.palette, 0, palette.Length);
        }

        public string GetName()
        {
            return name;
        }
    }
}