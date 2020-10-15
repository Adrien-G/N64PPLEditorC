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

namespace N64PPLEditorC
{

    public partial class Form1 : Form
    {
        CRessourceList ressourceList;
        UncompressedRomTexture romList;
        AudioList audioList;
        int freeSpaceLeft = 0;

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
            if(textBoxPPLLocation.Text == "")
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
                                    MessageBox.Show("There is not enought place...", "Free space in rom is needed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            pictureBoxUncompressedTexture.Image = romList.GetTexture(treeViewTexturesUncompressed.SelectedNode.Index);
        }

        private void buttonUncompressedTextureReplace_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openTexture = new OpenFileDialog())
            {
                openTexture.RestoreDirectory = true;

                if (openTexture.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(openTexture.FileNames.ToString());
                        if (img.Width <= 320 && img.Height <= 240)
                        {
                            //load texture
                            pictureBoxUncompressedTexture.Width = img.Width;
                            pictureBoxUncompressedTexture.Height = img.Height;
                            pictureBoxUncompressedTexture.Image = img;

                            //convert texture to byte array for future treatment 
                            byte[] rawData = CTextureManager.ConvertRGBABitmapToByteArrayRGBA((Bitmap)pictureBoxTexture.Image);

                            //make it at good format
                            byte[] palette;
                            (palette, rawData) = CTextureManager.ConvertPixelsToGoodFormat(rawData, Compression.trueColor16Bits);

                            romList.SetTexture(treeViewTexturesUncompressed.SelectedNode.Index, rawData);
                        }
                        else
                            MessageBox.Show("Texture must be at maximum of size 320x240 !", "Size too big...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unrecognized image format :( " + Environment.NewLine + "Error details : " + ex.Message, "Error loading texture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonUncompressedTextureExtractAll_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region scenes
        private void buttonScenesAddText_Click(object sender, EventArgs e)
        {
            ressourceList.sbfList[treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index].AddNewTextObject(true);
            numericUpDownSceneText.Maximum += 1;
            numericUpDownSceneText.Value = numericUpDownSceneText.Maximum;
            groupBoxSceneText.Text = "Text Edit (" + Convert.ToInt32(numericUpDownSceneText.Value+1) + " Text(s))";
            textBoxSceneText.Text = "(set new text here)";
            UpdateFreeSpaceLeft();
        }

        private void buttonScenesAddTextAtEnd_Click(object sender, EventArgs e)
        {
            ressourceList.sbfList[treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index].AddNewTextObject(false);
            numericUpDownSceneText.Maximum += 1;
            numericUpDownSceneText.Value = numericUpDownSceneText.Maximum;
            groupBoxSceneText.Text = "Text Edit (" + Convert.ToInt32(numericUpDownSceneText.Value+1) + " Text(s))";
            textBoxSceneText.Text = "(set new text here)";
            UpdateFreeSpaceLeft();
        }
        #endregion


        private void UpdateFreeSpaceLeft()
        {
            freeSpaceLeft = CGeneric.romSize;
            freeSpaceLeft -= ressourceList.indexRessourcesStart;
            freeSpaceLeft -= ressourceList.GetSizeOfAllRessourceList();
            freeSpaceLeft -= audioList.GetSizeOfAllAudio();

            labelFreeSpaceLeft.Text = freeSpaceLeft.ToString("### ### ### ###") + " bytes";
        }
        private void buttonGetRomFolder_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openRomFile = new OpenFileDialog())
            {
                openRomFile.InitialDirectory = Application.StartupPath;
                openRomFile.Filter = "rom file (*.z64)|*.z64";
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
            if(textBoxPPLLocation.Text == "")
            {
                MessageBox.Show("Please select a file... (in z64 format only)", "N64 PPL Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //try
                //{
                Byte[] buffRom = File.ReadAllBytes(textBoxPPLLocation.Text);

                //perform check verification (good game and good format) and register the lang
                var arrayZ64Format = CGeneric.GiveMeArray(buffRom, 0x20, 0x11);
                int isGoodFormat = CGeneric.SearchBytesInArray(arrayZ64Format, CGeneric.patternPuzzleLeagueN64);
                if(isGoodFormat == -1)
                        throw new Exception("The rom is not in the z64 format. Please convert it.");

                //grab rom langage
                RomLangAddress.romLang = (CGeneric.romLang)buffRom[0x3E];

                //load graphics compressed / hvqm and sbf
                LoadRessourcesList(buffRom);

                
                //load the uncompressed texture
                this.romList = new UncompressedRomTexture(buffRom);

                //load the audio part
                try
                {
                    LoadAudioList(buffRom);

                }
                catch (Exception)
                {
                    //just because only french version is implemented yet..
                }
                
                
                LoadTreeView();
                UpdateFreeSpaceLeft();
                tabControlTexMovSce.Enabled = true;
                buttonModifyRom.Enabled = true;
                buttonLoadRom.Enabled = false;
                buttonGetRomFolder.Enabled = false;
                buttonLoadRom.Text = "ROM Loaded";
                //}
                //catch (Exception ex)
                //{
                //MessageBox.Show("Error opening rom..." + Environment.NewLine + "details : " + ex.Message, "PPL Rom management error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
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
            for (int fib = 0; fib < this.ressourceList.GetFIBCount(); fib++)
            {
                treeViewTextures.Nodes.Add(fib + 1 + ", " + ressourceList.fibList[fib].GetFIBName());
                for (int bff = 0; bff < this.ressourceList.fibList[fib].GetBFFCount(); bff++)
                    treeViewTextures.Nodes[fib].Nodes.Add(bff + 1 +", " + this.ressourceList.fibList[fib].GetBFFName(bff));
            }

            for (int i = 0; i < this.ressourceList.GetHVQMCount(); i++)
                treeViewHVQM.Nodes.Add(i + 1 + ", " + ressourceList.GetHVQM(i).GetRessourceName());

            for (int sbf = 0; sbf < this.ressourceList.GetSBFCount(); sbf++) 
            {
                treeViewSBF.Nodes.Add(sbf + 1 + ", " + ressourceList.GetSBF1(sbf).GetRessourceName());

                for (int scene = 0; scene < this.ressourceList.GetSBF1(sbf).GetSceneCount(); scene++)
                    treeViewSBF.Nodes[sbf].Nodes.Add(scene + 1 + ", " + this.ressourceList.GetSBF1(sbf).GetScene(scene).GetSceneName());
            }

            for (int romGraphics = 0; romGraphics < this.romList.GetGraphicsCount(); romGraphics++)
                treeViewTexturesUncompressed.Nodes.Add(this.romList.GetGraphicsName(romGraphics));

            for (int audioSoundbank = 0; audioSoundbank < this.audioList?.soundBankList.Count(); audioSoundbank++)
                treeViewAudio.Nodes.Add("Soundbank " + Convert.ToString(audioSoundbank, 16).ToUpper());

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

            // search for "ABRA.BIF" pattern (start of array ressources location)
            int indexRessourcesArrayStart = CGeneric.SearchBytesInArray(buffRom, CGeneric.patternAbraBif) - 12;

            // search for "N64 PtrTablesV2" pattern (end of ressources location)
            int indexRessourcesEnd = CGeneric.SearchBytesInArray(buffRom, CGeneric.patternN64WaveTable);

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
            this.ressourceList = new CRessourceList(indexRessourcesArrayStart,indexRessourcesEnd);
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
            if(freeSpaceLeft < 0)
            {
                MessageBox.Show("There is not enought space in the rom file..."  + Environment.NewLine + "Please supress some things and try again.", "Error saving rom...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                buttonModifyRom.BackColor = Color.Orange;
            }
            else
            {
                FileStream fs = new FileStream(textBoxPPLLocation.Text, FileMode.Open, FileAccess.Write);
                //TODO add uncompressed image management
                ressourceList.WriteAllData(fs);
                audioList.WriteAllData(fs);
                fs.Close();
                buttonModifyRom.BackColor = Color.LimeGreen;
            }
        }




        private void treeViewSBF_AfterSelect(object sender, TreeViewEventArgs e)
        {
            numericUpDownSceneText.Value = 0;
            if (treeViewSBF.SelectedNode.Level == 1)
            {
                int nbTextObject = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObjectCount();
                if(nbTextObject == 0)
                {
                    groupBoxSceneText.Text = "Text Edit (0 Text)";
                    numericUpDownSceneText.Maximum = 0;
                }
                else
                {
                    groupBoxSceneText.Text = "Text Edit (" + nbTextObject + " Text(s))";
                    numericUpDownSceneText.Maximum = nbTextObject-1;
                }
                launchSceneDisplay();
            }
        }

        private void numericUpDownSceneText_ValueChanged(object sender, EventArgs e)
        {
            launchTextDisplayText();
        }

        private void launchSceneDisplay()
        {
            launchGraphicDisplayPart();
            launchTextDisplayGroup(0);
            launchTextDisplayText();
        }

        private void launchTextDisplayText() {
            if (treeViewSBF.SelectedNode.Level == 1)
            {
                int nbTextObject = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObjectCount();
                if(nbTextObject > 0) 
                {
                    var textObj = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value);
                    textBoxSceneText.Text = textObj.GetText();

                    numericUpDownScenePosX.Value = textObj.GetPosX();
                    numericUpDownScenePosY.Value = textObj.GetPosY();
                    buttonSceneBackColor.BackColor = textObj.BackColor;
                    buttonSceneForeColor.BackColor = textObj.ForeColor;
                }
            }
        }

        private void launchGraphicDisplayPart()
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
                if (indexData != -1 && scene.GetTextureManagementObject(i).isCompressedTexture)
                {
                    try
                    {
                        var bmp = this.ressourceList.fibList[indexData].GetBmpTexture(0);
                        var posY = scene.GetTextureManagementObject(i).getYLocation();
                        var posX = scene.GetTextureManagementObject(i).getXLocation();
                        this.drawScene1.AddBmp(bmp, new Point(posX, posY));

                    }
                    catch { }
                }
            }
            this.drawScene1.Invalidate();
        }
        private void launchTextDisplayGroup(int groupIndex) { 
            //text object
            int nbTextObject = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObjectCount();
            var sceneTxt = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObjectGroup(groupIndex);
            numericUpDownGroupText.Maximum = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).nbTextGroupObject;
            
            //clear all textbox
            foreach (TextBox txtObj in txtBox)
                drawScene1.Controls.Remove(txtObj);
            txtBox.Clear();
            
            if (nbTextObject > 0)
            {
                int index = 0;
                foreach(CSBF1TextObject txtObj in sceneTxt)
                {
                    var txtBoxTmp = new TextBox();
                    txtBoxTmp.Multiline = true;
                    txtBoxTmp.ReadOnly = true;
                    txtBoxTmp.BorderStyle = BorderStyle.None;
                    txtBox.Add(txtBoxTmp);
                    drawScene1.Controls.Add(txtBox[index]);
                    
                    txtBox[index].Text = txtObj.GetText();
                    txtBox[index].Top = txtObj.GetPosY();
                    txtBox[index].Left = txtObj.GetPosX();

                    Size size = TextRenderer.MeasureText(txtBox[index].Text, txtBox[index].Font);
                    txtBox[index].ClientSize = new Size(size.Width, size.Height);
                    txtBox[index].Show();
                    txtBox[index].BringToFront();
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
                int value = BitConverter.ToInt32(new[] { buffRom[i+3], buffRom[i+2], buffRom[i+1], buffRom[i]},0);
                if(value  >= val1 && value <= val2)
                {
                    if (value %4 == 0)
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
            textObj.SetPosX(Convert.ToInt32(numericUpDownScenePosX.Value));
        }
        private void numericUpDownScenePosY_ValueChanged(object sender, EventArgs e)
        {
            var textObj = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value);
            textObj.SetPosY(Convert.ToInt32(numericUpDownScenePosY.Value));
        }



        private void numericUpDownGropuText_ValueChanged(object sender, EventArgs e)
        {
            launchTextDisplayGroup((int)numericUpDownGroupText.Value);
        }

        private void textBoxSceneText_TextChanged(object sender, EventArgs e)
        {
            this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value).SetText(textBoxSceneText.Text); 
        }

        private void buttonSceneForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                var textObj = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value);
                textObj.ForeColor = colorDialog1.Color;
                launchTextDisplayText();
            }

        }

        private void buttonSceneBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                var textObj = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value);
                textObj.BackColor = colorDialog1.Color;
                launchTextDisplayText();
            }
        }

        private void buttonSceneSuppressText_Click(object sender, EventArgs e)
        {
            if((int)numericUpDownSceneText.Maximum > 0)
            {
                this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).RemoveText((int)numericUpDownSceneText.Value);
                numericUpDownSceneText.Maximum -= 1;
            }
                
        }

        private void buttonHVQMExtract_Click(object sender, EventArgs e)
        {
            foreach(CHVQM video in ressourceList.hvqmList)
            {
                var fileName = video.GetRessourceName().Replace("\0","");
                FileStream file = new FileStream(CGeneric.pathOtherContent + fileName, FileMode.Create);
                file.Write(video.GetRawData(),0,video.GetRawData().Length);
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
            for (int i = 0; i < 100; i++)
            {
                SendKeys.Send("{ENTER}");
                System.Threading.Thread.Sleep(8000);
            }
            
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
            int newAllLength = PtrTable.Length + WaveTable.Length + SfxTable.Length;

            if (PtrTable != null && WaveTable != null && SfxTable != null)
            {
                if( oldAllLength <= newAllLength || (newAllLength - oldAllLength) < freeSpaceLeft)
                    audioList.ReplaceSoundBank(PtrTable, WaveTable, SfxTable, treeViewAudio.SelectedNode.Index) ;
                else
                    MessageBox.Show("Not enought space in the rom.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UpdateFreeSpaceLeft();
            }
            else
                MessageBox.Show("Cancelled operation, No modification were made.", "PPL manager",MessageBoxButtons.OK ,MessageBoxIcon.Information);
        }

        private byte[] ReadAudioPtrTable()
        {
            byte[] PtrTable = null;
            using (OpenFileDialog openPointerTable = new OpenFileDialog())
            {
                openPointerTable.InitialDirectory = Application.StartupPath;
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
                openWaveTable.InitialDirectory = Application.StartupPath;
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
                openSfxTable.InitialDirectory = Application.StartupPath;
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
                if(newPtrTable.Length <= oldPtrTableLength || (newPtrTable.Length - oldPtrTableLength) < freeSpaceLeft)
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

       
    }
}
