using System;
using System.Collections.Generic;

namespace N64PPLEditorC
{
    public class CSBF1TextureObject
    {
        public CSBF1TextureFlags Flags { get; set; }
        public CSBF1TextureBase Base {get;set;}
        public CSBF1TextureAnimation Animation {get;set;}

        public int BifRessourceIndex { get; set;}

        //détermine potentiellement la taille de la texture
        public int BifField14InitialValue { get; set; }
        public int BifField94InitialValue { get; set; }
        public int BifField98InitialValue { get; set; }

        //old
        public bool isCompressedTexture;
        //old
        public byte transparency;

        //old
        public bool transparencybit;

        public CSBF1TextureObject(byte[] rawData,ref int globalIndex)
        {
            decomposeData(rawData, ref globalIndex);
        }

        private void decomposeData(byte[] rawData, ref int index)
        {
            this.Flags = new CSBF1TextureFlags(rawData, ref index);

            this.Base = new CSBF1TextureBase(rawData,ref index);
            this.Animation = new CSBF1TextureAnimation(rawData,ref index);
            Base.AddId(rawData, ref index);
            Animation.AddLinkedtextureId(rawData, ref index);

            this.BifRessourceIndex = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
            index += 4;

            if (Flags.HasBifField14InitialValue)
            {
                BifField14InitialValue = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
                index += 4;
            }
            if (Flags.HasBifFields94And98)
            {
                BifField94InitialValue = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
                BifField98InitialValue = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index+4, 4));
                index += 8;
            }
        }

        public byte[] RecomposeRawData()
        {
            var rawData = new List<byte>();

            //Flags
            this.Flags.UpdateFlags();
            rawData.AddRange(this.Flags.Flags);

            //Base
            rawData.AddRange(CGeneric.ConvertIntToByteArray(this.Base.X));
            rawData.AddRange(CGeneric.ConvertIntToByteArray(this.Base.Y));

            //Animation
            rawData.AddRange(CGeneric.ConvertIntToByteArray(this.Animation.InitialFrameIndex));

            //Id
            rawData.AddRange(CGeneric.ConvertIntToByteArray(this.Base.Id));

            //linkedTextureId
            rawData.AddRange(CGeneric.ConvertIntToByteArray(this.Animation.LinkedTextureId));

            //bif ressource Index
            rawData.AddRange(CGeneric.ConvertIntToByteArray(this.BifRessourceIndex));

            //additionnal
            if(this.Flags.HasBifField14InitialValue)
                rawData.AddRange(CGeneric.ConvertIntToByteArray(this.BifField14InitialValue));

            if (this.Flags.HasBifFields94And98)
            {
                rawData.AddRange(CGeneric.ConvertIntToByteArray(this.BifField94InitialValue));
                rawData.AddRange(CGeneric.ConvertIntToByteArray(this.BifField98InitialValue));
            }

            return rawData.ToArray();
        }

        public int getTextureIndex()
        {
            return BifRessourceIndex;
        }

        //TODO a refacto entièrement
        public void SetTransparencyValue(bool activated,byte value)
        {
            transparencybit = activated; 
            if (activated)
                this.transparency = value;
        }



     
    }
}
