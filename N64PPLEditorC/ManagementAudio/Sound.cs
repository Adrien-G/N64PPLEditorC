using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC.ManagementAudio
{
    public class Sound
    {
        public int start;
        public int index;
        public byte[] rawData;
        public loopData loop;
        public predictorData predictor;
        public int samplingRate;

        public struct loopData
        {
            public int offset;
            public int start;
            public int end;
            public int count;
            public int state;
        }
        
        public struct predictorData
        {
            public int offset;
            public int order;
            public int nPredictors;
            public byte[] predictors;
            public short[] predictorShort;
        }

        public Sound()
        {
            loop = new loopData();
            predictor = new predictorData();
        }

        public byte[] GetWave(int soundBank, int instrument)
        {
            var waveData = DecodeSound();
            return GetWavHeader(soundBank, instrument, waveData.Length).Concat(waveData).Concat(GetWavBottom()).ToArray();
        }

        private byte[] GetWavBottom()
        {
            byte[] wavBottom = CGeneric.patternBottomWavFile;
            if (loop.count > 0)
            {
                wavBottom[0x4] = 0x3C;
                wavBottom[0x24] = 0x01;

                var loopStartByte = CGeneric.ConvertIntToByteArray(loop.start);
                var loopEndByte = CGeneric.ConvertIntToByteArray(loop.end);
                wavBottom[0x34] = loopStartByte[3];
                wavBottom[0x35] = loopStartByte[2];
                wavBottom[0x36] = loopStartByte[1];
                wavBottom[0x37] = loopStartByte[0];
                wavBottom[0x38] = loopEndByte[3];
                wavBottom[0x39] = loopEndByte[2];
                wavBottom[0x3A] = loopEndByte[1];
                wavBottom[0x3B] = loopEndByte[0];

                if(loop.count != -1)
                {
                    var loopCountByte = CGeneric.ConvertIntToByteArray(loop.count);
                    wavBottom[0x40] = loopCountByte[3];
                    wavBottom[0x41] = loopCountByte[2];
                    wavBottom[0x42] = loopCountByte[1];
                    wavBottom[0x43] = loopCountByte[0];
                }
            }
            else
            {
                wavBottom[0x4] = 0x24;
            }
            //keybase to add
            wavBottom[0x14] = 0x3C;



            return wavBottom;
        }

        private byte[] GetWavHeader(int soundBank, int instrument,int lengthWave)
        {
            byte[] wavHeader = CGeneric.patternHeaderWavFile;

            int loopHeader = 0;
            if (loop.count > 0) loopHeader = 0x18;

            byte[] fileSize = CGeneric.ConvertIntToByteArray(0x4C + lengthWave + loopHeader);
            wavHeader[0x04] = fileSize[3];
            wavHeader[0x05] = fileSize[2];
            wavHeader[0x06] = fileSize[1];
            wavHeader[0x07] = fileSize[0];

            if (IsHalfSamplingRate(soundBank, instrument)){
                wavHeader[0x18] = 0x11;
                wavHeader[0x19] = 0x2B;
                wavHeader[0x1C] = 0x22;
                wavHeader[0x1D] = 0x56;
            }
            else
            {
                wavHeader[0x18] = 0x22;
                wavHeader[0x19] = 0x56;
                wavHeader[0x1C] = 0x44;
                wavHeader[0x1D] = 0xAC;
            }

            byte[] lengthFile = CGeneric.ConvertIntToByteArray(lengthWave);
            wavHeader[0x28] = lengthFile[3];
            wavHeader[0x29] = lengthFile[2];
            wavHeader[0x2A] = lengthFile[1];
            wavHeader[0x2B] = lengthFile[0];

            return wavHeader;
        }

        private byte[] DecodeSound()
        {
             var shortArray = AudioDecode.Decode(rawData, predictor.order, predictor.predictorShort, predictor.nPredictors);

            byte[] byteArray = new byte[shortArray.Length * 2];
            byte[] tmp = new byte[2];
            for (int i = 0; i < shortArray.Length; i++)
            {
                tmp = CGeneric.ConvertIntToByteArray16bits(shortArray[i]);
                byteArray[i * 2] = tmp[1];
                byteArray[i * 2 + 1] = tmp[0];
            }
            return byteArray;
        }

        private bool IsHalfSamplingRate(int soundBank,int instrument)
        {
            switch (soundBank)
            {
                case 0x0:
                    switch (instrument)
                    {
                        case int n when (n >= 0x9E && n <= 0xA4): return true;
                        default: return false;
                    }
                case 0x1:
                    switch (instrument)
                    {
                        case 0x5C: return false;
                        case int n when (n >= 0xAD && n <= 0xAF): return false;
                        default: return true;
                    }
                case 0x2:
                    switch (instrument)
                    {
                        case int n when (n <= 0x27 || n >= 0x40): return true;
                        default: return false;
                    }
                default: return true;
            }
        }
    }
}
