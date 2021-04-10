using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static N64PPLEditorC.CGeneric;

namespace N64PPLEditorC
{
    class C3FIB : AbsRessource
    {
        private List<CBFF2> bff2Childs;
        private Byte[] header3FIB;
        private Byte[] fibName;
        private byte fibNameSize;
        //only for keep compression ratio
        public Compression compressionType;

        public C3FIB(Byte[] rawData, Byte[] ressourceName) : base(rawData, ressourceName)
        {
            bff2Childs = new List<CBFF2>();
        }

        public void Init()
        {
            //get bff count and fibName size
            Byte bffCount = rawData[12];
            fibNameSize = rawData[16];

            //keep fibName
            fibName = new byte[16 + fibNameSize];
            Array.Copy(rawData, 20, fibName, 0, fibNameSize);

            //keep header information
            header3FIB = new byte[20 + fibNameSize];
            Array.Copy(rawData, 0, header3FIB, 0, header3FIB.Length);

            //exclude the header and prepare and Chunk each BFF2
            Byte[] bffData = new byte[rawData.Length - fibNameSize - 20];
            Array.Copy(rawData, 20 + fibNameSize, bffData, 0, bffData.Length);
            MakeBFF2Chunks(bffData, bffCount);
            
            //keep compression information based on the first BFF2
            if(bff2Childs.Count > 0)
                compressionType = bff2Childs[0].GetCompressionType();

        }

        private void MakeBFF2Chunks(Byte[] bffsData, int bffCount)
        {
            //store position and size of all BFF2
            List<int> indexBFF2 = new List<int>();
            List<int> sizeBFF2 = new List<int>();

            int tmpPositionBFF2;
            //search all bff2 present in the 3FIB and grab index
            
            do
            {
                tmpPositionBFF2 = CGeneric.SearchBytesInArray(bffsData, CGeneric.patternBFF2, indexBFF2.Count()) - 12;

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
                    try
                    {
                        sizeBFF2.Add(bffsData.Length - indexBFF2[indexBFF2.Count() - 1]);
                    } catch { } //can occur only when modified rom doesn't have any BFF2
                    
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

        public void AddBFF2Child(byte[] bff2Child)
        {
            bff2Childs.Add(new CBFF2(bff2Child));
            bff2Childs[bff2Childs.Count - 1].Init();
        }

        public void RemoveBFF2Child(int index)
        {
            bff2Childs.RemoveAt(index);
        }

        public string GetBFFName(int index)
        {
            return bff2Childs[index].GetName();
        }

        public int GetBFFCount()
        {
            return bff2Childs.Count();
        }

        public void SaveTexture(int indexFIB,int index)
        {
            bff2Childs[index].DecompressTexture();
            Bitmap bmp = bff2Childs[index].GetBmpTexture();

            bmp.Save(CGeneric.pathExtractedTexture + (indexFIB + 1) + "-" + (index + 1) + ", " + bff2Childs[index].GetName() + ".png");
        }

        public void GetTexture(PictureBox pictureBox, int index)
        {
            bff2Childs[index].DecompressTexture();
            Bitmap bmp = bff2Childs[index].GetBmpTexture();
            pictureBox.Image = bmp;
        }

        public Bitmap GetBmpTexture(int index)
        {
            bff2Childs[index].DecompressTexture();
            return bff2Childs[index].GetBmpTexture();
        }


        public override Int32 GetSize()
        {
            int totalSize = fibNameSize + 20;
            for (int i = 0; i < bff2Childs.Count; i++)
            {
                totalSize += bff2Childs[i].GetSize();
            }
            return totalSize;
        }

        public string GetFIBName()
        {
            return System.Text.Encoding.UTF8.GetString(fibName);
        }

        public override byte[] GetRawData()
        {
            Byte[] res = new byte[this.GetSize()];
            header3FIB[12] = (byte)bff2Childs.Count();
            
            Array.Copy(header3FIB, 0, res, 0,header3FIB.Length);
            int indexDst = header3FIB.Length;

            for (int i = 0; i < bff2Childs.Count; i++)
            {
                Array.Copy(bff2Childs[i].GetRawData(),0,res, indexDst,bff2Childs[i].GetSize());
                indexDst += bff2Childs[i].GetSize();
            }
            return res;
        }

        public CBFF2 GetBFF2(int index)
        {
            return bff2Childs[index];
        }

        public TextureDisplayStyle GetTextureDisplayStyle()
        {
            return (TextureDisplayStyle)this.header3FIB[4];
        } 
        public void SetTextureDisplayStyle(TextureDisplayStyle style)
        {
            this.header3FIB[4] = (byte)style;
        }
    }
}
