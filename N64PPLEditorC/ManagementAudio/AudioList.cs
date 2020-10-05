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
            //update address in the header (SoundBank 0)

            //update address in the header (others soundBank)

            //write rawData soundBank



        }

        //private void ChunkAudio()
        //{

        //    //store new data address and store data associated (rawData)
        //    foreach(int addressFr in soundBankPtrTableAddressFr)
        //    {
        //        SoundBank sbTmp = new SoundBank();

        //        //store address
        //        sbTmp.address.PtrTable  = CGeneric.GiveMeArray(rawData, addressFr, 4);
        //        sbTmp.address.WaveTable = CGeneric.GiveMeArray(rawData, addressFr + 4, 4);
        //        sbTmp.address.Sfx       = CGeneric.GiveMeArray(rawData, addressFr + 8, 4);
        //        sbTmp.address.end       = CGeneric.GiveMeArray(rawData, addressFr + 12, 4);
        //        sbTmp.address.WaveTable2= CGeneric.GiveMeArray(rawData, addressFr + 16, 4);

        //        //store data associated to address
        //        int addressPtrTableInt = CGeneric.ConvertByteArrayToInt(sbTmp.address.PtrTable);
        //        int addressWaveTableInt = CGeneric.ConvertByteArrayToInt(sbTmp.address.WaveTable);
        //        int addressSfxInt = CGeneric.ConvertByteArrayToInt(sbTmp.address.Sfx);
        //        int addressEndInt = CGeneric.ConvertByteArrayToInt(sbTmp.address.end);

        //        sbTmp.ptrTable = CGeneric.GiveMeArray(rawData, addressPtrTableInt, addressWaveTableInt - addressPtrTableInt);
        //        sbTmp.waveTable = CGeneric.GiveMeArray(rawData, addressWaveTableInt, addressSfxInt - addressWaveTableInt);
        //        sbTmp.sfx = CGeneric.GiveMeArray(rawData, addressSfxInt, addressEndInt - addressSfxInt);

        //        soundBankList.Add(sbTmp);
        //    }

        //    //soundBankAddress = new List<SoundBankAddressStruct>();

        //    ////store the address associated to each soundBank
        //    //for (int i = 0; i < soundBankPtrTableAddressFr.Length; i++)
        //    //{
        //    //    var soundBanktmp = new SoundBankAddressStruct();

        //    //    soundBanktmp.PtrTable   = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i], 4));
        //    //    soundBanktmp.WaveTable  = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i] + 4, 4));
        //    //    soundBanktmp.Sfx        = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i] + 8, 4));
        //    //    soundBanktmp.end        = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i] + 12, 4));
        //    //    soundBanktmp.WaveTable2 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, soundBankPtrTableAddressFr[i] + 16, 4));

        //    //    //check the case, because for bfx (of soundbank 1)  it seem's that rom has an error or unused data still stored (of 0x470 length)
        //    //    if (i == 1)
        //    //    {
        //    //        soundBanktmp.end += 0x470;
        //    //    }

        //    //    soundBankAddress.Add(soundBanktmp);

        //    //}

        //    //store rawData for each soundBank (with the address previously stored)
        //    //for (int i = 0; i < soundBankAddress.Count(); i++)
        //    //{
        //      //  var sndBankTmp = new SoundBank();
        //        //int addressPtrTable = soundBankAddress[i].PtrTable;
        //        //int addressWaveTable = soundBankAddress[i].WaveTable;
        //        //int addressSfx = soundBankAddress[i].Sfx;
        //        //int addressEnd = soundBankAddress[i].end;

        //        //sndBankTmp.ptrTableData = CGeneric.GiveMeArray(rawData, addressPtrTable, addressWaveTable - addressPtrTable);

        //        //specific for soundbank 0 and soundbank2 (sfx is set to 0)
        //        //if(addressSfx != 0)
        //        //{
        //        //    sndBankTmp.waveTableData = CGeneric.GiveMeArray(rawData, addressWaveTable, addressSfx - addressWaveTable);
        //        //    sndBankTmp.bfxData = CGeneric.GiveMeArray(rawData, addressSfx,addressEnd - addressSfx);
        //        //}
        //        //else
        //        //    sndBankTmp.waveTableData = CGeneric.GiveMeArray(rawData, addressWaveTable, soundBankAddress[i + 1].PtrTable -addressWaveTable);

        //        //soundBankData.Add(sndBankTmp);
        //    //}
        //}

        //public void WriteAllData(FileStream fs)
        //{
        //    //make an initial alignment for to be sure starting at good point
        //    int alignment = (int)fs.Position % 16;
        //    for (int i = 0; i < 16 - alignment; i++)
        //    {
        //        fs.WriteByte(0);
        //    }
        //    this.finalIndexAudioStart = (int)fs.Position;

        //    //TODO fill the rest (rewriting)

        //}

        //public void writeAllData(FileStream fs)
        //{
        //    //write alignment
        //    //int alignment = (int)fs.Position % 16;
        //    //for (int i = 0; i < 16 - alignment; i++)
        //    //{
        //    //    fs.WriteByte(0);
        //    //}
        //    //this.finalIndexAudioStart = (int)fs.Position;

        //    //write allData
        //    foreach (SoundBank sndBank in soundBankData)
        //    {
        //        fs.Write(sndBank.ptrTableData, 0, sndBank.ptrTableData.Length);
        //        fs.Write(sndBank.waveTableData, 0, sndBank.waveTableData.Length);
        //        if (sndBank.bfxData != null)
        //            fs.Write(sndBank.bfxData, 0, sndBank.bfxData.Length);
        //    }

        //    //update the two list at the start of the rom 
        //    UpdateAudioAddress(fs);

        //}

        //private void UpdateAudioAddress(FileStream fs)
        //{
        //    //set manually address for all address

        //    //grab the difference between initial audio start and now
        //    int difference;
        //    if (finalIndexAudioStart > initialIndexAudioStart)
        //        difference = finalIndexAudioStart - initialIndexAudioStart;
        //    else
        //        difference = initialIndexAudioStart - finalIndexAudioStart;


        //    //soundbank0, read initial values, make the difference and write values
        //    byte[] addressHeaderSb0 = new byte[0x238];
        //    Array.Copy(rawData,0xB4B00, addressHeaderSb0, 0, addressHeaderSb0.Length);


        //    for (int i = 0; i < 0x238; i += 4)
        //    {
        //        int tmpValue = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(addressHeaderSb0, i, 4));
        //        tmpValue += difference;
        //        var tmpBytesNew = CGeneric.ConvertIntToByteArray(tmpValue);
        //        addressHeaderSb0[i] = tmpBytesNew[0];
        //        addressHeaderSb0[i+1] = tmpBytesNew[1];
        //        addressHeaderSb0[i+2] = tmpBytesNew[2];
        //        addressHeaderSb0[i+3] = tmpBytesNew[3];
        //    }

        //    fs.Position = 0xB4B00;
        //    fs.Write(addressHeaderSb0,0,addressHeaderSb0.Length);

        //    //update all address positions for the soundBank 
        //    updateSoundBankAddressPosition();

        //    //write from 0 to 0x15 soundBank address
        //    writeSoundBankAddress(fs);
        //}

        //private void writeSoundBankAddress(FileStream fs)
        //{
        //    //set the 0x15 soundband address
        //    for(int i = 0; i < 0x15; i++)
        //    {
        //        fs.Position = soundBankPtrTableAddressFr[i];
        //        var dataToWrite = convertStructToByteArray(soundBankAddress[i],i);
        //        fs.Write(dataToWrite, 0, dataToWrite.Length);
        //    }

        //    //TODO redefine special soundbank 2 & 3 address


        //    //special hack for second address loop (add 0x168)
        //    for (int i = 4; i < 0x14; i++)
        //    {
        //        fs.Position = soundBankPtrTableAddressFr[i] + 0x168;
        //        var dataToWrite = convertStructToByteArray(soundBankAddress[i],i);
        //        fs.Write(dataToWrite, 0, dataToWrite.Length);
        //    }


        //}

        //private byte[] convertStructToByteArray(SoundBankAddressStruct addrStruct,int i)
        //{
        //    byte[] finalArray = new byte[20];

        //    Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.PtrTable), 0,finalArray, 0,4);
        //    Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.WaveTable), 0, finalArray, 4, 4);
        //    Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.Sfx), 0, finalArray, 8, 4);
        //    Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.end), 0, finalArray, 12, 4);
        //    Array.Copy(CGeneric.ConvertIntToByteArray(addrStruct.WaveTable2), 0, finalArray, 16, 4);

        //    return finalArray;
        //}

        //private void updateSoundBankAddressPosition()
        //{

        //    //set the first element to the good position
        //    SoundBankAddressStruct addressStruct0 = new SoundBankAddressStruct();
        //    addressStruct0.PtrTable   = finalIndexAudioStart;
        //    addressStruct0.Sfx = 0;
        //    addressStruct0.end = 0;
        //    addressStruct0.WaveTable  = finalIndexAudioStart + soundBankData[0].ptrTableData.Length;
        //    addressStruct0.WaveTable2 = finalIndexAudioStart + soundBankData[0].ptrTableData.Length;

        //    soundBankAddress[0] = addressStruct0;

        //    //the next element is based on the information of the previous element
        //    for(int i = 1; i < soundBankAddress.Count; i++)
        //    {
        //        SoundBankAddressStruct addressStructLoopTmp = new SoundBankAddressStruct();

        //        //previous location + sizeof previous soundbank
        //        int previousLocation = soundBankAddress[i - 1].PtrTable;
        //        int lengthDataPtr = soundBankData[i - 1].ptrTableData.Length;
        //        int lengthWaveTable = soundBankData[i - 1].waveTableData.Length;
        //        var lengthBfxM1 = 0;
        //        if (soundBankData[i - 1].bfxData != null)
        //            lengthBfxM1 = soundBankData[i - 1].bfxData.Length;

        //        addressStructLoopTmp.PtrTable   = previousLocation + lengthDataPtr + lengthWaveTable + lengthBfxM1;
        //        addressStructLoopTmp.WaveTable  = addressStructLoopTmp.PtrTable  + soundBankData[i].ptrTableData.Length;
        //        if(soundBankAddress[i].Sfx != 0 && soundBankAddress[i].end != 0)
        //        {
        //            addressStructLoopTmp.Sfx = addressStructLoopTmp.WaveTable + soundBankData[i].waveTableData.Length;
        //            addressStructLoopTmp.end = addressStructLoopTmp.Sfx + soundBankData[i].bfxData.Length;
        //        }
        //        addressStructLoopTmp.WaveTable2 = addressStructLoopTmp.WaveTable;

        //        soundBankAddress[i] = addressStructLoopTmp;
        //    }
        //}
    }
}
