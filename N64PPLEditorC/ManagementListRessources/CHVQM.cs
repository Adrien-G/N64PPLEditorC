﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CHVQM : AbsRessource
    {

        public CHVQM(Byte[] rawData, Byte[] ressourceName) : base(rawData, ressourceName)
        {

        }

        public void SetRawData(byte[] rawData)
        {
            this.rawData = rawData;
        }

    }
}
