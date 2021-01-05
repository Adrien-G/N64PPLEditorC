using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CSBF1 : AbsRessource
    {
        //texture scene name
        private List<byte[]> bifList;

        public List<CSBF1Scene> scenesList;

        public CSBF1(Byte[] rawData, Byte[] ressourceName) : base(rawData,ressourceName)
        {
            //grab nb ressources used in scene
            int nbTextureScene = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 4, 4));

            //grab names of texture used (bif list)
            bifList = new List<byte[]>();

            for (int i = 0; i < nbTextureScene; i++)
            {
                byte[] tmpArray = new byte[16];
                Array.Copy(rawData, 8 + i * 16, tmpArray, 0, tmpArray.Length);
                bifList.Add(tmpArray);
            }

            //grab index number of scene
            int headerSize = 12 + bifList.Count * 0x10;
            int nbScene = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, headerSize-4, 4));

            //send the data without SBF header for chuncking data..
            byte[] dataWithoutHeader = new byte[rawData.Length - headerSize];
            Array.Copy(rawData, headerSize ,dataWithoutHeader,0,dataWithoutHeader.Length);
            SetChunk(dataWithoutHeader, nbScene);
        }

        private void SetChunk(byte[] dataWithoutHeader,int nbScene)
        {
            scenesList = new List<CSBF1Scene>();
            int globalIndex = 0;
            
            for (int i = 0; i < nbScene; i++)
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

        public void AddTexture(int indexScene, string bifName)
        {
            //if the texture is already in the sbf, just add the texture to the scene
            for(int i = 0; i < bifList.Count; i++)
            {
                if(bifName == CGeneric.ConvertByteArrayToString(bifList[i]).ToUpper())
                {
                    scenesList[indexScene].AddNewTextureObject(i);
                    return;
                }
            }
            //else add before the texture and after add it to the scene
            //store the name in lower just because it will be more pretty in the rom :)
            bifList.Add(CGeneric.ConvertStringToByteArray(bifName.ToLower()));
            scenesList[indexScene].AddNewTextureObject(bifList.Count - 1);
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
            byte[] res = new byte[12 + (bifList.Count * 0x10)];
            Array.Copy(CGeneric.patternSBF1, 0, res, 0, CGeneric.patternSBF1.Length);
            Array.Copy(CGeneric.ConvertIntToByteArray(bifList.Count), 0, res, 4, 4);

            for (int i = 0; i < bifList.Count; i++)
                Array.Copy(bifList[i], 0, res,8+i*bifList[0].Length, bifList[0].Length);

            Array.Copy(CGeneric.ConvertIntToByteArray(scenesList.Count), 0, res, res.Length - 4, 4);

            //add each csbf1 scene
            foreach (CSBF1Scene scene in scenesList)
                res = res.Concat(scene.GetRawData()).ToArray();

            this.rawData = res;
            return res;
        }
    }
}
