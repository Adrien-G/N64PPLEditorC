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
                case CGeneric.romLang.French: addr = 0xB4D7C;
                    //return new int[] { 0xB4D7C,0xB4D40,0xB4D68,0xB4EE4,0xB4D90,0xB4DA4,0xB4DB8,0xB4DCC,0xB4DE0,0xB4DF4,0xB4E08,
                    //                   0xB4E1C,0xB4E30,0xB4E44,0xB4E58,0xB4E6C,0xB4E80,0xB4E94,0xB4EA8,0xB4EBC,0xB4ED0,0xB4D54 };
                    break;
                case CGeneric.romLang.German: addr = 0xABFAC; break;
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
                default: throw new NotImplementedException();
            }
        }

        public static int GetMidiSongSoundBank0()
        {
            switch (romLang)
            {
                case CGeneric.romLang.French: return 0xB4B00;
                case CGeneric.romLang.German: return 0xABD30;
                default: throw new NotImplementedException();
            }
        }

    }
}
