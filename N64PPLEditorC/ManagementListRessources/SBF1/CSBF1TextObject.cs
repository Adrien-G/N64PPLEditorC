using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace N64PPLEditorC
{

    class CSBF1TextObject
    {
        private byte[] rawData;
        public byte[] headerData { get; private set; }
        private byte[] textData;
        public int posX;
        public int posY;

        //flags
        public bool  isCenteredText;
        private bool isCenteredTextFirstBit;
        private bool isCenteredTextSecondBit;
        private bool isFontBig;
        private bool isFontMedium;
        public bool  isTextScrolling;
        private bool isManualSpace;
        private bool isFontColor;
        private bool isBigBorder;
        
        //additional size for header (unknow Data)
        private bool additionnalSize1;

        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public int id { get; set; }
        public int group { get; set; }

        public CSBF1TextObject(byte[] rawData, int headerSize, int textdataSize)
        {
            this.rawData = rawData;
            this.headerData = new byte[headerSize];
            this.textData = new byte[textdataSize];
            Array.Copy(rawData, 0, headerData, 0, headerSize);
            Array.Copy(rawData, headerSize, textData, 0, textdataSize);
            this.DecomposeHeader();
        }

        //create new object
        public CSBF1TextObject(byte[] id, int group)
        {
            var textHeader = new Byte[44];
            this.rawData = textHeader;
            this.headerData = textHeader;
            this.textData = new byte[0];
            this.group = group;
            this.id = CGeneric.ConvertByteArrayToInt(id);
            this.DecomposeHeader();
        }

        public static int GetHeaderLength(int headerValue)
        {
            int finalValue = 36;
            //to check the size, there is an initial of 36 bytes and a possible addition of 8 bytes at 4 different place
            //first has unknown effect
            if (CGeneric.GetBitStateFromInt(headerValue, 2))
                finalValue += 8;

            //second is for managing centering text
            if (CGeneric.GetBitStateFromInt(headerValue, 18))
                finalValue += 8;

            //third is for manual space define (define the space in x and y)
            if (CGeneric.GetBitStateFromInt(headerValue, 21))
                finalValue += 8;

            //fourth is the font color
            if (CGeneric.GetBitStateFromInt(headerValue, 32))
                finalValue += 8;

            return finalValue;
        }

        public int GetSize()
        {
            return headerData.Length + textData.Length;
        }

        private void DecomposeHeader()
        {
            //data grabbed from 4 first bytes (converted to bits)
            int dataInitial = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 0, 4));

            //unknown data for now
            this.additionnalSize1 = CGeneric.GetBitStateFromInt(dataInitial, 2);
            

            //flags discovered
            this.isFontBig = CGeneric.GetBitStateFromInt(dataInitial, 7);
            this.isCenteredTextFirstBit = CGeneric.GetBitStateFromInt(dataInitial, 9);
            this.isFontMedium = CGeneric.GetBitStateFromInt(dataInitial, 13);
            this.isCenteredTextSecondBit = CGeneric.GetBitStateFromInt(dataInitial, 17);
            this.isCenteredText = CGeneric.GetBitStateFromInt(dataInitial, 18);
            this.isTextScrolling = CGeneric.GetBitStateFromInt(dataInitial, 20);
            this.isManualSpace = CGeneric.GetBitStateFromInt(dataInitial, 21);
            this.isBigBorder = CGeneric.GetBitStateFromInt(dataInitial, 26);
            this.isFontColor = CGeneric.GetBitStateFromInt(dataInitial, 32);

            //data grabbed from the rest of the header
            this.posX = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 4, 4));
            this.posY = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 8, 4));
            this.id   = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 12, 4));

            int index = 16;
            if (this.additionnalSize1) index += 8;
            if (this.isCenteredText) index += 8;
            if (this.isManualSpace)   index += 8;
            if (this.isFontColor)
            {
                index += 8;
                this.BackColor = Color.FromArgb(rawData[index+3], rawData[index], rawData[index + 1], rawData[index + 2]);
                this.ForeColor = Color.FromArgb(rawData[index+7], rawData[index+4], rawData[index+5], rawData[index + 6]);
            }
        }

        public byte[] GetRawData()
        {
            byte[] res = new byte[0];

            //set differents flags
            isTextScrolling = true;
            int flags = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(headerData, 0, 4));
            CGeneric.SetBitInInt(ref flags, 7, isFontBig);
            CGeneric.SetBitInInt(ref flags, 9, isCenteredTextFirstBit);
            CGeneric.SetBitInInt(ref flags, 13, isFontMedium);
            CGeneric.SetBitInInt(ref flags, 17, isCenteredTextSecondBit);
            CGeneric.SetBitInInt(ref flags, 18, isCenteredText);
            CGeneric.SetBitInInt(ref flags, 20, isTextScrolling);
            CGeneric.SetBitInInt(ref flags, 21, isManualSpace);
            CGeneric.SetBitInInt(ref flags, 26, isBigBorder);
            CGeneric.SetBitInInt(ref flags, 32, isFontColor);
            Array.Copy(CGeneric.ConvertIntToByteArray(flags), 0, headerData, 0,4);

            //update X / Y position
            var sizeX = CGeneric.ConvertIntToByteArray(this.posX);
            var sizeY = CGeneric.ConvertIntToByteArray(this.posY);
            Array.Copy(sizeX, 0, headerData, 4, sizeX.Length);
            Array.Copy(sizeY, 0, headerData, 8, sizeY.Length);

            //update id
            var id = CGeneric.ConvertIntToByteArray(this.id);
            Array.Copy(id, 0, headerData, 12, id.Length);

            int index = 16;
            if (this.additionnalSize1) index += 8;
            if (this.isCenteredText) index += 8;
            if (this.isManualSpace) index += 8;
            if (this.isFontColor)
            {
                index += 8;
                //update forecolor and backcolor
                var foreColor = new byte[] { this.ForeColor.R, this.ForeColor.G, this.ForeColor.B, this.ForeColor.A };
                var backColor = new byte[] { this.BackColor.R, this.BackColor.G, this.BackColor.B, this.BackColor.A };
                Array.Copy(backColor, 0, headerData, index, foreColor.Length);
                Array.Copy(foreColor, 0, headerData, index + 4, backColor.Length);
            }

            

            //update the text size
            var lenText = CGeneric.ConvertIntToByteArray((this.textData.Length - 4) / 2);
            Array.Copy(lenText, 0, textData, 0, lenText.Length);

            //merge the two arrays
            res = res.Concat(headerData).ToArray();
            res = res.Concat(textData).ToArray();

            return res;


        }

        #region set text and show it
        public string GetText()
        {
            byte[] charText = new byte[2];
            StringBuilder sb = new StringBuilder();
            for (int i = 4; i < textData.Length; i += 2)
            {
                Array.Copy(textData, i, charText, 0, charText.Length);
                Int16 character = (short)CGeneric.ConvertByteArrayToInt(charText);
                sb.Append(ConvertByteToChar(character));
            }
            return sb.Replace("#", Environment.NewLine).ToString();
        }
        public void SetText(String text)
        {
            text = text.Replace("\r\n", "#").ToLower();
            byte[] res = new byte[text.Length * 2 + 4];
            //write size of the text
            Array.Copy(CGeneric.ConvertIntToByteArray(text.Length * 2), res, 4);

            for (int i = 0; i < text.Length; i++)
            {
                Array.Copy(ConvertCharToByteArray(text[i]), 0, res, i * 2 + 4, 2);
            }
            this.textData = res;
        }

        private byte[] ConvertCharToByteArray(char letter)
        {
            switch (RomLangAddress.romLang)
            {
                case CGeneric.romLang.French: return ConvertChToBaFr(letter);
                case CGeneric.romLang.German: return ConvertChToBaGer(letter);
                case CGeneric.romLang.European:
                case CGeneric.romLang.USA:
                    return ConvertChToBaEuOrUsa(letter);
                default: return new byte[2];
            }
        }
        private char ConvertByteToChar(Int16 letter)
        {
            switch (RomLangAddress.romLang)
            {
                case CGeneric.romLang.French: return ConvertByteToCharFr(letter);
                case CGeneric.romLang.German: return ConvertByteToCharGer(letter);
                case CGeneric.romLang.European:
                case CGeneric.romLang.USA:
                    return ConvertByteToCharEurOrUsa(letter);
                default: return '?';
            }
        }

        private byte[] ConvertChToBaEuOrUsa(char letter)
        {
            switch (letter)
            {
                case 'a': return new byte[] { 0x1C, 0x01 };
                case 'b': return new byte[] { 0x1C, 0x02 };
                case 'c': return new byte[] { 0x1C, 0x03 };
                case 'd': return new byte[] { 0x1C, 0x04 };
                case 'e': return new byte[] { 0x1C, 0x05 };
                case 'f': return new byte[] { 0x1C, 0x06 };
                case 'g': return new byte[] { 0x1C, 0x07 };
                case 'h': return new byte[] { 0x1C, 0x08 };
                case 'i': return new byte[] { 0x1C, 0x09 };
                case 'j': return new byte[] { 0x1C, 0x0A };
                case 'k': return new byte[] { 0x1C, 0x0B };
                case 'l': return new byte[] { 0x1C, 0x0C };
                case 'm': return new byte[] { 0x1C, 0x0D };
                case 'n': return new byte[] { 0x1C, 0x0E };
                case 'o': return new byte[] { 0x1C, 0x0F };
                case 'p': return new byte[] { 0x1C, 0x10 };
                case 'q': return new byte[] { 0x1C, 0x11 };
                case 'r': return new byte[] { 0x1C, 0x12 };
                case 's': return new byte[] { 0x1C, 0x13 };
                case 't': return new byte[] { 0x1C, 0x14 };
                case 'u': return new byte[] { 0x1C, 0x15 };
                case 'v': return new byte[] { 0x1C, 0x16 };
                case 'w': return new byte[] { 0x1C, 0x17 };
                case 'x': return new byte[] { 0x1C, 0x18 };
                case 'y': return new byte[] { 0x1C, 0x19 };
                case 'z': return new byte[] { 0x1C, 0x1A };
                case '1': return new byte[] { 0x14, 0x01 };
                case '2': return new byte[] { 0x14, 0x02 };
                case '3': return new byte[] { 0x14, 0x03 };
                case '4': return new byte[] { 0x14, 0x04 };
                case '5': return new byte[] { 0x14, 0x05 };
                case '6': return new byte[] { 0x14, 0x06 };
                case '7': return new byte[] { 0x14, 0x07 };
                case '8': return new byte[] { 0x14, 0x08 };
                case '9': return new byte[] { 0x14, 0x09 };
                case '0': return new byte[] { 0x14, 0x0A };
                case ' ': return new byte[] { 0x00, 0x01 };
                case '#': return new byte[] { 0x00, 0x02 }; //Hack, hashtag had to be remplaced by crlf
                case '!': return new byte[] { 0x10, 0x01 };
                case '&': return new byte[] { 0x10, 0x02 };
                case '(': return new byte[] { 0x10, 0x03 };
                case ')': return new byte[] { 0x10, 0x04 };
                case '_': return new byte[] { 0x10, 0x05 };
                case '-': return new byte[] { 0x10, 0x06 };
                case '+': return new byte[] { 0x10, 0x07 };
                case '=': return new byte[] { 0x10, 0x08 };
                case '\\': return new byte[] { 0x10, 0x09 };
                case ';': return new byte[] { 0x10, 0x0A };
                case ':': return new byte[] { 0x10, 0x0B };
                case '"': return new byte[] { 0x10, 0x0C };
                case '\'': return new byte[] { 0x10, 0x0D };
                case ',': return new byte[] { 0x10, 0x0E };
                case '.': return new byte[] { 0x10, 0x0F };
                case '?': return new byte[] { 0x10, 0x10 };
                case '/': return new byte[] { 0x10, 0x11 };
                case '>': return new byte[] { 0x10, 0x12 };
                case '<': return new byte[] { 0x10, 0x13 };
                case 'é': return new byte[] { 0x10, 0x14 };
                case '©': return new byte[] { 0x10, 0x15 };
                default: return new byte[] { 0x00, 0x01 };
            }
        }

        private byte[] ConvertChToBaGer(char letter)
        {
            switch (letter)
            {
                case 'a': return new byte[] { 0x18, 0x01 };
                case 'ä': return new byte[] { 0x18, 0x02 };
                case 'b': return new byte[] { 0x18, 0x03 };
                case 'c': return new byte[] { 0x18, 0x04 };
                case 'd': return new byte[] { 0x18, 0x05 };
                case 'e': return new byte[] { 0x18, 0x06 };
                case 'f': return new byte[] { 0x18, 0x07 };
                case 'g': return new byte[] { 0x18, 0x08 };
                case 'h': return new byte[] { 0x18, 0x09 };
                case 'i': return new byte[] { 0x18, 0x0A };
                case 'j': return new byte[] { 0x18, 0x0B };
                case 'k': return new byte[] { 0x18, 0x0C };
                case 'l': return new byte[] { 0x18, 0x0D };
                case 'm': return new byte[] { 0x18, 0x0E };
                case 'n': return new byte[] { 0x18, 0x0F };
                case 'o': return new byte[] { 0x18, 0x10 };
                case 'ö': return new byte[] { 0x18, 0x11 };
                case 'p': return new byte[] { 0x18, 0x12 };
                case 'q': return new byte[] { 0x18, 0x13 };
                case 'r': return new byte[] { 0x18, 0x14 };
                case 's': return new byte[] { 0x18, 0x15 };
                case 'ß': return new byte[] { 0x18, 0x16 };
                case 't': return new byte[] { 0x18, 0x17 };
                case 'u': return new byte[] { 0x18, 0x18 };
                case 'ü': return new byte[] { 0x18, 0x19 };
                case 'v': return new byte[] { 0x18, 0x1A };
                case 'w': return new byte[] { 0x18, 0x1B };
                case 'x': return new byte[] { 0x18, 0x1C };
                case 'y': return new byte[] { 0x18, 0x1D };
                case 'z': return new byte[] { 0x18, 0x1E };
                case '1': return new byte[] { 0x14, 0x01 };
                case '2': return new byte[] { 0x14, 0x02 };
                case '3': return new byte[] { 0x14, 0x03 };
                case '4': return new byte[] { 0x14, 0x04 };
                case '5': return new byte[] { 0x14, 0x05 };
                case '6': return new byte[] { 0x14, 0x06 };
                case '7': return new byte[] { 0x14, 0x07 };
                case '8': return new byte[] { 0x14, 0x08 };
                case '9': return new byte[] { 0x14, 0x09 };
                case '0': return new byte[] { 0x14, 0x0A };
                case ' ': return new byte[] { 0x00, 0x01 };
                case '#': return new byte[] { 0x00, 0x02 }; //Hack, hashtag had to be remplaced by crlf
                case '!': return new byte[] { 0x10, 0x01 };
                case '&': return new byte[] { 0x10, 0x02 };
                case '(': return new byte[] { 0x10, 0x03 };
                case ')': return new byte[] { 0x10, 0x04 };
                case '_': return new byte[] { 0x10, 0x05 };
                case '-': return new byte[] { 0x10, 0x06 };
                case '+': return new byte[] { 0x10, 0x07 };
                case '=': return new byte[] { 0x10, 0x08 };
                case '\\': return new byte[] { 0x10, 0x09 };
                case ';': return new byte[] { 0x10, 0x0A };
                case ':': return new byte[] { 0x10, 0x0B };
                case '"': return new byte[] { 0x10, 0x0C };
                case '\'': return new byte[] { 0x10, 0x0D };
                case ',': return new byte[] { 0x10, 0x0E };
                case '.': return new byte[] { 0x10, 0x0F };
                case '?': return new byte[] { 0x10, 0x10 };
                case '/': return new byte[] { 0x10, 0x11 };
                case '>': return new byte[] { 0x10, 0x12 };
                case '<': return new byte[] { 0x10, 0x13 };
                case 'é': return new byte[] { 0x10, 0x14 };
                case '©': return new byte[] { 0x10, 0x15 };
                default: return new byte[] { 0x00, 0x01 };
            }
        }

        private byte[] ConvertChToBaFr(char letter)
        {
            switch (letter)
            {
                case 'a': return new byte[] { 0x18, 0x01 };
                case 'b': return new byte[] { 0x18, 0x02 };
                case 'c': return new byte[] { 0x18, 0x03 };
                case 'ç': return new byte[] { 0x18, 0x04 };
                case 'd': return new byte[] { 0x18, 0x05 };
                case 'e': return new byte[] { 0x18, 0x06 };
                case 'f': return new byte[] { 0x18, 0x07 };
                case 'g': return new byte[] { 0x18, 0x08 };
                case 'h': return new byte[] { 0x18, 0x09 };
                case 'i': return new byte[] { 0x18, 0x0A };
                case 'j': return new byte[] { 0x18, 0x0B };
                case 'k': return new byte[] { 0x18, 0x0C };
                case 'l': return new byte[] { 0x18, 0x0D };
                case 'm': return new byte[] { 0x18, 0x0E };
                case 'n': return new byte[] { 0x18, 0x0F };
                case 'o': return new byte[] { 0x18, 0x10 };
                case 'p': return new byte[] { 0x18, 0x11 };
                case 'q': return new byte[] { 0x18, 0x12 };
                case 'r': return new byte[] { 0x18, 0x13 };
                case 's': return new byte[] { 0x18, 0x14 };
                case 't': return new byte[] { 0x18, 0x15 };
                case 'u': return new byte[] { 0x18, 0x16 };
                case 'v': return new byte[] { 0x18, 0x17 };
                case 'w': return new byte[] { 0x18, 0x18 };
                case 'x': return new byte[] { 0x18, 0x19 };
                case 'y': return new byte[] { 0x18, 0x1A };
                case 'z': return new byte[] { 0x18, 0x1B };
                case '1': return new byte[] { 0x14, 0x01 };
                case '2': return new byte[] { 0x14, 0x02 };
                case '3': return new byte[] { 0x14, 0x03 };
                case '4': return new byte[] { 0x14, 0x04 };
                case '5': return new byte[] { 0x14, 0x05 };
                case '6': return new byte[] { 0x14, 0x06 };
                case '7': return new byte[] { 0x14, 0x07 };
                case '8': return new byte[] { 0x14, 0x08 };
                case '9': return new byte[] { 0x14, 0x09 };
                case '0': return new byte[] { 0x14, 0x0A };
                case ' ': return new byte[] { 0x00, 0x01 };
                case '#': return new byte[] { 0x00, 0x02 }; //Hack, hashtag had to be remplaced by crlf
                case '!': return new byte[] { 0x10, 0x01 };
                case '&': return new byte[] { 0x10, 0x02 };
                case '(': return new byte[] { 0x10, 0x03 };
                case ')': return new byte[] { 0x10, 0x04 };
                case '_': return new byte[] { 0x10, 0x05 };
                case '-': return new byte[] { 0x10, 0x06 };
                case '+': return new byte[] { 0x10, 0x07 };
                case '=': return new byte[] { 0x10, 0x08 };
                case '\\': return new byte[] { 0x10, 0x09 };
                case ';': return new byte[] { 0x10, 0x0A };
                case ':': return new byte[] { 0x10, 0x0B };
                case '"': return new byte[] { 0x10, 0x0C };
                case '\'': return new byte[] { 0x10, 0x0D };
                case ',': return new byte[] { 0x10, 0x0E };
                case '.': return new byte[] { 0x10, 0x0F };
                case '?': return new byte[] { 0x10, 0x10 };
                case '/': return new byte[] { 0x10, 0x11 };
                case '>': return new byte[] { 0x10, 0x12 };
                case '<': return new byte[] { 0x10, 0x13 };
                case 'é': return new byte[] { 0x10, 0x14 };
                case '©': return new byte[] { 0x10, 0x15 };
                default: return new byte[] { 0x00, 0x01 };
            }
        }
        private char ConvertByteToCharEurOrUsa(short letter)
        {
            switch (letter)
            {
                case 0x1C01: return 'a';
                case 0x1C02: return 'b';
                case 0x1C03: return 'c';
                case 0x1C04: return 'd'; //TODO replace by uppercase 
                case 0x1C05: return 'e';
                case 0x1C06: return 'f';
                case 0x1C07: return 'g';
                case 0x1C08: return 'h';
                case 0x1C09: return 'i';
                case 0x1C0A: return 'j';
                case 0x1C0B: return 'k';
                case 0x1C0C: return 'l';
                case 0x1C0D: return 'm';
                case 0x1C0E: return 'n';
                case 0x1C0F: return 'o';
                case 0x1C10: return 'p';
                case 0x1C11: return 'q';
                case 0x1C12: return 'r';
                case 0x1C13: return 's';
                case 0x1C14: return 't';
                case 0x1C15: return 'u';
                case 0x1C16: return 'v';
                case 0x1C17: return 'w';
                case 0x1C18: return 'x';
                case 0x1C19: return 'y';
                case 0x1C1A: return 'z';
                case 0x1801: return 'a';
                case 0x1802: return 'b';
                case 0x1803: return 'c';
                case 0x1804: return 'd'; //TODO replace by uppercase 
                case 0x1805: return 'e';
                case 0x1806: return 'f';
                case 0x1807: return 'g';
                case 0x1808: return 'h';
                case 0x1809: return 'i';
                case 0x180A: return 'j';
                case 0x180B: return 'k';
                case 0x180C: return 'l';
                case 0x180D: return 'm';
                case 0x180E: return 'n';
                case 0x180F: return 'o';
                case 0x1810: return 'p';
                case 0x1811: return 'q';
                case 0x1812: return 'r';
                case 0x1813: return 's'; //ajusted
                case 0x1814: return 't';
                case 0x1815: return 'u';
                case 0x1816: return 'v';
                case 0x1817: return 'w';
                case 0x1818: return 'x';
                case 0x1819: return 'y';
                case 0x181A: return 'z';
                case 0x181B: return '0';
                case 0x181C: return '1';
                case 0x181D: return '2';
                case 0x181E: return '3';
                case 0x181F: return '4';
                case 0x1820: return '5';
                case 0x1821: return '6';
                case 0x1822: return '7';
                case 0x1823: return '8';
                case 0x1824: return '9';
                case 0x1401: return '1';
                case 0x1402: return '2';
                case 0x1403: return '3';
                case 0x1404: return '4';
                case 0x1405: return '5';
                case 0x1406: return '6';
                case 0x1407: return '7';
                case 0x1408: return '8';
                case 0x1409: return '9';
                case 0x140A: return '0';
                case 0x0001: return ' ';
                case 0x0002: return '#'; //Hack, will be remplaced by crlf
                case 0x1001: return '!';
                case 0x1002: return '&';
                case 0x1003: return '(';
                case 0x1004: return ')';
                case 0x1005: return '_';
                case 0x1006: return '-';
                case 0x1007: return '+';
                case 0x1008: return '=';
                case 0x1009: return '\\';
                case 0x100A: return ';';
                case 0x100B: return ':';
                case 0x100C: return '"';
                case 0x100D: return '\'';
                case 0x100E: return ',';
                case 0x100F: return '.';
                case 0x1010: return '?';
                case 0x1011: return '/';
                case 0x1012: return '>';
                case 0x1013: return '<';
                case 0x1014: return 'É';
                case 0x1015: return '©';
                default: throw new NotImplementedException();
            }
        }

        private char ConvertByteToCharGer(short letter)
        {
            switch (letter)
            {
                case 0x1801: return 'a';
                case 0x1802: return 'ä';
                case 0x1803: return 'b';
                case 0x1804: return 'c';
                case 0x1805: return 'd';
                case 0x1806: return 'e';
                case 0x1807: return 'f';
                case 0x1808: return 'g';
                case 0x1809: return 'h';
                case 0x180A: return 'i';
                case 0x180B: return 'j';
                case 0x180C: return 'k';
                case 0x180D: return 'l';
                case 0x180E: return 'm';
                case 0x180F: return 'n';
                case 0x1810: return 'o';
                case 0x1811: return 'ö';
                case 0x1812: return 'p';
                case 0x1813: return 'q';
                case 0x1814: return 'r';
                case 0x1815: return 's';
                case 0x1816: return 'ß';
                case 0x1817: return 't';
                case 0x1818: return 'u';
                case 0x1819: return 'ü';
                case 0x181A: return 'v';
                case 0x181B: return 'w';
                case 0x181C: return 'x';
                case 0x181D: return 'y';
                case 0x181E: return 'z';
                case 0x181F: return '0';
                case 0x1820: return '1';
                case 0x1821: return '2';
                case 0x1822: return '3';
                case 0x1823: return '4';
                case 0x1824: return '5';
                case 0x1825: return '6';
                case 0x1826: return '7';
                case 0x1827: return '8';
                case 0x1828: return '9';
                case 0x1401: return '1';
                case 0x1402: return '2';
                case 0x1403: return '3';
                case 0x1404: return '4';
                case 0x1405: return '5';
                case 0x1406: return '6';
                case 0x1407: return '7';
                case 0x1408: return '8';
                case 0x1409: return '9';
                case 0x140A: return '0';
                case 0x0001: return ' ';
                case 0x0002: return '#'; //Hack, will be remplaced by crlf
                case 0x1001: return '!';
                case 0x1002: return '&';
                case 0x1003: return '(';
                case 0x1004: return ')';
                case 0x1005: return '_';
                case 0x1006: return '-';
                case 0x1007: return '+';
                case 0x1008: return '=';
                case 0x1009: return '\\';
                case 0x100A: return ';';
                case 0x100B: return ':';
                case 0x100C: return '"';
                case 0x100D: return '\'';
                case 0x100E: return ',';
                case 0x100F: return '.';
                case 0x1010: return '?';
                case 0x1011: return '/';
                case 0x1012: return '>';
                case 0x1013: return '<';
                case 0x1014: return 'É';
                case 0x1015: return '©';
                default: throw new NotImplementedException();
            }
        }



        private char ConvertByteToCharFr(Int16 letter)
        {
            switch (letter)
            {
                case 0x1801: return 'a';
                case 0x1802: return 'b';
                case 0x1803: return 'c';
                case 0x1804: return 'ç'; //TODO replace by uppercase 
                case 0x1805: return 'd';
                case 0x1806: return 'e';
                case 0x1807: return 'f';
                case 0x1808: return 'g';
                case 0x1809: return 'h';
                case 0x180A: return 'i';
                case 0x180B: return 'j';
                case 0x180C: return 'k';
                case 0x180D: return 'l';
                case 0x180E: return 'm';
                case 0x180F: return 'n';
                case 0x1810: return 'o';
                case 0x1811: return 'p';
                case 0x1812: return 'q';
                case 0x1813: return 'r';
                case 0x1814: return 's';
                case 0x1815: return 't';
                case 0x1816: return 'u';
                case 0x1817: return 'v';
                case 0x1818: return 'w';
                case 0x1819: return 'x';
                case 0x181A: return 'y';
                case 0x181B: return 'z';
                case 0x181C: return '0';
                case 0x181D: return '1';
                case 0x181E: return '2';
                case 0x181F: return '3';
                case 0x1820: return '4';
                case 0x1821: return '5';
                case 0x1822: return '6';
                case 0x1823: return '7';
                case 0x1824: return '8';
                case 0x1825: return '9';
                case 0x1401: return '1';
                case 0x1402: return '2';
                case 0x1403: return '3';
                case 0x1404: return '4';
                case 0x1405: return '5';
                case 0x1406: return '6';
                case 0x1407: return '7';
                case 0x1408: return '8';
                case 0x1409: return '9';
                case 0x140A: return '0';
                case 0x0001: return ' ';
                case 0x0002: return '#'; //Hack, will be remplaced by crlf
                case 0x1001: return '!';
                case 0x1002: return '&';
                case 0x1003: return '(';
                case 0x1004: return ')';
                case 0x1005: return '_';
                case 0x1006: return '-';
                case 0x1007: return '+';
                case 0x1008: return '=';
                case 0x1009: return '\\';
                case 0x100A: return ';';
                case 0x100B: return ':';
                case 0x100C: return '"';
                case 0x100D: return '\'';
                case 0x100E: return ',';
                case 0x100F: return '.';
                case 0x1010: return '?';
                case 0x1011: return '/';
                case 0x1012: return '>';
                case 0x1013: return '<';
                case 0x1014: return 'É';
                case 0x1015: return '©';
                default: throw new NotImplementedException();
            }
        }

        #endregion
    }
}
