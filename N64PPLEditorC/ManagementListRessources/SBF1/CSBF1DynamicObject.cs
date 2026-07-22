using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    public class CSBF1DynamicObject
    {
        //les données brutes du dynamic object (header + data)
        private byte[] rawData;

        //les flags du header du dynamic object (4 premiers octets)
        public byte[] HeaderData { get; private set; }

        //position X de base de l'objet
        private int BaseX;

        //position Y de base de l'objet
        private int BaseY;

        //index dans la liste bif, est présent uniquement si le header 0x80 est set.
        private int? BifRessourceIndex;

        // Présent avec le flag 0x2000.
        // Bits 0..7  : nombre de textes associés à chaque ligne.
        // Bits 8..19 : ID du premier texte.
        private uint? LinkedTextDescriptor;

        private int LinkedTextCountPerRow;
        public int LinkedTextBaseId;
        //utilisé lorsque le flag 0x40 n'est pas set. il permet de distribuer les cellules (X)
        private int SpanX;

        //utilisé lorsque le flag 0x40 n'est pas set. il permet de distribuer les cellules (Y)
        private int SpanY;

        //id du dynamic object
        private int Id;

        //nombre et pointeur runtime des poisitions explicites
        private int PointCount;

        private List<CSBF1DynamicObjectPoint> PointTable = new List<CSBF1DynamicObjectPoint>();

        //dimensions logique du tableau (lignes), rempli uniquement si le mode de disposition est Grid2D ou Vertical
        private int GridHeight;  

        private LayoutMode Layout; //mode de disposition du dynamic object, 0x10 = vertical, 0x20 = horizontal, 0x30 = grid2D

        //dimensions logique du tableau (colonnes), rempli uniquement si le mode de disposition est Grid2D ou Horizontal
        private int GridWidth;   
        private int? RectX0;
        private int? RectY0;
        private int? RectX1;
        private int? RectY1;

        //Mode de disposition du dynamic object
        public enum LayoutMode : int
        {
            None = 0x00,
            Vertical = 0x10,
            Horizontal = 0x20,
            Grid2D = 0x30,
        }

        public CSBF1DynamicObject(byte[] rawData)
        {
            this.rawData = rawData;
            this.HeaderData = CGeneric.GiveMeArray(rawData, 0, 4);

            //générique
            this.BaseX = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 4, 4));
            this.BaseY = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 8, 4));
            this.Id = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 12, 4));
            this.SpanX = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 16, 4));
            this.SpanY = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 20, 4));
            this.PointCount = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, 24, 4));

            //conditionnelle
            int globalOffset = 28;
            if( this.PointCount > 0)
            {
                
                PointTable = new List<CSBF1DynamicObjectPoint>();
                for (int i = 0; i < this.PointCount; i++)
                {
                    int pointOffset = globalOffset + (i * 8);
                    var pointData = CGeneric.GiveMeArray(rawData, pointOffset, 8);
                    PointTable.Add(new CSBF1DynamicObjectPoint(pointData));
                }
                globalOffset += this.PointCount * 8;
            }
            

            //conditionnelle flag
            if (CGeneric.GetBitStateFromInt(CGeneric.ConvertByteArrayToInt(HeaderData), 19))
            {
                this.LinkedTextDescriptor = (uint?)CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalOffset, 4));
                this.LinkedTextCountPerRow = (int)(LinkedTextDescriptor.GetValueOrDefault() & 0xFF);
                this.LinkedTextBaseId = (int)((LinkedTextDescriptor.GetValueOrDefault() << 8) & 0xFFF);
                globalOffset += 4;
            }

            if (CGeneric.GetBitStateFromInt(CGeneric.ConvertByteArrayToInt(HeaderData), 28))
            {
                this.GridHeight = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalOffset, 4));
                globalOffset += 4;
            }

            if (CGeneric.GetBitStateFromInt(CGeneric.ConvertByteArrayToInt(HeaderData), 27))
            {
                this.GridWidth = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalOffset, 4));
                globalOffset += 4;
            }

            if (CGeneric.GetBitStateFromInt(CGeneric.ConvertByteArrayToInt(HeaderData), 25))
            {
                this.BifRessourceIndex = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalOffset, 4));
                globalOffset += 4;
            }

            if (CGeneric.GetBitStateFromInt(CGeneric.ConvertByteArrayToInt(HeaderData), 26))
            {
                this.RectX0 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalOffset, 4));
                this.RectY0 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalOffset+4, 4));
                this.RectX1 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalOffset+8, 4));
                this.RectY1 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(rawData, globalOffset+12, 4));
                globalOffset += 16;
            }

            if(globalOffset != rawData.Length)
            {
                throw new Exception("CSBF1DynamicObject : globalOffset != rawData.Length");
            }
            //set layout
            var hasVerticalLayout = CGeneric.GetBitStateFromInt(CGeneric.ConvertByteArrayToInt(HeaderData), 28);
            var hasHorizontalLayout = CGeneric.GetBitStateFromInt(CGeneric.ConvertByteArrayToInt(HeaderData), 27);

            if(hasVerticalLayout && hasHorizontalLayout)
                this.Layout = LayoutMode.Grid2D;
            else if (hasVerticalLayout)
            {
                this.Layout = LayoutMode.Vertical;
                this.GridWidth = 1;
            }
            else if (hasHorizontalLayout)
            {
                this.Layout = LayoutMode.Horizontal;
                this.GridHeight = 1;
            }
            else
                this.Layout = LayoutMode.None;


        }

        [Obsolete]
        public static int GetHeaderLength(int headerValue,int extra)
        {
            int finalValue = 28;
            if (CGeneric.GetBitStateFromInt(headerValue, 19)) //0x00002000
                finalValue += 4;

            if (CGeneric.GetBitStateFromInt(headerValue, 25)) //0x00000080
                finalValue += 4;

            if (CGeneric.GetBitStateFromInt(headerValue, 26)) //0x00000040
                finalValue += 16;

            if (CGeneric.GetBitStateFromInt(headerValue, 27)) //0x00000020
                finalValue += 4;

            if (CGeneric.GetBitStateFromInt(headerValue, 28)) //0x00000010
                finalValue += 4;

            if (extra > 0)
                finalValue += extra * 8;

            return finalValue;
        }

        [Obsolete]
        public byte[] GetRawData()
        {
            //todo reconstruire une sortie de rawData à partir des données du dynamic object, pour l'instant on retourne juste le rawData d'origine
            return rawData;
        }
    }
}
