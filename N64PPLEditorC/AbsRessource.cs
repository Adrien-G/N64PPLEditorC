using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    abstract class AbsRessource
    {

        protected Byte[] rawData;
        protected Byte[] ressourceName;

        public AbsRessource(Byte[] rawData, Byte[] ressourceName)
        {
            this.rawData = rawData;
            this.ressourceName = ressourceName;
        }


        public string GetRessourceName()
        {
            return System.Text.Encoding.Default.GetString(ressourceName);
        }



    }
}
