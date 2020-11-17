using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    class CSBF1Scene
    {
        private byte[] sceneName;
        private byte[] rawData;

        private List<CSBF1DynamicObject> dynamicObjectList;
        public List<CSBF1TextObject> textObjectList;
        private List<CSBF1TextureManagement> textureManagementObjectList;

        public int nbTextGroupObject { get; private set; }

        public CSBF1Scene(byte[] rawData)
        {
            this.rawData = rawData;
            //read sceneNameLength
            byte[] sceneNameLength = new byte[4];
            Array.Copy(rawData, 4, sceneNameLength, 0, sceneNameLength.Length);
            int sceneLength = CGeneric.ConvertByteArrayToInt(sceneNameLength);

            //extract sceneName
            this.sceneName = new byte[sceneLength];
            Array.Copy(rawData, 8, sceneName, 0, sceneLength);

            //decompose scene in 3 parts.
            byte[] dataWithoutHeader = new Byte[rawData.Length - 8 - sceneLength];
            Array.Copy(rawData, 8 + sceneLength, dataWithoutHeader, 0, dataWithoutHeader.Length);

            //decompose data of scene
            ChunkScene(dataWithoutHeader,8+sceneLength);

            //for text, decompose groups of text
            GroupTextData();
        }

        private void GroupTextData()
        {
            if (textObjectList.Count == 0)
                return;

            //set the first to the first group
            var id = textObjectList[0].id;
            textObjectList[0].group = 0;

            var group = 0;
            for(int i = 1; i < textObjectList.Count(); i++)
            {
                //si l'id est égal a 0xFFFFFFFF ou l'id précédent + 1 
                var tmpId = textObjectList[i].id;
                var tmpIdM1 = textObjectList[i - 1].id+1;
                //TODO if 44 -> set next group

                if (tmpIdM1 == 0xFFFFFFFF)
                { }
                else if (textObjectList[i].headerData.Length == 44)
                    group++;
                else if (textObjectList[i].headerData.Length == 52 && tmpId != tmpIdM1)
                    group++;
                else
                { }
                textObjectList[i].group = group;
            }
            this.nbTextGroupObject = group;
        }

        public int GetChunckSize()
        {
            return this.rawData.Length;
        }

        private void ChunkScene(byte[] data,int headerSize)
        {
            //separate 3 types of data -> dynamicObject, text and textureManagment
             int generalIndex = 0;

            //get the number of dynamic object
            byte[] nbDynamicArray = new byte[4];
            Array.Copy(data, generalIndex, nbDynamicArray, 0, nbDynamicArray.Length);
            //increment the global index with 4 (the size of nbDynamicArray.Length)
            generalIndex += nbDynamicArray.Length;

            //fill dynamic objects
            this.dynamicObjectList = new List<CSBF1DynamicObject>();
            generalIndex += ChunkDynamicObject(data, CGeneric.ConvertByteArrayToInt(nbDynamicArray), generalIndex);

            //get the number of text object
            byte[] nbTextArray = new byte[4];
            Array.Copy(data, generalIndex, nbTextArray, 0, nbTextArray.Length);
            //increment the global index with 4 (the size of nbDynamicArray.Length)
            generalIndex += nbTextArray.Length;

            //fill text objects
            this.textObjectList = new List<CSBF1TextObject>();
            generalIndex += ChunkTextObject(data, CGeneric.ConvertByteArrayToInt(nbTextArray), generalIndex);

            //get the number of texture management object
            byte[] nbTextureArray = new byte[4];
            Array.Copy(data, generalIndex, nbTextureArray, 0, nbTextureArray.Length);
            //increment the global index with 4 (the size of nbDynamicArray.Length)
            generalIndex += nbTextureArray.Length;

            //fill texture management object
            this.textureManagementObjectList = new List<CSBF1TextureManagement>();
            generalIndex += ChunkTextureManagementObject(data, CGeneric.ConvertByteArrayToInt(nbTextureArray), generalIndex);

            //add 4 because the last object didn't seems to contains data
            generalIndex += 4;

            //with the datasize determinated set the new rawData array
            byte[] newArrayRawData = new byte[generalIndex+headerSize];
            Array.Copy(this.rawData, 0, newArrayRawData, 0, newArrayRawData.Length);
            this.rawData = newArrayRawData;
        }

        private int ChunkDynamicObject(byte[] data, int nbDynamicObject, int indexDataStart)
        {
            byte[] lengthData = new byte[4];
            int lengthDataInt;
            int totalSize = 0;

            for (int i = 0; i < nbDynamicObject; i++)
            {
                // get the length of the dynamic object
                Array.Copy(data, indexDataStart, lengthData, 0, lengthData.Length);
                lengthDataInt = CSBF1DynamicObject.GetHeaderLength(CGeneric.ConvertByteArrayToInt(lengthData));

                //create the array to add the object
                byte[] dataDynamicObject = new byte[lengthDataInt];

                //create the new dynamicObject item
                Array.Copy(data, indexDataStart, dataDynamicObject, 0, dataDynamicObject.Length);
                dynamicObjectList.Add(new CSBF1DynamicObject(dataDynamicObject));

                //increment totalSize and indexStart
                indexDataStart += lengthDataInt;
                totalSize += lengthDataInt;
            }
            return totalSize;
        }

        private int ChunkTextObject(byte[] data, int nbTextObject, int indexDataStart)
        {
            byte[] lengthHeader = new byte[4];
            byte[] lengthText = new byte[4];
            int lengthHeaderInt;
            int lengthTextInt;
            int totalSize = 0;

            for (int i = 0; i < nbTextObject; i++)
            {
                // get the length of the header text object
                Array.Copy(data, indexDataStart, lengthHeader, 0, lengthHeader.Length);
                lengthHeaderInt = CSBF1TextObject.GetHeaderLength(CGeneric.ConvertByteArrayToInt(lengthHeader));

                //grab the size of the text (add 4 for the header (size text) and determine length of text (multiply per 2))
                Array.Copy(data, indexDataStart + lengthHeaderInt, lengthText, 0, lengthText.Length);
                lengthTextInt = lengthText.Length + CGeneric.ConvertByteArrayToInt(lengthText) * 2;

                //store the new item
                byte[] dataTextObject = new byte[lengthHeaderInt + lengthTextInt];
                Array.Copy(data, indexDataStart, dataTextObject, 0, dataTextObject.Length);
                textObjectList.Add(new CSBF1TextObject(dataTextObject,lengthHeaderInt,lengthTextInt));

                //increment totalSize and indexStart
                indexDataStart += lengthHeaderInt + lengthTextInt;
                totalSize += lengthHeaderInt + lengthTextInt;
            }
            return totalSize;
        }
        private int ChunkTextureManagementObject(byte[] data, int nbTextureManagementObject, int indexDataStart)
        {
            byte[] lengthData = new byte[4];
            int lengthDataInt;

            int totalSize = 0;
            for (int i = 0; i < nbTextureManagementObject; i++)
            {
                // get the length of the texture management object
                Array.Copy(data, indexDataStart, lengthData, 0, lengthData.Length);
                lengthDataInt = CSBF1TextureManagement.GetHeaderLength(CGeneric.ConvertByteArrayToInt(lengthData)) ;


                //create the new texture management item
                byte[] dataTextureManagementObject = new byte[lengthDataInt];
                Array.Copy(data, indexDataStart, dataTextureManagementObject, 0, dataTextureManagementObject.Length);
                textureManagementObjectList.Add(new CSBF1TextureManagement(dataTextureManagementObject));

                indexDataStart += lengthDataInt;
                totalSize += lengthDataInt;
            }
            return totalSize;
        }

        public string GetSceneName()
        {
            return Encoding.Default.GetString(this.sceneName);
        }

        public CSBF1TextObject GetTextObject(int index)
        {
            return textObjectList[index];
        }

        public List<CSBF1TextObject> GetTextObjectGroup(int group)
        {
            var endList = new List<CSBF1TextObject>();

            foreach(CSBF1TextObject txtObj in textObjectList)
            {
                if (txtObj.group == group)
                    endList.Add(txtObj);
            }
            return endList;
        }

        public CSBF1TextureManagement GetTextureManagementObject(int index)
        {
            return textureManagementObjectList[index];
        }
        public void AddNewTextObject(bool sameScene)
        {
            //get the last element
            var LastText = textObjectList[textObjectList.Count - 1];

            //add one to ID and take his group
            int textId = LastText.id + 1;
            int groupText = LastText.group;

            //if the text is independant
            if (!sameScene)
            {
                textId += 0x64;
                groupText += 1;
            }
            
            //create the new text object
            textObjectList.Add(new CSBF1TextObject(CGeneric.ConvertIntToByteArray(textId), groupText));
        }

        public int GetDynamicObjectCount()
        {
            return dynamicObjectList.Count();
        }

        public int GetTextObjectCount()
        {
            return textObjectList.Count();
        }

        public int GetTextureManagementCount()
        {
            return textureManagementObjectList.Count();
        }

        public int GetSize()
        {
            // 24 = size of header and number of elements
            int totalSize = 24 + sceneName.Length;

            foreach (CSBF1DynamicObject dynObj in dynamicObjectList)
                totalSize += dynObj.GetSize();
            foreach (CSBF1TextObject TexObj in textObjectList)
                totalSize += TexObj.GetSize();
            foreach (CSBF1TextureManagement texObj in textureManagementObjectList)
                totalSize += texObj.GetSize();
            return totalSize;
        }

        public void RemoveText(int index)
        {
            this.textObjectList.RemoveAt(index);
        }

        public byte[] GetRawData()
        {
            //init
            byte[] res = new byte[0];

            //add the 4 unknown bytes...
            res = res.Concat(CGeneric.GiveMeArray(rawData, 0, 4)).ToArray();

            //title scene length
            res = res.Concat(CGeneric.ConvertIntToByteArray(this.sceneName.Length)).ToArray();

            //scene name
            res = res.Concat(sceneName).ToArray();

            //size dynamicObject
            res = res.Concat(CGeneric.ConvertIntToByteArray(dynamicObjectList.Count)).ToArray();

            //dynamic objectData
            foreach (CSBF1DynamicObject dynObj in dynamicObjectList)
                res = res.Concat(dynObj.GetRawData()).ToArray();

            //size TextObject
            res = res.Concat(CGeneric.ConvertIntToByteArray(textObjectList.Count)).ToArray();

            //TextObject
            foreach (CSBF1TextObject TexObj in textObjectList)
                res = res.Concat(TexObj.GetRawData()).ToArray();

            //Size textureManagement
            res = res.Concat(CGeneric.ConvertIntToByteArray(textureManagementObjectList.Count)).ToArray();

            //TextureManagement
            foreach (CSBF1TextureManagement texManObj in textureManagementObjectList)
                res = res.Concat(texManObj.GetRawData()).ToArray();

            //dont renember why.. but needed
            byte[] test = new byte[] { 0x00,0x00,0x00,0x00 };
            res = res.Concat(test).ToArray();

            return res;
        }
    }
}
