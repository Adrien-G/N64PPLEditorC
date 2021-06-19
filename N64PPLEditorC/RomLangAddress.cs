using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{

    //all option who need specific address or "hardcoded" address (so depending of the rom lang.
    static class RomLangAddress
    {
        //this value is set when rom is loaded
        public static CGeneric.romLang romLang;

        public static int[] GetSoundBankPtrTable()
        {
            int starting = 0;
            switch (romLang)
            {
                //others address are calculated by relative address.
                case CGeneric.romLang.French:   starting = 0xB4D7C; break;
                case CGeneric.romLang.German:   starting = 0xABFAC; break;
                case CGeneric.romLang.European: starting = 0xB66CC; break;
                case CGeneric.romLang.USA:      starting = 0xB61AC; break;
                default: throw new NotImplementedException();
            }
            //relative address
            return new int[] { starting, starting - 60, starting - 20, starting + 360, starting + 20, starting + 40, starting + 60, starting + 80, starting + 100, starting + 120, starting + 140, starting + 160, starting + 180, starting + 200, starting + 220, starting + 240, starting + 260, starting + 280, starting + 300, starting + 320, starting + 340, starting - 40 };

        }

        public static int GetSizeMidiSoundBank1()
        {
            switch (romLang)
            {
                case CGeneric.romLang.French:
                    return 0x16D0;
                case CGeneric.romLang.German:
                    return 0x16E0;
                case CGeneric.romLang.European:
                case CGeneric.romLang.USA:
                case CGeneric.romLang.JAP:
                    return 0x16B0;
                default: throw new NotImplementedException();
            }
        }

        public static int GetSizeMidiSoundBank2()
        {
            switch (romLang)
            {
                case CGeneric.romLang.French:
                    return 0x2DB0;
                case CGeneric.romLang.German:
                    return 0x2760;
                case CGeneric.romLang.European:
                case CGeneric.romLang.USA:
                case CGeneric.romLang.JAP:
                    return 0x2730;
                default: throw new NotImplementedException();
            }
        }

        public static int GetMidiSongSoundBank0()
        {
            switch (romLang)
            {
                case CGeneric.romLang.French:   return 0xB4B00;
                case CGeneric.romLang.German:   return 0xABD30;
                case CGeneric.romLang.European: return 0xB6450;
                case CGeneric.romLang.JAP:
                case CGeneric.romLang.USA:      return 0xB5F30;
                default: throw new NotImplementedException();
            }
        }

    }
}
