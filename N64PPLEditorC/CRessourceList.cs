﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private List<C3FIB> fibList;
        private List<CHVQM> hvqmList;
        private List<CSBF1> sbfList;

        private int indexRessourcesStart;
        private int indexRessourcesEnd;

        public CRessourceList(int indexRessourcesStart,int indexRessourcesEnd)
        {
            fibList = new List<C3FIB>();
            hvqmList = new List<CHVQM>();
            sbfList = new List<CSBF1>();
            this.indexRessourcesStart = indexRessourcesStart;
            this.indexRessourcesEnd = indexRessourcesEnd;
        }

        public void Init(int nbElements, byte[] ressourcesList, byte[] ressourcesData)
        {
            ListFormat[] lst1 = LoadRessourcesList(nbElements,ressourcesList);
            ChunkDataToRessources(lst1, ressourcesData);

        }

        private ListFormat[] LoadRessourcesList(int nbElements,Byte[] ressourcesList)
        {
            ListFormat[] lst1 = new ListFormat[nbElements];
            
            //separate the ressources Index size and name
            for (int i = 0; i < nbElements; i++)
            {
                lst1[i].ressourceIndex = new byte[4];
                lst1[i].ressourceSize = new byte[4];
                lst1[i].ressourceName = new byte[16];
                Array.Copy(ressourcesList, i * CGeneric.sizeOfElementTable, lst1[i].ressourceSize, 0, 4);
                Array.Copy(ressourcesList, i * CGeneric.sizeOfElementTable + 4, lst1[i].ressourceIndex, 0, 4);
                Array.Copy(ressourcesList, i * CGeneric.sizeOfElementTable + 8, lst1[i].ressourceName, 0,16);
            }
            return lst1;
        }

        private void ChunkDataToRessources(ListFormat[] ressourcesList,Byte[] ressourceData)
        {

            int generalIndex = 0;
            
            // check all the ressources list...
            for( int i = 0; i < ressourcesList.Length; i++)
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
                        fibList.Add(new C3FIB(tmpContainerData,ressourcesList[i].ressourceName));
                        fibList[fibList.Count - 1].Init();
                        break;
                    case (int)CGeneric.RessourceType.HVQM:
                        hvqmList.Add(new CHVQM(tmpContainerData, ressourcesList[i].ressourceName));
                        break;
                    case (int)CGeneric.RessourceType.SBF:
                        sbfList.Add(new CSBF1(tmpContainerData, ressourcesList[i].ressourceName));
                        break;
                }
                generalIndex += sizeElement;
                if (sizeElement % 2 == 1)
                    generalIndex += 1;
            }
        }

        public void ShowTexture(System.Windows.Forms.PictureBox pictureBox,int indexFIB,int indexBFF)
        {
            fibList[indexFIB].GetTexture(pictureBox,indexBFF);
        }

        public void SaveTexture(int indexFIB, int indexBFF)
        {
            fibList[indexFIB].SaveTexture(indexBFF,indexFIB);
        }

        //fib public methods..
        public int GetFIBCount()
        {
            return fibList.Count();
        }
        public string GetFIBName(int index)
        {
            return fibList[index].getFIBName();
        }
        public string GetBFFName(int indexFIB, int indexBFF)
        {
            return fibList[indexFIB].GetBFFName(indexBFF);
        }
        public int GetBFFCount(int indexFIB)
        {
            return fibList[indexFIB].GetBFFCount();
        }
        public int GetTotalBFFCount()
        {
            int total = 0;
            for (int i = 0; i < fibList.Count; i++)
            {
                total += this.GetBFFCount(i);
            }
            return total;
        }

        //hvqm public methods...
        public int GetHVQMCount()
        {
            return hvqmList.Count();
        }
        public string GetHVQMName(int index)
        {
            return hvqmList[index].GetRessourceName();
        }

        //sbf1 public methods...
        public int GetSBFCount()
        {
            return sbfList.Count();
        }
        public string GetSBFName(int index)
        {
            return sbfList[index].GetRessourceName();
        }

        public void WriteAllData(string path)
        {
            FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Write);

            // write the number of elements
            fs.Write(CGeneric.ConvertIntArrayToByte(GetFIBCount() + GetHVQMCount() + GetSBFCount()), 0, 4);

            // index of data (for writing header)
            int indexData = (GetFIBCount() + GetHVQMCount() + GetSBFCount()) * 24 + 4;

            // write list header (FIB)
            for (int index = 0; index < GetFIBCount(); index++)
                indexData += WriteListHeader(fs,indexData, fibList[index].GetSize(), fibList[index].getFIBName());

            // write list header (HVQM)
            for (int index = 0; index < GetHVQMCount(); index++)
                indexData += WriteListHeader(fs, indexData, hvqmList[index].GetSize(), hvqmList[index].getFIBName());

            // write list header (FIB)
            for (int index = 0; index < GetSBFCount(); index++)
                indexData += WriteListHeader(fs, indexData, sbfList[index].GetSize(), sbfList[index].getFIBName());

        }

        private int WriteListHeader(FileStream fs, int indexData,int sizeItem,string nameItem)
        {

                //write size
                fs.Write(CGeneric.ConvertIntArrayToByte(sizeItem), 0, 4);

                // write index start
                fs.Write(CGeneric.ConvertIntArrayToByte(indexData), 0, 4);

                //write name of FIB (BIF Name)
                byte[] nameBIF = System.Text.Encoding.UTF8.GetBytes(nameItem);
                fs.Write(nameBIF, 0, nameBIF.Length);

                //fill free space (of name) by 0
                for (int freeSpace = 0; freeSpace < 16 - nameBIF.Length; freeSpace++)
                    fs.WriteByte(0);

                //if data is not pair, add a FF for to be sure it's pair... (because !)
                if (sizeItem % 2 == 1)
                    indexData++;

            return indexData + sizeItem;
        }
    }
}
