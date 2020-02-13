using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CHVQM
    {
        private Byte[] rawData;
        private Byte[] ressourceName;

        public CHVQM(Byte[] rawData, Byte[] ressourceName)
        {
            this.rawData = rawData;
            this.ressourceName = ressourceName;
        }
    }
}
