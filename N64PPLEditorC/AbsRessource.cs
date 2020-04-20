using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    public abstract class AbsRessource
    {

        protected Byte[] rawData;
        protected Byte[] ressourceName { get; }

        public AbsRessource(Byte[] rawData, Byte[] ressourceName)
        {
            this.rawData = rawData;
            this.ressourceName = ressourceName;
        }

        public string GetRessourceName()
        {
            return Encoding.Default.GetString(ressourceName);
        }

        public virtual Int32 GetSize()
        {
            return rawData.Length;
        }

        public virtual byte[] GetRawData()
        {
            return rawData;
        }

    }
}
