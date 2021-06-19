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
        private int extra1one;
        private int extra1two;
        private int centeredX;
        private int centeredY;
        private int manualSpaceX;
        private int manualSpaceY;
        private int nblines;
        private int extra5one;
        private int extra5two;

        private int posX;
        private int posY;
        //flags
        public bool  isCenteredText;
        private bool isCenteredTextFirstBit;
        private bool isCenteredTextSecondBit;
        public bool isFontSmall;
        public bool isFontMedium;
        public bool  isTextScrolling;
        public bool isHidden;
        public bool isManualSpace;
        public bool hasFontColor;
        public bool isForegroundText;
        private bool unknow3;
        private bool unknow4;
        private bool unknow8;
        private bool unknow12;
        private bool unknow28;
        private bool unknow30;
        //additional size for header (unknow Data)
        public bool isExtraSize1;

        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public int id { get; set; }
        public int group { get; set; }
        public bool isWaitingInput { get; set; }

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

        public int GetPosX()
        {
            if (!isCenteredText)
                return posX;

            var a=0;
            if (centeredX > 320 / 2)
                a = 320 - centeredX / 2;
            else
                a = 320 - centeredX;
            return a;
            
                
        }

        public int GetPosY()
        {
            if (!isCenteredText || posY > 0)
                return posY;

            return centeredY / 2;
        }

        public void SetPosX(int posX)
        {
            if (isCenteredText)
            {
            }
            else
                this.posX = posX;
        }

        public void SetPosY(int posY)
        {
            if (isCenteredText)
            {
            }
            else
                this.posY = posY;
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
            this.isExtraSize1 = CGeneric.GetBitStateFromInt(dataInitial, 2);
            this.unknow3 = CGeneric.GetBitStateFromInt(dataInitial, 3);
            this.unknow4 = CGeneric.GetBitStateFromInt(dataInitial, 4);
            this.unknow8 = CGeneric.GetBitStateFromInt(dataInitial, 8);
            this.unknow12 = CGeneric.GetBitStateFromInt(dataInitial, 12);
            this.unknow28 = CGeneric.GetBitStateFromInt(dataInitial, 28);
            this.unknow30 = CGeneric.GetBitStateFromInt(dataInitial, 30);

            //flags discovered
            this.isFontSmall = CGeneric.GetBitStateFromInt(dataInitial, 7);
            this.isCenteredTextFirstBit = CGeneric.GetBitStateFromInt(dataInitial, 9);
            this.isFontMedium = CGeneric.GetBitStateFromInt(dataInitial, 13);
            this.isCenteredTextSecondBit = CGeneric.GetBitStateFromInt(dataInitial, 17);
            this.isCenteredText = CGeneric.GetBitStateFromInt(dataInitial, 18);
            this.isTextScrolling = CGeneric.GetBitStateFromInt(dataInitial, 20);
            this.isManualSpace = CGeneric.GetBitStateFromInt(dataInitial, 21);
            this.isHidden = CGeneric.GetBitStateFromInt(dataInitial, 22);
            this.isForegroundText = CGeneric.GetBitStateFromInt(dataInitial, 26);
            this.hasFontColor = CGeneric.GetBitStateFromInt(dataInitial, 32);

            //data grabbed from the rest of the header
            this.posX = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 4, 4));
            this.posY = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 8, 4));
            this.id   = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 12, 4));
            if (CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 16, 4)) == 0)
                this.isWaitingInput = true;

            int index = 20;
            if (this.isExtraSize1)
            {
                this.extra1one = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
                this.extra1two = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index + 4, 4));
                index += 8;
            }
            if (this.isCenteredText)
            {
                this.centeredX = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
                this.centeredY = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index+4, 4));
                index += 8;
            }
            if (this.isManualSpace)
            {
                this.manualSpaceX = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
                this.manualSpaceY = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index + 4, 4));
                index += 8;
            }
            index += 4;//value always set to 1 .. skip.
            if (this.hasFontColor)
            {
                this.BackColor = Color.FromArgb(rawData[index+3], rawData[index], rawData[index + 1], rawData[index + 2]);
                this.ForeColor = Color.FromArgb(rawData[index+7], rawData[index+4], rawData[index+5], rawData[index + 6]);
                index += 8;
            }
            this.nblines = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index, 4));
            this.extra5one = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index+4, 4));
            this.extra5two = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, index+8, 4));
        }

        public byte[] GetRawData()
        {
            int newHeaderDataSize = 36;
            if (this.isExtraSize1) newHeaderDataSize += 8;
            if (this.isCenteredText) newHeaderDataSize += 8;
            if (this.isManualSpace) newHeaderDataSize += 8;
            if (this.hasFontColor) newHeaderDataSize += 8;

            byte[] newHeaderData = new byte[newHeaderDataSize];

            //set differents flags
            int flags = 0;
            CGeneric.SetBitInInt(ref flags, 2, this.isExtraSize1);
            CGeneric.SetBitInInt(ref flags, 7, this.isFontSmall);
            CGeneric.SetBitInInt(ref flags, 9, this.isCenteredTextFirstBit);
            CGeneric.SetBitInInt(ref flags, 13, this.isFontMedium);
            CGeneric.SetBitInInt(ref flags, 17, this.isCenteredTextSecondBit);
            CGeneric.SetBitInInt(ref flags, 18, this.isCenteredText);
            CGeneric.SetBitInInt(ref flags, 20, this.isTextScrolling);
            CGeneric.SetBitInInt(ref flags, 21, this.isManualSpace);
            CGeneric.SetBitInInt(ref flags, 22, this.isHidden);
            CGeneric.SetBitInInt(ref flags, 26, this.isForegroundText);
            CGeneric.SetBitInInt(ref flags, 32, this.hasFontColor);

            //set unknown flags
            CGeneric.SetBitInInt(ref flags, 3, this.unknow3);
            CGeneric.SetBitInInt(ref flags, 4, this.unknow4);
            CGeneric.SetBitInInt(ref flags, 8, this.unknow8);
            CGeneric.SetBitInInt(ref flags, 12, this.unknow12);
            CGeneric.SetBitInInt(ref flags, 28, this.unknow28);
            CGeneric.SetBitInInt(ref flags, 30, this.unknow30);
            CGeneric.SetBitInInt(ref flags, 31, true);

            Array.Copy(CGeneric.ConvertIntToByteArray(flags), 0, newHeaderData, 0, 4);

            //update X / Y position
            var sizeX = CGeneric.ConvertIntToByteArray(this.posX);
            var sizeY = CGeneric.ConvertIntToByteArray(this.posY);
            Array.Copy(sizeX, 0, newHeaderData, 4, sizeX.Length);
            Array.Copy(sizeY, 0, newHeaderData, 8, sizeY.Length);

            //update id
            var id = CGeneric.ConvertIntToByteArray(this.id);
            Array.Copy(id, 0, newHeaderData, 12, id.Length);

            if (!this.isWaitingInput)
                Array.Copy(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, newHeaderData, 16, 4);

            int index = 20;
            if (this.isExtraSize1)
            {
                Array.Copy(CGeneric.ConvertIntToByteArray(this.extra1one), 0, newHeaderData, index, 4);
                Array.Copy(CGeneric.ConvertIntToByteArray(this.extra1two), 0, newHeaderData, index+4, 4);
                index += 8;
            }
            if (this.isCenteredText) 
            {
                Array.Copy(CGeneric.ConvertIntToByteArray(this.centeredX), 0, newHeaderData, index, 4);
                Array.Copy(CGeneric.ConvertIntToByteArray(this.centeredY), 0, newHeaderData, index+4, 4);
                index += 8; 
            }
            if (this.isManualSpace)
            {
                Array.Copy(CGeneric.ConvertIntToByteArray(this.manualSpaceX), 0, newHeaderData, index, 4);
                Array.Copy(CGeneric.ConvertIntToByteArray(this.manualSpaceY), 0, newHeaderData, index+4, 4);
                index += 8; 
            }

            //special fix for header 36 (only used in debug by dev's i think)
            if(newHeaderData.Length == 36)
                Array.Copy(new byte[] { 0,0,0,1}, 0, newHeaderData, index+4, 4);
            else
                Array.Copy(new byte[] { 0, 0, 0, 1 }, 0, newHeaderData, index, 4);
            index += 4;
            
            if (this.hasFontColor)
            {
                //update forecolor and backcolor
                var foreColor = new byte[] { this.ForeColor.R, this.ForeColor.G, this.ForeColor.B, this.ForeColor.A };
                var backColor = new byte[] { this.BackColor.R, this.BackColor.G, this.BackColor.B, this.BackColor.A };
                Array.Copy(backColor, 0, newHeaderData, index, foreColor.Length);
                Array.Copy(foreColor, 0, newHeaderData, index + 4, backColor.Length);
                index += 8;
            }
            Array.Copy(CGeneric.ConvertIntToByteArray(this.nblines), 0, newHeaderData, index, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(this.extra5one), 0, newHeaderData, index + 4, 4);
            Array.Copy(CGeneric.ConvertIntToByteArray(this.extra5two), 0, newHeaderData, index + 8, 4);

            //update the text size
            var lenText = CGeneric.ConvertIntToByteArray((this.textData.Length - 4) / 2);
            Array.Copy(lenText, 0, textData, 0, lenText.Length);

            //merge the two arrays
            byte[] res = new byte[0];
            res = res.Concat(newHeaderData).ToArray();
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
                case CGeneric.romLang.JAP:
                    return ConvertByteToCharJap(letter);
                default: return '?';
            }
        }

        //thanks to April93 for the japanese translation of bytes
        private char ConvertByteToCharJap(short letter)
        {
            switch(letter){
                case 0x0000: return ' '; // spaces ?
                //Kanji
                case 0x0400 :return '人';
                case 0x0401 :return '遊';
                case 0x0402 :return '説';
                case 0x0403 :return '明';
                case 0x0404 :return '上';
                case 0x0405 :return '達';
                case 0x0406 :return '道';
                case 0x0407 :return '名';
                case 0x0408 :return '前';
                case 0x0409 :return '登';
                case 0x040A :return '録';
                case 0x040B :return '操';
                case 0x040C :return '作';
                case 0x040D :return '基';
                case 0x040E :return '本';
                case 0x040F :return '同';
                case 0x0410 :return '時';
                case 0x0411 :return '連';
                case 0x0412 :return '鎖';
                case 0x0413 :return '消';
                case 0x0414 :return '記';
                case 0x0415 :return '自';
                case 0x0416 :return '分';
                case 0x0417 :return '設';
                case 0x0418 :return '定';
                case 0x0419 :return '画';
                case 0x041A :return '面';
                case 0x041B :return '級';
                case 0x041C :return '仕';
                case 0x041D :return '方';
                case 0x041E :return '見';
                case 0x041F :return '新';
                case 0x0420 :return '下';
                case 0x0421 :return '左';
                case 0x0422 :return '右';
                case 0x0423 :return '数';
                case 0x0424 :return '最';
                case 0x0425 :return '大';
                case 0x0426 :return '妖';
                case 0x0427 :return '精';
                case 0x0428 :return '選';
                case 0x0429 :return '択';
                case 0x042A :return '手';
                case 0x042B :return '終';
                case 0x042C :return '了';
                case 0x042D :return '使';
                case 0x042E :return '問';
                case 0x042F :return '題';
                case 0x0430 :return '愛';
                case 0x0431 :return '悪';
                case 0x0432 :return '一';
                case 0x0433 :return '運';
                case 0x0434 :return '雲';
                case 0x0435 :return '炎';
                case 0x0436 :return '旺';
                case 0x0437 :return '王';
                case 0x0438 :return '温';
                case 0x0439 :return '音';
                case 0x043A :return '何';
                case 0x043B :return '歌';
                case 0x043C :return '火';
                case 0x043D :return '花';
                case 0x043E :return '会';
                case 0x043F :return '海';
                case 0x0440 :return '界';
                case 0x0441 :return '覚';
                case 0x0442 :return '感';
                case 0x0443 :return '奇';
                case 0x0444 :return '気';
                case 0x0445 :return '起';
                case 0x0446 :return '義';
                case 0x0447 :return '強';
                case 0x0448 :return '狂';
                case 0x0449 :return '驚';
                case 0x044A :return '空';
                case 0x044B :return '月';
                case 0x044C :return '元';
                case 0x044D :return '言';
                case 0x044E :return '個';
                case 0x044F :return '光';
                case 0x0450 :return '好';
                case 0x0451 :return '幸';
                case 0x0452 :return '広';
                case 0x0453 :return '荒';
                case 0x0454 :return '高';
                case 0x0455 :return '歳';
                case 0x0456 :return '在';
                case 0x0457 :return '三';
                case 0x0458 :return '姉';
                case 0x0459 :return '子';
                case 0x045A :return '事';
                case 0x045B :return '持';
                case 0x045C :return '者';
                case 0x045D :return '主';
                case 0x045E :return '守';
                case 0x045F :return '宿';
                case 0x0460 :return '出';
                case 0x0461 :return '緒';
                case 0x0462 :return '女';
                case 0x0463 :return '傷';
                case 0x0464 :return '少';
                case 0x0465 :return '情';
                case 0x0466 :return '色';
                case 0x0467 :return '心';
                case 0x0468 :return '真';
                case 0x0469 :return '神';
                case 0x046A :return '水';
                case 0x046B :return '世';
                case 0x046C :return '性';
                case 0x046D :return '正';
                case 0x046E :return '盛';
                case 0x046F :return '声';
                case 0x0470 :return '右';
                case 0x0471 :return '責';
                case 0x0472 :return '双';
                case 0x0473 :return '存';
                case 0x0474 :return '他';
                case 0x0475 :return '太';
                case 0x0476 :return '体';
                case 0x0477 :return '短';
                case 0x0478 :return '知';
                case 0x0479 :return '中';
                case 0x047A :return '仲';
                case 0x047B :return '帝';
                case 0x047C :return '敵';
                case 0x047D :return '的';
                case 0x047E :return '天';
                case 0x047F :return '怒';
                case 0x0480 :return '倒';
                case 0x0481 :return '頭';
                case 0x0482 :return '動';
                case 0x0483 :return '任';
                case 0x0484 :return '番';
                case 0x0485 :return '秘';
                case 0x0486 :return '氷';
                case 0x0487 :return '品';
                case 0x0488 :return '不';
                case 0x0489 :return '負';
                case 0x048A :return '物';
                case 0x048B :return '聞';
                case 0x048C :return '慕';
                case 0x048D :return '母';
                case 0x048E :return '宝';
                case 0x048F :return '豊';
                case 0x0490 :return '魔';
                case 0x0491 :return '妹';
                case 0x0492 :return '味';
                case 0x0493 :return '魅';
                case 0x0494 :return '命';
                case 0x0495 :return '鳴';
                case 0x0496 :return '目';
                case 0x0497 :return '役';
                case 0x0498 :return '優';
                case 0x0499 :return '勇';
                case 0x049A :return '由';
                case 0x049B :return '様';
                case 0x049C :return '羊';
                case 0x049D :return '葉';
                case 0x049E :return '陽';
                case 0x049F :return '欲';
                case 0x04A0 :return '竜';
                case 0x04A1 :return '良';
                case 0x04A2 :return '力';
                case 0x04A3 :return '緑';
                case 0x04A4 :return '霊';
                case 0x04A5 :return '話';
                case 0x04A6 :return '安';
                case 0x04A7 :return '栄';
                case 0x04A8 :return '加';
                case 0x04A9 :return '嘉';
                case 0x04AA :return '雅';
                case 0x04AB :return '介';
                case 0x04AC :return '外';
                case 0x04AD :return '学';
                case 0x04AE :return '丸';
                case 0x04AF :return '久';
                case 0x04B0 :return '宮';
                case 0x04B1 :return '居';
                case 0x04B2 :return '橋';
                case 0x04B3 :return '啓';
                case 0x04B4 :return '賢';
                case 0x04B5 :return '玄';
                case 0x04B6 :return '宏';
                case 0x04B7 :return '康';
                case 0x04B8 :return '弘';
                case 0x04B9 :return '晃';
                case 0x04BA :return '浩';
                case 0x04BB :return '豪';
                case 0x04BC :return '佐';
                case 0x04BD :return '崎';
                case 0x04BE :return '山';
                case 0x04BF :return '史';
                case 0x04C0 :return '志';
                case 0x04C1 :return '治';
                case 0x04C2 :return '実';
                case 0x04C3 :return '仁';
                case 0x04C4 :return '生';
                case 0x04C5 :return '西';
                case 0x04C6 :return '仙';
                case 0x04C7 :return '川';
                case 0x04C8 :return '浅';
                case 0x04C9 :return '村';
                case 0x04CA :return '代';
                case 0x04CB :return '滝';
                case 0x04CC :return '沢';
                case 0x04CD :return '智';
                case 0x04CE :return '池';
                case 0x04CF :return '鳥';
                case 0x04D0 :return '田';
                case 0x04D1 :return '嶋';
                case 0x04D2 :return '湯';
                case 0x04D3 :return '藤';
                case 0x04D4 :return '奈';
                case 0x04D5 :return '内';
                case 0x04D6 :return '曰';
                case 0x04D7 :return '八';
                case 0x04D8 :return '妃';
                case 0x04D9 :return '敏';
                case 0x04DA :return '夫';
                case 0x04DB :return '武';
                case 0x04DC :return '福';
                case 0x04DD :return '文';
                case 0x04DE :return '茂';
                case 0x04DF :return '木';
                case 0x04E0 :return '野';
                case 0x04E1 :return '弥';
                case 0x04E2 :return '矢';
                case 0x04E3 :return '悠';
                case 0x04E4 :return '裕';
                case 0x04E5 :return '洋';
                case 0x04E6 :return '利';
                case 0x04E7 :return '隆';
                case 0x04E8 :return '鈴';
                case 0x04E9 :return '泳';
                case 0x04EA :return '回';
                case 0x04EB :return '間';
                case 0x04EC :return '希';
                case 0x04ED :return '帰';
                case 0x04EE :return '玉';
                case 0x04EF :return '思';
                case 0x04F0 :return '借';
                case 0x04F1 :return '助';
                case 0x04F2 :return '勝';
                case 0x04F3 :return '城';
                case 0x04F4 :return '聖';
                case 0x04F5 :return '切';
                case 0x04F6 :return '全';
                case 0x04F7 :return '伝';
                case 0x04F8 :return '当';
                case 0x04F9 :return '平';
                case 0x04FA :return '返';
                case 0x04FB :return '法';
                case 0x04FC :return '望';
                case 0x04FD :return '夢';
                case 0x04FE :return '無';
                case 0x04FF :return '和';
                case 0x0500 :return '伊';
                case 0x0501 :return '井';
                case 0x0502 :return '葛';
                case 0x0503 :return '季';
                case 0x0504 :return '紀';
                case 0x0505 :return '吉';
                case 0x0506 :return '勲';
                case 0x0507 :return '軍';
                case 0x0508 :return '健';
                case 0x0509 :return '呼';
                case 0x050A :return '孝';
                case 0x050B :return '鋼';
                case 0x050C :return '剛';
                case 0x050D :return '根';
                case 0x050E :return '示';
                case 0x050F :return '殊';
                case 0x0510 :return '樹';
                case 0x0511 :return '将';
                case 0x0512 :return '松';
                case 0x0513 :return '晋';
                case 0x0514 :return '森';
                case 0x0515 :return '瀬';
                case 0x0516 :return '清';
                case 0x0517 :return '善';
                case 0x0518 :return '泰';
                case 0x0519 :return '谷';
                case 0x051A :return '津';
                case 0x051B :return '島';
                case 0x051C :return '特';
                case 0x051D :return '二';
                case 0x051E :return '之';
                case 0x051F :return '美';
                case 0x0520 :return '百';
                case 0x0521 :return '表';
                case 0x0522 :return '富';
                case 0x0523 :return '扶';
                case 0x0524 :return '輔';
                case 0x0525 :return '北';
                case 0x0526 :return '也';
                case 0x0527 :return '亮';
                case 0x0528 :return '呂';
                case 0x0529 :return '郎';
                case 0x052A :return '修';
                case 0x052B :return '正';
                case 0x052C :return '戻';
                case 0x052D :return '峻';
                case 0x052E :return '潘';
                case 0x052F :return '薙';
                case 0x0530 :return '九';
                case 0x0531 :return '後';
                case 0x0532 :return '角';
                case 0x0533 :return '四';
                case 0x0534 :return '路';
                case 0x0535 :return '亜';
                case 0x0536 :return '岸';
                case 0x0537 :return '有';
                case 0x0538 :return '寺';
                case 0x0539 :return '永';
                case 0x053A :return '圭';
                case 0x053B :return '渚';
                case 0x053C :return '今';
                case 0x053D :return '足';
                case 0x053E :return '立';
                case 0x053F :return '綾';
                case 0x0540 :return '舞';
                case 0x0541 :return '鳴';
                case 0x0542 :return '佳';
                case 0x0543 :return '英';
                case 0x0544 :return '細';
                case 0x0545 :return '俊';
                case 0x0546 :return '宗';
                case 0x0547 :return '章';
                case 0x0548 :return '小';
                case 0x0549 :return '寺';
                case 0x054A :return '明';
                case 0x054B :return '黒';
                case 0x054C :return '育';
                case 0x054D :return '辻';
                case 0x054E :return '臣';
                case 0x054F :return '江';
                case 0x0550 :return '口';
                case 0x0551 :return '勉';
                case 0x0552 :return '差';
                case 0x0553 :return '準';
                case 0x0554 :return '備';
                case 0x0555 :return '練';
                case 0x0556 :return '習';
                case 0x0557 :return '建';
                case 0x0558 :return '林';
                case 0x0559 :return '絵';
                case 0x055A :return '里';
                case 0x055B :return '伯';
                case 0x055C :return '直';
                case 0x055D :return '敬';
                case 0x055E :return '春';
                case 0x055F :return '竹';
                case 0x0560 :return '男';
                case 0x0561 :return '理';
                case 0x0562 :return '風';
                //katakana
                case 0x0800 :return 'ア';
                case 0x0801 :return 'イ';
                case 0x0802 :return 'ウ';
                case 0x0803 :return 'エ';
                case 0x0804 :return 'オ';
                case 0x0805 :return 'カ';
                case 0x0806 :return 'キ';
                case 0x0807 :return 'ク';
                case 0x0808 :return 'ケ';
                case 0x0809 :return 'コ';
                case 0x080A :return 'サ';
                case 0x080B :return 'シ';
                case 0x080C :return 'ス';
                case 0x080D :return 'セ';
                case 0x080E :return 'ソ';
                case 0x080F :return 'タ';
                case 0x0810 :return 'チ';
                case 0x0811 :return 'ツ';
                case 0x0812 :return 'テ';
                case 0x0813 :return 'ト';
                case 0x0814 :return 'ナ';
                case 0x0815 :return 'ニ';
                case 0x0816 :return 'ヌ';
                case 0x0817 :return 'ネ';
                case 0x0818 :return 'ノ';
                case 0x0819 :return 'ハ';
                case 0x081A :return 'ヒ';
                case 0x081B :return 'フ';
                case 0x081C :return 'ヘ';
                case 0x081D :return 'ホ';
                case 0x081E :return 'マ';
                case 0x081F :return 'ミ';
                case 0x0820 :return 'ム';
                case 0x0821 :return 'メ';
                case 0x0822 :return 'モ';
                case 0x0823 :return 'ヤ';
                case 0x0824 :return 'ユ';
                case 0x0825 :return 'ヨ';
                case 0x0826 :return 'ラ';
                case 0x0827 :return 'リ';
                case 0x0828 :return 'ル';
                case 0x0829 :return 'レ';
                case 0x082A :return 'ロ';
                case 0x082B :return 'ワ';
                case 0x082C :return 'ヲ';
                case 0x082D :return 'ン';
                case 0x082E :return 'ガ';
                case 0x082F :return 'ギ';
                case 0x0830 :return 'グ';
                case 0x0831 :return 'ゲ';
                case 0x0832 :return 'ゴ';
                case 0x0833 :return 'ザ';
                case 0x0834 :return 'ジ';
                case 0x0835 :return 'ズ';
                case 0x0836 :return 'ゼ';
                case 0x0837 :return 'ゾ';
                case 0x0838 :return 'ダ';
                case 0x0839 :return 'ヂ';
                case 0x083A :return 'ズ';
                case 0x083B :return 'デ';
                case 0x083C :return 'ド';
                case 0x083D :return 'バ';
                case 0x083E :return 'ビ';
                case 0x083F :return 'ブ';
                case 0x0840 :return 'ベ';
                case 0x0841 :return 'ボ';
                case 0x0842 :return '?';
                case 0x0843 :return 'パ';
                case 0x0844 :return 'ピ';
                case 0x0845 :return 'プ';
                case 0x0846 :return 'ペ';
                case 0x0847 :return 'ポ';
                case 0x0848 :return 'ァ';
                case 0x0849 :return 'ィ';
                case 0x084A :return 'ゥ';
                case 0x084B :return 'ェ';
                case 0x084C :return 'ォ';
                case 0x084D :return 'ャ';
                case 0x084E :return 'ュ';
                case 0x084F :return 'ョ';
                case 0x0850: return 'ッ';
                case 0x0851: return 'ッ';
                //Hiragana
                case 0x0C00: return 'あ';
                case 0x0C01: return 'い';
                case 0x0C02: return 'う';
                case 0x0C03: return 'え';
                case 0x0C04: return 'お';
                case 0x0C05: return 'か';
                case 0x0C06: return 'き';
                case 0x0C07: return 'く';
                case 0x0C08: return 'け';
                case 0x0C09: return 'こ';
                case 0x0C0A: return 'さ';
                case 0x0C0B: return 'し';
                case 0x0C0C: return 'す';
                case 0x0C0D: return 'せ';
                case 0x0C0E: return 'そ';
                case 0x0C0F: return 'た';
                case 0x0C10: return 'ち';
                case 0x0C11: return 'つ';
                case 0x0C12: return 'て';
                case 0x0C13: return 'と';
                case 0x0C14: return 'な';
                case 0x0C15: return 'に';
                case 0x0C16: return 'ぬ';
                case 0x0C17: return 'ね';
                case 0x0C18: return 'の';
                case 0x0C19: return 'は';
                case 0x0C1A: return 'ひ';
                case 0x0C1B: return 'ふ';
                case 0x0C1C: return 'へ';
                case 0x0C1D: return 'ほ';
                case 0x0C1E: return 'ま';
                case 0x0C1F: return 'み';
                case 0x0C20: return 'む';
                case 0x0C21: return 'め';
                case 0x0C22: return 'も';
                case 0x0C23: return 'や';
                case 0x0C24: return 'ゆ';
                case 0x0C25: return 'よ';
                case 0x0C26: return 'ら';
                case 0x0C27: return 'り';
                case 0x0C28: return 'る';
                case 0x0C29: return 'れ';
                case 0x0C2A: return 'ろ';
                case 0x0C2B: return 'わ';
                case 0x0C2C: return 'を';
                case 0x0C2D: return 'ん';
                case 0x0C2E: return 'が';
                case 0x0C2F: return 'ぎ';
                case 0x0C30: return 'ぐ';
                case 0x0C31: return 'げ';
                case 0x0C32: return 'ご';
                case 0x0C33: return 'ざ';
                case 0x0C34: return 'じ';
                case 0x0C35: return 'ず';
                case 0x0C36: return 'ぜ';
                case 0x0C37: return 'ぞ';
                case 0x0C38: return 'だ';
                case 0x0C39: return 'ぢ';
                case 0x0C3A: return 'づ';
                case 0x0C3B: return 'で';
                case 0x0C3C: return 'ど';
                case 0x0C3D: return 'ば';
                case 0x0C3E: return 'び';
                case 0x0C3F: return 'ぶ';
                case 0x0C40: return 'べ';
                case 0x0C41: return 'ぼ';
                case 0x0C42: return 'ぱ';
                case 0x0C43: return 'ぴ';
                case 0x0C44: return 'ぷ';
                case 0x0C45: return 'ぺ';
                case 0x0C46: return 'ぽ';
                case 0x0C47: return 'ぁ';
                case 0x0C48: return 'ぃ';
                case 0x0C49: return 'ぅ';
                case 0x0C4A: return 'ぇ';
                case 0x0C4B: return 'ぉ';
                case 0x0C4C: return 'ゃ';
                case 0x0C4D: return 'ゅ';
                case 0x0C4E: return 'ょ';
                case 0x0C4F: return 'っ';
                case 0x0C50: return 'っ';
                default: return ConvertByteToCharEurOrUsa(letter);
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
                default: return ' ';
                //default: throw new NotImplementedException();
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
