using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        public byte[] Name { get; set; }
        public string NameString { get { return Encoding.ASCII.GetString(Name)?.TrimEnd('\0'); } }

        public BFF2Object()
        {
            var rawData = new List<byte>();
            rawData.AddRange(CGeneric.patternBFF2);
            this.Base = new CBFF2Base();
        }

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
                this.Name = CGeneric.GiveMeArray(rawData,globalIndex, NameSize);
                globalIndex += NameSize;
            }

            uint subImageCount = 0;
            uint subImagePayloadSize = 0;
            if (this.Flags.HasSubImages)
            {
                subImageCount = (uint)CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex, 4));
                subImagePayloadSize = (uint)CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalIndex+4, 4));
                globalIndex += 8;
            }


            if ((this.Flags.PixelFormat & 0xF0) == 0x30)
            {
                Palette = new CBFF2Palette(rawData,ref globalIndex);
            }

            if (this.Flags.HasSubImages)
            {
                this.SubImageData = new CBFF2SubImage(rawData,ref globalIndex,subImageCount,subImagePayloadSize,this.Flags.PixelFormat);
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

        public byte[] RecomposeRawData()
        {
            var rawData = new List<byte>();

            //BFF2
            rawData.AddRange(CGeneric.patternBFF2);

            //Flags
            this.Flags.UpdateFlags();
            rawData.AddRange(this.Flags.Flags);

            //Base
            //TODO revoir pour recomposer la base

            //bytes per line
            rawData.AddRange(CGeneric.ConvertIntToByteArray((int)this.BytePerLines));

            if (this.Flags.HasName)
            {
                var nameLenght = (int)this.Name.Length;
                if (nameLenght % 2 == 0)
                {
                    rawData.AddRange(CGeneric.ConvertIntToByteArray(nameLenght));
                    rawData.AddRange(this.Name);
                }

                else
                {
                    rawData.AddRange(CGeneric.ConvertIntToByteArray(nameLenght + 1));
                    rawData.AddRange(this.Name); //TODO attention a rajouter un 0x00en cas de nom a longueur impaire
                }
            }

            if (this.Flags.HasSubImages)
            {
                rawData.AddRange(CGeneric.ConvertIntToByteArray((int)this.SubImageData.SubImageCount));
                rawData.AddRange(CGeneric.ConvertIntToByteArray((int)this.SubImageData.PayloadSize));
            }
            if ((this.Flags.PixelFormat & 0xF0) == 0x30)
            {
                rawData.AddRange(CGeneric.ConvertIntToByteArray((int)this.Palette.ColorCount));
                rawData.AddRange(this.Palette.RawData);
            }

            if (this.Flags.HasSubImages)
            {
                //TODO
            }
            else
            {
                rawData.AddRange(this.EncodedPixelData);
            }


            return rawData.ToArray();
        }


        public Bitmap GetBmpTexture()
        {
            byte[] rgbaData = CTextureManager.ConvertByteArrayToRGBA(DecodedPixelData, (CGeneric.Compression)Flags.PixelFormat, Palette?.RawData);
            return CTextureManager.ConvertRGBAByteArrayToBitmap(rgbaData, (int)this.Base.PixelWidth, (int)this.Base.DisplayHeight);
           
        }

        public Bitmap GetSubImageBitmap(int index)
        {
            CBFF2SubImageData image = SubImageData.ImageData[index];
            byte[] rgbaData = CTextureManager.ConvertByteArrayToRGBA( image.PixelData,(CGeneric.Compression)Flags.PixelFormat);
            return CTextureManager.ConvertRGBAByteArrayToBitmap(rgbaData, (int)image.Width, (int)image.Height);
        }

        public void SaveTexture(int indexFIB, int index)
        {
            if(Flags.HasSubImages)
            {
                for(int i = 0; i < SubImageData.SubImageCount; i++)
                    GetSubImageBitmap(i).Save(CGeneric.pathExtractedTexture + (indexFIB + 1) + "-" + i + ".png");
            }
            else
                GetBmpTexture().Save(CGeneric.pathExtractedTexture + (indexFIB + 1) + "-" + (index + 1) + ", " + Name + ".png");
        }
    }
}