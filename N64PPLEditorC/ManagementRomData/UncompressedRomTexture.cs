﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC
{
    public class UncompressedRomTexture
    {
        public List<UncompressedRomTextureData> graphicsRom { get; set; }

        private byte[] rawData;

        public UncompressedRomTexture(byte[] rawData)
        {
            this.rawData = rawData;
            graphicsRom = new List<UncompressedRomTextureData>();
            InitGraphics();
        }

        private void AddTexture(int location, int sizeX, int sizeY, int bpp, string text)
        {

            graphicsRom.Add(new UncompressedRomTextureData(CGeneric.GiveMeArray(rawData, location, sizeX * sizeY * (bpp / 8)), location, sizeX, sizeY, bpp, text));
        }

        private void InitGraphics()
        {
            switch (RomLangAddress.romLang)
            {
                case CGeneric.romLang.French: InitGraphicsFr(); break;
            }
        }

        private void InitGraphicsFr()
        {
            //32bpp textures
            AddTexture(0x97C850, 104, 88, 32, "stage fini (cœur)");
            AddTexture(0x985750, 104, 97, 32, "manche finie (etoile)");
            AddTexture(0x98F4F0, 104, 96, 32, "game over (rond)");
            AddTexture(0x999290, 104, 79, 32, "temps écoulé");
            AddTexture(0x9A1150, 104, 17, 32, "essayer encore");
            AddTexture(0x9A31D0, 104, 94, 32, "oui non multiples…");
            AddTexture(0x9ACDD0, 104, 16, 32, "appuyez sur un bouton!");
            AddTexture(0x937CD0, 64 , 23, 32, "PAUSE");
            AddTexture(0x93CC50, 104, 88, 32, "stage fini (cœur)");
            AddTexture(0x945B4C, 104, 97, 32, "manche finie (etoile)");
            AddTexture(0x97C850, 104, 88, 32, "stage fini (cœur)");
            AddTexture(0x94F8EC, 104, 96, 32, "game over (rond)");
            AddTexture(0x9594E8, 104, 79, 32, "temps écoulé!");
            AddTexture(0x96154C, 104, 16, 32, "essayer encore");
            AddTexture(0x9630EC, 104, 98, 32, "oui non multiples…");
            AddTexture(0x96CE90, 104, 18, 32, "appuyez sur un bouton!");
            AddTexture(0x0F9D90, 64 , 24, 32, "prêt J1 ? (bleu)");
            AddTexture(0x0FB590, 24 , 72, 32, "1 2 3 (bleu)");
            AddTexture(0x0FD090, 64 , 24, 32, "prêt J2 ? (rouge)");
            AddTexture(0x0FE890, 24 , 72, 32, "1 2 3 (rouge)");

            //16bpp textures
            AddTexture(0x0F8690, 64 , 23 , 16, "Curseur Gros");
            AddTexture(0x0F9290, 64 , 23 , 16, "Curseur Petit");
            AddTexture(0x100390, 320, 232, 16, "continue screen (try again ?)");
            AddTexture(0x124790, 64 , 60 , 16, "continue screen (sacha head only)");
            AddTexture(0x183900, 320, 232, 16, "stade 1");
            AddTexture(0x1A7D00, 320, 232, 16, "stade 2");
            AddTexture(0x1CC100, 320, 232, 16, "Team rocket bateau");
            AddTexture(0X1F0500, 320, 232, 16, "Team rocket ville");
            AddTexture(0X214900, 320, 232, 16, "Team rocket sous l'eau");
            AddTexture(0x238D00, 320, 232, 16, "Team rocket marriage");
            AddTexture(0x25D100, 320, 232, 16, "Team rocket japon");
            AddTexture(0x281500, 320, 232, 16, "Team rocket étoiles");
            AddTexture(0x2A5900, 320, 232, 16, "Team rocket B tuyaux");
            AddTexture(0x2C9D00, 320, 232, 16, "Giovanni");
            AddTexture(0x2EE100, 320, 232, 16, "Bibliotheque richie");
            AddTexture(0x312500, 320, 232, 16, "bibliothèque Olga");
            AddTexture(0x336900, 320, 232, 16, "Bibliothèque pierre");
            AddTexture(0x35AD00, 320, 232, 16, "arène jour");
            AddTexture(0x37F100, 320, 232, 16, "arène nuit");
            AddTexture(0x3A3500, 320, 232, 16, "arène aube");
            AddTexture(0x3C7900, 320, 232, 16, "arène avec pierre");
            AddTexture(0x3EBD00, 320, 232, 16, "arène avec pierre nuit");
            AddTexture(0x410100, 320, 232, 16, "arène avec pierre aube");
            AddTexture(0x434500, 320, 232, 16, "arène avec eau");
            AddTexture(0x458900, 320, 232, 16, "arène avec eau nuit");
            AddTexture(0x47CD00, 320, 232, 16, "arène avec eau aube");
            AddTexture(0x4A1100, 320, 232, 16, "arène avec terre");
            AddTexture(0x4C5500, 320, 232, 16, "arène avec terre nuit");
            AddTexture(0x4E9900, 320, 232, 16, "arène avec terre aube");
            AddTexture(0x50DD00, 320, 232, 16, "arène finale mewtwo");
            AddTexture(0x536300, 64 , 96 , 16, "bloc jaunes multiplicateur de combo");
            AddTexture(0x53D500, 64 , 96 , 16, "bloc rouge multiplicateur de combo");
            AddTexture(0x544700, 64 , 96 , 16, "bloc gris multiplicateur de combo");
            AddTexture(0x54B900, 64 , 96 , 16, "bloc violet multiplicateur de combo");
            AddTexture(0x552B00, 64 , 96 , 16, "bloc orange multiplicateur de combo");
            AddTexture(0x559D00, 64 , 96 , 16, "bloc vert multiplicateur de combo");
            AddTexture(0x560F00, 64 , 96 , 16, "bloc rose multiplicateur de combo");
            AddTexture(0x568100, 64 , 96 , 16, "bloc bleu moyen clair multiplicateur de combo");
            AddTexture(0x56F300, 64 , 96 , 16, "bloc bleu clair multiplicateur de combo");
            AddTexture(0x576500, 64 , 96 , 16, "bloc bleu foncé multiplicateur de combo");
            AddTexture(0x57D700, 64 , 96 , 16, "bloc rose foncé multiplicateur de combo");
            AddTexture(0x580718, 112, 155, 16, "stade : ondine");
            AddTexture(0x588EB8, 88 , 72 , 16, "stade : pikachu et togepi");
            AddTexture(0x58C058, 112, 167, 16, "stade : prof chen pute et richie");
            AddTexture(0x595338, 96 , 71 , 16, "stade : pikachu et marill");
            AddTexture(0x598898, 128, 184, 16, "bateau : team rocket");
            AddTexture(0x5A4098, 80 , 84 , 16, "bateau : miaouss");
            AddTexture(0x5A7538, 128, 156, 16, "ville : team rocket");
            AddTexture(0x5B1138, 96 , 84 , 16, "ville : miaouss");
            AddTexture(0x5B5058, 128, 184, 16, "eau : team rocket");
            AddTexture(0x5C0858, 96 , 90 , 16, "eau : miaouss");
            AddTexture(0x5C4BF8, 128, 156, 16, "mariage : team rocket");
            AddTexture(0x5CE7F8, 112, 84 , 16, "mariage : miaouss");
            AddTexture(0x5D3298, 128, 185, 16, "ninja : team rocket");
            AddTexture(0x5DEB98, 128, 98 , 16, "ninja : miaouss");
            AddTexture(0x5E4DB8, 112, 173, 16, "etoile : team rocket");
            AddTexture(0x5EE518, 80 , 84 , 16, "etoile : miaouss");
            AddTexture(0x5F19B8, 112, 162, 16, "labo : team rocket 2");
            AddTexture(0x5FA778, 80 , 86 , 16, "labo : soporifik");
            AddTexture(0x5FDD58, 144, 113, 16, "labo : giovanni rire");
            AddTexture(0x605B58, 96 , 98 , 16, "labo : persian");
            AddTexture(0x60A4F8, 96 , 135, 16, "bibliothèque : richie");
            AddTexture(0x610A38, 80 , 77 , 16, "bibliothèque : pikachu");
            AddTexture(0x613A78, 64 , 170, 16, "bibliothèque : olga");
            AddTexture(0x618F78, 96 , 72 , 16, "bibliothèque : tetarte");
            AddTexture(0x61C598, 128, 169, 16, "bibliothèque : pierre");
            AddTexture(0x626E98, 64 , 75 , 16, "bibliothèque : goupix");
            AddTexture(0x629420, 112, 387, 16, "fond ingame J1 sacha");
            AddTexture(0x63E6C0, 112, 381, 16, "fond ingame J1 Regis");
            AddTexture(0x653420, 112, 390, 16, "fond ingame J1 Pierre");
            AddTexture(0x668960, 112, 405, 16, "fond ingame J1 Ondine");
            AddTexture(0x67EBC0, 112, 495, 16, "fond ingame J1 Major bob");
            AddTexture(0x699CE0, 112, 426, 16, "fond ingame J1 Erika");
            AddTexture(0x6B11A0, 112, 570, 16, "fond ingame J1 Koga");
            AddTexture(0x6D0460, 112, 429, 16, "fond ingame J1 Morganne");
            AddTexture(0x6E7BC0, 112, 471, 16, "fond ingame J1 Auguste");
            AddTexture(0x7017E0, 112, 453, 16, "fond ingame J1 Jacky");
            AddTexture(0x71A440, 112, 708, 16, "fond ingame J1 Team Rocket");
            AddTexture(0x740FC0, 112, 447, 16, "fond ingame J1 Giovanni");
            AddTexture(0x7596E0, 112, 431, 16, "fond ingame J1 Ritchie");
            AddTexture(0x7710E0, 112, 531, 16, "fond ingame J1 Olga");
            AddTexture(0x78E180, 112, 768, 16, "fond ingame J1 Aldo");
            AddTexture(0x7B8180, 112, 477, 16, "fond ingame J1 Regis2");
            AddTexture(0x7D22E0, 112, 318, 16, "fond ingame J2 Sacha");
            AddTexture(0x7E3920, 112, 306, 16, "fond ingame J2 Regis");
            AddTexture(0x7F44E0, 112, 344, 16, "fond ingame J2 Pierre");
            AddTexture(0x8071E0, 112, 324, 16, "fond ingame J2 Ondine");
            AddTexture(0x818D60, 112, 334, 16, "fond ingame J2 Major bob");
            AddTexture(0x82B1A0, 112, 332, 16, "fond ingame J2 Erika");
            AddTexture(0x83D420, 112, 354, 16, "fond ingame J2 Koga");
            AddTexture(0x8509E0, 112, 362, 16, "fond ingame J2 Morganne");
            AddTexture(0x8646A0, 112, 374, 16, "fond ingame J2 Auguste");
            AddTexture(0x878DE0, 112, 354, 16, "fond ingame J2 Jacky");
            AddTexture(0x88C3A0, 112, 482, 16, "fond ingame J2 Team Rocket");
            AddTexture(0x8A6960, 112, 354, 16, "fond ingame J2 Giovanni");
            AddTexture(0x8B9F20, 112, 344, 16, "fond ingame J2 Ritchie");
            AddTexture(0x8CCC20, 112, 374, 16, "fond ingame J2 Olga");
            AddTexture(0x8E1360, 112, 576, 16, "fond ingame J2 Aldo");
            AddTexture(0x900B60, 112, 390, 16, "fond ingame J2 Regis2");
            AddTexture(0x96EBD0, 112, 252, 16, "Gagné (J1); Gagné(J2) Perdu (J1) Perdu (J2)Nul (J1) Nul (J2)");
            AddTexture(0x9AE7D0, 112, 252, 16, "Gagné (J1); Gagné(J2) Perdu (J1) Perdu (J2) Nul (J1) Nul (J2)");

            //8bpp textures
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0xE8340, 64 * 192), 0xE8340, 64, 192, 8, "numeros…"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0xEB440, 64 * 26), 0xEB440, 64, 26, 8, "texte reac. Combos"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0xEBCF9, 64 * 28), 0xEBCF9, 64, 28, 8, "texte fini"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0xEC808, 64 * 8), 0xEC808, 64, 8, 8, "fleche démonstration comment jouer"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0xECA30, 64 * 64), 0xECA30, 64, 64, 8, "mini etoiles de jeu victoire"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0xF7E78, 64*32), 0xF7E78, 64, 32, 8, "ready 1 2 3",CGeneric.GiveMeArray(rawData, 0xF7C62,200)));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x126A80, 192 * 47), 0x126A80, 192, 47, 8, "game over", CGeneric.GiveMeArray(rawData, 0x126690, 1024)));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x128DC0, 64*116), 0x128DC0, 64, 116, 8, "game over OUI NON",CGeneric.GiveMeArray(rawData, 0x126880,1024)));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x532100, 64 * 256), 0x532100, 64, 256, 8, "blog gris et eclair jaunes", CGeneric.GiveMeArray(rawData, 0x536100, 200)));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x539300, 64 * 256), 0x539300, 64, 256, 8, "bloc rouge combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x540500, 64 * 256), 0x540500, 64, 256, 8, "bloc gris combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x547700, 64 * 256), 0x547700, 64, 256, 8, "bloc violet combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x54E900, 64 * 256), 0x54E900, 64, 256, 8, "bloc orange combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x555B00, 64 * 256), 0x555B00, 64, 256, 8, "bloc vert combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x55CD00, 64 * 256), 0x55CD00, 64, 256, 8, "bloc rose combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x563F00, 64 * 256), 0x563F00, 64, 256, 8, "bloc bleu moyen clair combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x56B100, 64 * 256), 0x56B100, 64, 256, 8, "bloc bleu clair combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x572300, 64 * 256), 0x572300, 64, 256, 8, "bloc bleu foncé combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x579500, 64 * 256), 0x579500, 64, 256, 8, "bloc rose foncé combo dans ta gueule"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x91609E, 96 * 200), 0x91609E, 96, 200, 8, "rondoudou STOP!"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x91AD40, 64 * 138), 0x91AD40, 64, 138, 8, "rondoudou boite de dialogue"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x91D110, 128 * 443), 0x91D110, 128, 443, 8, "ouverture en début de match 2D"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x92AF30, 128 * 393), 0x92AF30, 128, 393, 8, "ouverture en début de match 3D"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x937530, 128 * 15), 0x937530, 128, 15, 8, "texte statistik"));
            //graphicsRom.Add(new GraphicsRomAddress(CGeneric.GiveMeArray(rawData, 0x9394D0, 128 * 107), 0x9394D0, 128, 107, 8, "continuer; ret. Selection; continuer; recommencer; ret.selection"));
        }

        public int GetGraphicsCount()
        {
            return graphicsRom.Count();
        }

        public string GetGraphicsName(int index)
        {
            return graphicsRom[index].GetName();
        }
        public Bitmap GetTexture(int index)
        {
            return graphicsRom[index].ShowTexture();
        }

        public void SetTexture(int index, byte[] rawData)
        {
            graphicsRom[index].SetTexture(rawData);
        }


        //private void ChunkDataToRessources(byte[] rawData)
        //{


        //    //ChunkGraphicsData(rawData);
        //}


        //private void ChunkGraphicsData(byte[] rawData)
        //{


        //    addGraphicsElement(rawData, 12, 16, GraphicsAddressFr.backgroundArenaStartingLeague);
        //    addGraphicsElement(rawData, 12, 16, GraphicsAddressFr.backgroundArenaPokemonTrainer);
        //    addGraphicsElement(rawData, 12, 16, GraphicsAddressFr.backgroundArenaPokemonSelector);
        //    addGraphicsElement(rawData, 12, 16, GraphicsAddressFr.backgroundInGameSPAMode);

        //}



        //    private void addGraphicsElement(byte[] rawData,int nbElements, int bytePerPixel,GraphicsAddressFr graphicsAddress)
        //    {
        //        for (int i = 0; i < nbElements; i++)
        //        {
        //            //get the graphics address starting point
        //            var adressStartingData = new Byte[4];
        //            Array.Copy(rawData,(int)graphicsAddress+i*4, adressStartingData, 0, adressStartingData.Length);

        //            //two case here, 1 : this is only a texture, 2 : this is a 2 texture merge with a header
        //            var situationCase = new Byte[4];
        //            Array.Copy(adressStartingData, situationCase, situationCase.Length);
        //            //cheack if header..
        //            GraphicsRomAddress tmpGraphic;
        //            if (CGeneric.ConvertByteArrayToInt(situationCase) == 0x00100000)
        //            {
        //                //extract Header Data for determining length of raw Data
        //                var dataHeader = CGeneric.GiveMeArray(rawData, CGeneric.ConvertByteArrayToInt(adressStartingData), 24);
        //                var sizeX = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(dataHeader, 4, 2));
        //                var sizeY = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(dataHeader, 6, 2));
        //                var sizeX2 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(dataHeader, 16, 2));
        //                var SizeY2 = CGeneric.ConvertByteArrayToInt(CGeneric.GiveMeArray(dataHeader, 18, 2));
        //                var dataTmp = CGeneric.GiveMeArray(rawData, (int)(graphicsAddress + i * 4), 24 + (sizeX * sizeY) + (sizeX2 * SizeY2));
        //                tmpGraphic = new GraphicsRomAddress(dataTmp, graphicsAddress + i * 4, CGeneric.ConvertByteArrayToInt(adressStartingData), bytePerPixel, sizeX, sizeY, sizeX2, SizeY2, dataHeader);
        //            }
        //            else
        //            {
        //                var dataTmp = new Byte[320 * 232 * bytePerPixel];
        //                Array.Copy(rawData, CGeneric.ConvertByteArrayToInt(adressStartingData), dataTmp, 0, dataTmp.Length);
        //                tmpGraphic = new GraphicsRomAddress(dataTmp, graphicsAddress + i * 4, CGeneric.ConvertByteArrayToInt(adressStartingData), bytePerPixel);
        //            }
        //            graphicsRom.Add(tmpGraphic);
        //        }
        //    }
    }
}
