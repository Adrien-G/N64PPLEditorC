﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace N64PPLEditorC
{
    public static class CGeneric
    {
        //different path used in the application
        public static readonly String pathExtractedTexture = Application.StartupPath + @"\extractedTexture\";
        public static readonly String pathExtractedTexture2 = Application.StartupPath + @"\extractedTexture2\";
        public static readonly String pathOtherContent = Application.StartupPath + @"\OtherContent\";
        public static readonly String pathExtractedSound = Application.StartupPath + @"\extractedSounds\";

        //size of the elements present in the table of ressources
        public static int sizeOfElementTable = 24;

        //some different pattern in the rom
        public static readonly Byte[] patternPuzzleLeagueN64 = { 0x50, 0x55, 0x5A, 0x5A, 0x4C, 0x45, 0x20, 0x4C, 0x45, 0x41, 0x47, 0x55, 0x45, 0x20, 0x4E, 0x36, 0x34 };
        public static readonly Byte[] patternN64WaveTable = { 0x4E, 0x36, 0x34, 0x20, 0x57, 0x61, 0x76, 0x65, 0x54, 0x61, 0x62, 0x6C, 0x65, 0x73, 0x20, 0x00 };
        public static readonly Byte[] patternN64PtrTableV2= { 0x4E, 0x36, 0x34, 0x20, 0x50, 0x74, 0x72, 0x54, 0x61, 0x62, 0x6C, 0x65, 0x73, 0x56, 0x32, 0x00 };
        public static readonly Byte[] patternMidiSoundBank1 = { 0x00, 0x00, 0x00, 0xCE, 0x00, 0x00, 0x00, 0xBA, 0x00, 0x00, 0x00, 0xA9, 0x00, 0x00, 0x00, 0x00 };
        public static readonly Byte[] patternAbraBif = { 65, 66, 82, 65, 46, 66, 73, 70 };
        //public static readonly Byte[] patternAbraBif = {0x42, 0x41, 0x42, 0x59, 0x5F, 0x44, 0x52, 0x41, 0x2E, 0x42, 0x49, 0x46 };
        public static readonly Byte[] patternBFF2 = { 0x42,0x46,0x46, 0x32 };
        public static readonly Byte[] patternSBF1 = { 0x53, 0x42, 0x46, 0x31};
        public static readonly Byte[] endOfRom = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};
        public static readonly int romSize = 0x2000000;
        public static readonly int romSizeExtended = 0x4000000;

        //just for read the GC iso
        public static readonly int IsoPart1 = 0x2650D58;
        public static readonly int IsoPart2 = 0x26554F8;
        public static readonly int IsoPart3 = 0x7E402C4;
        public static readonly int IsoPart4 = 0x7EC6850;
        public static readonly int IsoPart5 = 0x7ECAFE4;
        public static readonly int IsoPart6 = 0x824661C;
        public static readonly int IsoPart7 = 0x894C3F8;
        public static readonly int IsoEnd1 = 0x26554F8;
        public static readonly int IsoEnd2 = 0x26CB1C6;
        public static readonly int IsoEnd3 = 0x7EC684D;
        public static readonly int IsoEnd4 = 0x7ECAFE4;
        public static readonly int IsoEnd5 = 0x824661B;
        public static readonly int IsoEnd6 = 0x894C41C;
        public static readonly int IsoEnd7 = 0x89C20EA;

        public static readonly Byte[] patternHeaderWavFile = { 
            0x52, 0x49, 0x46, 0x46, // RIFF
            0x00, 0x00, 0x00, 0x00, // File size
            0x57, 0x41, 0x56, 0x45, // WAVE
            0x66, 0x6D, 0x74, 0x20, // fmt 
            0x10, 0x00, 0x00, 0x00, // SubChunk1 fmt size
            0x01, 0x00,             // type of format (01 = PCM)
            0x01, 0x00,             // nb channel
            0x00, 0x00, 0x00, 0x00, // sample rate
            0x00, 0x00, 0x00, 0x00, // byte rate
            0x02, 0x00,             // block align
            0x10, 0x00,             // bits per sample
            0x64, 0x61, 0x74, 0x61, // data
            0x00, 0x00, 0x00, 0x00  // data length
        };

        public static readonly Byte[] patternBottomWavFile = {
            0x73, 0x6D, 0x70, 0x6C, // smp1
            0x00, 0x00, 0x00, 0x00, //
            0x00, 0x00, 0x00, 0x00, //
            0x00, 0x00, 0x00, 0x00, //
            0x00, 0x00, 0x00, 0x00, //
            0x00, 0x00, 0x00, 0x00, //
            0x00, 0x00, 0x00, 0x00, //
            0x00, 0x00, 0x00, 0x00, //
            0x00, 0x00, 0x00, 0x00, //
            0x00, 0x00, 0x00, 0x00, //
            0x00, 0x00, 0x00, 0x00  //
        };

        public enum TextType : int
        {
            Unused = 0x36, // unused, think only used by dev..
            Dialog = 0x44, //dialog, show text caracter by caracter
            Unknown52 = 0x52,
            Unknown60 = 0x60,
        }

        //check the decimal value of ressources type
        public enum RessourceType : int
        {
            FIB = 860244290,
            HVQM = 1213616461,
            SBF = 1396852273,
            BFF = 1111901746
        }

        public enum Compression : byte
        {
            unknow22 = 0x22, 
            unknow23 = 0x23,
            greyscale = 0x24,       // 8 bits for color (R,G,B shared) and 8 bits for alpha
            max16Colors = 0x32,     // indexed color, palette reference a 16 bit color
            max256Colors = 0x33,    // indexed color, palette reference a 32 bit color
            trueColor16Bits = 0x54, // 5 bits for R, G and B, one bit for Alpha
            trueColor32Bits = 0x55  // 8 bits for each R,G,B,A
        }

        public enum romLang : byte
        {
            French = 0x46,
            European = 0x50,
            German = 0x44,
            USA = 0x45,
            JAP = 0xFF //specific japanese for the ISO game
        }

        public enum TextureDisplayStyle : byte
        {
            Fixed = 0x08,
            Animated = 0x2E,
            AnimatedScroll = 0x09
        }

        public static byte[] GiveMeArray(byte[] rawData, int startingData, int sizeData)
        {
            var data = new byte[sizeData];
            Array.Copy(rawData, startingData, data, 0, data.Length);

            return data;
        }

        public static List<byte> ByteToNibble(byte byteToDecompose)
        {
            List<byte> byteList = new List<byte>();
            byte nibble1 = byteToDecompose;
            byte nibble2 = byteToDecompose;

            nibble1 /= 16;
            nibble2 %= 16;

            byteList.Add(nibble1);
            byteList.Add(nibble2);

            return byteList;
        }

        public static byte NibbleToByte(byte nibble1, byte nibble2)
        {
            if(nibble1 > 15 || nibble2 > 15)
                throw new OverflowException();

            byte res;
            res = nibble1;
            res <<= 4;
            res += nibble2;

            return res;
        }

        public static int ConvertByteArrayToInt(Byte[] byteArray)
        {
            //dont know why.. but need copy for not modify the initial array.. (??)
            Byte[] tmp = new byte[byteArray.Length];
            Array.Copy(byteArray, tmp, byteArray.Length);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(tmp);

            if (tmp.Length == 2)
                return BitConverter.ToInt16(tmp, 0);
            else
                return BitConverter.ToInt32(tmp, 0);
        }

        public static Byte[] ConvertIntToByteArray(Int32 nb)
        {
            byte[] res = BitConverter.GetBytes(nb);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(res);

            return res;
        }

        public static Byte[] ConvertIntToByteArray16bits(Int32 nb)
        {
            byte[] res = BitConverter.GetBytes(nb);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(res);

            return new byte[] { res[2], res[3] }; ;
        }

        public static byte[] ConvertStringToByteArray(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static string ConvertByteArrayToString(Byte[] text)
        {
            return Encoding.UTF8.GetString(text);
        }

        public static int SearchBytesInArray(Byte[] arraySource, Byte[] dataSearched,int nbOccurence=0,int startingAddress=0,int step=1)
        {
            //allow to search the second, third (and so on) value of the occurence
            int nbOcc = 0;


            // loop on the first array for find occurence
            for (int i = startingAddress; i <= arraySource.Length - dataSearched.Length; i +=step)
            {
                if (arraySource[i] == dataSearched[0])
                {
                    //when occurence founded, check the rest of the array to compare
                    for (int j = 0; j < dataSearched.Length; j++)
                    {
                        if (arraySource[i + j] != dataSearched[j])
                            break;
                        if (j == dataSearched.Length - 1)
                            if (nbOcc == nbOccurence)
                                return i;
                            else
                                nbOcc += 1;
                    }
                }
            }
            return -1;
        }

        public static bool GetBitStateFromInt(int value, int position)
        {
            position = 32 - position;
            return (((value >> position) & 1) == 1);
        }

        public static void SetBitInInt(ref int value, int position,bool state)
        {
            position = 32 - position;
            if (state)
                value |= (1 << position);
            else
                value &= ~(1 << position);
        }

        public static void VerifyExistingPath()
        {
            Directory.CreateDirectory(pathExtractedTexture);
            Directory.CreateDirectory(pathExtractedTexture2);
            Directory.CreateDirectory(pathOtherContent);
            Directory.CreateDirectory(pathExtractedSound);

            for(int i = 0; i < 22; i++)
             Directory.CreateDirectory(pathExtractedSound + @"Soundbank " + i.ToString("D" + 2));
        }
    }
}
