using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC.ManagementRomData
{
    class AssemblyReversedAddress
    {
        private byte J2DifficultyEasy { get; set; }
        private byte J2DifficultyMedium { get; set; }
        private byte J2DifficultyHard { get; set; }
        private byte J2DiffcultyVeryHard0102 { get; set; }
        private byte J2DiffcultyVeryHard0304 { get; set; }
        private byte J2DiffcultyVeryHard0507 { get; set; }
        private byte J2DiffcultyVeryHard0810 { get; set; }
        private byte J2DiffcultyVeryHard1113 { get; set; }
        private byte J2DiffcultyVeryHard1416 { get; set; }
        private byte J2DifficultySuperHard { get; set; }

        //default difficulty for easy mode
        private readonly byte easyModeDifficulty = 0x23;
        private byte difficulty;
        public AssemblyReversedAddress(byte[] buffrom) {
            difficulty = 0;
            switch (RomLangAddress.romLang)
            {
                case CGeneric.romLang.French:
                    J2DifficultyEasy = buffrom[0x74CCF];
                    J2DifficultyMedium = buffrom[0x74D53];
                    J2DifficultyHard = buffrom[0x74DEF];
                    //J2DiffcultyVeryHard0102 = buffrom[];
                    //J2DiffcultyVeryHard0304 = buffrom[];
                    //J2DiffcultyVeryHard0507 = buffrom[];
                    //J2DiffcultyVeryHard0810 = buffrom[];
                    //J2DiffcultyVeryHard1113 = buffrom[];
                    //J2DiffcultyVeryHard1416 = buffrom[];
                    break;
            }
        }

        public int GetDifficultyLevel()
        {
            return easyModeDifficulty- J2DifficultyEasy;
        }

        public void WriteToRom(FileStream fs)
        {
            long[] addressToModify = new long[0];
            switch (RomLangAddress.romLang)
            {
                case CGeneric.romLang.French:
                    addressToModify = new long[] { 0x74CCF, 0x74D53,0x74DEF };
                    break;
            }
            var tmpByte = 0;
            for (int i = 0; i < addressToModify.Length; i++)
            {
                fs.Position = addressToModify[i];
                tmpByte = fs.ReadByte();
                fs.Position = addressToModify[i];
                fs.WriteByte((byte)(tmpByte + difficulty));
            }
        }

        internal void SetDifficulty(int value)
        {
            this.difficulty = (byte)value;
        }
    }
}
