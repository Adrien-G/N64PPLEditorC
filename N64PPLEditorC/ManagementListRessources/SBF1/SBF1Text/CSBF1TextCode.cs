using System;
using System.CodeDom;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.XPath;

namespace N64PPLEditorC
{
    /// <summary>
    /// représente une donnée de 2 bytes, comprenant différentes informations sur le texte
    /// </summary>
    public class CSBF1TextCode
    {
        public ushort RawData { get; private set; }

        //type de la donnée (famille)
        private int Type { get; set; }
        //valeur de la donnée (précision)
        private int Value { get; set; }

        public CSBF1TextCode(ushort rawData)
        {
            RawData = rawData;
            SetRawDataToTypeAndValue();
        }

        public CSBF1TextCode(string asciiCode)
        {
            SetFromAsciiCar(asciiCode);
        }

        private void SetRawDataToTypeAndValue()
        {
            Type = (byte)(RawData >> 10); // les 6 premiers bits uniquement
            Value = (ushort)(RawData & 0x03FF); // les 10 derniers
        }

        public string GetAsciiChar()
        {
            switch (Type)
            {
                case 0x00: return GetSpecialCar();
                case 0x04: return GetSymbolAndPunctuation();
                case 0x05: return GetNumber();
                case 0x06: return GetLetter();
                case 0x20: return "[" + RawData.ToString("X4") + "]";//possiblement permet de modifier la cadance du progressif
                case 0x21: return "[" + RawData.ToString("X4") + "]";//délai d'affichage avant de passer au prochain caractère
                case 0x22: return "[" + RawData.ToString("X4") + "]";//unknow for now
                case 0x23: return "[" + RawData.ToString("X4") + "]";//unknow for now
                case 0x24: return "[" + RawData.ToString("X4") + "]";//force une position en X via les XPositions
                case 0x25: return "[" + RawData.ToString("X4") + "]";//permet de changer la police/style
                case 0x26: return "[" + RawData.ToString("X4") + "]";//changement de palette
                case 0x27: return "[" + RawData.ToString("X4") + "]";//définir directement X
                case 0x28: return "[" + RawData.ToString("X4") + "]";//unknow for now (active un flag de contrôle)
                default:
                    throw new NotImplementedException();
            }
        }

        public void SetFromAsciiCar(string car)
        {
            if (car.Contains("[")) // cas des champs spécifiques
            {
                car = car.Replace("[", "").Replace("]", "");
                var rawBytes = CGeneric.ConvertRawStringToByteArray(car);
                RawData = CGeneric.ConvertBytesToUshort(rawBytes);
                SetRawDataToTypeAndValue();
            }
            else
            {
                SetTypeAndValues(car);
                RawData = (ushort)(((Type & 0x3F) << 10) | (Value & 0x03FF));
            }
        }

        private void SetTypeAndValues(string car)
        {
            Value = -1;

            //special car
            switch (car)
            {
                case " ": Value = 0x01; break;
                case "#": Value = 0x02; break;//Hack, will be remplaced by crlf
            }

            if (Value != -1)
            {
                Type = 0x00;
                return;
            }
                

            //symboles et ponctuation
            switch (car)
            {
                case "!": Value = 0x01; break;
                case "&": Value = 0x02; break;
                case "(": Value = 0x03; break;
                case ")": Value = 0x04; break;
                case "_": Value = 0x05; break;
                case "-": Value = 0x06; break;
                case "+": Value = 0x07; break;
                case "=": Value = 0x08; break;
                case "\\": Value = 0x09; break;
                case ";": Value = 0x0A; break;
                case ":": Value = 0x0B; break;
                case "\"": Value = 0x0C; break;
                case "\'": Value = 0x0D; break;
                case ",": Value = 0x0E; break;
                case ".": Value = 0x0F; break;
                case "?": Value = 0x10; break;
                case "/": Value = 0x11; break;
                case ">": Value = 0x12; break;
                case "<": Value = 0x13; break;
                case "É": Value = 0x14; break;
                case "©": Value = 0x15; break;
            }

            if (Value != -1)
            {
                Type = 0x04;
                return;
            }

            //nombres
            switch (car)
            {
                case "1": Value = 0x01; break;
                case "2": Value = 0x02; break;
                case "3": Value = 0x03; break;
                case "4": Value = 0x04; break;
                case "5": Value = 0x05; break;
                case "6": Value = 0x06; break;
                case "7": Value = 0x07; break;
                case "8": Value = 0x08; break;
                case "9": Value = 0x09; break;
                case "0": Value = 0x0A; break;
            }

            if (Value != -1)
            {
                Type = 0x05;
                return;
            }

            //lettres
            switch (car)
            {
                case "a": Value = 0x01; break;
                case "b": Value = 0x02; break;
                case "c": Value = 0x03; break;
                case "ç": Value = 0x04; break;
                case "d": Value = 0x05; break;
                case "e": Value = 0x06; break;
                case "f": Value = 0x07; break;
                case "g": Value = 0x08; break;
                case "h": Value = 0x09; break;
                case "i": Value = 0x0A; break;
                case "j": Value = 0x0B; break;
                case "k": Value = 0x0C; break;
                case "l": Value = 0x0D; break;
                case "m": Value = 0x0E; break;
                case "n": Value = 0x0F; break;
                case "o": Value = 0x10; break;
                case "p": Value = 0x11; break;
                case "q": Value = 0x12; break;
                case "r": Value = 0x13; break;
                case "s": Value = 0x14; break;
                case "t": Value = 0x15; break;
                case "u": Value = 0x16; break;
                case "v": Value = 0x17; break;
                case "w": Value = 0x18; break;
                case "x": Value = 0x19; break;
                case "y": Value = 0x1A; break;
                case "z": Value = 0x1B; break;

            }

            if (Value != -1)
            {
                Type = 0x06;
                return;
            }

            
            throw new NotImplementedException();

        }

        private string GetSpecialCar()
        {
            switch (Value)
            {
                case 0x01: return " ";
                case 0x02: return "#"; //Hack, will be remplaced by crlf
                default: throw new NotImplementedException();
            }
        }

        private string GetNumber()
        {
            switch (Value)
            {
                case 0x01: return "1";
                case 0x02: return "2";
                case 0x03: return "3";
                case 0x04: return "4";
                case 0x05: return "5";
                case 0x06: return "6";
                case 0x07: return "7";
                case 0x08: return "8";
                case 0x09: return "9";
                case 0x0A: return "0";
                default: throw new NotImplementedException();
            }
        }

        private string GetSymbolAndPunctuation()
        {
            switch (Value)
            {
                case 0x01: return "!";
                case 0x02: return "&";
                case 0x03: return "(";
                case 0x04: return ")";
                case 0x05: return "_";
                case 0x06: return "-";
                case 0x07: return "+";
                case 0x08: return "=";
                case 0x09: return "\\";
                case 0x0A: return ";";
                case 0x0B: return ":";
                case 0x0C: return "\"";
                case 0x0D: return "\'";
                case 0x0E: return ",";
                case 0x0F: return ".";
                case 0x10: return "?";
                case 0x11: return "/";
                case 0x12: return ">";
                case 0x13: return "<";
                case 0x14: return "É";
                case 0x15: return "©";
                    default: throw new NotImplementedException();
            }
        }

        public string GetLetter()
        {
            switch (Value)
            {
                case 0x01: return "a";
                case 0x02: return "b";
                case 0x03: return "c";
                case 0x04: return "ç";
                case 0x05: return "d";
                case 0x06: return "e";
                case 0x07: return "f";
                case 0x08: return "g";
                case 0x09: return "h";
                case 0x0A: return "i";
                case 0x0B: return "j";
                case 0x0C: return "k";
                case 0x0D: return "l";
                case 0x0E: return "m";
                case 0x0F: return "n";
                case 0x10: return "o";
                case 0x11: return "p";
                case 0x12: return "q";
                case 0x13: return "r";
                case 0x14: return "s";
                case 0x15: return "t";
                case 0x16: return "u";
                case 0x17: return "v";
                case 0x18: return "w";
                case 0x19: return "x";
                case 0x1A: return "y";
                case 0x1B: return "z";
                default:
                    throw new NotImplementedException();
            }   
        }
    }
}