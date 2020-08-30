using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    public static class RomAddressFr
    {
        public static List<Int32> BackgroundPlayer1RomAddress { get; private set; }

        private const int nbBackgroundArenaStartingLeague = 15;

        public static void InitValues(byte[] byteData)
        {
            //for(int i = 0; i < nbBackgroundArenaStartingLeague; i++)
            //    BackgroundPlayer1RomAddress.Add();
        }
    }
}
