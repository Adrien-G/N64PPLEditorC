using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CRessourceList
    {
        public enum RessourceType
        {
            FIB,
            HVQM,
            SBF
        }
        
        private struct ListFormat
        {
            public Byte[] ressourceIndex;
            public Byte[] ressourceSize;
            public Byte[] ressourceName;
        }

        private List<C3FIB> fibList;

        public CRessourceList()
        {
            fibList = new List<C3FIB>();
        }

        public void Init(int nbElements, byte[] ressourcesList, byte[] ressourcesData)
        {
            

            ListFormat[] lst1 = LoadRessourcesList(nbElements,ressourcesList);
            ChunkDataToRessources(lst1, ressourcesData);
        }

        private ListFormat[] LoadRessourcesList(int nbElements,Byte[] ressourcesList)
        {
            ListFormat[] lst1 = new ListFormat[nbElements];
            

            for (int i = 0; i < nbElements; i++)
            {
                lst1[i].ressourceIndex = new byte[4];
                lst1[i].ressourceSize = new byte[4];
                lst1[i].ressourceName = new byte[16];
                Array.Copy(ressourcesList, i * CGenericFunctions.sizeOfElementTable,lst1[i].ressourceIndex, 0, 4);
                Array.Copy(ressourcesList, i * CGenericFunctions.sizeOfElementTable + 4, lst1[i].ressourceSize, 0, 4);
                Array.Copy(ressourcesList, i * CGenericFunctions.sizeOfElementTable + 8, lst1[i].ressourceName, 0,16);
            }

            return lst1;
        }

        private void ChunkDataToRessources(ListFormat[] ressourcesList,Byte[] ressourceData)
        {
            

        }



    }
}
