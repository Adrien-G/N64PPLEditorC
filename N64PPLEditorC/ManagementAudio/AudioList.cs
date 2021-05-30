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

        public int GetSizeOfAllAudio()
        {
            int totalSize = 0;
            foreach(SoundBank soundb in soundBankList)
            totalSize += soundb.ptrTable.Length + soundb.waveTable.Length + soundb.GetRawSongs().Length/*soundb.midi.Length*/ + soundb.sfx.Length;

            return totalSize;
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
                if(i == 0)//for the first soundBank, give 
                {
                    byte[] startingEndingAddress = CGeneric.GiveMeArray(rawData,RomLangAddress.GetMidiSongSoundBank0(),0x238);
                    soundBankList.Add(new SoundBank(CGeneric.GiveMeArray(rawData, position, sizeOfSoundBank), soundBankList.Count,position,startingEndingAddress));
                }
                else
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

        public void ReplaceSong(byte[] songData,int indexSbf,int indexSong)
        {
            soundBankList[indexSbf].songList[indexSong].rawData = Set16BitAlignment(songData);
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

            //update address of midi (SoundBank 0) by making the difference between old position and new position
            this.finalIndexAudioStart = (int)fs.Position;


            //Array.Copy(rawData, RomLangAddress.GetMidiSongSoundBank0(), addressHeaderSb0, 0, addressHeaderSb0.Length);

            //set the good position of song, and write to rom
            int valueToStore1 = 0;
            int lengthData = 0;
            int adder = 0;
            fs.Position = RomLangAddress.GetMidiSongSoundBank0();
            for (int i = 0; i < soundBankList[0].songList.Count; i++)
            {
                //finalindexaudiostart + ptrlength+wavedatalength
                valueToStore1 = finalIndexAudioStart + soundBankList[0].ptrTable.Length + soundBankList[0].waveTable.Length + adder;
                lengthData = soundBankList[0].songList[i].rawData.Length;
                fs.Write(CGeneric.ConvertIntToByteArray(valueToStore1), 0, 4);
                fs.Write(CGeneric.ConvertIntToByteArray(valueToStore1 + lengthData), 0, 4);
                adder += lengthData;
            }

            /*for (int i = 0; i < addressHeaderSb0.Length; i += 4)
            {
                int tmpValue = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(addressHeaderSb0, i, 4));
                if (finalIndexAudioStart > initialIndexAudioStart)
                    tmpValue += finalIndexAudioStart - initialIndexAudioStart;
                else
                    tmpValue -= initialIndexAudioStart - finalIndexAudioStart;
                byte[] tmpBytesNew = CGeneric.ConvertIntToByteArray(tmpValue);
                for(int j = 0; j < tmpBytesNew.Length; j++)
                    addressHeaderSb0[i + j] = tmpBytesNew[j];
                
            }*/

            //write rawData soundBank
            fs.Position = finalIndexAudioStart;

            //write ptrData, waveTable, midi and sfx
            for (int i = 0; i <= 0x15; i++)
            {
                //write ptrData and update position
                soundBankList[i].address.PtrTable = CGeneric.ConvertIntToByteArray((int)fs.Position);
                fs.Write(soundBankList[i].ptrTable, 0, soundBankList[i].ptrTable.Length);

                //write waveTable and update position
                soundBankList[i].address.WaveTable = CGeneric.ConvertIntToByteArray((int)fs.Position);
                fs.Write(soundBankList[i].waveTable, 0, soundBankList[i].waveTable.Length);

                //write midi song and update sfx position ! (will only change the position of soundbank 1 and 2, the others doesn't have midi)
                soundBankList[i].address.Sfx = CGeneric.ConvertIntToByteArray((int)fs.Position);
                fs.Write(soundBankList[i].GetRawSongs()/*midi*/, 0, soundBankList[i].GetRawSongs().Length/*midi.Length*/);

                //write sfx and update position
                fs.Write(soundBankList[i].sfx, 0, soundBankList[i].sfx.Length);

                //write end position
                soundBankList[i].address.end = CGeneric.ConvertIntToByteArray((int)fs.Position);
            }

            long savedFsPosition = fs.Position;

            //update address in the header (first part)
            for (int i = 0; i <= 0x15; i++)
            {
                fs.Position = RomLangAddress.GetSoundBankPtrTable()[i];
                fs.Write(soundBankList[i].address.PtrTable, 0, 4);

                fs.Write(soundBankList[i].address.WaveTable, 0, 4);
                if (i == 0 || i == 2)
                {
                    if (i == 2)
                    {
                        fs.Position -= 0x38;
                        fs.Write(soundBankList[i].address.Sfx, 0, 4); // in fact, it's the start of midi
                        fs.Write(soundBankList[i].address.end, 0, 4); // and it's end here (and not the sfx)
                        fs.Position += 0x30;
                    }
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
                fs.Position = RomLangAddress.GetSoundBankPtrTable()[i] + 0x168;
                fs.Write(soundBankList[i].address.PtrTable, 0, 4);
                fs.Write(soundBankList[i].address.WaveTable, 0, 4);
                fs.Write(soundBankList[i].address.Sfx, 0, 4);
                fs.Write(soundBankList[i].address.end, 0, 4);
                fs.Write(soundBankList[i].address.WaveTable, 0, 4);

            }

            fs.Position = savedFsPosition;
        }
    }
}
