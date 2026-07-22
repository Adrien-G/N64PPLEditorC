using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace N64PPLEditorC
{
    public class CRessourceList
    {
        
        private struct ListFormat
        {
            public Byte[] ressourceSize;
            public Byte[] ressourceIndex;
            public Byte[] ressourceName;
        }

        private List<ListFormat> ressourcesList;

        public List<C3FIBObject> Fib;
        public List<CHVQM> Hvqm;
        public List<CSBF1> Sbf1;
        public List<CRDF> Rdf;

        public int indexRessourcesStart { get; private set; }

        public CRessourceList(int indexRessourcesStart)
        {
            Fib = new List<C3FIBObject>();
            Hvqm = new List<CHVQM>();
            Sbf1 = new List<CSBF1>();
            Rdf = new List<CRDF>();
            this.indexRessourcesStart = indexRessourcesStart;
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
                        Fib.Add(new C3FIBObject(tmpContainerData, ressourcesList[i].ressourceName));
                        break;
                    case (int)CGeneric.RessourceType.HVQM:
                        Hvqm.Add(new CHVQM(tmpContainerData, ressourcesList[i].ressourceName));
                        break;
                    case (int)CGeneric.RessourceType.SBF:
                        Sbf1.Add(new CSBF1(tmpContainerData, ressourcesList[i].ressourceName));
                        break;
                    default:
                        Rdf.Add(new CRDF(tmpContainerData, ressourcesList[i].ressourceName));
                        break;
                }
                generalIndex += sizeElement;
                if (sizeElement % 2 == 1)
                    generalIndex += 1;
            }

        }

        public void Add3FIB(string name)
        {
            //set name and header initial data

            Fib.Add(new C3FIBObject(name));

            var element = new ListFormat();
            element.ressourceIndex = new byte[4];
            element.ressourceSize = new byte[4];
            element.ressourceName = new byte[16];

            //write name of FIB (BIF Name)
            byte[] nameBIF = System.Text.Encoding.UTF8.GetBytes(name.ToUpper() + ".BIF");
            Array.Copy(nameBIF, 0, element.ressourceName, 0, nameBIF.Length);
            this.ressourcesList.Add(element);
        }

        public CSBF1 GetSBF1(int index)
        {
            return Sbf1[index];
        }

        public void SetSBF1(byte[] scene, int index)
        {
            var name = CGeneric.ConvertStringToByteArray(Sbf1[index].GetRessourceName());
            Sbf1[index] = new CSBF1(scene,name);
        }

        public int Get3FIBIndexWithFIBName(string name)
        {
            int increm = 0;
            var b = name.ToUpper().Replace("\0", "");
            for (int z = 0; z < ressourcesList.Count(); z++)
            {
                var a = CGeneric.ConvertByteArrayToString(ressourcesList[z].ressourceName).ToUpper().Replace("\0","");
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
            return Hvqm[indexHVQM];
        }

        public int GetSBFCount()
        {
            return Sbf1.Count();
        }
        public int GetHVQMCount()
        {
            return Hvqm.Count();
        }
        public int GetRTFCount()
        {
            return Rdf.Count();
        }

        public void WriteAllData(FileStream fs)
        {
            fs.Position = indexRessourcesStart;

            // write the number of elements
            fs.Write(CGeneric.ConvertIntToByteArray(Fib.Count() + GetHVQMCount() + GetSBFCount() + GetRTFCount()), 0, 4);

            // index of data (for writing header)
            int indexData = (Fib.Count() + GetHVQMCount() + GetSBFCount() + GetRTFCount()) * 24 + 4;

            //write list header (FIB,HVQM,SBF1,RDF1)
            WriteListHeaderSpecific(ref fs, ref indexData, Fib);
            WriteListHeader(ref fs, ref indexData, Hvqm);
            WriteListHeader(ref fs, ref indexData, Sbf1);
            WriteListHeader(ref fs, ref indexData, Rdf);

            //write data associated
            WriteRessourceDataSpecific(ref fs, Fib);
            WriteRessourceData(ref fs, Hvqm);
            WriteRessourceData(ref fs, Sbf1);
            WriteRessourceData(ref fs, Rdf);


        }

        private void WriteRessourceData<T>(ref FileStream fs, List<T> listOfressource) where T : AbsRessource
        {
            for (int index = 0; index < listOfressource.Count(); index++)
            {
                var a = listOfressource[index].GetRawData();
                var b = a.Length;

                fs.Write(a, 0,b);

                //check if the size is pair, if not add a byte. (don't know why..but required)
                if(listOfressource[index].GetRawData().Length % 2 == 1){
                    fs.WriteByte(255);
                }
            }
        }

        private void WriteRessourceDataSpecific(ref FileStream fs, List<C3FIBObject> listOfressource) 
        {
            for (int index = 0; index < listOfressource.Count(); index++)
            {
                var a = listOfressource[index].RecomposeRawData();
                var b = a.Length;

                fs.Write(a, 0, b);

                //check if the size is pair, if not add a byte. (don't know why..but required)
                if (listOfressource[index].RecomposeRawData().Length % 2 == 1)
                {
                    fs.WriteByte(255);
                }
            }
        }

        private void WriteListHeaderSpecific(ref FileStream fs, ref int indexData, List<C3FIBObject> listOfressource)
        {
            for (int index = 0; index < listOfressource.Count(); index++)
            {
                //write size
                fs.Write(CGeneric.ConvertIntToByteArray(listOfressource[index].RecomposeRawData().Length), 0, 4);

                // write index start
                fs.Write(CGeneric.ConvertIntToByteArray(indexData), 0, 4);

                //write name of FIB (BIF Name)
                byte[] nameBIF = System.Text.Encoding.UTF8.GetBytes(listOfressource[index].RessourceNameString.ToUpper());
                fs.Write(nameBIF, 0, nameBIF.Length);

                //fill free space (of name) by 0
                for (int freeSpace = 0; freeSpace < 16 - nameBIF.Length; freeSpace++)
                    fs.WriteByte(0);

                //if data is not pair, add a FF for to be sure it's pair... (because !)
                if (listOfressource[index].RecomposeRawData().Length % 2 == 1)
                    indexData++;

                indexData += listOfressource[index].RecomposeRawData().Length;
            }
        }

        private void WriteListHeader<T>(ref FileStream fs, ref int indexData, List<T> listOfressource) where T : AbsRessource
        {
            for (int index = 0; index < listOfressource.Count(); index++)
            {
                //write size
                fs.Write(CGeneric.ConvertIntToByteArray(listOfressource[index].GetRawData().Length), 0, 4);

                // write index start
                fs.Write(CGeneric.ConvertIntToByteArray(indexData), 0, 4);

                //write name of FIB (BIF Name)
                byte[] nameBIF = System.Text.Encoding.UTF8.GetBytes(listOfressource[index].GetRessourceName().ToUpper());
                fs.Write(nameBIF, 0, nameBIF.Length);

                //fill free space (of name) by 0
                for (int freeSpace = 0; freeSpace < 16 - nameBIF.Length; freeSpace++)
                    fs.WriteByte(0);

                //if data is not pair, add a FF for to be sure it's pair... (because !)
                if (listOfressource[index].GetRawData().Length % 2 == 1)
                    indexData++;

                indexData += listOfressource[index].GetRawData().Length;
            }
        }
        public int GetSizeOfAllRessourceList()
        {
            int size = 0;
            foreach (C3FIBObject c3fibdata in Fib)
            {
                var tmp = c3fibdata.RecomposeRawData().Length;
                if (tmp % 2 == 0) size += tmp; else size += tmp + 1;
            }
            foreach (CHVQM hvqmdata in Hvqm)
            {
                var tmp = hvqmdata.GetRawData().Length;
                if (tmp % 2 == 0) size += tmp; else size += tmp + 1;
            }
            foreach (CSBF1 csbf1data in Sbf1)
            {
                var tmp = csbf1data.GetRawData().Length;
                if (tmp % 2 == 0) size += tmp; else size += tmp + 1;
            }
            foreach (CRDF crdfdata in Rdf)
            {
                var tmp = crdfdata.GetRawData().Length;
                if (tmp % 2 == 0) size += tmp; else size += tmp + 1;
            }

            //add header before ressource list
            size += 0x12;

            //add the ressource list (header of all data)
            size += ressourcesList.Count() * 24;

            return size;
        }
    }
}
