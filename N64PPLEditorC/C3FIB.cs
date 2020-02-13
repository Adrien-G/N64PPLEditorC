using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class C3FIB
    {

        private Byte[] data3Fib;
        private List<CBFF2> bff2Childs;
        private Byte[] ressourceName;
        private Byte[] fibName;
        private Byte[] header3FIB;
        private Byte textureType;


        public C3FIB(Byte[] data3Fib, Byte[] ressourceName)
        {
            this.data3Fib = data3Fib;
            bff2Childs = new List<CBFF2>();
            this.ressourceName = ressourceName;
        }

        public void Init()
        {
            //get bff count and fibName size
            textureType = data3Fib[4];
            Byte bffCount = data3Fib[12];
            Byte fibNameSize = data3Fib[16];

            //keep fibName
            fibName = new byte[16 + fibNameSize];
            Array.Copy(data3Fib,20,fibName,0,fibNameSize);

            //keep header information
            header3FIB = new byte[20 + fibNameSize];
            Array.Copy(data3Fib,0,header3FIB,0,header3FIB.Length);

            //exclude the header and prepare and Chunk each BFF2
            Byte[] bffData = new byte[data3Fib.Length- fibNameSize - 20];
            Array.Copy(data3Fib, 20 + fibNameSize,bffData,0,bffData.Length);
            MakeBFF2Chunks(bffData,bffCount);

        }

        private void MakeBFF2Chunks(Byte[] bffsData,int bffCount)
        {
            int indexBFF2 = 0;
            do
            {
                //beware, maybe add index pattern to search byte in array... (the n number...)
                indexBFF2 = CGeneric.SearchBytesInArray(bffsData, CGeneric.patternBFF2);

                



            } while (indexBFF2 != -1);
        }

    }
}
