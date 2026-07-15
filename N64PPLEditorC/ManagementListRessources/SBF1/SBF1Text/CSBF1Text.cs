using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace N64PPLEditorC
{
    public class CSBF1Text
    {
        private byte[] RawData;
        public List<CSBF1TextCode> Character;

        public CSBF1Text()
        {
            Character = new List<CSBF1TextCode>();
        }

        public CSBF1Text(ref int index, byte[] rawData)
        {
            RawData = rawData;
            Character = new List<CSBF1TextCode>();
            var lenText = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(RawData, index, 4));
            index += 4;
            for (int i = 0; i < lenText; i++)
            {
                var byteArr = CGeneric.GiveMeArray(RawData, index, 2);
                Character.Add(new CSBF1TextCode(CGeneric.ConvertBytesToUshort(byteArr)));
                index += 2;
            }
        }

        public string GetAsciiText()
        {
            var sb = new StringBuilder();
            foreach (var car in Character)
            {
                sb.Append(car.GetAsciiChar());
            }
            return sb.Replace("#", Environment.NewLine).ToString();
        }

        public void SetAsciiText(string asciiText)
        {
            asciiText = asciiText.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "#").ToLower();
            Character = new List<CSBF1TextCode>();
            var sb = new StringBuilder();
            bool isSpecialCar = false;


            for (int i = 0; i < asciiText.Length; i++)
            {
                if (asciiText[i] == '[')
                    isSpecialCar = true;
                if (asciiText[i] == ']')
                {
                    isSpecialCar = false;
                    sb.Append(asciiText[i]);
                    Character.Add(new CSBF1TextCode(sb.ToString()));
                    sb.Clear();
                    continue;
                }

                if (isSpecialCar)
                    sb.Append(asciiText[i]);
                else
                    Character.Add(new CSBF1TextCode(asciiText[i].ToString()));
            }
            if (isSpecialCar)
                throw new Exception("Le caractère n'est pas terminé correctement");
        }
    }
}