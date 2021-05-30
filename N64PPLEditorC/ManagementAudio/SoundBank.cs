using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC.ManagementAudio
{
    public class SoundBank
    {
        public byte[] ptrTable;
        public byte[] waveTable;
        private byte[] midi;
        public byte[] sfx;
        public List<Sound> soundList;
        public List<Song> songList;
        public int nbInstruments;
        private int id;

        //only used for soundBank0 and soundbank1 (for separate song)
        public int positionPtrStart;
        public byte[] songDataAddress;

        public struct SoundBankAddressStruct
        {
            public byte[] PtrTable;
            public byte[] WaveTable;
            public byte[] Sfx;
            public byte[] end;
        }

        private byte[] rawData;

        public SoundBankAddressStruct address;

        public SoundBank(byte[] rawData, int id)
        {
            address = new SoundBankAddressStruct();
            soundList = new List<Sound>();
            this.rawData = rawData;
            this.id = id;
            ChunkRawData(id);
            DecomposeSounds();
        }

        public SoundBank(byte[] rawData,int id,int positionPtrStart,byte[] startingEndingAddress)
        {
            address = new SoundBankAddressStruct();
            soundList = new List<Sound>();
            songList = new List<Song>();
            this.rawData = rawData;
            this.positionPtrStart = positionPtrStart;
            this.songDataAddress = startingEndingAddress;
            ChunkRawData(id);
            DecomposeSounds();
        }

        private void DecomposeSounds()
        {
            nbInstruments = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable, 0x20, 4));

            int indexSoundPtr = 0;
            int soundLength = 0;
            for (int i = 0; i < nbInstruments; i++)
            {
                Sound sound = new Sound();
                
                sound.start = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable,indexSoundPtr + 0x30, 4));
                soundLength = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable,indexSoundPtr + 0x34, 4));

                //grab sound data
                sound.rawData = CGeneric.GiveMeArray(waveTable, sound.start, soundLength);

                sound.loop.offset =      CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable, indexSoundPtr + 0x3C, 4));
                sound.predictor.offset = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable, indexSoundPtr + 0x40, 4));

                //loop
                if (sound.loop.offset != 0)
                {
                    sound.loop.start = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable, sound.loop.offset, 4));
                    sound.loop.end = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable, sound.loop.offset + 4, 4));
                    sound.loop.count = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable, sound.loop.offset + 8, 4));
                    sound.loop.state = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable, sound.loop.offset + 0xC, 0x20));
                }

                //predictors
                sound.predictor.order       = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable, sound.predictor.offset, 4));
                sound.predictor.nPredictors = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(ptrTable, sound.predictor.offset+4, 4));
                sound.predictor.predictors   = CGeneric.GiveMeArray(ptrTable, sound.predictor.offset + 8, sound.predictor.order * sound.predictor.nPredictors * 8 * 2);

                sound.predictor.predictorShort = new short[sound.predictor.predictors.Length / 2];
                for (int j = 0; j < sound.predictor.predictorShort.Length; j++)
                    sound.predictor.predictorShort[j] = BitConverter.ToInt16(new[] {sound.predictor.predictors[j*2+1], sound.predictor.predictors[j * 2] }, 0);

                if (sound.loop.offset != 0)
                    indexSoundPtr = sound.loop.offset;
                else
                    indexSoundPtr += 0xA0;
                soundList.Add(sound);
            }
        }



        //chunk RawData into PtrTable, WaveTable and Sfx
        private void ChunkRawData(int id)
        {

            //initialized to 16 to avoid first soundbank set to 0..
            int sizeOfPtrTable=0;

            //searching waveTable (end of PtrTable) and write ptrTable rawData
            sizeOfPtrTable = CGeneric.SearchBytesInArray(rawData, CGeneric.patternN64WaveTable, 0,16, 16);
            ptrTable = CGeneric.GiveMeArray(rawData, 0, sizeOfPtrTable);

            //waveTable = CGeneric.GiveMeArray(rawData,ptrTable.Length,rawData.Length - ptrTable.Length);

            int sizeSfx = 0;
            int sizeMidi = 0;

            //searching midi and sfx
            switch (id)
            {
                case 0:
                    sizeMidi = DecomposeSong();
                    break;
                //there is a midi song and sfx, so with a unknow wavetable length, we need to determine sfx and midi length..
                case 1:
                    var startMidi = CGeneric.SearchBytesInArray(rawData, CGeneric.patternMidiSoundBank1, 0, 0, 16);
                    sizeMidi = RomLangAddress.GetSizeMidiSoundBank1();
                    sizeSfx = rawData.Length - (startMidi + sizeMidi);
                    break;
                //there is song but no sfx
                case 2:
                    sizeMidi = RomLangAddress.GetSizeMidiSoundBank2();
                    break;
                //standard computed sfx size and no midi file (dirty check, but seems working pretty well).
                default:
                    int tmpSfxEqualValue;
                    int tmpSfxEqualValue2;
                    int tmpSfxEqualValue3;
                    int tmpSfxNull;

                    for (int i = ptrTable.Length; i < rawData.Length - 16; i += 16)
                    {
                        //quick test, for avoiding cpu consuption
                        tmpSfxNull = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, i + 12, 4));
                        if (tmpSfxNull == 0)
                        {
                            tmpSfxEqualValue = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, i, 4));
                            tmpSfxEqualValue2 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, i + 4, 4));
                            tmpSfxEqualValue3 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, i + 8, 4));
                            var tmpSfxEqualValue4 = tmpSfxEqualValue3++;
                            if (tmpSfxEqualValue != 0 && tmpSfxEqualValue == tmpSfxEqualValue2 && (tmpSfxEqualValue2 == tmpSfxEqualValue4 || tmpSfxEqualValue2 == tmpSfxEqualValue3))
                            {
                                sizeSfx = rawData.Length - i;
                                break;
                            }
                        }
                    }
                    break;
            }

            //store date with size found previously
            waveTable = CGeneric.GiveMeArray(rawData, ptrTable.Length, rawData.Length - (ptrTable.Length + sizeMidi + sizeSfx));
            midi = CGeneric.GiveMeArray(rawData, ptrTable.Length + waveTable.Length, sizeMidi);
            sfx = CGeneric.GiveMeArray(rawData, ptrTable.Length + waveTable.Length + sizeMidi, sizeSfx);
        }

        public byte[] GetRawSongs()
        {
            if (this.id == 0)
            {
                var dataLength = 0;
                //get data length, for faster concat
                for (int i = 0; i < songList.Count; i++)
                {
                    dataLength += songList[i].rawData.Length;
                }


                byte[] midiData = new byte[dataLength];
                int index = 0;

                //do fast concat
                for (int i = 0; i < songList.Count; i++)
                {
                    var songRawData = CGeneric.GiveMeArray(songList[i].rawData, 0, songList[i].rawData.Length);
                    for(int j = 0; j < songList[i].rawData.Length; j++)
                    {
                        midiData[index + j] = songRawData[j];
                    }
                    index += songList[i].rawData.Length;
                }
                return midiData;
            }
            else
                return midi;
        }

        private int DecomposeSong()
        {
            int songTotalLength = 0;
            for(int i = 0; i < songDataAddress.Length; i+=8)
            {
                //starting address song0
                int songAddress = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(songDataAddress,i,4));
                int songLength = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(songDataAddress, i+4, 4)) - songAddress;

                //grab the start address of song
                byte[] songRawData = CGeneric.GiveMeArray(rawData, songAddress - positionPtrStart, songLength);

                Song song = new Song(songRawData);
                songList.Add(song);
                songTotalLength += songLength;
            }
            return songTotalLength;
        }
    }
}
