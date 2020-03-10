using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CSBF1 : AbsRessource
    {

        public CSBF1(Byte[] rawData, Byte[] ressourceName) : base(rawData,ressourceName)
        {
        }

        public override int GetSize()
        {
            throw new NotImplementedException();
        }

    }
}
