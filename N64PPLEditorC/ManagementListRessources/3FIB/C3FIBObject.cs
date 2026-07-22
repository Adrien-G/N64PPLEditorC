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
        public List<C3FIBContainer> Container;
        public C3FIBFlags Flags;
        private byte[] Name;
        private byte[] NameLength;
        public string NameString { get { return Encoding.ASCII.GetString(Name).TrimEnd('\0'); } }
        public int NameLengthInt { get { return CGeneric.ConvertByteArrayToInt(NameLength); } }
        private byte[] RessourceName;
        public string RessourceNameString { get { return Encoding.ASCII.GetString(RessourceName).TrimEnd('\0'); } }
        private byte[] PrimaryColor;
        private byte[] SecondColor;

        public C3FIBObject(Byte[] rawData, Byte[] ressourceName)
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

            Container = new List<C3FIBContainer>();
            while(rawData.Length > globalIndex)
                Container.Add(new C3FIBContainer(rawData, ref globalIndex));

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
            rawData.AddRange(CGeneric.SwapBigAndLittleEndian(CGeneric.ConvertIntToByteArray(this.Container.Count)));

            if (this.Flags.SecondRGBAColor)
                rawData.AddRange(this.SecondColor);

            if (this.Flags.Name)
            {
                rawData.AddRange(CGeneric.SwapBigAndLittleEndian(this.NameLength));
                rawData.AddRange(this.Name);
            }
            foreach (var bff2 in this.Container)
                rawData.AddRange(bff2.RecomposeRawData());

            return rawData.ToArray();
        }

        public C3FIBObject(string name)
        {
            this.Name = CGeneric.ConvertStringToByteArray(name);
            Container = new List<C3FIBContainer>();
        }














        //only for keep compression ratio
        public Compression compressionType;

        
        public void AddBFF2Child(byte[] bff2Child)
        {
            Container.Add(new C3FIBContainer(bff2Child));
            Container[Container.Count - 1].Init();
        }
    }
}
