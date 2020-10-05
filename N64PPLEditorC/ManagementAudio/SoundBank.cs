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
        public byte[] sfx;

        public struct SoundBankAddressStruct
        {
            public byte[] PtrTable;
            public byte[] WaveTable;
            public byte[] Sfx;
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

            //searching sfx
            switch (id)
            {
                //0 or 2, there is no sfx
                case 0:
                case 2:
                    break;
                // manually set sfx Size
                case 1:
                    sizeSfx = 0x1B40;
                    break;
                
                //standard computed sfx size (dirty check, but seems working pretty well).
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

                            if (tmpSfxEqualValue == tmpSfxEqualValue2 && (tmpSfxEqualValue2 == tmpSfxEqualValue3 || tmpSfxEqualValue2 == tmpSfxEqualValue3 - 1))
                            {
                                sizeSfx = rawData.Length - ptrTable.Length - i;
                                break;
                            }
                        }
                    }
                    break;
            }

            //store date with size found previously
            waveTable = CGeneric.GiveMeArray(rawData,ptrTable.Length,rawData.Length - ptrTable.Length - sizeSfx);
            sfx = CGeneric.GiveMeArray(rawData, ptrTable.Length + waveTable.Length, sizeSfx);
        }

        public byte[] WriteAllData()
        {
            //beware of the alignment of the data (by defaut 16bit i think)
            return new byte[0];
        }
    }
}
