using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC.ManagementAudio
{
    public class AudioList
    {
        //partie rawData
        public List<SoundBank> soundBankList { get; private set; }

        //partie addresses
        //private List<> soundBankAddress;

        private int[] soundBankPtrTableAddressFr = { 
            0xB4D7C,
            0xB4D40,
            0xB4D68,
            0xB4EE4, 
            0xB4D90,
            0xB4DA4,
            0xB4DB8, 
            0xB4DCC, 
            0xB4DE0, 
            0xB4DF4, 
            0xB4E08, 
            0xB4E1C, 
            0xB4E30, 
            0xB4E44, 
            0xB4E58, 
            0xB4E6C, 
            0xB4E80, 
            0xB4E94, 
            0xB4EA8, 
            0xB4EBC, 
            0xB4ED0, 
            0xB4D54 
        };

        private byte[] rawData;

        

        private int initialIndexAudioStart;
        private int finalIndexAudioStart;

        public AudioList(byte[] rawData, int indexAudioStart)
        {
            soundBankList = new List<SoundBank>();
            this.initialIndexAudioStart = indexAudioStart;
            this.rawData = rawData;
            ChunkSoundBank();
        }

        //search for N64 PtrTable V2 for cutting soundbank
        private void ChunkSoundBank()
        {
            byte[] tmpRawDataN64PtrTable = new byte[16];

            //initialized to 16 to avoid first soundbank set to 0..
            int sizeOfSoundBank = 16;

            //searching N64PtrTableV2 pattern (each one will be stored in a soundbank)
            int position = initialIndexAudioStart;
            int positionNext = 0;
            for (int i =0; i <= 0x15; i++)
            {
                
                positionNext = CGeneric.SearchBytesInArray(rawData, CGeneric.patternN64PtrTableV2, 0, position + 16, 16);

                //if it's the last, don't search the next, just search the end of the rom
                if(positionNext == -1)
                    positionNext = CGeneric.SearchBytesInArray(rawData, CGeneric.endOfRom, 0, position + 16, 16);

                sizeOfSoundBank = positionNext - position;
                soundBankList.Add(new SoundBank(CGeneric.GiveMeArray(rawData, position, sizeOfSoundBank), soundBankList.Count));

                position += sizeOfSoundBank;
                sizeOfSoundBank = 16;
            }
        }

        public void ReplaceSoundBank(byte[] ptr, byte[] wave, byte[] sfx, int index)
        {
            ReplacePointerTable(ptr, index);
            ReplaceWaveTable(wave, index);
            ReplaceSfxTable(sfx, index);
        }

        public void ReplacePointerTable(byte[] data, int index)
        {
            soundBankList[index].ptrTable = Set16BitAlignment(data);
        }
        public void ReplaceWaveTable(byte[] data, int index)
        {
            soundBankList[index].waveTable = Set16BitAlignment(data);
        }
        public void ReplaceSfxTable(byte[] data, int index)
        {
            soundBankList[index].sfx = Set16BitAlignment(data);
        }

        private byte[] Set16BitAlignment(byte[] data)
        {
            //add 00 byte for 16 bit alignment and return it
            int difference = 16 - (data.Length % 16);

            byte[] finalArray = new byte[data.Length + difference];
            Array.Copy(data, 0, finalArray, 0, data.Length);

            return finalArray;
        }


        public void WriteAllData(FileStream fs)
        {
            //set 16bit alignment for first soundbank 
            int alignment = 16 - ((int)fs.Position % 16);
            for (int i = 0; i < alignment; i++)
                fs.WriteByte(0);

            //update address in the header (SoundBank 0)
            //by making the difference between old position and new position
            this.finalIndexAudioStart = (int)fs.Position;

            byte[] addressHeaderSb0 = new byte[0x238];
            Array.Copy(rawData, 0xB4B00, addressHeaderSb0, 0, addressHeaderSb0.Length);

            //convert to int, add the difference, convert to byte, store the new value
            for (int i = 0; i < addressHeaderSb0.Length; i += 4)
            {
                int tmpValue = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(addressHeaderSb0, i, 4));
                if (finalIndexAudioStart > initialIndexAudioStart)
                    tmpValue += finalIndexAudioStart - initialIndexAudioStart;
                else
                    tmpValue -= initialIndexAudioStart - finalIndexAudioStart;
                byte[] tmpBytesNew = CGeneric.ConvertIntToByteArray(tmpValue);
                for(int j = 0; j < tmpBytesNew.Length; j++)
                    addressHeaderSb0[i + j] = tmpBytesNew[j];
            }

            //set the good position, and write to rom
            fs.Position = 0xB4B00;
            fs.Write(addressHeaderSb0, 0, addressHeaderSb0.Length);

            //write rawData soundBank
            fs.Position = finalIndexAudioStart;

            //write ptrData, waveTable and sfx
            for (int i = 0; i <= 0x15; i++)
            {
                //write ptrData and update position
                soundBankList[i].address.PtrTable = CGeneric.ConvertIntToByteArray((int)fs.Position);
                fs.Write(soundBankList[i].ptrTable, 0, soundBankList[i].ptrTable.Length);

                //write waveTable and update position
                soundBankList[i].address.WaveTable = CGeneric.ConvertIntToByteArray((int)fs.Position);
                fs.Write(soundBankList[i].waveTable, 0, soundBankList[i].waveTable.Length);

                //write sfx and update position
                soundBankList[i].address.Sfx = CGeneric.ConvertIntToByteArray((int)fs.Position);
                fs.Write(soundBankList[i].sfx, 0, soundBankList[i].sfx.Length);

                //write end position
                soundBankList[i].address.end = CGeneric.ConvertIntToByteArray((int)fs.Position);
            }

            //update address in the header (first part)
            for (int i = 0; i <= 0x15; i++)
            {
                fs.Position = soundBankPtrTableAddressFr[i];
                fs.Write(soundBankList[i].address.PtrTable, 0, 4);

                fs.Write(soundBankList[i].address.WaveTable, 0, 4);
                if (i == 0 || i == 2)
                {
                    byte[] nullBytes = new byte[4];
                        fs.Write(nullBytes, 0, 4);
                        fs.Write(nullBytes, 0, 4);
                }
                else
                {
                    fs.Write(soundBankList[i].address.Sfx, 0, 4);

                    //small hack.. weird part in the rom...
                    if ( i != 1)
                        fs.Write(soundBankList[i].address.end, 0, 4);
                    else
                    {
                        int oldValue = CGeneric.ConvertByteArrayToInt(soundBankList[i].address.end);
                        byte[] newValue = CGeneric.ConvertIntToByteArray(oldValue - 0x470); 
                        fs.Write(newValue, 0, 4);
                    }
                }
                fs.Write(soundBankList[i].address.WaveTable, 0, 4);
            }

            //update address in the header(second part)
            for (int i = 4; i <= 0x14; i++)
            {
                fs.Position = soundBankPtrTableAddressFr[i] + 0x168;
                fs.Write(soundBankList[i].address.PtrTable, 0, 4);
                fs.Write(soundBankList[i].address.WaveTable, 0, 4);
                fs.Write(soundBankList[i].address.Sfx, 0, 4);
                fs.Write(soundBankList[i].address.end, 0, 4);
                fs.Write(soundBankList[i].address.WaveTable, 0, 4);

            }
        }
    }
}
