using System;
using System.Collections.Generic;
using System.Drawing;

namespace N64PPLEditorC
{
    public class CBFF2SubImage
    {

        public List<CBFF2SubImageData> ImageData;
        public uint SubImageCount { get; }
        public uint PayloadSize { get; }
        public byte[] Payload { get; }


        public CBFF2SubImage(byte[] rawData, ref int globalIndex, uint subImageCount, uint payloadSize,byte pixelFormat)
        {
            this.SubImageCount = subImageCount;
            this.PayloadSize = payloadSize;

            ImageData = new List<CBFF2SubImageData>((int)SubImageCount);

            //récupération des descripteurs
            for (uint i = 0; i < SubImageCount; i++)
                ImageData.Add( new CBFF2SubImageData(rawData, ref globalIndex));

            Payload = CGeneric.GiveMeArray(rawData,globalIndex, (int)PayloadSize);
            globalIndex += (int)PayloadSize;

           
            ExtractImages(pixelFormat);
        }

        private void ExtractImages(byte pixelFormat)
        {
            for (int i = 0; i < ImageData.Count; i++)
            {
                CBFF2SubImageData image = ImageData[i];
                int dataOffset = (int)image.DataOffset;
                int nextOffset;

                if (i + 1 < ImageData.Count)
                    nextOffset = (int)ImageData[i + 1].DataOffset;
                else
                    nextOffset =(int)PayloadSize;

                if (dataOffset < 0 || nextOffset < dataOffset || nextOffset > Payload.Length)
                    throw new InvalidOperationException("Invalid BFF2 sub-image offsets.");
                
                image.BytesPerRow = GetBytesPerRow(image.Width, pixelFormat);
                image.PixelDataLength = image.BytesPerRow * (int)image.Height;
                image.StoredSpanLength = nextOffset - dataOffset;
                image.PixelData = new byte[image.PixelDataLength];

                Array.Copy(Payload,dataOffset,image.PixelData,0,image.PixelDataLength);
                int paddingLength = image.StoredSpanLength - image.PixelDataLength;
                image.PaddingData = new byte[paddingLength];

                if (paddingLength > 0)
                    Array.Copy(Payload,dataOffset + image.PixelDataLength,image.PaddingData, 0, paddingLength);
            }
        }

        private static int GetBytesPerRow(uint width, byte pixelFormat)
        {
            switch (pixelFormat & 0x0F)
            {
                case 2: return (int)((width + 1) / 2); // IA4 ou CI4
                case 3: return (int)width; // IA8 ou CI8
                case 4: return checked((int)width * 2); // IA16 ou RGBA16
                case 5: return checked((int)width * 4); // RGBA32
                default: throw new NotSupportedException($"Unsupported BFF2 pixel format: " + $"0x{pixelFormat:X2}");
            }
        }
    }
}