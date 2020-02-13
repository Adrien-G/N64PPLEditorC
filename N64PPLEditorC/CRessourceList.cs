using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CRessourceList
    {
        
        private enum RessourceType : int
        {
            FIB=860244290,
            HVQM=1213616461,
            SBF=1396852273
        }
        
        private struct ListFormat
        {
            public Byte[] ressourceSize;
            public Byte[] ressourceIndex;
            public Byte[] ressourceName;
        }

        private List<C3FIB> fibList;
        private List<CHVQM> hvqmList;
        private List<CSBF1> sbfList;

        public CRessourceList()
        {
            fibList = new List<C3FIB>();
            hvqmList = new List<CHVQM>();
            sbfList = new List<CSBF1>();
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
                Array.Copy(ressourcesList, i * CGenericFunctions.sizeOfElementTable, lst1[i].ressourceSize, 0, 4);
                Array.Copy(ressourcesList, i * CGenericFunctions.sizeOfElementTable + 4, lst1[i].ressourceIndex, 0, 4);
                Array.Copy(ressourcesList, i * CGenericFunctions.sizeOfElementTable + 8, lst1[i].ressourceName, 0,16);
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
                int sizeElement = CGenericFunctions.ConvertByteArrayToInt(ressourcesList[i].ressourceSize);
                Byte[] tmpContainerData = new byte[sizeElement];
                Array.Copy(ressourceData, generalIndex,tmpContainerData,0,tmpContainerData.Length);


                //determine the type of data and fill in the apropriated list
                Byte[] dataPattern = new byte[4];
                Array.Copy(tmpContainerData, 0, dataPattern, 0, dataPattern.Length);

                switch (CGenericFunctions.ConvertByteArrayToInt(dataPattern))
                {
                    case (int)RessourceType.FIB:
                        fibList.Add(new C3FIB(tmpContainerData));
                        break;
                    case (int)RessourceType.HVQM:
                        hvqmList.Add(new CHVQM(tmpContainerData));
                        break;
                    case (int)RessourceType.SBF:
                        sbfList.Add(new CSBF1(tmpContainerData));
                        break;
                }
                generalIndex += sizeElement;
                if (sizeElement % 2 == 1)
                    generalIndex += 1;
            }
        }



    }
}
