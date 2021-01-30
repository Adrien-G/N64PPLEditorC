using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        private void AddTexture(int location, int sizeX, int sizeY, int bpp, string text,int paletteLocation=0,int paletteSize=0)
        {
            graphicsRom.Add(new UncompressedRomTextureData(CGeneric.GiveMeArray(rawData, location, sizeX * sizeY * (bpp / 8)), location, sizeX, sizeY, bpp, text, CGeneric.GiveMeArray(rawData, paletteLocation, paletteSize),paletteLocation));
        }

        private void InitGraphics()
        {
            switch (RomLangAddress.romLang)
            {
                case CGeneric.romLang.French: InitGraphicsFr(); break;
                case CGeneric.romLang.German: InitGraphicsGer(); break;
                case CGeneric.romLang.European: InitGraphicsEur(); break;
                case CGeneric.romLang.USA: InitGraphicsUsa(); break;
            }
        }

        private void InitGraphicsEur()
        {
            //32bpp textures
            AddTexture(0x972FB0, 104, 88, 32, "ended stage (heart)");
        }
        private void InitGraphicsUsa()
        {
            //32bpp textures


            //16bpp textures
            AddTexture(0x9702C0, 112, 88,  16, "1 - Win Lose");
            AddTexture(0x62AB10, 112, 387, 16, "2 - fond ingame J1 1 - sacha");
            AddTexture(0x63FDB0, 112, 381, 16, "3 - fond ingame J1 2 - Regis");
            AddTexture(0x7E5010, 112, 306, 16, "4 - fond ingame J2 2 - Regis");

            //french version : 
            //AddTexture(0x, 112, 390, 16, "3 - fond ingame J1 3 - Pierre");
            //AddTexture(0x, 112, 405, 16, "4 - fond ingame J1 4 - Ondine");
            //AddTexture(0x, 112, 495, 16, "92 - fond ingame J1 5 - Major bob");
            //AddTexture(0x, 112, 426, 16, "93 - fond ingame J1 6 - Erika");
            //AddTexture(0x, 112, 570, 16, "94 - fond ingame J1 7 - Koga");
            //AddTexture(0x, 112, 429, 16, "95 - fond ingame J1 8 - Morganne");
            //AddTexture(0x, 112, 471, 16, "96 - fond ingame J1 9 - Auguste");
            //AddTexture(0x, 112, 453, 16, "97 - fond ingame J1 10 - Jacky");
            //AddTexture(0x, 112, 708, 16, "98 - fond ingame J1 11 - Team Rocket");
            //AddTexture(0x, 112, 447, 16, "99 - fond ingame J1 12 - Giovanni");
            //AddTexture(0x, 112, 431, 16, "100 - fond ingame J1 13 - Ritchie");
            //AddTexture(0x, 112, 531, 16, "101 - fond ingame J1 14 - Olga");
            //AddTexture(0x, 112, 768, 16, "102 - fond ingame J1 15 - Aldo");
            //AddTexture(0x, 112, 477, 16, "103 - fond ingame J1 16 - Regis2");
            //AddTexture(0x, 112, 318, 16, "104 - fond ingame J2 1 - Sacha");
            //AddTexture(0x, 112, 306, 16, "105 - fond ingame J2 2 - Regis");
            //AddTexture(0x, 112, 344, 16, "106 - fond ingame J2 3 - Pierre");
            //AddTexture(0x, 112, 324, 16, "107 - fond ingame J2 4 - Ondine");
            //AddTexture(0x, 112, 334, 16, "108 - fond ingame J2 5 - Major bob");
            //AddTexture(0x, 112, 332, 16, "109 - fond ingame J2 6 - Erika");
            //AddTexture(0x, 112, 354, 16, "110 - fond ingame J2 7 - Koga");
            //AddTexture(0x, 112, 362, 16, "111 - fond ingame J2 8 - Morganne");
            //AddTexture(0x, 112, 374, 16, "112 - fond ingame J2 9 - Auguste");
            //AddTexture(0x, 112, 354, 16, "113 - fond ingame J2 10 - Jacky");
            //AddTexture(0x, 112, 482, 16, "114 - fond ingame J2 11 - Team Rocket");
            //AddTexture(0x, 112, 354, 16, "115 - fond ingame J2 12 - Giovanni");
            //AddTexture(0x, 112, 344, 16, "116 - fond ingame J2 13 - Ritchie");
            //AddTexture(0x, 112, 374, 16, "117 - fond ingame J2 14 - Olga");
            //AddTexture(0x, 112, 576, 16, "118 - fond ingame J2 15 - Aldo");
            //AddTexture(0x, 112, 390, 16, "119 - fond ingame J2 16 - Regis2");
        }

        private void InitGraphicsGer()
        {
            //32bpp textures
            AddTexture(0x972FB0, 104, 88, 32, "ended stage (heart)");
            AddTexture(0x97BEB0, 104, 97, 32, "manche finie (etoile)");
            AddTexture(0x985C50, 104, 96, 32, "game over (rond)");
            AddTexture(0x98F9F0, 104, 79, 32, "temps écoulé");
            AddTexture(0x9978B0, 104, 17, 32, "essayer encore");
            AddTexture(0x999930, 104, 94, 32, "oui non multiples…");
            AddTexture(0x9A3530, 104, 16, 32, "appuyez sur un bouton!");
            AddTexture(0x92D4AC, 64 , 23, 32, "PAUSE");
            AddTexture(0x9333B0, 104, 88, 32, "stage fini (cœur)");
            AddTexture(0x93C2AC, 104, 97, 32, "manche finie (etoile)");
            AddTexture(0x972FB0, 104, 88, 32, "stage fini (cœur)");
            AddTexture(0x94604C, 104, 96, 32, "game over (rond)");
            AddTexture(0x94FC48, 104, 79, 32, "temps écoulé!");
            AddTexture(0x957CAC, 104, 16, 32, "essayer encore");
            AddTexture(0x95984C, 104, 98, 32, "oui non multiples…");
            AddTexture(0x9635F0, 104, 18, 32, "appuyez sur un bouton!");

            //AddTexture(0x0F9D90, 64, 24, 32, "prêt J1 ? (bleu)");
            //AddTexture(0x0FB590, 24, 72, 32, "1 2 3 (bleu)");
            //AddTexture(0x0FD090, 64, 24, 32, "prêt J2 ? (rouge)");
            //AddTexture(0x0FE890, 24, 72, 32, "1 2 3 (rouge)");
        }
            private void InitGraphicsFr()
        {
            //32bpp textures
            AddTexture(0x97C850, 104, 88, 32, "1 - stage fini (cœur)");
            AddTexture(0x985750, 104, 97, 32, "2 - manche finie (etoile)");
            AddTexture(0x98F4F0, 104, 96, 32, "3 - game over (rond)");
            AddTexture(0x999290, 104, 79, 32, "4 - temps écoulé");
            AddTexture(0x9A1150, 104, 17, 32, "5 - essayer encore");
            AddTexture(0x9A31D0, 104, 94, 32, "6 - oui non multiples…");
            AddTexture(0x9ACDD0, 104, 16, 32, "7 - appuyez sur un bouton!");
            AddTexture(0x937CD0, 64 , 23, 32, "8 - PAUSE");
            AddTexture(0x93CC50, 104, 88, 32, "9 - stage fini (cœur)");
            AddTexture(0x945B4C, 104, 97, 32, "10 - manche finie (etoile)");
            AddTexture(0x97C850, 104, 88, 32, "11 - stage fini (cœur)");
            AddTexture(0x94F8EC, 104, 96, 32, "12 - game over (rond)");
            AddTexture(0x9594E8, 104, 79, 32, "13 - temps écoulé!");
            AddTexture(0x96154C, 104, 16, 32, "14 - essayer encore");
            AddTexture(0x9630EC, 104, 98, 32, "15 - oui non multiples…");
            AddTexture(0x96CE90, 104, 18, 32, "16 - appuyez sur un bouton!");
            AddTexture(0x0F9D90, 64 , 24, 32, "17 - prêt J1 (bleu)");
            AddTexture(0x0FB590, 24 , 72, 32, "18 - 1 2 3 (bleu)");
            AddTexture(0x0FD090, 64 , 24, 32, "19 - prêt J2 (rouge)");
            AddTexture(0x0FE890, 24 , 72, 32, "20 - 1 2 3 (rouge)");

            //16bpp textures
            AddTexture(0x0F8690, 64 , 23 , 16, "21 - Curseur Gros");
            AddTexture(0x0F9290, 64 , 23 , 16, "22 - Curseur Petit");
            AddTexture(0x100390, 320, 232, 16, "23 - continue screen (try again)");
            AddTexture(0x124790, 64 , 60 , 16, "24 - continue screen (tête de sacha)");
            AddTexture(0x183900, 320, 232, 16, "25 - stade 1");
            AddTexture(0x1A7D00, 320, 232, 16, "26 - stade 2");
            AddTexture(0x1CC100, 320, 232, 16, "27 - Team rocket bateau");
            AddTexture(0X1F0500, 320, 232, 16, "28 - Team rocket ville");
            AddTexture(0X214900, 320, 232, 16, "29 - Team rocket sous l'eau");
            AddTexture(0x238D00, 320, 232, 16, "30 - Team rocket marriage");
            AddTexture(0x25D100, 320, 232, 16, "31 - Team rocket japon");
            AddTexture(0x281500, 320, 232, 16, "32 - Team rocket étoiles");
            AddTexture(0x2A5900, 320, 232, 16, "33 - Team rocket B tuyaux");
            AddTexture(0x2C9D00, 320, 232, 16, "34 - Giovanni");
            AddTexture(0x2EE100, 320, 232, 16, "35 - Bibliotheque richie");
            AddTexture(0x312500, 320, 232, 16, "36 - bibliothèque Olga");
            AddTexture(0x336900, 320, 232, 16, "37 - Bibliothèque pierre");
            AddTexture(0x35AD00, 320, 232, 16, "38 - arène jour");
            AddTexture(0x37F100, 320, 232, 16, "39 - arène nuit");
            AddTexture(0x3A3500, 320, 232, 16, "40 - arène aube");
            AddTexture(0x3C7900, 320, 232, 16, "41 - arène avec pierre");
            AddTexture(0x3EBD00, 320, 232, 16, "42 - arène avec pierre nuit");
            AddTexture(0x410100, 320, 232, 16, "43 - arène avec pierre aube");
            AddTexture(0x434500, 320, 232, 16, "44 - arène avec eau");
            AddTexture(0x458900, 320, 232, 16, "45 - arène avec eau nuit");
            AddTexture(0x47CD00, 320, 232, 16, "46 - arène avec eau aube");
            AddTexture(0x4A1100, 320, 232, 16, "47 - arène avec terre");
            AddTexture(0x4C5500, 320, 232, 16, "48 - arène avec terre nuit");
            AddTexture(0x4E9900, 320, 232, 16, "49 - arène avec terre aube");
            AddTexture(0x50DD00, 320, 232, 16, "50 - arène finale mewtwo");
            AddTexture(0x536300, 64 , 96 , 16, "51 - bloc jaunes multiplicateur de combo");
            AddTexture(0x53D500, 64 , 96 , 16, "52 - bloc rouge multiplicateur de combo");
            AddTexture(0x544700, 64 , 96 , 16, "53 - bloc gris multiplicateur de combo");
            AddTexture(0x54B900, 64 , 96 , 16, "54 - bloc violet multiplicateur de combo");
            AddTexture(0x552B00, 64 , 96 , 16, "55 - bloc orange multiplicateur de combo");
            AddTexture(0x559D00, 64 , 96 , 16, "56 - bloc vert multiplicateur de combo");
            AddTexture(0x560F00, 64 , 96 , 16, "57 - bloc rose multiplicateur de combo");
            AddTexture(0x568100, 64 , 96 , 16, "58 - bloc bleu moyen clair multiplicateur de combo");
            AddTexture(0x56F300, 64 , 96 , 16, "59 - bloc bleu clair multiplicateur de combo");
            AddTexture(0x576500, 64 , 96 , 16, "60 - bloc bleu foncé multiplicateur de combo");
            AddTexture(0x57D700, 64 , 96 , 16, "61 - bloc rose foncé multiplicateur de combo");
            AddTexture(0x580718, 112, 155, 16, "62 - stade, ondine");
            AddTexture(0x588EB8, 88 , 72 , 16, "63 - stade, pikachu et togepi");
            AddTexture(0x58C058, 112, 167, 16, "64 - stade, prof chen, madame et richie");
            AddTexture(0x595338, 96 , 71 , 16, "65 - stade, pikachu et marill");
            AddTexture(0x598898, 128, 184, 16, "66 - bateau, team rocket");
            AddTexture(0x5A4098, 80 , 84 , 16, "67 - bateau, miaouss");
            AddTexture(0x5A7538, 128, 156, 16, "68 - ville, team rocket");
            AddTexture(0x5B1138, 96 , 84 , 16, "69 - ville, miaouss");
            AddTexture(0x5B5058, 128, 184, 16, "70 - eau, team rocket");
            AddTexture(0x5C0858, 96 , 90 , 16, "71 - eau, miaouss");
            AddTexture(0x5C4BF8, 128, 156, 16, "72 - mariage, team rocket");
            AddTexture(0x5CE7F8, 112, 84 , 16, "73 - mariage, miaouss");
            AddTexture(0x5D3298, 128, 185, 16, "74 - ninja, team rocket");
            AddTexture(0x5DEB98, 128, 98 , 16, "75 - ninja, miaouss");
            AddTexture(0x5E4DB8, 112, 173, 16, "76 - etoile, team rocket");
            AddTexture(0x5EE518, 80 , 84 , 16, "77 - etoile, miaouss");
            AddTexture(0x5F19B8, 112, 162, 16, "78 - labo, team rocket 2");
            AddTexture(0x5FA778, 80 , 86 , 16, "79 - labo, soporifik");
            AddTexture(0x5FDD58, 144, 113, 16, "80 - labo, giovanni rire");
            AddTexture(0x605B58, 96 , 98 , 16, "81 - labo, persian");
            AddTexture(0x60A4F8, 96 , 135, 16, "82 - bibliothèque, richie");
            AddTexture(0x610A38, 80 , 77 , 16, "83 - bibliothèque, pikachu");
            AddTexture(0x613A78, 64 , 170, 16, "84 - bibliothèque, olga");
            AddTexture(0x618F78, 96 , 72 , 16, "85 - bibliothèque, tetarte");
            AddTexture(0x61C598, 128, 169, 16, "86 - bibliothèque, pierre");
            AddTexture(0x626E98, 64 , 75 , 16, "87 - bibliothèque, goupix");
            AddTexture(0x629420, 112, 387, 16, "88 - fond ingame J1 1 - sacha");
            AddTexture(0x63E6C0, 112, 381, 16, "89 - fond ingame J1 2 - Regis");
            AddTexture(0x653420, 112, 390, 16, "90 - fond ingame J1 3 - Pierre");
            AddTexture(0x668960, 112, 405, 16, "91 - fond ingame J1 4 - Ondine");
            AddTexture(0x67EBC0, 112, 495, 16, "92 - fond ingame J1 5 - Major bob");
            AddTexture(0x699CE0, 112, 426, 16, "93 - fond ingame J1 6 - Erika");
            AddTexture(0x6B11A0, 112, 570, 16, "94 - fond ingame J1 7 - Koga");
            AddTexture(0x6D0460, 112, 429, 16, "95 - fond ingame J1 8 - Morganne");
            AddTexture(0x6E7BC0, 112, 471, 16, "96 - fond ingame J1 9 - Auguste");
            AddTexture(0x7017E0, 112, 453, 16, "97 - fond ingame J1 10 - Jacky");
            AddTexture(0x71A440, 112, 708, 16, "98 - fond ingame J1 11 - Team Rocket");
            AddTexture(0x740FC0, 112, 447, 16, "99 - fond ingame J1 12 - Giovanni");
            AddTexture(0x7596E0, 112, 431, 16, "100 - fond ingame J1 13 - Ritchie");
            AddTexture(0x7710E0, 112, 531, 16, "101 - fond ingame J1 14 - Olga");
            AddTexture(0x78E180, 112, 768, 16, "102 - fond ingame J1 15 - Aldo");
            AddTexture(0x7B8180, 112, 477, 16, "103 - fond ingame J1 16 - Regis2");
            AddTexture(0x7D22E0, 112, 318, 16, "104 - fond ingame J2 1 - Sacha");
            AddTexture(0x7E3920, 112, 306, 16, "105 - fond ingame J2 2 - Regis");
            AddTexture(0x7F44E0, 112, 344, 16, "106 - fond ingame J2 3 - Pierre");
            AddTexture(0x8071E0, 112, 324, 16, "107 - fond ingame J2 4 - Ondine");
            AddTexture(0x818D60, 112, 334, 16, "108 - fond ingame J2 5 - Major bob");
            AddTexture(0x82B1A0, 112, 332, 16, "109 - fond ingame J2 6 - Erika");
            AddTexture(0x83D420, 112, 354, 16, "110 - fond ingame J2 7 - Koga");
            AddTexture(0x8509E0, 112, 362, 16, "111 - fond ingame J2 8 - Morganne");
            AddTexture(0x8646A0, 112, 374, 16, "112 - fond ingame J2 9 - Auguste");
            AddTexture(0x878DE0, 112, 354, 16, "113 - fond ingame J2 10 - Jacky");
            AddTexture(0x88C3A0, 112, 482, 16, "114 - fond ingame J2 11 - Team Rocket");
            AddTexture(0x8A6960, 112, 354, 16, "115 - fond ingame J2 12 - Giovanni");
            AddTexture(0x8B9F20, 112, 344, 16, "116 - fond ingame J2 13 - Ritchie");
            AddTexture(0x8CCC20, 112, 374, 16, "117 - fond ingame J2 14 - Olga");
            AddTexture(0x8E1360, 112, 576, 16, "118 - fond ingame J2 15 - Aldo");
            AddTexture(0x900B60, 112, 390, 16, "119 - fond ingame J2 16 - Regis2");
            AddTexture(0x96EBD0, 112, 252, 16, "120 - Gagné (J1); Gagné(J2) Perdu (J1) Perdu (J2)Nul (J1) Nul (J2)");
            AddTexture(0x9AE7D0, 112, 252, 16, "121 - Gagné (J1); Gagné(J2) Perdu (J1) Perdu (J2) Nul (J1) Nul (J2)");

            //8bpp textures
            AddTexture(0xCA218, 64, 448, 8, "122 - Bloc pokemon", 0xC8FA0, 0x200);
            AddTexture(0xD1217, 64, 320, 8, "123 - Bloc basic", 0xC93D0, 0x200);
            AddTexture(0xE226F, 64, 320, 8, "124 - Bloc basic", 0xE2058, 0x200);
            AddTexture(0xE8340, 64, 192, 8, "125 - numeros…", 0xE8128, 0x200);
            AddTexture(0xEB440, 64, 26, 8, "126 - texte reac. Combos", 0xE8128, 0x200);
            AddTexture(0xEBCF9, 64, 28, 8, "127 - texte fini", 0xE8128, 0x200);
            AddTexture(0xEC808, 64, 8, 8, "128 - fleche démonstration comment jouer", 0xE8128, 0x200);
            AddTexture(0xECA30, 64, 64, 8, "129 - mini etoiles de jeu victoire", 0xE8128, 0x200);
            AddTexture(0xF7E78, 64, 32, 8, "130 - ready 1 2 3", 0x0F7C60, 0x200);
            AddTexture(0x126A80, 192, 47, 8, "131 - game over", 0x126690, 0x200);
            AddTexture(0x128DC0, 64, 116, 8, "132 - game over OUI NON", 0x126880, 0x200);
            AddTexture(0x131FBF, 320, 232, 8, "133 - boiteJeu modeTemps", 0x1441C0, 0x200);
            AddTexture(0x1443C0, 320, 232, 8, "134 - boiteJeu 2 joueurs", 0x1565C0, 0x200);
            AddTexture(0x1567C0, 320, 232, 8, "135 - boiteJeu modeTemps 3D", 0x1689C0, 0x200);
            AddTexture(0x168BC0, 320, 232, 8, "136 - boiteJeu 2 joueurs 3D", 0x17ADC0, 0x200);
            AddTexture(0x17AFC0, 64, 170, 8, "137 - Hud pendant une partie 2P", 0x17DA40, 0x200);
            AddTexture(0x17DB80, 64, 205, 8, "138 - Hud pendant une partie Score", 0x180EC0, 0x200);
            AddTexture(0x180FFA, 64, 156, 8, "139 - Hud pendant une partie 1P", 0x183700, 0x200);
            AddTexture(0x532100, 64, 256, 8, "140 - blog gris et eclair jaunes", 0x536100, 0x200);
            AddTexture(0x539300, 64, 256, 8, "141 - bloc rouge combo dans ta gueule", 0x53D300, 0x200);
            AddTexture(0x540500, 64, 256, 8, "142 - bloc gris combo dans ta gueule", 0x544500, 0x200);
            AddTexture(0x547700, 64, 256, 8, "143 - bloc violet combo dans ta gueule", 0x54B700, 0x200);
            AddTexture(0x54E900, 64, 256, 8, "144 - bloc orange combo dans ta gueule", 0x552900, 0x200);
            AddTexture(0x555B00, 64, 256, 8, "145 - bloc vert combo dans ta gueule", 0x559B00, 0x200);
            AddTexture(0x55CD00, 64, 256, 8, "146 - bloc rose combo dans ta gueule", 0x560D00, 0x200);
            AddTexture(0x563F00, 64, 256, 8, "147 - bloc bleu moyen clair combo dans ta gueule", 0x567F00, 0x200);
            AddTexture(0x56B100, 64, 256, 8, "148 - bloc bleu clair combo dans ta gueule", 0x56F100, 0x200);
            AddTexture(0x572300, 64, 256, 8, "149 - bloc bleu foncé combo dans ta gueule", 0x576300, 0x200);
            AddTexture(0x579500, 64, 256, 8, "150 - bloc rose foncé combo dans ta gueule", 0x57D500, 0x200);
            AddTexture(0x91609E, 96, 200, 8, "151 - rondoudou STOP!", 0x91ABA0, 0x200);
            AddTexture(0x91AD40, 64, 138, 8, "152 - rondoudou boite de dialogue", 0x91CFC0, 0x200);
            AddTexture(0x91D110, 128, 442,8, "153 - ouverture en début de match 2D", 0x92AE10, 0x200);
            AddTexture(0x92AF30, 128, 392,8, "154 - ouverture en début de match 3D", 0x937330, 0x200);
            AddTexture(0x937530, 128, 15, 8, "155 - texte statistik", 0x937CB0, 0x200);
            AddTexture(0x9394D0, 128, 107,8, "156 - continuer; ret. Selection; continuer; recommencer; ret.selection", 0x93CA50, 0x200);
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

        public void SetTexture(int index, byte[] rawData,byte[] palette = null)
        {
            graphicsRom[index].SetTexture(rawData,palette);
        }

        internal void WriteToRom(FileStream fs)
        {
            foreach(UncompressedRomTextureData texture in graphicsRom)
            {
                if(texture.palette != null)
                {
                    fs.Position = texture.paletteLocation;
                    fs.Write(texture.palette, 0, texture.palette.Length);
                }
                fs.Position = texture.location;
                fs.Write(texture.rawData,0, texture.rawData.Length);
            }
            
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
