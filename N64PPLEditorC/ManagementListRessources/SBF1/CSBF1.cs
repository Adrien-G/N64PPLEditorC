using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CSBF1 : AbsRessource
    {
        //nb textures scenes
        private int nbTextureScenes;

        //texture scene name
        private List<byte[]> bifList;

        // scene count
        private int sceneCount;

        private byte[] header;

        public List<CSBF1Scene> scenesList;

        public CSBF1(Byte[] rawData, Byte[] ressourceName) : base(rawData,ressourceName)
        {
            //grab ressources used in scenes
            byte[] nbTextureScenes = new byte[4];
            Array.Copy(rawData, 4, nbTextureScenes, 0, nbTextureScenes.Length);
            this.nbTextureScenes = CGeneric.ConvertByteArrayToInt(nbTextureScenes);

            //grab names of texture used (bif list)
            bifList = new List<byte[]>();

            
            for (int i = 0; i < this.nbTextureScenes; i++)
            {
                byte[] tmpArray = new byte[16];
                Array.Copy(rawData, 8 + i * 16, tmpArray, 0, tmpArray.Length);
                bifList.Add(tmpArray);
            }

            //grab index number of scene
            int indexSceneCount = 8 + this.nbTextureScenes * 0x10;
            byte[] sceneCount = new byte[4];
            Array.Copy(rawData, indexSceneCount, sceneCount, 0, sceneCount.Length);
            this.sceneCount = CGeneric.ConvertByteArrayToInt(sceneCount);

            //send the data without SBF header for chuncking data..
            this.header = CGeneric.GiveMeArray(rawData, 0, indexSceneCount + sceneCount.Length);
            byte[] dataWithoutHeader = new byte[rawData.Length - (indexSceneCount + sceneCount.Length)];
            Array.Copy(rawData,indexSceneCount+sceneCount.Length,dataWithoutHeader,0,dataWithoutHeader.Length);
            SetChunk(dataWithoutHeader);
        }

        private void SetChunk(byte[] dataWithoutHeader)
        {
            scenesList = new List<CSBF1Scene>();
            int globalIndex = 0;
            
            for (int i = 0; i < this.sceneCount; i++)
            {
                byte[] tmpData = new byte[dataWithoutHeader.Length - globalIndex];
                Array.Copy(dataWithoutHeader, globalIndex, tmpData, 0, dataWithoutHeader.Length - globalIndex);

                CSBF1Scene item = new CSBF1Scene(tmpData);
                globalIndex += item.GetChunckSize();
                scenesList.Add(item);
            }
        }

        public CSBF1Scene GetScene(int index)
        {
            return scenesList[index];
        }

        public List<string> GetBifList()
        {
            var list = new List<String>();
            foreach (byte[] element in bifList)
            {
                list.Add(CGeneric.ConvertByteArrayToString(element));
            }
            return list;
        }
        public int GetSceneCount()
        {
            return scenesList.Count();
        }

        public override byte[] GetRawData()
        {
            //return rawData;
            byte[] res = this.header;

            //add each csbf1 scene
            foreach (CSBF1Scene scene in scenesList)
                res = res.Concat(scene.GetRawData()).ToArray();

            this.rawData = res;
            return res;
        }
    }
}
