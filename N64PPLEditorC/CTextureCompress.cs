using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    static class CTextureCompress
    {

        private static byte GetByteForCompression(int qtReaded,int qtRepeated)
        {
            byte nib1 = 0;
            
            switch (qtReaded)
            {
                case 1 : nib1 = 0x8; break;
                case int n when (n == 2 || n == 3) : nib1 = 0xC; break;
                case int n when (n == 4 || n == 5) : nib1 = 0xD; break;
                case int n when (n == 6 || n == 7) : nib1 = 0xE; break;
                case int n when (n == 8 || n == 9) : nib1 = 0xF; break;
            }
             byte nib2 = (byte)(qtRepeated - 2);

            if (qtReaded % 2 == 1 && nib1 != 0x8)
                nib2 += 8;

            return CGeneric.NibbleToByte(nib1,nib2);
        }

        public static byte[] MakeCompression(byte[] uncompressedArray)
        {
            //set the bounds for "virtual arrays"...
            int boundIndex1;
            int boundIndex2;
            int boundSize;

            int globalIndex = 0;

            //core of algorithm, try to find the biggest quantity..
            int biggestQuantityReaded = 0;
            int biggestQuantityRepeated = 0;

            //when no repetition is found, it's a lonely pixel
            byte lonelyPixelCount = 0;

            var compressedArray = new MemoryStream();

            //loop until the end of the uncompressed texture array.
            do
            {
                //search best combinaison for compressing...
                for(int readedByte = 1; readedByte <= 9; readedByte++)
                {
                    if (globalIndex + readedByte > uncompressedArray.Length)
                        break;

                    boundIndex1 = globalIndex;
                    boundSize = readedByte;

                    for (int repeatedByte = 2; repeatedByte <= 9; repeatedByte++)
                    {
                        //check for avoiding buffer overflow (when reach end of array)
                        if (globalIndex + readedByte * repeatedByte + readedByte > uncompressedArray.Length)
                            break;

                        //set the 2nd bound
                        boundIndex2 = globalIndex + readedByte * repeatedByte;

                        //and compare with the first bound
                        if (CheckIfSameArray(uncompressedArray, boundIndex1, boundIndex2, boundSize))
                        {
                            //if the same, check if the compression is more interesting...
                            if ((biggestQuantityReaded * biggestQuantityRepeated) < (readedByte * repeatedByte))
                            {
                                biggestQuantityReaded = readedByte;
                                biggestQuantityRepeated = repeatedByte;
                            }
                        }
                        else
                            break;
                    } //end for repeated byte
                } // end for readed byte

                //if no combinaison were found...
                if (biggestQuantityRepeated == 0 && biggestQuantityReaded == 0)
                {
                    lonelyPixelCount += 1;
                    globalIndex += 1;
                }
                else
                {
                    //empty the buffer of lonelypixel (if exist)
                    if (lonelyPixelCount > 0)
                    {
                        //write previous lonelypixels bytes to memory stream
                        compressedArray.WriteByte((byte)(lonelyPixelCount-1));
                        compressedArray.Write(uncompressedArray, globalIndex - lonelyPixelCount, lonelyPixelCount);
                        lonelyPixelCount = 0;
                    }
                    //write bytes to memory stream
                    compressedArray.WriteByte(GetByteForCompression(biggestQuantityReaded, biggestQuantityRepeated));
                    compressedArray.Write(uncompressedArray, globalIndex, biggestQuantityReaded);
                    globalIndex += biggestQuantityReaded * biggestQuantityRepeated;
                }

                //check for maximal value for compression of lonely pixel (128) and force writing.
                if (lonelyPixelCount == 128)
                {
                    //write previous lonelypixels bytes to memory stream
                    compressedArray.WriteByte((byte)(lonelyPixelCount - 1));
                    compressedArray.Write(uncompressedArray, globalIndex - lonelyPixelCount, lonelyPixelCount);
                    lonelyPixelCount = 0;
                }

                //reinitialize default values
                biggestQuantityReaded = 0;
                biggestQuantityRepeated = 0;

            } while (globalIndex < uncompressedArray.Length);
            return compressedArray.ToArray();
        }

        private static bool CheckIfSameArray(byte[] array, int index1,int index2, int size)
        {
            for(int i = 0; i < size; i++)
            {
                if (array[index1 + i] != array[index2 + i])
                    return false;
            }
            return true;
        }
    }
}
