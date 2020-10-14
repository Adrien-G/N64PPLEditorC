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
        public byte[] midi;
        public byte[] sfx;
       

        public struct SoundBankAddressStruct
        {
            public byte[] PtrTable;
            public byte[] WaveTable;
            public byte[] Sfx;
            public byte[] end;
        }

        private byte[] rawData;

        public SoundBankAddressStruct address;
        public SoundBank(byte[] rawData,int id)
        {
            address = new SoundBankAddressStruct();
            this.rawData = rawData;
            ChunkRawData(id);
        }

        //chunk RawData into PtrTable, WaveTable and Sfx
        private void ChunkRawData(int id)
        {

            byte[] tmpRawDataN64WaveTable = new byte[16];

            //initialized to 16 to avoid first soundbank set to 0..
            int sizeOfPtrTable = 0;

            //searching waveTable (end of PtrTable) and write ptrTable rawData
            sizeOfPtrTable = CGeneric.SearchBytesInArray(rawData, CGeneric.patternN64WaveTable, 0,16, 16);
            ptrTable = CGeneric.GiveMeArray(rawData, 0, sizeOfPtrTable);

            int sizeSfx = 0;
            int sizeMidi = 0;

            //searching midi and sfx
            switch (id)
            {
                //there is only midi, no sfx.. (leave this soundbank, maybe managing later)
                case 0:
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
                        tmpSfxNull = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, i+12, 4));
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
            waveTable = CGeneric.GiveMeArray(rawData,ptrTable.Length,rawData.Length - (ptrTable.Length + sizeMidi + sizeSfx));
            midi = CGeneric.GiveMeArray(rawData, ptrTable.Length + waveTable.Length, sizeMidi);
            sfx = CGeneric.GiveMeArray(rawData, ptrTable.Length + waveTable.Length + sizeMidi, sizeSfx);
        }
    }
}
