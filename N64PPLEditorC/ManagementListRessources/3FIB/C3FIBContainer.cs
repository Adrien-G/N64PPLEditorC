using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static N64PPLEditorC.CGeneric;

namespace N64PPLEditorC
{ 
    public class C3FIBContainer
    {
        public C3FIBHeader Header {get;set;}
        public BFF2Object Bff2 { get; set;}

        public C3FIBContainer(byte[] rawData, ref int globalIndex)
        {
            Header = new C3FIBHeader(rawData,ref globalIndex);
            Bff2 = new BFF2Object(rawData, ref globalIndex);
        }

        public C3FIBContainer(PictureBox pictureBox, string name)
        {
            Header = new C3FIBHeader();
            Bff2 = new BFF2Object(pictureBox,name);
        }

        public byte[] RecomposeRawData()
        {
            var rawData = new List<byte>();
            rawData.AddRange(CGeneric.SwapBigAndLittleEndian(CGeneric.ConvertIntToByteArray((int)Header.FrameX))); 
            rawData.AddRange(CGeneric.SwapBigAndLittleEndian(CGeneric.ConvertIntToByteArray((int)Header.FrameY)));
            rawData.AddRange(CGeneric.SwapBigAndLittleEndian(CGeneric.ConvertIntToByteArray((int)Header.DisplayTime)));
            rawData.AddRange(Bff2.RecomposeRawData());
            return rawData.ToArray();
        }
    }
}
