using System;
using System.Drawing.Imaging;

namespace N64PPLEditorC
{
    public class CBFF2Flags
    {
        // byte array des différents Flags
        public byte[] Flags { get; private set; }

        //représentations sur un int
        public int FlagsInt { get { return CGeneric.ConvertByteArrayToInt(Flags); } }

        public byte PixelFormat { get; set; }

        public CGeneric.Compression CompressionType { get { return (CGeneric.Compression)PixelFormat;}}
        public bool HasName { get; private set; }
        public bool HasTransparentPaletteIndex { get; private set; }
        public bool IsCompressed { get; private set; }
        public byte TransparentPaletteIndex { get; private set; }
        public bool HasPackedWidths { get; private set; }
        public bool HasSubImages { get; private set; }

        public CBFF2Flags(byte pixelFormat)
        {
            PixelFormat = pixelFormat;
            HasName = true;
            IsCompressed = true;
            Flags = new byte[] { 0, 0, 0, 0 };
        }
        public CBFF2Flags(byte[] rawData,ref int index)
        {
            var flags = CGeneric.GiveMeArray(rawData, index, 4);
            this.Flags = flags;

            this.PixelFormat =                (byte)(FlagsInt & 0xFF);
            this.HasName =                    CGeneric.GetBitInMask(FlagsInt, 0x00000200);
            this.HasTransparentPaletteIndex = CGeneric.GetBitInMask(FlagsInt, 0x00000400);
            this.IsCompressed =               CGeneric.GetBitInMask(FlagsInt, 0x00000800);
            this.TransparentPaletteIndex =    (byte)((FlagsInt >> 12) & 0xFF);
            this.HasPackedWidths =            CGeneric.GetBitInMask(FlagsInt, 0x00200000);
            this.HasSubImages =               CGeneric.GetBitInMask(FlagsInt, 0x00400000);
            index += 4;
        }

        public void UpdateFlags()
        {
            var newFlags = this.FlagsInt;

            newFlags = (newFlags & ~0xFF) | this.PixelFormat;
            CGeneric.SetBitInMask(ref newFlags, 0x00000200, this.HasName);
            CGeneric.SetBitInMask(ref newFlags, 0x00000400, this.HasTransparentPaletteIndex);
            CGeneric.SetBitInMask(ref newFlags, 0x00000800, this.IsCompressed);
            newFlags = (newFlags & ~(0xFF << 12)) | ((int)this.TransparentPaletteIndex << 12);
            CGeneric.SetBitInMask(ref newFlags, 0x00200000, this.HasPackedWidths);
            CGeneric.SetBitInMask(ref newFlags, 0x00400000, this.HasSubImages);

            this.Flags = CGeneric.ConvertIntToByteArray(newFlags);
        }
    }
}
