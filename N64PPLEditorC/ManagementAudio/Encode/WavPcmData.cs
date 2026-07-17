using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC.ManagementAudio
{
    public class WavPcmData
    {
        public int SampleRate;
        public short[] Samples;
        public int? LoopStart;
        public int? LoopEnd;
    }
}
