using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static N64PPLEditorC.CGeneric;

namespace N64PPLEditorC
{
    public class C3FIBObject
    {
        public List<C3FIBContainer> C3FibContainer;
        public C3FIBFlags Flags;
        private byte[] Name;
        private byte[] NameLength;
        public string NameString { get { return Encoding.ASCII.GetString(Name).TrimEnd('\0'); } }
        public int NameLengthInt { get { return CGeneric.ConvertByteArrayToInt(NameLength); } }
        private byte[] RessourceName;

        private byte[] PrimaryColor;
        private byte[] SecondColor;

        //nouveau constructeur pour ré-écriture
        public C3FIBObject(Byte[] rawData, Byte[] ressourceName, bool newVersion)
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

            C3FibContainer = new List<C3FIBContainer>();
            //TODO faire une boucle par la suite
            while(rawData.Length > globalIndex)
            {
                C3FibContainer.Add(new C3FIBContainer(rawData, ref globalIndex));
            }

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
            rawData.AddRange(CGeneric.SwapBigAndLittleEndian(CGeneric.ConvertIntToByteArray(this.C3FibContainer.Count)));

            if (this.Flags.SecondRGBAColor)
                rawData.AddRange(this.SecondColor);

            if (this.Flags.Name)
            {
                rawData.AddRange(CGeneric.SwapBigAndLittleEndian(this.NameLength));
                rawData.AddRange(this.Name);
            }
            foreach (var bff2 in this.C3FibContainer)
                rawData.AddRange(bff2.GetRawData());

            return rawData.ToArray();
        }








        //only for keep compression ratio
        public Compression compressionType;

        public C3FIBObject(Byte[] rawData, Byte[] ressourceName)
        {
        }
        public void AddBFF2Child(byte[] bff2Child)
        {
            C3FibContainer.Add(new C3FIBContainer(bff2Child));
            C3FibContainer[C3FibContainer.Count - 1].Init();
        }

        public void RemoveBFF2Child(int index)
        {
            C3FibContainer.RemoveAt(index);
        }


        public int GetBFFCount()
        {
            return C3FibContainer.Count();
        }

        public void SaveTexture(int indexFIB,int index)
        {
            C3FibContainer[index].DecompressTexture();
            Bitmap bmp = C3FibContainer[index].GetBmpTexture();

            bmp.Save(CGeneric.pathExtractedTexture + (indexFIB + 1) + "-" + (index + 1) + ", " + C3FibContainer[index].GetName() + ".png");
        }

        public void GetTexture(PictureBox pictureBox, int index)
        {
            Bitmap bmp = C3FibContainer[index].GetBmpTexture();
            pictureBox.Image = bmp;
        }

        public Bitmap GetBmpTexture(int index)
        {
            C3FibContainer[index].DecompressTexture();
            return C3FibContainer[index].GetBmpTexture();
        }

        public string GetFIBName()
        {
            return System.Text.Encoding.UTF8.GetString(this.Name);
        }

        public C3FIBContainer GetBFF2(int index)
        {
            return C3FibContainer[index];
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
