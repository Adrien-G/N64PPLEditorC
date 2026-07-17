using System;
using System.IO;

namespace N64PPLEditorC.ManagementAudio
{
    public static class N64AdpcmEncoder
    {
        private const int SamplesPerFrame = 16;
        private const int BytesPerFrame = 9;
        private const int MaximumScale = 12;

        public static byte[] Encode(short[] samples, int predictorOrder, short[] predictors, int predictorCount)
        {
            if (samples == null)
                throw new ArgumentNullException(nameof(samples));

            if (predictors == null)
                throw new ArgumentNullException(nameof(predictors));

            if (predictorOrder != 2)
                throw new NotSupportedException("Only order-2 N64 ADPCM codebooks are supported.");

            if (predictorCount <= 0 || predictorCount > 16)
                throw new InvalidDataException("Invalid predictor count.");

            int requiredCoefficientCount = predictorCount * 16;

            if (predictors.Length < requiredCoefficientCount)
                throw new InvalidDataException("The ADPCM codebook is incomplete.");

            int frameCount = (samples.Length + SamplesPerFrame - 1) / SamplesPerFrame;

            byte[] encoded = new byte[frameCount * BytesPerFrame];
            short[] history = new short[8];
            sbyte[] candidateCodes = new sbyte[16];
            sbyte[] bestCodes = new sbyte[16];
            short[] firstHalf = new short[8];
            short[] secondHalf = new short[8];
            short[] bestSecondHalf = new short[8];

            for (int frame = 0; frame < frameCount; frame++)
            {
                int sampleOffset = frame * SamplesPerFrame;

                long bestError = long.MaxValue;
                int bestPredictor = 0;
                int bestScale = 0;

                for (int predictorIndex = 0; predictorIndex < predictorCount;predictorIndex++)
                {
                    int predictorOffset = predictorIndex * 16;

                    for (int scale = 0; scale <= MaximumScale; scale++)
                    {
                        EncodeHalf(samples,sampleOffset,history,predictors,predictorOffset,scale,candidateCodes,0,firstHalf);
                        EncodeHalf(samples,sampleOffset + 8,firstHalf,predictors,predictorOffset,scale,candidateCodes,8,secondHalf);

                        long error = CalculateError(samples,sampleOffset,firstHalf,secondHalf);

                        if (error < bestError)
                        {
                            bestError = error;
                            bestPredictor = predictorIndex;
                            bestScale = scale;

                            Array.Copy(candidateCodes,bestCodes,16);

                            Array.Copy(secondHalf, bestSecondHalf,8);
                        }
                    }
                }

                int outputOffset = frame * BytesPerFrame;

                encoded[outputOffset] = (byte)( (bestScale << 4) | bestPredictor);

                for (int i = 0; i < 16; i += 2)
                {
                    int high = bestCodes[i] & 0x0F;
                    int low =  bestCodes[i + 1] & 0x0F;

                    encoded[outputOffset + 1 + i / 2] = (byte)((high << 4) | low);
                }

                Array.Copy( bestSecondHalf,history,8);
            }

            return encoded;
        }

        private static void EncodeHalf(short[] source,int sourceOffset,short[] history,short[] predictors,int predictorOffset,int scale,sbyte[] codes,int codeOffset,short[] decoded)
        {
            int step = 1 << scale;

            for (int i = 0; i < 8; i++)
            {
                short target = GetSample( source, sourceOffset + i);
                long total =(long)predictors[predictorOffset + i] * history[6];

                total += (long)predictors[predictorOffset + i + 8] *history[7];

                for (int previous = 0;previous < i;previous++)
                {
                    long residual = (long)codes[codeOffset + previous] *step;
                    int coefficientIndex = predictorOffset +(i - 1 - previous) + 8;
                    total += residual * predictors[coefficientIndex];
                }

                long predicted = total >> 11;

                double idealCode = (target - predicted) / (double)step;

                int code = (int)Math.Round(idealCode, MidpointRounding.AwayFromZero);

                code = Math.Max(-8, Math.Min(7, code));

                long reconstructed = predicted + (long)code * step;

                decoded[i] = ClampToInt16(reconstructed);

                codes[codeOffset + i] = (sbyte)code;
            }
        }

        private static long CalculateError(short[] source,int sourceOffset,short[] firstHalf,short[] secondHalf)
        {
            long error = 0;

            for (int i = 0; i < 16; i++)
            {
                int expected = GetSample(source,sourceOffset + i);
                int actual = i < 8 ? firstHalf[i] : secondHalf[i - 8];
                long difference = expected - actual;
                error += difference * difference;
            }

            return error;
        }

        private static short GetSample( short[] source, int index)
        {
            if (index < 0 ||index >= source.Length)
                return 0;

            return source[index];
        }

        private static short ClampToInt16(long value)
        {
            if (value > short.MaxValue)
                return short.MaxValue;

            if (value < short.MinValue)
                return short.MinValue;

            return (short)value;
        }
    }
}