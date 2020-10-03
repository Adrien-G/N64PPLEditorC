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
        private int nbItems;
        private byte[] rawData;
        private byte[] headerData;
        //length of text + text data
        private byte[] textData;
        private int posX;
        private int posY;
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public UInt32 id { get; set; }
        public int group { get; set; }

        enum textType : byte
        {
            unknown36 = 0x36,
            dialog = 0x44,
            title = 0x52,
            unknown60 = 0x60
        }

        public CSBF1TextObject(byte[] rawData,int headerSize,int textdataSize)
        {
            this.rawData = rawData;
            this.headerData = new byte[headerSize];
            this.textData = new byte[textdataSize];
            Array.Copy(rawData, 0, headerData, 0, headerSize);
            Array.Copy(rawData, headerSize, textData, 0, textdataSize);
            this.DecomposeHeader(headerSize);
        }

        //for creating new text objects from zero
        public CSBF1TextObject(byte[] id,int group)
        {
            var textHeader = new Byte[] { 0x22, 0x10, 0x14, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, id[0], id[1], id[2], id[3], 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x08, 0x12, 0x6D, 0xFF, 0x8D, 0xBE, 0xD9, 0xFF, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
            this.rawData = textHeader;
            this.headerData = textHeader;
            this.textData = new byte[0];
            this.group = group;
            this.DecomposeHeader(textHeader.Length);
        }

        public int getHeaderLength()
        {
            return this.headerData.Length;
        }

        public string GetText()
        {
            byte[] charText = new byte[2];
            StringBuilder sb = new StringBuilder();
            for(int i = 4; i < textData.Length; i += 2)
            {
                Array.Copy(textData,i,charText,0,charText.Length);
                Int16 character = (short)CGeneric.ConvertByteArrayToInt(charText);
                sb.Append(ConvertCaracterToChar(character));
            }
            return sb.Replace("#",Environment.NewLine).ToString();
        }

        public void SetText(String text)
        {
            text = text.Replace("\r\n", "#").ToLower();
            byte[] res = new byte[text.Length * 2+4];
            //write size of the text
            Array.Copy(CGeneric.ConvertIntToByteArray(text.Length*2), res, 4);

            for(int i = 0; i < text.Length; i++)
            {
                Array.Copy(ConvertCharToByteArray(text[i]), 0, res, i * 2+4, 2);
            }
            this.textData = res;
        }

        private void DecomposeHeader(int headerValue)
        {
            var posX = new byte[4];
            var posY = new byte[4];
            var id = new byte[4];
            var foreColor = new byte[4];
            var backColor = new byte[4];
            Array.Copy(rawData, 4, posX, 0, posX.Length);
            Array.Copy(rawData, 8, posY, 0, posY.Length);
            Array.Copy(rawData, 12, id, 0, id.Length);
            Array.Copy(rawData, headerData.Length - 16, foreColor, 0, foreColor.Length);
            Array.Copy(rawData, headerData.Length - 20, backColor, 0, backColor.Length);

            //switch (headerValue)
            //{
            //    case 36:
            //        break;
            //    case 44:
            //        break;
            //    case 52:
            //        //if flag is true, set the component at the middle of the screen (and retract the next value / 2)
            //        if (rawData[22] == 1)
            //        {
            //            posX = CGeneric.ConvertIntToByteArray(160 - rawData[23]);
            //        }

            //        break;
            //    case 60:
            //        break;
            //}
            this.id = CGeneric.ConvertByteArrayToUInt(id);
            this.posX = CGeneric.ConvertByteArrayToInt(posX);
            this.posY = CGeneric.ConvertByteArrayToInt(posY);
            this.ForeColor = Color.FromArgb(foreColor[3], foreColor[0], foreColor[1], foreColor[2]);
            this.BackColor = Color.FromArgb(backColor[3], backColor[0], backColor[1], backColor[2]);

        }

        public int GetPosX()
        {
            if (posX > 320 || posX < 0)
                return 0;
            return posX;
        }
        public int GetPosY()
        {
            if (posY > 232 || posY < 0)
                return 0;
            return posY;
        }

        public void SetPosX(int posX)
        {
            this.posX = posX;
        }
        public void SetPosY(int posY)
        {
            this.posY = posY;
        }

        public byte[] GetRawData()
        {
            byte[] res = new byte[0/*headerData.Length + textData.Length*/];


            //text header, update new informations about header
            var sizeX = CGeneric.ConvertIntToByteArray(this.posX);
            var sizeY = CGeneric.ConvertIntToByteArray(this.posY);
            var id = CGeneric.ConvertUIntToByteArray(this.id);

            var foreColor = new byte[] { this.ForeColor.R, this.ForeColor.G, this.ForeColor.B, this.ForeColor.A }; 
            var backColor = new byte[] { this.BackColor.R, this.BackColor.G, this.BackColor.B, this.BackColor.A };

            //update the position and the id in rawHeader
            Array.Copy(sizeX, 0, headerData, 4, sizeX.Length);
            Array.Copy(sizeY, 0, headerData, 8, sizeY.Length);
            Array.Copy(id, 0, headerData, 12, id.Length);
            Array.Copy(foreColor, 0, headerData, headerData.Length - 16, foreColor.Length);
            Array.Copy(backColor, 0, headerData, headerData.Length - 20, backColor.Length);

            //update the text size
            var lenText = CGeneric.ConvertIntToByteArray((this.textData.Length - 4) / 2);
            Array.Copy(lenText, 0, textData, 0, lenText.Length);

            //merge the two arrays
            res = res.Concat(headerData).ToArray();
            res = res.Concat(textData).ToArray();

            return res;
        }

        private byte[] ConvertCharToByteArray(char letter)
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
                case '\\': return new byte[]{ 0x10, 0x09 };
                case ';': return new byte[] { 0x10, 0x0A };
                case ':': return new byte[] { 0x10, 0x0B };
                case '"': return new byte[] { 0x10, 0x0C };
                case '\'': return new byte[]{ 0x10, 0x0D };
                case ',': return new byte[] { 0x10, 0x0E };
                case '.': return new byte[] { 0x10, 0x0F };
                case '?': return new byte[] { 0x10, 0x10 };
                case '/': return new byte[] { 0x10, 0x11 };
                case '>': return new byte[] { 0x10, 0x12 };
                case '<': return new byte[] { 0x10, 0x13 };
                case 'é': return new byte[] { 0x10, 0x14 };
                case '©': return new byte[] { 0x10, 0x15 };
                default:  return new byte[] { 0x00, 0x01 };
            }
        }

        private char ConvertCaracterToChar(Int16 letter)
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

        public int GetSize()
        {
            return headerData.Length + textData.Length;
        }

        public static int GetHeaderLength(int headerValue)
        {
            switch (headerValue)
            {
                case 0x0008C403: return 52; // tested
                case 0x20080006: return 36;
                case 0x20880442: return 36;
                case 0x20000003: return 44;
                case 0x22000003: return 44; //tested
                case 0x20080003: return 44;
                case 0x20880403: return 44;
                case 0x20000403: return 44;
                case 0x23000403: return 44;
                case 0x20080403: return 44;
                case 0x20080443: return 44;
                case 0x2000C013: return 52; //tested
                case 0x20080C03: return 52; //tested
                case 0x20804003: return 52;
                case 0x22804003: return 52;
                case 0x20884003: return 52;
                case 0x21884003: return 52;
                case 0x20804403: return 52;
                case 0x22804403: return 52;
                case 0x20884403: return 52;
                case 0x2000C003: return 52;
                case 0x2008C003: return 52;
                case 0x2000C403: return 52;
                case 0x2008C403: return 52;
                case 0x2008C443: return 52; //tested
                case 0x20884043: return 52;
                case 0x2000C443: return 52; //tested
                case 0x20084C43: return 60;
                case 0x21884007: return 52;
                case 0x22101443: return 44;
                case 0x2208D403: return 52;
                case 0x22804443: return 52;
                case 0x2200C443: return 52;
                case 0x33804407: return 52;
                case 0x62804403: return 60; //tested
                case 0x60884003: return 60; //added for english and german version
                case 0x22804043: return 52; //added for english version
                default: throw new NotImplementedException();
            }
        }
    }
}
