namespace N64PPLEditorC
{
    public class CSBF1TextureFlags
    {
        // byte array des différents Flags
        public byte[] Flags { get; private set; }

        //représentations sur un int
        public int FlagsInt { get { return CGeneric.ConvertByteArrayToInt(Flags); } }

        //détermine si la texture est affichée ou non
        public bool IsHidden { get; set; }

        //passe de rendu
        public bool IsRenderedLate { get; set; }

        //détermine si la texture reviens a l'image affiché ou a zero
        public bool IsFramePreservedOnShow { get; set; }

        //callback détermine si le bif est affiché ou si c'est une fonction propre a la scène
        public bool IsCallbackSuppressed { get; set; }
        public bool IsCallbackObject { get; set; }

        //rendu BIF spécifique
        public bool IsNormalBifRenderingSkipped { get; set; }

        //a priori gestion de la taille de la texture
        //initialement gestion de la transparence
        public bool HasBifBlendAlphaOverride { get; set; }

        //+8 cotet et active un flag du BIF
        public bool HasBifFields94And98 { get; set; }


        //encore pas trop connus
        public bool IsBifBlendAlphaForcedToZero { get; set; }
        //visiblement une animation BIF (évolution de l'animation)
        public bool IsBifPingPongAnimationEnabled { get; set; }

        //Le point d’ancrage devient centré horizontalement et proche du bas verticalement
        public bool UsesCenteredBifAnchor { get; set; }

        // Modifie la hauteur d’un texel et l’état de rendu du BIF.
        public bool IsBifFlag1000Enabled { get; set; }
        
       
        public bool IsBifPreparedOnActivation { get; set; }
        

        public bool IsBifFlag10000Enabled { get; set; }


        public CSBF1TextureFlags(byte[] rawData, ref int index)
        {
            this.Flags = CGeneric.GiveMeArray(rawData, index, 4);
            index += 4;

            //flags déterminés
            this.IsCallbackSuppressed =          CGeneric.GetBitInMask(FlagsInt, 0x00000001);
            this.IsRenderedLate =                CGeneric.GetBitInMask(FlagsInt, 0x00000002);
            this.IsHidden =                      CGeneric.GetBitInMask(FlagsInt, 0x00000004);
            this.IsCallbackObject =              CGeneric.GetBitInMask(FlagsInt, 0x00000008);
            this.IsBifBlendAlphaForcedToZero =   CGeneric.GetBitInMask(FlagsInt, 0x00000010);
            this.IsBifPingPongAnimationEnabled = CGeneric.GetBitInMask(FlagsInt, 0x00000020);
            this.HasBifFields94And98 =           CGeneric.GetBitInMask(FlagsInt, 0x00000040);
            this.IsFramePreservedOnShow =        CGeneric.GetBitInMask(FlagsInt, 0x00000080);
            this.UsesCenteredBifAnchor =          CGeneric.GetBitInMask(FlagsInt, 0x00000100);
            this.IsBifFlag1000Enabled =          CGeneric.GetBitInMask(FlagsInt, 0x00000200);
            this.HasBifBlendAlphaOverride =      CGeneric.GetBitInMask(FlagsInt, 0x00000400);
            this.IsBifPreparedOnActivation =     CGeneric.GetBitInMask(FlagsInt, 0x00000800);
            this.IsNormalBifRenderingSkipped =   CGeneric.GetBitInMask(FlagsInt, 0x00001000);
            this.IsBifFlag10000Enabled =         CGeneric.GetBitInMask(FlagsInt, 0x00002000);


        }

        public void UpdateFlags()
        {
            var newFlags = this.FlagsInt;
            CGeneric.SetBitInMask(ref newFlags, 0x00000001, this.IsCallbackSuppressed);
            CGeneric.SetBitInMask(ref newFlags, 0x00000002, this.IsRenderedLate);
            CGeneric.SetBitInMask(ref newFlags, 0x00000004, this.IsHidden);
            CGeneric.SetBitInMask(ref newFlags, 0x00000008, this.IsCallbackObject);
            CGeneric.SetBitInMask(ref newFlags, 0x00000010, this.IsBifBlendAlphaForcedToZero);
            CGeneric.SetBitInMask(ref newFlags, 0x00000020, this.IsBifPingPongAnimationEnabled);
            CGeneric.SetBitInMask(ref newFlags, 0x00000040, this.HasBifFields94And98);
            CGeneric.SetBitInMask(ref newFlags, 0x00000080, this.IsFramePreservedOnShow);
            CGeneric.SetBitInMask(ref newFlags, 0x00000100, this.UsesCenteredBifAnchor);
            CGeneric.SetBitInMask(ref newFlags, 0x00000200, this.IsBifFlag1000Enabled);
            CGeneric.SetBitInMask(ref newFlags, 0x00000400, this.HasBifBlendAlphaOverride);
            CGeneric.SetBitInMask(ref newFlags, 0x00000800, this.IsBifPreparedOnActivation);
            CGeneric.SetBitInMask(ref newFlags, 0x00001000, this.IsNormalBifRenderingSkipped);
            CGeneric.SetBitInMask(ref newFlags, 0x00002000, this.IsBifFlag10000Enabled);

            this.Flags = CGeneric.ConvertIntToByteArray(newFlags);
        }
    }
}