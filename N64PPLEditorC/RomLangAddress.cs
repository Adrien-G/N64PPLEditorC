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
            int addr = 0;
            switch (romLang)
            {
                //others address are calculated by relative address.
                case CGeneric.romLang.French:   addr = 0xB4D7C; break;
                case CGeneric.romLang.German:   addr = 0xABFAC; break;
                case CGeneric.romLang.European: addr = 0xB66CC; break;
                case CGeneric.romLang.USA:      addr = 0xB61AC; break;

                default: throw new NotImplementedException();
            }
            //see if relavite address works..
            return new int[] { addr, addr - 60, addr - 20, addr + 360, addr + 20, addr + 40, addr + 60, addr + 80, addr + 100, addr + 120, addr + 140, addr + 160, addr + 180, addr + 200, addr + 220, addr + 240, addr + 260, addr + 280, addr + 300, addr + 320, addr + 340, addr - 40 };

        }

        public static int GetSizeMidiSoundBank1()
        {
            switch (romLang)
            {
                case CGeneric.romLang.French:
                case CGeneric.romLang.German: 
                    return 0x16D0;
                case CGeneric.romLang.European:
                case CGeneric.romLang.USA:
                    return 0x16B0;
                default: throw new NotImplementedException();
            }
        }

        public static int GetSizeMidiSoundBank2()
        {
            switch (romLang)
            {
                case CGeneric.romLang.French:
                case CGeneric.romLang.German:
                    return 0x2DB0;
                case CGeneric.romLang.European:
                case CGeneric.romLang.USA:
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
                case CGeneric.romLang.USA:      return 0xB5F30;
                default: throw new NotImplementedException();
            }
        }

    }
}
