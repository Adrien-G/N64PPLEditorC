﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class C3FIB
    {

        private Byte[] rawData;
        private List<CBFF2> bff2Childs;

        public C3FIB(Byte[] rawData)
        {
            this.rawData = rawData;
            bff2Childs = new List<CBFF2>();
        }
    }
}
