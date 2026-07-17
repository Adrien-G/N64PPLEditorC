using System;

namespace N64PPLEditorC.ManagementAudio
{
    public static class PcmSampleConverter
    {
        public static short[] Resample(short[] samples, int sourceRate, int targetRate)
        {
            if (samples == null)
                throw new ArgumentNullException(nameof(samples));

            if (sourceRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sourceRate));

            if (targetRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(targetRate));

            if (samples.Length == 0)
                return new short[0];

            if (sourceRate == targetRate)
                return (short[])samples.Clone();

            int outputLength = checked((int)Math.Round(samples.Length * targetRate / (double)sourceRate));

            outputLength = Math.Max(1, outputLength);

            short[] output = new short[outputLength];
            double sourceStep = sourceRate / (double)targetRate;

            for (int i = 0; i < output.Length; i++)
            {
                double sourcePosition = i * sourceStep;
                int leftIndex = (int)sourcePosition;

                if (leftIndex >= samples.Length - 1)
                {
                    output[i] = samples[samples.Length - 1];
                    continue;
                }

                int rightIndex = leftIndex + 1;
                double fraction = sourcePosition - leftIndex;

                double value =
                    samples[leftIndex] * (1.0 - fraction) +
                    samples[rightIndex] * fraction;

                value = Math.Max(short.MinValue, Math.Min(short.MaxValue, value));

                output[i] = (short)Math.Round(value);
            }

            return output;
        }
    }
}