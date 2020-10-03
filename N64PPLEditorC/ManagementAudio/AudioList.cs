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
        public List<SoundBank> soundBankList;

        //partie addresses
        private List<SoundBankAddressStruct> soundBankAddress;

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

        public struct SoundBankAddressStruct
        {
            public int PtrTable;
            public int WaveTable;
            public int Sfx;
            public int end;
            public int WaveTable2;
        }

        private int initialIndexAudioStart;
        private int finalIndexAudioStart;

        public AudioList(byte[] rawData, int indexAudioStart)
        {
            soundBankList = new List<SoundBank>();
            this.rawData = rawData;
            ChunkAudio();
        }

        private void ChunkAudio()
        {
            soundBankAddress = new List<SoundBankAddressStruct>();

            //store the address associated to each soundBank
            for (int i = 0; i < soundBankPtrTableAddressFr.Length; i++)
            {
                var soundBanktmp = new SoundBankAddressStruct();

                soundBanktmp.PtrTable   = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i], 4));
                soundBanktmp.WaveTable  = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i] + 4, 4));
                soundBanktmp.Sfx        = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i] + 8, 4));
                soundBanktmp.end        = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i] + 12, 4));
                soundBanktmp.WaveTable2 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i] + 16, 4));

                soundBankAddress.Add(soundBanktmp);

            }

            //store rawData for each soundBank (with the address previously stored)
            for (int i = 0; i < soundBankAddress.Count(); i++)
            {
                var sndBankTmp = new SoundBank();
                int addressPtrTable = soundBankAddress[i].PtrTable;
                int addressWaveTable = soundBankAddress[i].WaveTable;
                int addressSfx = soundBankAddress[i].Sfx;
                int addressEnd = soundBankAddress[i].end;

                sndBankTmp.ptrTableData = CGeneric.GiveMeArray(rawData, addressPtrTable, addressWaveTable - addressPtrTable);

                //specific for soundbank 0 and soundbank2 (sfx is set to 0)
                if(addressSfx != 0)
                {
                    sndBankTmp.waveTableData = CGeneric.GiveMeArray(rawData, addressWaveTable, addressSfx - addressWaveTable);
                    sndBankTmp.bfxData = CGeneric.GiveMeArray(rawData, addressSfx, addressEnd - addressSfx);
                }
                else
                    sndBankTmp.waveTableData = CGeneric.GiveMeArray(rawData, addressWaveTable, soundBankAddress[i + 1].PtrTable -addressWaveTable);

                soundBankList.Add(sndBankTmp);
            }
        }

        public void replacePointerTable(byte[] data, int index)
        {
            //by defaut align to 16 bit (so add difference if necessary)
            int difference = data.Length % 16;
            byte[] finalArray = new byte[data.Length + difference];

            Array.Copy(data, 0, finalArray, 0, data.Length);

            soundBankList[index].ptrTableData = finalArray;
        }
        public void replaceWaveTable(byte[] data, int index)
        {
            //by defaut align to 16 bit (so add difference if necessary)
            int difference = data.Length % 16;
            byte[] finalArray = new byte[data.Length + difference];

            Array.Copy(data, 0, finalArray, 0, data.Length);
            soundBankList[index].waveTableData = finalArray;
        }
        public void replaceSfxTable(byte[] data, int index)
        {
            //by defaut align to 16 bit (so add difference if necessary)
            int difference = data.Length % 16;
            byte[] finalArray = new byte[data.Length + difference];

            Array.Copy(data, 0, finalArray, 0, data.Length);
            soundBankList[index].bfxData = finalArray;
        }

        public void replaceSoundBank(byte[] ptr, byte[] wave, byte[] sfx, int index)
        {
            replacePointerTable(ptr, index);
            replaceWaveTable(wave, index);
            replaceSfxTable(sfx, index);
        }

        public void writeAllData(FileStream fs)
        {
            //write allData
            foreach(SoundBank sndBank in soundBankList)
            {
                fs.Write(sndBank.ptrTableData, 0, sndBank.ptrTableData.Length);
                fs.Write(sndBank.waveTableData, 0, sndBank.waveTableData.Length);
                if(sndBank.bfxData != null)
                    fs.Write(sndBank.bfxData, 0, sndBank.bfxData.Length);
            }

            //update the two list at the start of the rom 
            UpdateAudioAddress(fs);

        }

        private void UpdateAudioAddress(FileStream fs)
        {
            this.finalIndexAudioStart = (int)fs.Position;
            //set manually address for all address

            //grab the difference between initial audio start and now
            int difference;
            if (fs.Position > initialIndexAudioStart)
                difference = (int)(fs.Position) - initialIndexAudioStart;
            else
                difference = initialIndexAudioStart - (int)(fs.Position);


            //soundbank0, read initial values, make the difference and write values

            byte[] addressHeaderSb0 = new byte[0x238];
            Array.Copy(rawData,0, addressHeaderSb0, 0, addressHeaderSb0.Length);


            for (int i = 0; i < 0x238; i += 4)
            {
                int tmpValue = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(addressHeaderSb0, i, 4));
                tmpValue += difference;
                var tmpBytesNew = CGeneric.ConvertIntToByteArray(tmpValue);
                addressHeaderSb0[i] = tmpBytesNew[0];
                addressHeaderSb0[i+1] = tmpBytesNew[1];
                addressHeaderSb0[i+2] = tmpBytesNew[2];
                addressHeaderSb0[i+3] = tmpBytesNew[3];
            }

            fs.Position = 0xB4B00;
            fs.Write(addressHeaderSb0,0,addressHeaderSb0.Length);

            //update all address positions for the soundBank 
            updateSoundBankAddressPosition();

            //write from 0 to 0x15 soundBank address
            writeSoundBankAddress(fs);


        }

        private void writeSoundBankAddress(FileStream fs)
        {
            //set the 0x15 soundband address
            for(int i = 0; i < 0x15; i++)
            {
                fs.Position = soundBankPtrTableAddressFr[i];
                var dataToWrite = convertStructToByteArray(soundBankAddress[i]);
                fs.Write(dataToWrite, 0, dataToWrite.Length);
            }

            //TODO redefine special soundbank 2 & 3 address


            //special hack for second address loop (add 0x168)
            for (int i = 4; i < 0x14; i++)
            {
                fs.Position = soundBankPtrTableAddressFr[i]+0x168;
                var dataToWrite = convertStructToByteArray(soundBankAddress[i]);
                fs.Write(dataToWrite, 0, dataToWrite.Length);
            }


        }

        private byte[] convertStructToByteArray(SoundBankAddressStruct addrStruct)
        {
            byte[] finalArray = new byte[20];

            Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.PtrTable), 0,finalArray, 0,4);
            Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.WaveTable), 0, finalArray, 4, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.Sfx), 0, finalArray, 8, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.end), 0, finalArray, 12, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.WaveTable2), 0, finalArray, 16, 4);

            return finalArray;
        }

        private void updateSoundBankAddressPosition()
        {

            //set the first element to the good position
            SoundBankAddressStruct addressStruct0 = new SoundBankAddressStruct();
            addressStruct0.PtrTable   = finalIndexAudioStart;
            addressStruct0.Sfx = 0;
            addressStruct0.end = 0;
            addressStruct0.WaveTable  = finalIndexAudioStart + soundBankList[0].ptrTableData.Length;
            addressStruct0.WaveTable2 = finalIndexAudioStart + soundBankList[0].ptrTableData.Length;

            soundBankAddress[0] = addressStruct0;

            //the next element is based on the information of the previous element
            for(int i = 1; i < soundBankAddress.Count; i++)
            {
                SoundBankAddressStruct addressStructLoopTmp = new SoundBankAddressStruct();
                var bfxM1 = 0;
                if (soundBankList[i - 1].bfxData != null)
                    bfxM1 = soundBankList[i - 1].bfxData.Length;

                addressStructLoopTmp.PtrTable   = soundBankList[i - 1].ptrTableData.Length + soundBankList[i - 1].waveTableData.Length + bfxM1;
                addressStructLoopTmp.WaveTable  = addressStructLoopTmp.PtrTable  + soundBankList[i].ptrTableData.Length;
                if(soundBankAddress[i].Sfx != 0 && soundBankAddress[i].end != 0)
                {
                    addressStructLoopTmp.Sfx = addressStructLoopTmp.WaveTable + soundBankList[i].waveTableData.Length;
                    addressStructLoopTmp.end = addressStructLoopTmp.Sfx + soundBankList[i].bfxData.Length; ;
                }
                addressStructLoopTmp.WaveTable2 = addressStructLoopTmp.WaveTable;

                soundBankAddress[i] = addressStructLoopTmp;
            }
        }


    }
}
