using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{ 
    class CBFF2
    {
        private Byte[] rawData;
        
        //create the BFF2 struct, represent information of the BFF header and also calculated fields.
        public struct BFFHeader
        {
            public int sizeX;
            public int sizeY;
            public byte bytePerPixel;
        }

        public CBFF2(Byte[] rawData)
        {
            this.rawData = rawData;
        }

        public void Init()
        {

        }
    }
}
