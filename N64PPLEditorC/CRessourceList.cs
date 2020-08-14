using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N64PPLEditorC
{
    class CRessourceList
    {
        
        private struct ListFormat
        {
            public Byte[] ressourceSize;
            public Byte[] ressourceIndex;
            public Byte[] ressourceName;
        }

        private List<ListFormat> ressourcesList;

        private List<C3FIB> fibList;
        private List<CHVQM> hvqmList;
        private List<CSBF1> sbfList;
        private List<CRTF> rtfList;

        private int indexRessourcesStart;
        private int indexRessourcesEnd;

        public CRessourceList(int indexRessourcesStart,int indexRessourcesEnd)
        {
            fibList = new List<C3FIB>();
            hvqmList = new List<CHVQM>();
            sbfList = new List<CSBF1>();
            rtfList = new List<CRTF>();
            this.indexRessourcesStart = indexRessourcesStart;
            this.indexRessourcesEnd = indexRessourcesEnd;
        }

        public void Init(int nbElements, byte[] ressourcesList, byte[] ressourcesData)
        {
            this.ressourcesList = new List<ListFormat>();
            LoadRessourcesList(nbElements,ressourcesList);
            ChunkDataToRessources(ressourcesData);
        }

        private void LoadRessourcesList(int nbElements, Byte[] ressourcesList)
        {
            for (int i = 0; i < nbElements; i++)
            {
                var element = new ListFormat();
                element.ressourceIndex = new byte[4];
                element.ressourceSize = new byte[4];
                element.ressourceName = new byte[16];
                Array.Copy(ressourcesList, i * CGeneric.sizeOfElementTable, element.ressourceSize, 0, 4);
                Array.Copy(ressourcesList, i * CGeneric.sizeOfElementTable + 4, element.ressourceIndex, 0, 4);
                Array.Copy(ressourcesList, i * CGeneric.sizeOfElementTable + 8, element.ressourceName, 0, 16);
                this.ressourcesList.Add(element);
            }
        }

        private void ChunkDataToRessources(Byte[] ressourceData)
        {
            int generalIndex = 0;
            
            // check all the ressources list...
            for( int i = 0; i < ressourcesList.Count(); i++)
            {
                //grab only one item by one
                int sizeElement = CGeneric.ConvertByteArrayToInt(ressourcesList[i].ressourceSize);
                Byte[] tmpContainerData = new byte[sizeElement];
                Array.Copy(ressourceData, generalIndex,tmpContainerData,0,tmpContainerData.Length);

                //determine the type of data and fill in the apropriated list
                Byte[] dataPattern = new byte[4];
                Array.Copy(tmpContainerData, 0, dataPattern, 0, dataPattern.Length);

                switch (CGeneric.ConvertByteArrayToInt(dataPattern))
                {
                    case (int)CGeneric.RessourceType.FIB:
                        fibList.Add(new C3FIB(tmpContainerData, ressourcesList[i].ressourceName));
                        fibList[fibList.Count - 1].Init();
                        break;
                    case (int)CGeneric.RessourceType.HVQM:
                        hvqmList.Add(new CHVQM(tmpContainerData, ressourcesList[i].ressourceName));
                        break;
                    case (int)CGeneric.RessourceType.SBF:
                        sbfList.Add(new CSBF1(tmpContainerData, ressourcesList[i].ressourceName));
                        break;
                    default:
                        rtfList.Add(new CRTF(tmpContainerData, ressourcesList[i].ressourceName));
                        break;
                }
                generalIndex += sizeElement;
                if (sizeElement % 2 == 1)
                    generalIndex += 1;
            }
        }

        //fib to simplify
        public CSBF1 GetSBF1(int index)
        {
            return sbfList[index];
        }
        
        public C3FIB Get3FIB(int indexFib)
        {
            return fibList[indexFib];
        }

        public int Get3FIBIndexWithFIBName(string name)
        {
            int increm = 0;
            var b = name.ToUpper().Replace("\0", "");
            for (int z = 0; z < ressourcesList.Count(); z++)
            {
                var a = CGeneric.ConvertByteArrayToString(ressourcesList[z].ressourceName).Replace("\0","");
                if (a == b)
                {
                    return increm;
                }
                    
                if (a.EndsWith(".BIF"))
                    increm++;
            }
            return -1;
        }

        public CHVQM GetHVQM(int indexHVQM)
        {
            return hvqmList[indexHVQM];
        }

        public int GetFIBCount()
        {
            return fibList.Count();
        }
        public int GetSBFCount()
        {
            return sbfList.Count();
        }
        public int GetHVQMCount()
        {
            return hvqmList.Count();
        }
        public int GetRTFCount()
        {
            return rtfList.Count();
        }

        public int GetTotalBFFCount()
        {
            int total = 0;
            for (int i = 0; i < fibList.Count; i++)
            {
                total += this.fibList[i].GetBFFCount();
            }
            return total;
        }

        public void WriteAllData(string path)
        {
            FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Write);
            fs.Position = indexRessourcesStart;

            // write the number of elements
            fs.Write(CGeneric.ConvertIntToByteArray(fibList.Count() + GetHVQMCount() + GetSBFCount()+GetRTFCount()), 0, 4);

            // index of data (for writing header)
            int indexData = (fibList.Count() + GetHVQMCount() + GetSBFCount() + GetRTFCount()) * 24 + 4;

            //write list header (FIB,HVQM,SBF1)
            WriteListHeader(ref fs, ref indexData, fibList);
            WriteListHeader(ref fs, ref indexData, hvqmList);
            WriteListHeader(ref fs, ref indexData, sbfList);
            WriteListHeader(ref fs, ref indexData, rtfList);

            //write data associated
            WriteRessourceData(ref fs, fibList);
            WriteRessourceData(ref fs, hvqmList);
            WriteRessourceData(ref fs, sbfList);
            WriteRessourceData(ref fs, rtfList);

            fs.Close();
        }

        private void WriteRessourceData<T>(ref FileStream fs, List<T> listOfressource) where T : AbsRessource
        {
            for (int index = 0; index < listOfressource.Count(); index++)
            {
                fs.Write(listOfressource[index].GetRawData(), 0,listOfressource[index].GetSize());

                //check if the size is pair, if not add a byte. (don't know why..but required)
                if(listOfressource[index].GetSize() % 2 == 1){
                    fs.WriteByte(255);
                }
            }
        }

        private void WriteListHeader<T>(ref FileStream fs, ref int indexData, List<T> listOfressource) where T : AbsRessource
        {
            for (int index = 0; index < listOfressource.Count(); index++)
            {
                //write size
                fs.Write(CGeneric.ConvertIntToByteArray(listOfressource[index].GetSize()), 0, 4);

                // write index start
                fs.Write(CGeneric.ConvertIntToByteArray(indexData), 0, 4);

                //write name of FIB (BIF Name)
                byte[] nameBIF = System.Text.Encoding.UTF8.GetBytes(listOfressource[index].GetRessourceName());
                fs.Write(nameBIF, 0, nameBIF.Length);

                //fill free space (of name) by 0
                for (int freeSpace = 0; freeSpace < 16 - nameBIF.Length; freeSpace++)
                    fs.WriteByte(0);

                //if data is not pair, add a FF for to be sure it's pair... (because !)
                if (listOfressource[index].GetSize() % 2 == 1)
                    indexData++;

                indexData += listOfressource[index].GetSize();
            }
        }
    }
}
