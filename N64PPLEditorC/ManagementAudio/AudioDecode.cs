using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC.ManagementAudio
{
    /// <summary>
    /// Thanks to the N64 sound list tool by SubDrag, Ice Mario, Zoinkity
    /// the decompression routine is based and adapted (with lot modifications) to C# from N64 sound list tool (what a pain :))
    /// </summary>
    static class AudioDecode
    {
        private static readonly short[] itable =
        {
            0,1,2,3,4,5,6,7,
            -8,-7,-6,-5,-4,-3,-2,-1,
        };
        private static short SignExtend(int b, int x)
        {
            int m = 1 << (b - 1); // mask can be pre-computed if b is fixed

            x = x & ((1 << b) - 1);  // (Skip this if bits in x above position b are already zero.)
            return (short)((x ^ m) - m);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wavData">the waveData</param>
        /// <param name="predictorOrder"></param>
        /// <param name="predictors"></param>
        /// <param name="npredictors"></param>
        /// <returns></returns>
        public static short[] Decode(byte[] wavData, int predictorOrder, short[] predictors, int npredictors)
        {
            int index;
            int pred;
            short[] output = new short[wavData.Length * 2];
            short[] tmpOut = new short[8];

            //flip the predictors
            short[] preds = new short[32 * npredictors];
            for (int j = 0; j < (8 * predictorOrder * npredictors); j++)
                preds[j] = predictors[j];

            int indexIn = 0;
            int indexOut = 0;

            int i = (wavData.Length / 9) * 9;
            while (i > 0)
            {
                index = (wavData[indexIn] >> 4) & 0xF;
                pred = (wavData[indexIn] & 0xF);

                i--;

                short[] pred1 = new short[16];
                for (int j = 0; j < 16; j++)
                    pred1[j] = preds[pred * 16 + j];

                indexIn++;
                tmpOut = Decode8(CGeneric.GiveMeArray(wavData, indexIn, 8), index, pred1, tmpOut, pred);
                Array.Copy(tmpOut, 0, output, indexOut, 8);
                indexIn += 4;
                i -= 4;
                indexOut += 8;

                tmpOut = Decode8(CGeneric.GiveMeArray(wavData, indexIn, i == 4 ? i:8), index, pred1, tmpOut, pred);
                Array.Copy(tmpOut, 0, output, indexOut, 8);
                indexIn += 4;
                i -= 4;
                indexOut += 8;

            }

            //trim 00 end padding
            return output.Take(indexOut).ToArray();
        }

        private static short[] Decode8(byte[] input, int index, short[] preds, short[] lastsmp, int pred)
        {
            short[] tmp = new short[8];
            long total = 0;
            short sample = 0;

            short[] output = new short[8];

            int tmpIndex = 0;
            for (int i = 0; i < 8; i++)
            {
                var a = i & 1;
                bool booleanValue = a != 0;

                if (booleanValue)
                {
                    tmp[i] = (short)(itable[(input[tmpIndex] & 0xF)] << index);
                    tmpIndex++;
                }
                else
                    tmp[i] = (short)(itable[((input[tmpIndex] >> 4) & 0xF)] << index);

                tmp[i] = SignExtend(index + 4, tmp[i]);
            }

            for (int i = 0; i < 8; i++)
            {
                total = (preds[i] * lastsmp[6]);
                total += (preds[i + 8] * lastsmp[7]);

                if (i > 0)
                    for (int x = i - 1; x > -1; x--)
                        total += (tmp[((i - 1) - x)] * preds[x + 8]);

                float result = ((tmp[i] << 0xb) + total) >> 0xb;

                if (result > 32767)
                    sample = 32767;
                else if (result < -32768)
                    sample = -32768;
                else
                    sample = (short)result;

                output[i] = (short)sample;
            }

            return output;
        }
    }
}
