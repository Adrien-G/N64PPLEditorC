using System;
using System.IO;
using System.Text;

namespace N64PPLEditorC.ManagementAudio
{
    public static class WavPcmReader
    {
        public static WavPcmData Read(byte[] wavData)
        {
            if (wavData == null)
                throw new ArgumentNullException(nameof(wavData));

            if (wavData.Length < 12)
                throw new InvalidDataException("WAV file is too short.");

            ushort audioFormat = 0;
            ushort channels = 0;
            ushort bitsPerSample = 0;
            int sampleRate = 0;

            byte[] pcmBytes = null;
            int? loopStart = null;
            int? loopEnd = null;

            using (MemoryStream stream = new MemoryStream(wavData))
            using (BinaryReader reader = new BinaryReader(stream, Encoding.ASCII))
            {
                string riff = ReadFourCC(reader);
                reader.ReadUInt32();
                string wave = ReadFourCC(reader);

                if (riff != "RIFF" || wave != "WAVE")
                    throw new InvalidDataException("The selected file is not a RIFF/WAVE file.");

                while (stream.Position + 8 <= stream.Length)
                {
                    string chunkId = ReadFourCC(reader);
                    uint chunkSize = reader.ReadUInt32();

                    long chunkDataPosition = stream.Position;
                    long chunkEnd = chunkDataPosition + chunkSize;

                    if (chunkEnd > stream.Length)
                        throw new InvalidDataException($"Invalid WAV chunk '{chunkId}'.");

                    if (chunkId == "fmt ")
                    {
                        if (chunkSize < 16)
                            throw new InvalidDataException("Invalid WAV fmt chunk.");

                        audioFormat = reader.ReadUInt16();
                        channels = reader.ReadUInt16();
                        sampleRate = checked((int)reader.ReadUInt32());

                        reader.ReadUInt32(); // Byte rate
                        reader.ReadUInt16(); // Block align
                        bitsPerSample = reader.ReadUInt16();
                    }
                    else if (chunkId == "data" && pcmBytes == null)
                    {
                        pcmBytes = reader.ReadBytes(checked((int)chunkSize));
                    }
                    else if (chunkId == "smpl" && chunkSize >= 60)
                    {
                        // Nombre de boucles à l'offset 28
                        stream.Position = chunkDataPosition + 28;

                        uint loopCount = reader.ReadUInt32();

                        if (loopCount > 0)
                        {
                            // Première boucle :
                            // +36 cue ID
                            // +40 type
                            // +44 start
                            // +48 end
                            stream.Position = chunkDataPosition + 44;

                            loopStart = checked((int)reader.ReadUInt32());

                            // WAV = fin inclusive.
                            loopEnd = checked((int)reader.ReadUInt32() + 1);
                        }
                    }

                    stream.Position = chunkEnd + (chunkSize & 1u);
                }
            }

            if (audioFormat != 1)
                throw new NotSupportedException("Only uncompressed PCM WAV files are supported.");

            if (channels != 1)
                throw new NotSupportedException("Only mono WAV files are currently supported.");

            if (bitsPerSample != 16)
                throw new NotSupportedException("Only 16-bit WAV files are currently supported.");

            if (sampleRate <= 0)
                throw new InvalidDataException("Invalid WAV sampling rate.");

            if (pcmBytes == null)
                throw new InvalidDataException("The WAV file has no data chunk.");

            if ((pcmBytes.Length & 1) != 0)
                throw new InvalidDataException("Invalid 16-bit PCM data length.");

            short[] samples = new short[pcmBytes.Length / 2];

            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = unchecked((short)(pcmBytes[i * 2] | (pcmBytes[i * 2 + 1] << 8)));
            }

            return new WavPcmData
            {
                SampleRate = sampleRate,
                Samples = samples,
                LoopStart = loopStart,
                LoopEnd = loopEnd
            };
        }

        private static string ReadFourCC(BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(4);

            if (bytes.Length != 4)
                throw new EndOfStreamException();

            return Encoding.ASCII.GetString(bytes);
        }
    }
}