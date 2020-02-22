using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //store position and size of all BFF2
            List<int> indexBFF2 = new List<int>();
            List<int> sizeBFF2 = new List<int>();

            int tmpPositionBFF2;
            //search all bff2 present in the 3FIB and grab index
            do
            {
                tmpPositionBFF2 = CGeneric.SearchBytesInArray(bffsData, CGeneric.patternBFF2, indexBFF2.Count())-12;

                if (tmpPositionBFF2 != -13)
                {
                    //add index
                    indexBFF2.Add(tmpPositionBFF2);
                    //calculate size of previous BFF2 (if not the last)
                    if (tmpPositionBFF2 != 0)
                        sizeBFF2.Add(indexBFF2[indexBFF2.Count() - 1] - indexBFF2[indexBFF2.Count() - 2]);
                }
                else
                    //calculate size of last BFF2
                    sizeBFF2.Add(bffsData.Length - indexBFF2[indexBFF2.Count() - 1]);
            } while (tmpPositionBFF2 != -13);

            //add bff2 list in the bff2Childs
            for (int i = 0; i < indexBFF2.Count(); i++)
            {
                Byte[] tmpByteBFF2 = new byte[sizeBFF2[i]];
                Array.Copy(bffsData, indexBFF2[i], tmpByteBFF2, 0, sizeBFF2[i]);
                bff2Childs.Add(new CBFF2(tmpByteBFF2));
                bff2Childs[bff2Childs.Count - 1].Init();
            }
        }

        public string getFIBName()
        {
            return System.Text.Encoding.Default.GetString(fibName);
        }

        public string GetBFFName(int index)
        {
            return bff2Childs[index].GetName();
        }

        public int GetBFFCount()
        {
            return bff2Childs.Count();
        }

        public void SaveTexture(int index,int num)
        {
            bff2Childs[index].DecompressTexture();
            Bitmap bmp = bff2Childs[index].GetBmpTexture();
            bmp.Save(CGeneric.pathExtractedTexture + num + ", " + bff2Childs[index].GetName() + ".bmp");
        }

        public void GetTexture(PictureBox pictureBox, int index)
        {
            bff2Childs[index].DecompressTexture();
            Bitmap bmp = bff2Childs[index].GetBmpTexture();
            pictureBox.Image = bmp;
        }
    }
}
