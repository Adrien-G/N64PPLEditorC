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

        private List<C3FIB> fibChilds;



        public CRessourceList(int nbElements, int indexStartingList)
        {
            fibChilds = new List<C3FIB>();
        }





    }
}
