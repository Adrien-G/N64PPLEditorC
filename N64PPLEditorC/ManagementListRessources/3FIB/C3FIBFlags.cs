using System;

namespace N64PPLEditorC
{
    public class C3FIBFlags
    {
        // byte array des différents Flags
        public byte[] Flags { get; private set; }

        //représentations sur un int
        public int FlagsInt { get { return CGeneric.ConvertByteArrayToInt(Flags); } }

        public bool AutoScroll { get;private set; }
        public bool Unk00000002 { get; private set; }
        public bool AnimationLoop { get; private set; }
        public bool Name { get; private set; }
        public bool SecondRGBAColor { get; private set; }
        public bool Animated { get; private set; }
        public bool AdjustedLocation { get; private set; }
        public bool LoopDataAdditional { get; private set; }
        public bool Unknow00000100 { get; private set; }
        public bool UsesSharedFrameBuffers { get; private set; }
        public bool PingPongAnimation { get; private set; }
        public bool PingPongDirection { get; private set; }

        //attention les flags sont en little endian
        public C3FIBFlags(byte[] flags,ref int index)
        {
            this.Flags = CGeneric.SwapBigAndLittleEndian(flags);

            this.AutoScroll =             CGeneric.GetBitInMask(FlagsInt, 0x00000001);
            this.Unk00000002 =            CGeneric.GetBitInMask(FlagsInt, 0x00000002);
            this.AnimationLoop =          CGeneric.GetBitInMask(FlagsInt, 0x00000004);
            this.Name =                   CGeneric.GetBitInMask(FlagsInt, 0x00000008);
            this.SecondRGBAColor =        CGeneric.GetBitInMask(FlagsInt, 0x00000010);
            this.Animated =               CGeneric.GetBitInMask(FlagsInt, 0x00000020);
            this.LoopDataAdditional =     CGeneric.GetBitInMask(FlagsInt, 0x00000040);
            this.AdjustedLocation =       CGeneric.GetBitInMask(FlagsInt, 0x00000080);
            this.Unknow00000100 =         CGeneric.GetBitInMask(FlagsInt, 0x00000100);
            this.UsesSharedFrameBuffers = CGeneric.GetBitInMask(FlagsInt, 0x00000200);
            this.PingPongAnimation =      CGeneric.GetBitInMask(FlagsInt, 0x00000400);
            this.PingPongDirection =      CGeneric.GetBitInMask(FlagsInt, 0x00000400);
            index += 4;
        }

        public void UpdateFlags()
        {
            var newFlags = this.FlagsInt;
            CGeneric.SetBitInMask(ref newFlags, 0x00000001, this.AutoScroll);
            CGeneric.SetBitInMask(ref newFlags,0x00000002 , this.Unk00000002);
            CGeneric.SetBitInMask(ref newFlags,0x00000004 , this.AnimationLoop);
            CGeneric.SetBitInMask(ref newFlags,0x00000008 , this.Name);
            CGeneric.SetBitInMask(ref newFlags,0x00000010 , this.SecondRGBAColor);
            CGeneric.SetBitInMask(ref newFlags,0x00000020 , this.Animated);
            CGeneric.SetBitInMask(ref newFlags,0x00000040 , this.LoopDataAdditional);
            CGeneric.SetBitInMask(ref newFlags,0x00000080 , this.AdjustedLocation);
            CGeneric.SetBitInMask(ref newFlags,0x00000100 , this.Unknow00000100);
            CGeneric.SetBitInMask(ref newFlags,0x00000200 , this.UsesSharedFrameBuffers);
            CGeneric.SetBitInMask(ref newFlags,0x00000400 , this.PingPongAnimation);
            CGeneric.SetBitInMask(ref newFlags,0x00000400 , this.PingPongDirection);

          

            this.Flags = CGeneric.SwapBigAndLittleEndian(CGeneric.ConvertIntToByteArray(newFlags));
        }
    }
}
