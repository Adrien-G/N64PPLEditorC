using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static N64PPLEditorC.CGeneric;

namespace N64PPLEditorC
{
    public class C3FIB
    {
        private List<CBFF2> Bff2Childs;
        public C3FIBFlags Flags;
        private byte[] Name;
        private byte[] NameLength;
        public int NameLengthInt { get { return CGeneric.ConvertByteArrayToInt(NameLength); } }
        private byte[] RessourceName;

        private byte[] PrimaryColor;
        private byte[] SecondColor;

        //nouveau constructeur pour ré-écriture
        public C3FIB(Byte[] rawData, Byte[] ressourceName, bool newVersion)
        {

            int globalIndex = 0;
            var Magic3FIB = CGeneric.GiveMeArray(rawData, globalIndex, 4);
            globalIndex += 4;

            if (CGeneric.ConvertByteArrayToInt(Magic3FIB) != CGeneric.ConvertByteArrayToInt(CGeneric.pattern3FIB))
                throw new Exception("Not a Valid 3FIB");

            this.Flags = new C3FIBFlags(CGeneric.GiveMeArray(rawData,globalIndex, 4),ref globalIndex);
            this.PrimaryColor = CGeneric.GiveMeArray(rawData, globalIndex, 4);
            globalIndex += 4;

            var FrameCount = CGeneric.GiveMeArray(rawData, globalIndex, 4);
            globalIndex += 4;

            if (this.Flags.SecondRGBAColor)
            {
                this.SecondColor = CGeneric.GiveMeArray(rawData, globalIndex, 4);
                globalIndex += 4;
            }

            if (this.Flags.Name)
            {
                NameLength = CGeneric.SwapBigAndLittleEndian(CGeneric.GiveMeArray(rawData, globalIndex, 4));
                globalIndex += 4;
                this.Name = CGeneric.GiveMeArray(rawData, globalIndex, NameLengthInt);
                globalIndex += NameLengthInt;
            }

            this.RessourceName = ressourceName;
            TempPartInit(rawData,FrameCount);

        }

        public byte[] RecomposeRawData()
        {
            var rawData = new List<byte>();

            //3FIB
            rawData.AddRange(CGeneric.pattern3FIB);

            //Flags
            this.Flags.UpdateFlags();
            rawData.AddRange(CGeneric.SwapBigAndLittleEndian(this.Flags.Flags));

            //PrimaryColor
            rawData.AddRange(this.PrimaryColor);

            //FrameCount
            rawData.AddRange(CGeneric.SwapBigAndLittleEndian(CGeneric.ConvertIntToByteArray(this.Bff2Childs.Count)));

            if (this.Flags.SecondRGBAColor)
                rawData.AddRange(this.SecondColor);

            if (this.Flags.Name)
            {
                rawData.AddRange(CGeneric.SwapBigAndLittleEndian(this.NameLength));
                rawData.AddRange(this.Name);
            }
            foreach (var bff2 in this.Bff2Childs)
            rawData.AddRange(bff2.GetRawData());

            return rawData.ToArray();
        }








        //only for keep compression ratio
        public Compression compressionType;

        public C3FIB(Byte[] rawData, Byte[] ressourceName)
        {
        }

        private void TempPartInit(byte[] rawData,byte[] frameCount)
        {
            Bff2Childs = new List<CBFF2>();

            //exclude the header and prepare and Chunk each BFF2
            Byte[] bffData = new byte[rawData.Length - NameLengthInt - 20];
            Array.Copy(rawData, 20 + NameLengthInt, bffData, 0, bffData.Length);
            MakeBFF2Chunks(bffData, CGeneric.ConvertByteArrayToInt(CGeneric.SwapBigAndLittleEndian(frameCount)));

            //keep compression information based on the first BFF2
            if (Bff2Childs.Count > 0)
                compressionType = Bff2Childs[0].GetCompressionType();
        }

        private void MakeBFF2Chunks(Byte[] bffsData, int bffCount)
        {
            //store position and size of all BFF2
            List<int> indexBFF2 = new List<int>();
            List<int> sizeBFF2 = new List<int>();

            int tmpPositionBFF2;
            //search all bff2 present in the 3FIB and grab index
            
            do
            {
                tmpPositionBFF2 = CGeneric.SearchBytesInArray(bffsData, CGeneric.patternBFF2, indexBFF2.Count()) - 12;

                if (tmpPositionBFF2 != -13)
                {
                    //add index
                    indexBFF2.Add(tmpPositionBFF2);
                    //calculate size of previous BFF2 (if not the last)
                    if (tmpPositionBFF2 != 0)
                        sizeBFF2.Add(indexBFF2[indexBFF2.Count() - 1] - indexBFF2[indexBFF2.Count() - 2]);
                }
                else
                    //calculate size of last BFF2
                    try
                    {
                        sizeBFF2.Add(bffsData.Length - indexBFF2[indexBFF2.Count() - 1]);
                    } catch { } //can occur only when modified rom doesn't have any BFF2
                    
            } while (tmpPositionBFF2 != -13);

            //add bff2 list in the bff2Childs
            for (int i = 0; i < indexBFF2.Count(); i++)
            {
                Byte[] tmpByteBFF2 = new byte[sizeBFF2[i]];
                Array.Copy(bffsData, indexBFF2[i], tmpByteBFF2, 0, sizeBFF2[i]);
                Bff2Childs.Add(new CBFF2(tmpByteBFF2));
                Bff2Childs[Bff2Childs.Count - 1].Init();
            }
        }

        public void AddBFF2Child(byte[] bff2Child)
        {
            Bff2Childs.Add(new CBFF2(bff2Child));
            Bff2Childs[Bff2Childs.Count - 1].Init();
        }

        public void RemoveBFF2Child(int index)
        {
            Bff2Childs.RemoveAt(index);
        }

        public string GetBFFName(int index)
        {
            return Bff2Childs[index].GetName();
        }

        public int GetBFFCount()
        {
            return Bff2Childs.Count();
        }

        public void SaveTexture(int indexFIB,int index)
        {
            Bff2Childs[index].DecompressTexture();
            Bitmap bmp = Bff2Childs[index].GetBmpTexture();

            bmp.Save(CGeneric.pathExtractedTexture + (indexFIB + 1) + "-" + (index + 1) + ", " + Bff2Childs[index].GetName() + ".png");
        }

        public void GetTexture(PictureBox pictureBox, int index)
        {
            Bff2Childs[index].DecompressTexture();
            Bitmap bmp = Bff2Childs[index].GetBmpTexture();
            pictureBox.Image = bmp;
        }

        public Bitmap GetBmpTexture(int index)
        {
            Bff2Childs[index].DecompressTexture();
            return Bff2Childs[index].GetBmpTexture();
        }

        public string GetFIBName()
        {
            return System.Text.Encoding.UTF8.GetString(this.Name);
        }

        public CBFF2 GetBFF2(int index)
        {
            return Bff2Childs[index];
        }

        public TextureDisplayStyle GetTextureDisplayStyle()
        {
            //TODO adapt.
            return TextureDisplayStyle.Fixed;
            //return (TextureDisplayStyle)this.header3FIB[4];
        } 
        public void SetTextureDisplayStyle(TextureDisplayStyle style)
        {
            //TODO adapt.
            //this.header3FIB[4] = (byte)style;
        }

        internal string GetRessourceName()
        {
            return Encoding.Default.GetString(RessourceName);
        }
    }
}
