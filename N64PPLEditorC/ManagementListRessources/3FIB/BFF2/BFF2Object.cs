using System;
using System.Collections.Generic;
using System.Text;

namespace N64PPLEditorC
{
    public class BFF2Object
    {
        public CBFF2Flags Flags { get; set; }
        public CBFF2Base Base { get; set; }
        public CBFF2Palette Palette { get; set; }
        public CBFF2SubImage SubImageData { get; set; }

        public uint BytePerLines { get; set; }

        public byte[] EncodedPixelData { get; set; }
        public byte[] DecodedPixelData { get; set; }

        public string Name { get; set; }

        public BFF2Object(byte[] rawData, ref int globalIndex)
        {
            var MagicBFF2 = CGeneric.GiveMeArray(rawData, globalIndex, 4);
            globalIndex += 4;

            if (CGeneric.ConvertByteArrayToInt(MagicBFF2) != CGeneric.ConvertByteArrayToInt(CGeneric.patternBFF2))
                throw new Exception("Not a Valid BFF2");

            this.Flags = new CBFF2Flags(rawData, ref globalIndex);

            //size
            this.Base = new CBFF2Base(rawData,ref globalIndex,this.Flags.HasPackedWidths);

            //byte per line (dépend du nombre d'octet par pixel en fonction de la compression)
            this.BytePerLines = (uint)CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex, 4));
            globalIndex += 4;

            if (this.Flags.HasName)
            {
                var NameSize = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex, 4));
                globalIndex += 4;
                //doit toujours rester pair (supprimer le 0 inutile a la fin si besoin)
                this.Name = Encoding.ASCII.GetString(CGeneric.GiveMeArray(rawData,globalIndex, NameSize)).TrimEnd('\0');
                globalIndex += NameSize;
            }

            var subImageCount = 0;
            var subImagePayloadSize = 0;
            if (this.Flags.HasSubImages)
            {
                subImageCount = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex, 4));
                subImagePayloadSize = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex+4, 4));
                globalIndex += 8;
            }


            if ((this.Flags.PixelFormat & 0xF0) == 0x30)
            {
                Palette = new CBFF2Palette(rawData,ref globalIndex);
            }

            if (this.Flags.HasSubImages)
            {
                this.SubImageData = new CBFF2SubImage(rawData,ref globalIndex);
            }
            else
            {
                if (this.Flags.IsCompressed)
                {
                    int expectedDecodedSize = (int)(BytePerLines * Base.DisplayHeight);
                    int indexBeforeDecompress = globalIndex;

                    this.DecodedPixelData = CTextureDecompress.DecompressTexture(rawData, ref globalIndex, expectedDecodedSize);
                    this.EncodedPixelData = CGeneric.GiveMeArray(rawData, indexBeforeDecompress, globalIndex - indexBeforeDecompress);
                }
                else
                {
                    var size = (int)(BytePerLines * Base.DisplayHeight);
                    EncodedPixelData = CGeneric.GiveMeArray(rawData,globalIndex, size);
                    DecodedPixelData = (byte[])EncodedPixelData.Clone();
                    globalIndex += size;
                }
            }
               

        }
    }
}