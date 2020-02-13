using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CSBF1
    {
        private Byte[] rawData;
        private Byte[] ressourceName;

        public CSBF1(Byte[] rawData, Byte[] ressourceName)
        {
            this.rawData = rawData;
            this.ressourceName = ressourceName;
        }
    }
}
