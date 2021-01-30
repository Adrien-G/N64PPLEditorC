using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static N64PPLEditorC.CGeneric;
using N64PPLEditorC.ManagementAudio;
using N64PPLEditorC.ManagementRomData;

namespace N64PPLEditorC
{

    
    public partial class Form1 : Form
    {
        CRessourceList ressourceList;
        UncompressedRomTexture romList;
        AudioList audioList;
        AssemblyReversedAddress misc;
        int freeSpaceLeft = 0;
        bool extendedRom = false;
        bool N64Game = true;


        List<TextBox> txtBox = new List<TextBox>();

        #region form
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CGeneric.VerifyExistingPath();
            textBoxPPLLocation.Text = Properties.Settings.Default.txtPPLLocation;
            if (textBoxPPLLocation.Text == "")
            {
                buttonGetRomFolder.TabIndex = 0;
                buttonLoadRom.TabIndex = 1;
            }
            else
            {
                buttonGetRomFolder.TabIndex = 1;
                buttonLoadRom.TabIndex = 0;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.txtPPLLocation = textBoxPPLLocation.Text;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region toolstrip textures

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewTextures.BeginUpdate();
            treeViewHVQM.BeginUpdate();
            treeViewSBF.BeginUpdate();
            treeViewTextures.ExpandAll();
            treeViewSBF.ExpandAll();
            treeViewHVQM.ExpandAll();
            treeViewTextures.EndUpdate();
            treeViewHVQM.EndUpdate();
            treeViewSBF.EndUpdate();
        }
        private void collpseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewTextures.BeginUpdate();
            treeViewHVQM.BeginUpdate();
            treeViewSBF.BeginUpdate();
            treeViewTextures.CollapseAll();
            treeViewSBF.CollapseAll();
            treeViewHVQM.CollapseAll();
            treeViewTextures.EndUpdate();
            treeViewHVQM.EndUpdate();
            treeViewSBF.EndUpdate();
        }
        private void addNewTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openTexture = new OpenFileDialog())
            {
                //openTexture.InitialDirectory = Application.StartupPath;
                openTexture.RestoreDirectory = true;
                openTexture.Multiselect = true;
                if (openTexture.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        foreach (String inputTexture in openTexture.FileNames)
                        {

                            Image img = Image.FromFile(inputTexture);
                            if (img.Width <= 320 && img.Height <= 240)
                            {
                                //load texture
                                pictureBoxTexture.Width = img.Width;
                                pictureBoxTexture.Height = img.Height;
                                pictureBoxTexture.Image = img;
                                
                                //test the best compression available
                                var compressionMethod = CTextureManager.TestBestCompression((Bitmap)pictureBoxTexture.Image);

                                //convert texture to byte array for future treatment 
                                byte[] rawData = CTextureManager.ConvertRGBABitmapToByteArrayRGBA((Bitmap)pictureBoxTexture.Image);

                                //make it at good format
                                byte[] palette;
                                (palette, rawData) = CTextureManager.ConvertPixelsToGoodFormat(rawData, compressionMethod);

                                //compress data
                                byte[] compressedData = CTextureCompress.MakeCompression(rawData);

                                //check if the size of the new data is not too much than the free space
                                UpdateFreeSpaceLeft();
                                if (freeSpaceLeft < compressedData.Length)
                                {
                                    MessageBox.Show("There is not enought space...", "Free space in rom is needed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    //if it's lower than 64Mb.
                                    if (!extendedRom)
                                    {
                                        var res = MessageBox.Show("Do you want to make extended rom file ? (32 to 64Mb)", "Extend PPL rom ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (res == DialogResult.Yes)
                                        {
                                            extendedRom = true;
                                            labelExtendedRom.Visible = true;
                                        }
                                    }
                                    else
                                        break;
                                }

                                //add header and generate the bff2
                                string safeFileName = Path.GetFileNameWithoutExtension(inputTexture);
                                //check if the name has same naming convention than the extract (remove index before coma)
                                if (safeFileName.Split(',').Count() == 2)
                                    safeFileName = safeFileName.Split(',')[1].Trim();

                                byte[] finalBFF2 = CBFF2.GenerateBFF2(palette, compressedData, compressionMethod, img.Width, img.Height, safeFileName);

                                //add texture to the bff2 files and update treeview
                                if (treeViewTextures.SelectedNode.Level == 0)
                                {
                                    ressourceList.fibList[treeViewTextures.SelectedNode.Index].AddBFF2Child(finalBFF2);
                                    treeViewTextures.SelectedNode.Nodes.Add("[added] , " + safeFileName);
                                    treeViewTextures.SelectedNode = treeViewTextures.Nodes[treeViewTextures.SelectedNode.Index].LastNode;
                                }
                                else
                                {
                                    ressourceList.fibList[treeViewTextures.SelectedNode.Parent.Index].AddBFF2Child(finalBFF2);
                                    treeViewTextures.SelectedNode.Parent.Nodes.Add("[added] , " + safeFileName);
                                    treeViewTextures.SelectedNode = treeViewTextures.Nodes[treeViewTextures.SelectedNode.Parent.Index].LastNode;
                                }
                                img.Dispose();

                            }
                            else
                                MessageBox.Show("Texture must be at maximum of size 320x240 !", "Size too big...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unrecognized image format :( " + Environment.NewLine + "Error details : " + ex.Message, "Error loading texture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    UpdateFreeSpaceLeft();
                }
            }
        }
        private void removeThisTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewTextures.SelectedNode.Level == 1)
            {
                ressourceList.fibList[treeViewTextures.SelectedNode.Parent.Index].RemoveBFF2Child(treeViewTextures.SelectedNode.Index);
                treeViewTextures.SelectedNode.Remove();
                UpdateFreeSpaceLeft();
            }

        }
        private void contextMenuStripForTreeview_Opening(object sender, CancelEventArgs e)
        {
            if (treeViewTextures.SelectedNode.Level == 1)
            {
                removeThisTextureToolStripMenuItem.Enabled = true;
                containerTypetoolStripMenuItem.Enabled = false;
            }

            else
            {
                removeThisTextureToolStripMenuItem.Enabled = false;
                containerTypetoolStripMenuItem.Enabled = true;

                var textureDisplay = this.ressourceList.fibList[treeViewTextures.SelectedNode.Index].GetTextureDisplayStyle();

                fixedToolStripMenuItem.Checked = false;
                animatedBadgesToolStripMenuItem.Checked = false;
                textureScrollbluePokeballBackgroundToolStripMenuItem.Checked = false;
                switch (textureDisplay)
                {
                    case TextureDisplayStyle.Fixed: fixedToolStripMenuItem.Checked = true; break;
                    case TextureDisplayStyle.Animated: animatedBadgesToolStripMenuItem.Checked = true; break;
                    case TextureDisplayStyle.AnimatedScroll: textureScrollbluePokeballBackgroundToolStripMenuItem.Checked = true; break;
                }
            }


        }

        private void fixedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ressourceList.fibList[treeViewTextures.SelectedNode.Index].SetTextureDisplayStyle(TextureDisplayStyle.Fixed);
        }
        private void animatedBadgesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ressourceList.fibList[treeViewTextures.SelectedNode.Index].SetTextureDisplayStyle(TextureDisplayStyle.Animated);
        }
        private void textureScrollbluePokeballBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ressourceList.fibList[treeViewTextures.SelectedNode.Index].SetTextureDisplayStyle(TextureDisplayStyle.AnimatedScroll);
        }

        #endregion

        #region helping form
        private void buttonLoadRom_MouseEnter(object sender, EventArgs e) { helpStatus.Text = "load PPL rom for editing content"; }
        private void buttonGetRomFolder_MouseEnter(object sender, EventArgs e) { helpStatus.Text = "open PPL rom, can only take .z64 file."; }
        private void treeViewTextures_MouseEnter(object sender, EventArgs e) { helpStatus.Text = "Texture management, right click for options (add, remove texture, etc...)"; }
        private void buttonLoadRom_MouseLeave(object sender, EventArgs e) { helpStatus.Text = ""; }
        private void buttonGetRomFolder_MouseLeave(object sender, EventArgs e) { helpStatus.Text = ""; }
        private void treeViewTextures_MouseLeave(object sender, EventArgs e) { helpStatus.Text = ""; }

        #endregion

        #region textures management
        private void treeViewTextures_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //small hack
            treeViewTextures.SelectedNode = e.Node;
        }
        private void buttonExtractAllTextures_Click(object sender, EventArgs e)
        {
            if (!bWDecompress.IsBusy)
                bWDecompress.RunWorkerAsync();
        }
        private void numericUpDownTextureDisplayTime_ValueChanged(object sender, EventArgs e)
        {
            if(treeViewTextures.SelectedNode.Level == 1)
                this.ressourceList.fibList[treeViewTextures.SelectedNode.Parent.Index].GetBFF2(treeViewTextures.SelectedNode.Index).SetTextureDisplayTime((byte)numericUpDownTextureDisplayTime.Value);

        }

        private void treeViewTextures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int level = treeViewTextures.SelectedNode.Level;
            if (level != 0)
            {
                labelIsTextureContainer.Hide();
                this.ressourceList.fibList[treeViewTextures.SelectedNode.Parent.Index].GetTexture(pictureBoxTexture, treeViewTextures.SelectedNode.Index);
                pictureBoxTexture.Show();
                numericUpDownTextureDisplayTime.Value = this.ressourceList.fibList[treeViewTextures.SelectedNode.Parent.Index].GetBFF2(treeViewTextures.SelectedNode.Index).GetTextureDisplayTime();
            }
            if (level == 0)
            {
                labelIsTextureContainer.Show();
                pictureBoxTexture.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBoxTexture.Hide();
            }
        }

        private void treeViewTextures_KeyDown(object sender, KeyEventArgs e)
        {
            if (treeViewTextures.SelectedNode.Level == 1)
                if (e.KeyCode == Keys.Delete && treeViewTextures.SelectedNode != null)
                {
                    ressourceList.fibList[treeViewTextures.SelectedNode.Parent.Index].RemoveBFF2Child(treeViewTextures.SelectedNode.Index);
                    treeViewTextures.SelectedNode.Remove();
                    UpdateFreeSpaceLeft();
                }
        }
        #endregion

        #region textures uncompressed
        private void treeViewTexturesUncompressed_AfterSelect(object sender, TreeViewEventArgs e)
        {

            try
            {
                pictureBoxUncompressedTexture.Image = romList.GetTexture(treeViewTexturesUncompressed.SelectedNode.Index);
            }
            catch
            {

            }
        }

        private void buttonUncompressedTextureReplace_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openTexture = new OpenFileDialog())
            {
                openTexture.RestoreDirectory = true;

                if (openTexture.ShowDialog() == DialogResult.OK)
                {
#if !DEBUG
                    try
                    {
#endif
                    Image img = Image.FromFile(openTexture.FileNames[0]);
                        
                    //load texture
                    pictureBoxUncompressedTexture.Width = img.Width;
                    pictureBoxUncompressedTexture.Height = img.Height;
                    pictureBoxUncompressedTexture.Image = img;

                    //convert texture to byte array for future treatment 
                    byte[] rawData = CTextureManager.ConvertRGBABitmapToByteArrayRGBA((Bitmap)pictureBoxUncompressedTexture.Image);

                    //make it at good format
                    byte[] palette;
                    var compression = Compression.trueColor16Bits;
                    if (romList.graphicsRom[treeViewTexturesUncompressed.SelectedNode.Index].bytePerPixel == 8)
                        compression = Compression.max256Colors;

                    //(palette, rawData) = CTextureManager.ConvertPixelsToGoodFormat(rawData, Compression.trueColor16Bits,true);
                    (palette, rawData) = CTextureManager.ConvertPixelsToGoodFormat(rawData, compression,true);
                    romList.SetTexture(treeViewTexturesUncompressed.SelectedNode.Index, rawData,palette);
#if !DEBUG
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unrecognized image format :( " + Environment.NewLine + "Error details : " + ex.Message, "Error loading texture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
#endif
                }
            }
        }

        private void buttonUncompressedTextureExtractAll_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < romList.GetGraphicsCount(); i++)
            {
                try
                {
                    romList.GetTexture(i).Save(CGeneric.pathExtractedTexture2 + romList.GetGraphicsName(i) + ".png");
                }
                catch
                {

                }
                
            }
            Process.Start(CGeneric.pathExtractedTexture2);
        }
        
        #endregion

        #region scenes

        #endregion


        private void UpdateFreeSpaceLeft()
        {
            if (extendedRom)
                freeSpaceLeft = CGeneric.romSizeExtended;
            else
                freeSpaceLeft = CGeneric.romSize;

            freeSpaceLeft -= ressourceList.indexRessourcesStart;
            freeSpaceLeft -= ressourceList.GetSizeOfAllRessourceList();
            if (audioList != null)//TODO To remove when ok
                freeSpaceLeft -= audioList.GetSizeOfAllAudio();

            labelFreeSpaceLeft.Text = freeSpaceLeft.ToString("### ### ### ###") + " bytes";
        }
        private void buttonGetRomFolder_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openRomFile = new OpenFileDialog())
            {
                openRomFile.Filter = "rom file (*.z64)|*.z64|iso file (*.iso)|*.iso";
                openRomFile.FilterIndex = 1;
                openRomFile.RestoreDirectory = true;

                if (openRomFile.ShowDialog() == DialogResult.OK)
                {
                    textBoxPPLLocation.Text = openRomFile.FileName;
                }
            }

        }

        private void buttonLoadRom_Click(object sender, EventArgs e)
        {
            if (textBoxPPLLocation.Text == "")
            {
                MessageBox.Show("Please select a file... if ROM in .z64 format only! or an ISO file", "N64 PPL Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
#if !DEBUG
            try
            {
#endif
            Byte[] buffRom = File.ReadAllBytes(textBoxPPLLocation.Text);
            if (buffRom.Length > CGeneric.romSize)
            {
                extendedRom = true;
                labelExtendedRom.Visible = true;
            }

            if (textBoxPPLLocation.Text.EndsWith("iso"))
            {
                Form1.ActiveForm.Text = "Panepon GC viewer";
                buttonModifyRom.Visible = false;
                label2.Visible = false;
                labelExtendedRom.Visible = false;
                labelFreeSpaceLeft.Visible = false;
                comboBoxRessourcesISO.Visible = true;
                comboBoxRessourcesISO.Text = "Ressources 1";
                N64Game = false;
            }
                

            if (N64Game) {
            //perform check verification (good game and good format) and register the lang
            var arrayZ64Format = CGeneric.GiveMeArray(buffRom, 0x20, 0x11);
#if !DEBUG
            int isGoodFormat = CGeneric.SearchBytesInArray(arrayZ64Format, CGeneric.patternPuzzleLeagueN64);
            if (isGoodFormat == -1)
                throw new Exception("The rom is not in the z64 format. Please convert it.");
#endif
            //grab rom langage
            RomLangAddress.romLang = (CGeneric.romLang)buffRom[0x3E];
            }
            //load graphics compressed / hvqm and sbf
            LoadRessourcesList(buffRom);

            if (N64Game)
            {
                //load the uncompressed texture
                this.romList = new UncompressedRomTexture(buffRom);

                //load the audio part
                LoadAudioList(buffRom);

                //load misc part
                //LoadMisc(buffRom);
            }
            LoadTreeView();
            UpdateFreeSpaceLeft();
            tabControlTexMovSce.Enabled = true;
            buttonModifyRom.Enabled = true;
            buttonLoadRom.Enabled = false;
            buttonGetRomFolder.Enabled = false;
            buttonLoadRom.Text = "ROM Loaded";
#if !DEBUG
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening rom..." + Environment.NewLine + "details : " + ex.Message, "PPL Rom management error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
#endif
        }

        private void LoadMisc(byte[] buffRom)
        {
            this.misc = new AssemblyReversedAddress(buffRom);
            this.MiscTrackBarDifficultyLevel.Value = misc.GetDifficultyLevel();

        }


        private void LoadAudioList(byte[] buffRom)
        {
            int indexAudioStart = CGeneric.SearchBytesInArray(buffRom, CGeneric.patternN64PtrTableV2);
            this.audioList = new AudioList(buffRom, indexAudioStart);

        }
        private void LoadTreeView()
        {
            //add treeview items' (BIF,HVQM and SBF)
            treeViewTextures.BeginUpdate();
            treeViewTexturesUncompressed.BeginUpdate();
            treeViewHVQM.BeginUpdate();
            treeViewSBF.BeginUpdate();
            treeViewAudio.BeginUpdate();
            treeViewTextures.Nodes.Clear();
            treeViewTexturesUncompressed.Nodes.Clear();
            treeViewHVQM.Nodes.Clear();
            treeViewSBF.Nodes.Clear();
            treeViewAudio.Nodes.Clear();



            for (int fib = 0; fib < this.ressourceList.GetFIBCount(); fib++)
            {
                treeViewTextures.Nodes.Add(fib + 1 + ", " + ressourceList.fibList[fib].GetFIBName());
                for (int bff = 0; bff < this.ressourceList.fibList[fib].GetBFFCount(); bff++)
                    treeViewTextures.Nodes[fib].Nodes.Add(bff + 1 + ", " + this.ressourceList.fibList[fib].GetBFFName(bff));
            }

            for (int i = 0; i < this.ressourceList.GetHVQMCount(); i++)
                treeViewHVQM.Nodes.Add(i + 1 + ", " + ressourceList.GetHVQM(i).GetRessourceName());

            for (int sbf = 0; sbf < this.ressourceList.GetSBFCount(); sbf++)
            {
                treeViewSBF.Nodes.Add(sbf + 1 + ", " + ressourceList.GetSBF1(sbf).GetRessourceName());

                for (int scene = 0; scene < this.ressourceList.GetSBF1(sbf).GetSceneCount(); scene++)
                    treeViewSBF.Nodes[sbf].Nodes.Add(scene + 1 + ", " + this.ressourceList.GetSBF1(sbf).GetScene(scene).GetSceneName());
            }

            if (N64Game) {
                for (int romGraphics = 0; romGraphics < this.romList.GetGraphicsCount(); romGraphics++)
                    treeViewTexturesUncompressed.Nodes.Add(this.romList.GetGraphicsName(romGraphics));

                for (int audioSoundbank = 0; audioSoundbank < this.audioList?.soundBankList.Count(); audioSoundbank++)
                    treeViewAudio.Nodes.Add("Soundbank " + Convert.ToString(audioSoundbank, 16).ToUpper());
            }
            CheckIfTreeViewEmpty(treeViewTextures);
            CheckIfTreeViewEmpty(treeViewTexturesUncompressed);
            CheckIfTreeViewEmpty(treeViewHVQM);
            CheckIfTreeViewEmpty(treeViewSBF);
            CheckIfTreeViewEmpty(treeViewAudio);

            treeViewTextures.EndUpdate();
            treeViewTexturesUncompressed.EndUpdate();
            treeViewHVQM.EndUpdate();
            treeViewSBF.EndUpdate();
            treeViewAudio.EndUpdate();
            

        }

        private void CheckIfTreeViewEmpty(TreeView tv)
        {
            if (tv.Nodes.Count > 0)
                tv.SelectedNode = tv.Nodes[0];
            else
                tv.Nodes.Add("No data were found :(");
        }

        private void LoadRessourcesList(byte[] buffRom)
        {
            int indexRessourcesArrayStart;
            int indexRessourcesEnd;

            if (N64Game)
            {
                // search for "ABRA.BIF" pattern (start of array ressources location)
                indexRessourcesArrayStart = CGeneric.SearchBytesInArray(buffRom, CGeneric.patternAbraBif) - 12;
                // search for "N64 PtrTablesV2" pattern (end of ressources location)
                indexRessourcesEnd = CGeneric.SearchBytesInArray(buffRom, CGeneric.patternN64WaveTable);
            }
            else
            {
                switch (comboBoxRessourcesISO.SelectedIndex)
                {
                    case 1: indexRessourcesArrayStart = CGeneric.IsoPart2; indexRessourcesEnd = CGeneric.IsoEnd2; break;
                    case 2: indexRessourcesArrayStart = CGeneric.IsoPart3; indexRessourcesEnd = CGeneric.IsoEnd3; break;
                    case 3: indexRessourcesArrayStart = CGeneric.IsoPart4; indexRessourcesEnd = CGeneric.IsoEnd4; break;
                    case 4: indexRessourcesArrayStart = CGeneric.IsoPart5; indexRessourcesEnd = CGeneric.IsoEnd5; break;
                    case 5: indexRessourcesArrayStart = CGeneric.IsoPart6; indexRessourcesEnd = CGeneric.IsoEnd6; break;
                    case 6: indexRessourcesArrayStart = CGeneric.IsoPart7; indexRessourcesEnd = CGeneric.IsoEnd7; break;
                    case 0:
                    default:
                        indexRessourcesArrayStart = CGeneric.IsoPart1; indexRessourcesEnd = CGeneric.IsoEnd1; break;
                }
            }

            //read header of table data
            Byte[] nbElementsTable = new Byte[4];
            Array.Copy(buffRom, indexRessourcesArrayStart, nbElementsTable, 0, nbElementsTable.Length);
            int nbElementsInTable = CGeneric.ConvertByteArrayToInt(nbElementsTable);


            //read data table for getting data information location and length
            Byte[] dataTable = new Byte[CGeneric.sizeOfElementTable * nbElementsInTable];
            Array.Copy(buffRom, indexRessourcesArrayStart + 4, dataTable, 0, dataTable.Length);

            //get array between load and end data
            Byte[] ressourcesData = new Byte[indexRessourcesEnd - indexRessourcesArrayStart - dataTable.Length - 4];
            Array.Copy(buffRom, indexRessourcesArrayStart + dataTable.Length + 4, ressourcesData, 0, ressourcesData.Length);


            //init the ressources list and data associated
            this.ressourceList = new CRessourceList(indexRessourcesArrayStart);
            this.ressourceList.Init(nbElementsInTable, dataTable, ressourcesData);
        }



        //decompress all texture task..
        private void bWDecompress_DoWork(object sender, DoWorkEventArgs e)
        {
            int index = 1;
            for (int i = 0; i < ressourceList.GetFIBCount(); i++)
            {
                for (int j = 0; j < ressourceList.fibList[i].GetBFFCount(); j++)
                {
                    try
                    {
                        index++;
                        this.ressourceList.fibList[i].SaveTexture(i, j);
                        bWDecompress.ReportProgress(index);
                    }
                    catch { }
                }
            }
        }

        private void bWDecompress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            buttonExtractAllTextures.Text = e.ProgressPercentage + "/" + ressourceList.GetTotalBFFCount();
        }
        private void bWDecompress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonExtractAllTextures.Text = "Extract all textures";
            Process.Start(CGeneric.pathExtractedTexture);
        }

        private void buttonModifyRom_Click(object sender, EventArgs e)
        {
            UpdateFreeSpaceLeft();
            if (freeSpaceLeft > 0)
            {
                WriteToRom();
            }
            else
            {
                MessageBox.Show("There is not enought space in the rom file..." + Environment.NewLine + "Please supress some things and try again.", "Error saving rom...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                buttonModifyRom.BackColor = Color.Orange;
                //if it's lower than 64Mb.
                if (freeSpaceLeft > -CGeneric.romSize)
                {

                    var res = MessageBox.Show("Do you want to make extended rom file ? (32 to 64Mb)", "Extend PPL rom ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        extendedRom = true;
                        labelExtendedRom.Visible = true;
                        WriteToRom();
                    }
                }
            }
        }

        private void WriteToRom()
        {
            FileStream fs = new FileStream(textBoxPPLLocation.Text, FileMode.Open, FileAccess.ReadWrite);
            romList.WriteToRom(fs);
            //misc.WriteToRom(fs);
            ressourceList.WriteAllData(fs);
            audioList.WriteAllData(fs);
            fillNullData(fs);
            fs.Close();
            buttonModifyRom.BackColor = Color.LimeGreen;
        }

        private void fillNullData(FileStream fs)
        {
            int romSize;
            if (extendedRom)
                romSize = CGeneric.romSizeExtended;
            else
                romSize = CGeneric.romSize;

            var fillArray = new Byte[romSize - fs.Position];
            for (int i = 0; i < fillArray.Length; i++)
            {
                fillArray[i] = 0xFF;
            }


            fs.Write(fillArray, 0, fillArray.Length);
        }


        private void treeViewSBF_AfterSelect(object sender, TreeViewEventArgs e)
        {
            numericUpDownSceneText.Value = 0;
            numericUpDownSceneTexture.Value = -1;

            if (treeViewSBF.SelectedNode.Level == 0)
            {
                groupBoxSceneText.Enabled = false;
                groupBoxSceneFontColor.Enabled = false;
                groupBoxSceneTextureManagement.Enabled = true;
                drawScene1.BackColor = Color.Gray;
                return;
            }
            CSBF1 sbf = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index);
            CSBF1Scene scene = sbf.GetScene(treeViewSBF.SelectedNode.Index);

            //list the texture (combobox) present in the scene
            comboBoxSceneChangeTexture.Items.Clear();
            for (int i = 0; i < sbf.GetBifList().Count(); i++)
                comboBoxSceneChangeTexture.Items.Add(sbf.GetBifName(i).ToLower().Replace(".bif", ""));

            //list all the textures present in the rom (pretty name)
            int indexData = 0;
            comboBoxSceneAddTexture.Items.Clear();
            for (int i = 0; i < this.ressourceList.fibList.Count; i++)
            {
                indexData = this.ressourceList.Get3FIBIndexWithFIBName(this.ressourceList.fibList[i].GetRessourceName());
                comboBoxSceneAddTexture.Items.Add(this.ressourceList.fibList[indexData].GetFIBName());
            }
                

            int nbTextObject = scene.GetTextObjectCount();
            if (nbTextObject == 0)
            {
                groupBoxSceneText.Text = "Text Edit (0 Text)";
                numericUpDownSceneText.Maximum = 0;
            }
            else
            {
                groupBoxSceneText.Text = "Text Edit (" + nbTextObject + " Text(s))";
                numericUpDownSceneText.Maximum = nbTextObject - 1;
            }

            int nbTextureObject = scene.GetTextureManagementCount();
            if (nbTextureObject == 0)
                numericUpDownSceneTexture.Maximum = 0;
            else
                numericUpDownSceneTexture.Maximum = nbTextureObject - 1;

            groupBoxSceneText.Enabled = true;
            groupBoxSceneFontColor.Enabled = true;
            groupBoxSceneTextureManagement.Enabled = true;
            launchSceneDisplay();

        }

        private void numericUpDownSceneText_ValueChanged(object sender, EventArgs e)
        {
            launchTextDisplayGroup();
        }

        private void launchSceneDisplay()
        {
            launchGraphicDisplayPart();
            launchTextDisplayGroup();
        }

        private void launchGraphicDisplayPart(int displaySpecificTexture = -1)
        {
            var sbf = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index);
            var scene = sbf.GetScene(treeViewSBF.SelectedNode.Index);
            var ListTextureName = sbf.GetBifList();

            int nbItem = scene.GetTextureManagementCount() - 1;

            
            drawScene1.Init();
            for (int i = 0; i <= nbItem; i++)
            {
                string textureInsideSbfName = ListTextureName[scene.GetTextureManagementObject(i).getTextureIndex()]; //select good texture
                int indexData = this.ressourceList.Get3FIBIndexWithFIBName(textureInsideSbfName);
                if (indexData != -1 && scene.GetTextureManagementObject(i).isCompressedTexture && (displaySpecificTexture == -1 || displaySpecificTexture == i))
                {
                    try
                    {
                        var bmp = this.ressourceList.fibList[indexData].GetBmpTexture(0);
                        var posY = scene.GetTextureManagementObject(i).posY;
                        var posX = scene.GetTextureManagementObject(i).posX;
                        this.drawScene1.AddBmp(bmp, new Point(posX, posY));
                    }
                    catch { }
                }
                if (displaySpecificTexture != -1)
                {
                    numericUpDownSceneTexturePosX.Value = scene.GetTextureManagementObject(displaySpecificTexture).posX;
                    numericUpDownSceneTexturePosY.Value = scene.GetTextureManagementObject(displaySpecificTexture).posY;
                }
            }
            this.drawScene1.Invalidate();
        }
        private void launchTextDisplayGroup()
        {
            //clear all textbox
            foreach (TextBox txtObj in txtBox)
                drawScene1.Controls.Remove(txtObj);
            txtBox.Clear();

            if (treeViewSBF.SelectedNode.Level != 1)
                return;

            //get scene
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);

            //get object count and verify > 0
            int nbTextObject = scene.GetTextObjectCount();

            if (nbTextObject > 0)
            {
                var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);

                //print text for editing
                textBoxSceneText.Text = textObject.GetText();

                //add text options (posX,posY, backColor, forecolor)
                numericUpDownSceneTextPosX.Value = textObject.GetPosX();
                numericUpDownSceneTextPosY.Value = textObject.GetPosY();
                buttonSceneBackColor.BackColor = textObject.BackColor;
                buttonSceneForeColor.BackColor = textObject.ForeColor;
                checkBoxSceneScrolling.Checked = textObject.isTextScrolling;
                checkBoxScenesForegroundText.Checked = textObject.isForegroundText;
                checkBoxSceneCentered.Checked = textObject.isCenteredText;
                checkBoxScenesExtra1.Checked = textObject.isExtraSize1;
                checkBoxScenesExtra3.Checked = textObject.isManualSpace;
                checkBoxScenesWaitInput.Checked = textObject.isWaitingInput;
                checkBoxScenesIsHidden.Checked = textObject.isHidden;
                checkBoxScenesExtra4.Checked = textObject.hasFontColor;

                if (textObject.isFontSmall)
                    comboBoxSceneFontSize.SelectedIndex = 0;
                else
                    if (textObject.isFontMedium)
                    comboBoxSceneFontSize.SelectedIndex = 1;
                else
                    comboBoxSceneFontSize.SelectedIndex = 2;

                //text object
                var sceneTxt = scene.GetTextObjectGroup(textObject.group);

                int index = 0;
                foreach (CSBF1TextObject txtObj in sceneTxt)
                {
                    var txtBoxTmp = new TextBox();
                    txtBoxTmp.Multiline = true;
                    txtBoxTmp.ReadOnly = true;
                    txtBoxTmp.BorderStyle = BorderStyle.None;
                    txtBox.Add(txtBoxTmp);

                    txtBox[index].Text = txtObj.GetText();
                    txtBox[index].Top = txtObj.GetPosY();
                    txtBox[index].Left = txtObj.GetPosX();

                    Size size = TextRenderer.MeasureText(txtBox[index].Text, txtBox[index].Font);
                    txtBox[index].ClientSize = new Size(size.Width, size.Height);

                    if (textBoxSceneText.Text == txtBox[index].Text && sceneTxt.Count > 1)
                        txtBox[index].BackColor = Color.LightGreen;

                    drawScene1.Controls.Add(txtBox[index]);
                    index++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            Byte[] buffRom = File.ReadAllBytes(textBoxPPLLocation.Text);
            List<string> list = new List<string>();

            int val1 = Convert.ToInt32(textBox2.Text, 16);
            int val2 = Convert.ToInt32(textBox3.Text, 16);

            for (int i = 0; i < 10208296; i += 4)
            {
                int value = BitConverter.ToInt32(new[] { buffRom[i + 3], buffRom[i + 2], buffRom[i + 1], buffRom[i] }, 0);
                if (value >= val1 && value <= val2)
                {
                    if (value % 4 == 0)
                        list.Add(Convert.ToString(i, 16));
                }
            }
            foreach (string str in list)
            {
                textBox1.AppendText(str);
                textBox1.AppendText(Environment.NewLine);
            }

        }


        private void buttonHvqmPathOpen_Click(object sender, EventArgs e)
        {
            Process.Start(CGeneric.pathOtherContent);
        }

        private void numericUpDownScenePosX_ValueChanged(object sender, EventArgs e)
        {
            var textObj = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value);
            textObj.SetPosX(Convert.ToInt32(numericUpDownSceneTextPosX.Value));
            launchTextDisplayGroup();
        }
        private void numericUpDownScenePosY_ValueChanged(object sender, EventArgs e)
        {
            var textObj = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value);
            textObj.SetPosY(Convert.ToInt32(numericUpDownSceneTextPosY.Value));
            launchTextDisplayGroup();
        }



        private void numericUpDownGropuText_ValueChanged(object sender, EventArgs e)
        {
            //launchTextDisplayGroup((int)numericUpDownGroupText.Value);
        }

        private void textBoxSceneText_TextChanged(object sender, EventArgs e)
        {
            if (treeViewSBF.SelectedNode.Level == 1)
                this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value).SetText(textBoxSceneText.Text);
        }

        private void buttonSceneForeColor_Click(object sender, EventArgs e)
        {
            var textObj = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value);
            colorDialog1.Color = textObj.ForeColor;
            colorDialog1.CustomColors = new int[] { 0xb17941, 0xffaf55, 0xef9542, 0xd9be8d, 0x36cfff, 0x4254ef, 0xc955ff, 0xef42bb, 0xffce65, 0xf7b982, 0xf18379, 0x66ff83, 0x84dffc, 0x8085f3, 0xd176ff, 0xc074b9, 0x57ccd6 };

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {

                textObj.ForeColor = colorDialog1.Color;
                buttonSceneForeColor.BackColor = textObj.ForeColor;
                //launchTextDisplayText();
            }

        }

        private void buttonSceneBackColor_Click(object sender, EventArgs e)
        {
            var textObj = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value);
            colorDialog1.Color = textObj.BackColor;
            colorDialog1.CustomColors = new int[] { 0xe4b88d, 0xe4b88d, 0xbc5746, 0x6d128, 0xb42ff0, 0x3362f, 0x431c19, 0 };

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textObj.BackColor = colorDialog1.Color;
                buttonSceneBackColor.BackColor = textObj.BackColor;
                //launchTextDisplayText();
            }
        }

        private void buttonSceneSuppressText_Click(object sender, EventArgs e)
        {
            var scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            if (scene.GetTextObjectCount() > 0)
            {
                scene.RemoveText((int)numericUpDownSceneText.Value);
                if (numericUpDownSceneText.Value > 0)
                    numericUpDownSceneText.Maximum -= 1;
                else
                    numericUpDownSceneText.Maximum = 0;
            }
        }

        private void buttonHVQMExtract_Click(object sender, EventArgs e)
        {
            foreach (CHVQM video in ressourceList.hvqmList)
            {
                var fileName = video.GetRessourceName().Replace("\0", "");
                FileStream file = new FileStream(CGeneric.pathOtherContent + fileName, FileMode.Create);
                file.Write(video.GetRawData(), 0, video.GetRawData().Length);
                file.Close();
            }
            Process.Start(CGeneric.pathOtherContent);
        }

        private void buttonHVQMReplace_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openHvqm = new OpenFileDialog())
            {
                openHvqm.InitialDirectory = Application.StartupPath;
                openHvqm.Filter = "hvqm file (*.hvqm)|*.hvqm";
                openHvqm.FilterIndex = 1;
                openHvqm.RestoreDirectory = true;

                if (openHvqm.ShowDialog() == DialogResult.OK)
                {
                    Byte[] buffRom = File.ReadAllBytes(openHvqm.FileName);
                    this.ressourceList.hvqmList[treeViewHVQM.SelectedNode.Index].SetRawData(buffRom);
                    UpdateFreeSpaceLeft();
                }
            }
        }

        private void buttonHVQMRemove_Click(object sender, EventArgs e)
        {
            this.ressourceList.hvqmList.RemoveAt(treeViewHVQM.SelectedNode.Index);
            treeViewHVQM.Nodes.RemoveAt(treeViewHVQM.SelectedNode.Index);
            UpdateFreeSpaceLeft();
        }

        private void button4_Click(object sender, EventArgs e)
        {
#if DEBUG
            int sbfIndex = 0;
            foreach (CSBF1 sbf in ressourceList.sbfList)
            {

                foreach (CSBF1Scene scene in sbf.scenesList)
                {

                    //foreach (CSBF1TextureManagement txt in scene.textureManagementObjectList)
                    //{
                    //    textBox1.AppendText(sbfIndex.ToString());
                    //    textBox1.AppendText("[" + scene.GetSceneName() + "[");
                    //    //textBox1.AppendText(txt. + "[");
                    //    textBox1.AppendText(BitConverter.ToString(txt.GetRawData()).Replace("-", " "));

                    //    textBox1.AppendText(Environment.NewLine);
                    //}
                }
                sbfIndex++;
            }
#endif
        }

        private void toolStripMenuItemReplaceAudioAllSoundBank_Click(object sender, EventArgs e)
        {
            byte[] PtrTable = ReadAudioPtrTable();
            byte[] WaveTable = ReadAudioWaveTable();
            byte[] SfxTable = ReadAudioSfxTable();
            int oldAllLength =
                audioList.soundBankList[treeViewAudio.SelectedNode.Index].ptrTable.Length +
                audioList.soundBankList[treeViewAudio.SelectedNode.Index].waveTable.Length +
                audioList.soundBankList[treeViewAudio.SelectedNode.Index].sfx.Length;


            if (PtrTable != null && WaveTable != null && SfxTable != null)
            {
                int newAllLength = PtrTable.Length + WaveTable.Length + SfxTable.Length;
                if (oldAllLength <= newAllLength || (newAllLength - oldAllLength) < freeSpaceLeft)
                    audioList.ReplaceSoundBank(PtrTable, WaveTable, SfxTable, treeViewAudio.SelectedNode.Index);
                else
                    MessageBox.Show("Not enought space in the rom.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UpdateFreeSpaceLeft();
            }
            else
                MessageBox.Show("Cancelled operation, No modification were made.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private byte[] ReadAudioPtrTable()
        {
            byte[] PtrTable = null;
            using (OpenFileDialog openPointerTable = new OpenFileDialog())
            {
                openPointerTable.Filter = "ptr file (*.ptr)|*.ptr";
                openPointerTable.FilterIndex = 1;
                openPointerTable.RestoreDirectory = true;

                if (openPointerTable.ShowDialog() == DialogResult.OK)
                {
                    PtrTable = File.ReadAllBytes(openPointerTable.FileName);
                }
            }
            return PtrTable;
        }

        private byte[] ReadAudioWaveTable()
        {
            byte[] waveTable = null;
            using (OpenFileDialog openWaveTable = new OpenFileDialog())
            {
                openWaveTable.Filter = "wbk file (*.wbk)|*.wbk";
                openWaveTable.FilterIndex = 1;
                openWaveTable.RestoreDirectory = true;

                if (openWaveTable.ShowDialog() == DialogResult.OK)
                {
                    waveTable = File.ReadAllBytes(openWaveTable.FileName);
                }
            }
            return waveTable;
        }

        private byte[] ReadAudioSfxTable()
        {
            byte[] SfxTable = null;
            using (OpenFileDialog openSfxTable = new OpenFileDialog())
            {
                openSfxTable.Filter = "sfx file (*.bfx)|*.bfx";
                openSfxTable.FilterIndex = 1;
                openSfxTable.RestoreDirectory = true;

                if (openSfxTable.ShowDialog() == DialogResult.OK)
                {
                    SfxTable = File.ReadAllBytes(openSfxTable.FileName);
                }
            }
            return SfxTable;
        }

        private void toolStripMenuItemReplacePointerTable_Click(object sender, EventArgs e)
        {
            byte[] newPtrTable = ReadAudioPtrTable();
            int oldPtrTableLength = audioList.soundBankList[treeViewAudio.SelectedNode.Index].ptrTable.Length;

            if (newPtrTable != null)
            {
                if (newPtrTable.Length <= oldPtrTableLength || (newPtrTable.Length - oldPtrTableLength) < freeSpaceLeft)
                {
                    audioList.ReplacePointerTable(newPtrTable, treeViewAudio.SelectedNode.Index);
                    UpdateFreeSpaceLeft();
                }
                else
                    MessageBox.Show("Not enought space in the rom.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
                MessageBox.Show("Cancelled operation, No modification were made.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItemReplaceWaveTable_Click(object sender, EventArgs e)
        {
            byte[] newWaveTable = ReadAudioWaveTable();
            int oldWaveTableLength = audioList.soundBankList[treeViewAudio.SelectedNode.Index].waveTable.Length;

            if (newWaveTable != null)
            {
                if (newWaveTable.Length <= oldWaveTableLength || (newWaveTable.Length - oldWaveTableLength) < freeSpaceLeft)
                {
                    audioList.ReplaceWaveTable(newWaveTable, treeViewAudio.SelectedNode.Index);
                    UpdateFreeSpaceLeft();
                }
                else
                    MessageBox.Show("Not enought space in the rom.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Cancelled operation, No modification were made.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItemReplaceSfxTable_Click(object sender, EventArgs e)
        {
            byte[] newSfxTable = ReadAudioSfxTable();
            int oldSfxLength = audioList.soundBankList[treeViewAudio.SelectedNode.Index].sfx.Length;

            if (newSfxTable != null)
            {
                if (newSfxTable.Length <= oldSfxLength || (newSfxTable.Length - oldSfxLength) < freeSpaceLeft)
                {
                    audioList.ReplaceSfxTable(newSfxTable, treeViewAudio.SelectedNode.Index);
                    UpdateFreeSpaceLeft();
                }
                else
                    MessageBox.Show("Not enought space in the rom.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Cancelled operation, No modification were made.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void contextMenuStripAudioTreeview_Opening(object sender, CancelEventArgs e)
        {
            toolStripMenuItemReplaceAudioAllSoundBank.Enabled = true;
            toolStripMenuItemReplacePointerTable.Enabled = true;
            toolStripMenuItemReplaceSfxTable.Enabled = true;
            toolStripMenuItemReplaceWaveTable.Enabled = true;

            switch (treeViewAudio.SelectedNode?.Index)
            {
                case 0:
                    toolStripMenuItemReplaceAudioAllSoundBank.Enabled = false;
                    toolStripMenuItemReplacePointerTable.Enabled = false;
                    toolStripMenuItemReplaceSfxTable.Enabled = false;
                    toolStripMenuItemReplaceWaveTable.Enabled = false;
                    break;
                case 2:
                    toolStripMenuItemReplaceSfxTable.Enabled = false;
                    break;
            }
        }

        private void treeViewAudio_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //small hack
            treeViewAudio.SelectedNode = e.Node;
        }

        private void textBoxSceneText_Leave(object sender, EventArgs e)
        {
            UpdateFreeSpaceLeft();
        }

        private void numericUpDownScenePosX_Leave(object sender, EventArgs e)
        {

        }

        private void checkBoxSceneTextScrolling_CheckedChanged(object sender, EventArgs e)
        {
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.isTextScrolling = checkBoxSceneScrolling.Checked;
        }

        private void checkBoxSceneCentered_CheckedChanged(object sender, EventArgs e)
        {
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.isCenteredText = checkBoxSceneCentered.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textBox1.Clear();
            //foreach(CSBF1 sceneList in ressourceList.sbfList)
            //{
            //    foreach (CSBF1Scene scene in sceneList.scenesList)
            //    {
            //        foreach(CSBF1TextObject txt in scene.textObjectList)
            //        {
            //            textBox1.AppendText("0x"+txt.BackColor.B.ToString("x"));
            //            textBox1.AppendText(txt.BackColor.G.ToString("x"));
            //            textBox1.AppendText(txt.BackColor.R.ToString("x"));
            //            textBox1.AppendText(Environment.NewLine);

            //        }
            //    }
            //}
        }

        private void buttonScenesAddText_Click(object sender, EventArgs e)
        {
            ressourceList.sbfList[treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index].AddNewTextObject(radioButtonScenesNewScene.Checked);
            numericUpDownSceneText.Maximum += 1;
            numericUpDownSceneText.Value = numericUpDownSceneText.Maximum;
            groupBoxSceneText.Text = "Text Edit (" + Convert.ToInt32(numericUpDownSceneText.Value + 1) + " Text(s))";
            textBoxSceneText.Text = "(set new text here)";
            UpdateFreeSpaceLeft();
        }

        private void comboBoxSceneFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);

            switch (comboBoxSceneFontSize.SelectedIndex)
            {
                case 0: // small
                    textObject.isFontSmall = true;
                    break;
                case 1: // medium
                    textObject.isFontSmall = false;
                    textObject.isFontMedium = true;
                    break;
                case 2: // big
                    textObject.isFontSmall = false;
                    textObject.isFontMedium = false;
                    break;
            }
        }

        private void checkBoxScenesExtra1_CheckedChanged(object sender, EventArgs e)
        {
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.isExtraSize1 = checkBoxScenesExtra1.Checked;
        }

        private void checkBoxScenesExtra3_CheckedChanged(object sender, EventArgs e)
        {
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.isManualSpace = checkBoxScenesExtra3.Checked;
        }

        private void checkBoxScenesWaitInput_CheckedChanged(object sender, EventArgs e)
        {
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.isWaitingInput = checkBoxScenesWaitInput.Checked;
        }

        private void checkBoxScenesForegroundText_CheckedChanged(object sender, EventArgs e)
        {
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.isForegroundText = checkBoxScenesForegroundText.Checked;
        }

        private void checkBoxScenesIsHidden_CheckedChanged(object sender, EventArgs e)
        {
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.isHidden = checkBoxScenesIsHidden.Checked;

        }

        private void MiscTrackBarDifficultyLevel_Scroll(object sender, EventArgs e)
        {
            this.MisclabelDifficultyLevel.Text = this.MiscTrackBarDifficultyLevel.Value.ToString();
            if (this.MiscTrackBarDifficultyLevel.Value == 0)
                this.MisclabelDifficultyLevel.ForeColor = Color.Black;
            else if (this.MiscTrackBarDifficultyLevel.Value < 0)
                this.MisclabelDifficultyLevel.ForeColor = Color.FromArgb(255, 0, Math.Abs(this.MiscTrackBarDifficultyLevel.Value) * 35, 0);
            else
                this.MisclabelDifficultyLevel.ForeColor = Color.FromArgb(255, this.MiscTrackBarDifficultyLevel.Value * 35, 0,0);

            misc.SetDifficulty(-this.MiscTrackBarDifficultyLevel.Value);

        }

        private void checkBoxScenesExtra4_CheckedChanged(object sender, EventArgs e)
        {
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            var textObject = scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.hasFontColor = checkBoxScenesExtra4.Checked;
        }

        private void numericUpDownSceneTexture_ValueChanged(object sender, EventArgs e)
        {
            if (treeViewSBF.SelectedNode.Level == 0)
                return;
            launchGraphicDisplayPart((int)numericUpDownSceneTexture.Value);
            if((int)numericUpDownSceneTexture.Value == -1)
            {
                numericUpDownSceneTexturePosX.Enabled = false;
                numericUpDownSceneTexturePosY.Enabled = false;
                comboBoxSceneChangeTexture.SelectedIndex = -1;
            }
            else
            {
                //set the name for the combobox when changing index.
                CSBF1 sbf1 = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index);
                CSBF1Scene scene = sbf1.GetScene(treeViewSBF.SelectedNode.Index);
                //grab index of the selected texture
                int indexTextureSbf = scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value).getTextureIndex();
                comboBoxSceneChangeTexture.SelectedIndex = indexTextureSbf;
               


                numericUpDownSceneTexturePosX.Enabled = true;
                numericUpDownSceneTexturePosY.Enabled = true;
            }
            

        }

        private void buttonScenesTextureAdd_Click(object sender, EventArgs e)
        {
            CSBF1 sbf1 = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index);
            string fibName = this.ressourceList.fibList[comboBoxSceneAddTexture.SelectedIndex].GetRessourceName();

            sbf1.AddBifToSbf(fibName);

            //update the texture (combobox) present in the scene
            comboBoxSceneChangeTexture.Items.Clear();
            for (int i = 0; i < sbf1.GetBifList().Count(); i++)
                comboBoxSceneChangeTexture.Items.Add(sbf1.GetBifName(i).ToLower().Replace(".bif", ""));


        }

        private void numericUpDownSceneTexturePosX_ValueChanged(object sender, EventArgs e)
        {
            if (treeViewSBF.SelectedNode.Level == 0)
                return;
            var scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value).posX = (int)numericUpDownSceneTexturePosX.Value;
            launchGraphicDisplayPart((int)numericUpDownSceneTexture.Value);
        }

        private void numericUpDownSceneTexturePosY_ValueChanged(object sender, EventArgs e)
        {
            if (treeViewSBF.SelectedNode.Level == 0)
                return;

            var scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value).posY = (int)numericUpDownSceneTexturePosY.Value;
            launchGraphicDisplayPart((int)numericUpDownSceneTexture.Value);
        }

        private void buttonScenesTextureReplace_Click(object sender, EventArgs e)
        {
            byte indexNewTexture = (byte)comboBoxSceneChangeTexture.SelectedIndex;
            CSBF1Scene scene = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index);
            scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value).textureIndex = indexNewTexture;
            launchGraphicDisplayPart((int)numericUpDownSceneTexture.Value);
        }

        private void saveSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sbf = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Index);

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "sbf files (*.sbf)|*.sbf";
            saveFileDialog1.FileName = sbf.GetRessourceName();
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    
                    var b = sbf.GetRawData();
                    myStream.Write(b,0,b.Length);
                    myStream.Close();
                }
            }
           
        }

        private void contextMenuStripScenesTreeView_Opening(object sender, CancelEventArgs e)
        {
            if (treeViewSBF.SelectedNode.Level == 0)
            {
                ToolStripMenuItemLoadSBF.Enabled = true;
                ToolStripMenuItemSaveSBF.Enabled = true;

            }
            else
            {
                ToolStripMenuItemLoadSBF.Enabled = false;
                ToolStripMenuItemSaveSBF.Enabled = false;
            }
                
        }

        private void treeViewSBF_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //small hack
            treeViewSBF.SelectedNode = e.Node;
        }

        private void ToolStripMenuItemLoadSBF_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openSbfFile = new OpenFileDialog())
            {
                openSbfFile.Filter = "sbf file (*.sbf)|*.sbf";
                openSbfFile.FilterIndex = 1;
                openSbfFile.RestoreDirectory = true;

                if (openSbfFile.ShowDialog() == DialogResult.OK)
                {
                    Byte[] buffRom = File.ReadAllBytes(openSbfFile.FileName);
                    this.ressourceList.SetSBF1(buffRom, treeViewSBF.SelectedNode.Index);
                }
            }
            LoadTreeView();
            UpdateFreeSpaceLeft();
        }

        private void ExtractBinaryTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            C3FIB fib;
            if (treeViewTextures.SelectedNode.Level == 0)
                fib = this.ressourceList.fibList[treeViewTextures.SelectedNode.Index];
            else
                fib = this.ressourceList.fibList[treeViewTextures.SelectedNode.Parent.Index];

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "3fib files (*.3fib)|*.3fib";
            saveFileDialog1.FileName = fib.GetFIBName();
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    var b = fib.GetRawData();
                    myStream.Write(b, 0, b.Length);
                    myStream.Close();
                }
            }
        }

        private void comboBoxRessourcesISO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (N64Game)
                return;
            Byte[] buffRom = File.ReadAllBytes(textBoxPPLLocation.Text);
            LoadRessourcesList(buffRom);

            LoadTreeView();
            UpdateFreeSpaceLeft();
            tabControlTexMovSce.Enabled = true;
            buttonModifyRom.Enabled = true;
            buttonLoadRom.Enabled = false;
            buttonGetRomFolder.Enabled = false;
            buttonLoadRom.Text = "ROM Loaded";
            buffRom = new byte[0];
        }

        private void CreateNewContainertoolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nameContainer = "New container";
            if (InputBox("Please set name", "Name for new container : ", ref nameContainer) == DialogResult.OK)
            {
                this.ressourceList.Add3FIB(nameContainer);
                treeViewTextures.Nodes.Add((treeViewTextures.Nodes.Count+1) + ", " + nameContainer);

                //visual, show where the data is added and select it
                treeViewTextures.Nodes[treeViewTextures.Nodes.Count-1].EnsureVisible();
                treeViewTextures.SelectedNode = treeViewTextures.Nodes[treeViewTextures.Nodes.Count - 1];
            }
            
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            textBox.MaxLength = 12;
            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
