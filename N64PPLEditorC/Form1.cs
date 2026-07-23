using N64PPLEditorC.ManagementAudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static N64PPLEditorC.CGeneric;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace N64PPLEditorC
{

    
    public partial class Form1 : Form
    {
        CRessourceList ressourceList;
        UncompressedRomTexture romList;
        AudioList audioList;
        int freeSpaceLeft = 0;
        bool extendedRom = false;
        bool N64Game = true;

        //store iso data, avoir reading all when changing ressources...
        byte[] isoRawData;


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
                openTexture.RestoreDirectory = true;
                openTexture.Multiselect = true;

                if (openTexture.ShowDialog() != DialogResult.OK)
                    return;

                foreach (String inputTexture in openTexture.FileNames)
                {
                    Image img = Image.FromFile(inputTexture);


                    if (img.Width > 320 || img.Height > 240)
                        MessageBox.Show("Texture must be at maximum of size 320x240 !", "Size too big...", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //load texture
                    pictureBoxTexture.Width = img.Width;
                    pictureBoxTexture.Height = img.Height;
                    pictureBoxTexture.Image = img;

                    //add to th container
                    string safeFileName = Path.GetFileNameWithoutExtension(inputTexture);

                    //check if the name has same naming convention than the extract (remove index before coma)
                    if (safeFileName.Split(',').Count() == 2)
                        safeFileName = safeFileName.Split(',')[1].Trim();

                    this.ressourceList.Fib[treeViewTextures.SelectedNode.Index].Container.Add(new C3FIBContainer(pictureBoxTexture,safeFileName));
                    treeViewTextures.SelectedNode.Nodes.Add("[added] , " + safeFileName);
                }

               treeViewTextures.Nodes[treeViewTextures.SelectedNode.Index].Expand();

                UpdateFreeSpaceLeft();
            }
        }
        private void removeThisTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewTextures.SelectedNode.Level == 1)
            {
                ressourceList.Fib[treeViewTextures.SelectedNode.Parent.Index].Container.RemoveAt(treeViewTextures.SelectedNode.Index);
                treeViewTextures.SelectedNode.Remove();
                UpdateFreeSpaceLeft();
            }

        }
        private void contextMenuStripForTreeview_Opening(object sender, CancelEventArgs e)
        {
            if (treeViewTextures.SelectedNode.Level == 1)
            {
                removeThisTextureToolStripMenuItem.Enabled = true;
                addNewTextureToolStripMenuItem.Enabled = false;
            }

            else
            {
                removeThisTextureToolStripMenuItem.Enabled = false;
                addNewTextureToolStripMenuItem.Enabled = true;
            }
               
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
            {
                var index = 0;
                if (!this.ressourceList.Fib[treeViewTextures.SelectedNode.Parent.Index].Container[0].Bff2.Flags.HasSubImages)
                    index = treeViewTextures.SelectedNode.Index;
                this.ressourceList.Fib[treeViewTextures.SelectedNode.Parent.Index].Container[index].Header.DisplayTime = (uint)numericUpDownTextureDisplayTime.Value;

            }

        }

        private void treeViewTextures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int level = treeViewTextures.SelectedNode.Level;
            if (level != 0)
            {
                labelIsTextureContainer.Hide();
                var bff2Data = this.ressourceList.Fib[treeViewTextures.SelectedNode.Parent.Index].Container[0].Bff2;
                var index = 0;
                if (!bff2Data.Flags.HasSubImages)
                {
                    pictureBoxTexture.Image = this.ressourceList.Fib[treeViewTextures.SelectedNode.Parent.Index].Container[treeViewTextures.SelectedNode.Index].Bff2.GetBmpTexture();
                    index = treeViewTextures.SelectedNode.Index;
                }
                else
                    pictureBoxTexture.Image = bff2Data.GetSubImageBitmap(treeViewTextures.SelectedNode.Index);

                numericUpDownTextureDisplayTime.Value = this.ressourceList.Fib[treeViewTextures.SelectedNode.Parent.Index].Container[index].Header.DisplayTime;
                pictureBoxTexture.Show();
                labelTextureCompression.Text = "Compression : " + this.ressourceList.Fib[treeViewTextures.SelectedNode.Parent.Index].Container[index].Bff2.Flags.CompressionType.ToString();
            }
            if (level == 0)
            {
                labelIsTextureContainer.Show();
                pictureBoxTexture.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBoxTexture.Hide();

                //flags
                var fib = this.ressourceList.Fib[treeViewTextures.SelectedNode.Index];
                checkBoxAutoScroll.Checked = fib.Flags.AnimationLoopXY;
                checkBoxUnk00000002.Checked = fib.Flags.Unk00000002;
                checkBoxAnimationLoop.Checked = fib.Flags.AnimationLoop;
                checkBoxName.Checked = fib.Flags.Name;
                checkBoxSecondRGBAColor.Checked = fib.Flags.SecondRGBAColor;
                checkBoxAnimatedSequence.Checked = fib.Flags.Animated;
                checkBoxAdjustedLocation.Checked = fib.Flags.AdjustedLocation;
                checkBoxLoopDataAdditional.Checked = fib.Flags.LoopDataAdditional;
                checkBoxUnknow00000100.Checked = fib.Flags.Unknow00000100;
                checkBoxUsesSharedFrameBuffers.Checked = fib.Flags.UsesSharedFrameBuffers;
                checkBoxPingPongAnimation.Checked = fib.Flags.PingPongAnimation;
                checkBoxPingPongDirection.Checked = fib.Flags.PingPongDirection;
                labelTextureCompression.Text = "Compression : -";
            }
            
        }

        private void treeViewTextures_KeyDown(object sender, KeyEventArgs e)
        {
            if (treeViewTextures.SelectedNode.Level == 1)
                if (e.KeyCode == Keys.Delete && treeViewTextures.SelectedNode != null)
                {
                    ressourceList.Fib[treeViewTextures.SelectedNode.Parent.Index].Container.RemoveAt(treeViewTextures.SelectedNode.Index);
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

        private void UpdateFreeSpaceLeft()
        {
            try
            {
                if (extendedRom)
                    freeSpaceLeft = CGeneric.romSizeExtended;
                else
                    freeSpaceLeft = CGeneric.romSize;

                freeSpaceLeft -= ressourceList.indexRessourcesStart;
                freeSpaceLeft -= ressourceList.GetSizeOfAllRessourceList();
                if (audioList != null)//TODO To remove when ok
                    freeSpaceLeft -= audioList.GetSizeOfAllAudio();

                if (freeSpaceLeft < 0)
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
                }

                labelFreeSpaceLeft.Text = freeSpaceLeft.ToString("### ### ### ###") + " bytes";
            }
            catch
            {

            }
           
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
            else
            {
                isoRawData = buffRom;
                RomLangAddress.romLang = CGeneric.romLang.JAP;
            }
            //load graphics compressed / hvqm and sbf
            LoadRessourcesList(buffRom);
            if (N64Game)
            {
                //load the uncompressed texture
                this.romList = new UncompressedRomTexture(buffRom);

                //load the audio part
                LoadAudioList(buffRom);
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


            for (int fib = 0; fib < this.ressourceList.Fib.Count(); fib++)
            {
                treeViewTextures.Nodes.Add(fib + 1 + ", " + ressourceList.Fib[fib].NameString);
                for (int bff = 0; bff < this.ressourceList.Fib[fib].Container.Count; bff++)
                {
                    var bff2Data = this.ressourceList.Fib[fib].Container[bff].Bff2;
                    if (!bff2Data.Flags.HasSubImages)
                        treeViewTextures.Nodes[fib].Nodes.Add(bff + 1 + ", " + bff2Data.NameString);
                    else
                        for(int i = 0; i < bff2Data.SubImageData.ImageData.Count; i++)
                            treeViewTextures.Nodes[fib].Nodes.Add(i + 1 + "");
                }
                    
            }

            for (int i = 0; i < this.ressourceList.GetHVQMCount(); i++)
                treeViewHVQM.Nodes.Add(i + 1 + ", " + ressourceList.GetHVQM(i).GetRessourceName());

            for (int sbf = 0; sbf < this.ressourceList.GetSBFCount(); sbf++)
            {
                treeViewSBF.Nodes.Add(sbf + 1 + ", " + ressourceList.Sbf1[sbf].GetRessourceName());

                for (int scene = 0; scene < this.ressourceList.GetSBF1(sbf).GetSceneCount(); scene++)
                    treeViewSBF.Nodes[sbf].Nodes.Add(scene + 1 + ", " + this.ressourceList.GetSBF1(sbf).GetScene(scene).GetSceneName());
            }

            if (N64Game) {
                for (int romGraphics = 0; romGraphics < this.romList.GetGraphicsCount(); romGraphics++)
                    treeViewTexturesUncompressed.Nodes.Add(this.romList.GetGraphicsName(romGraphics));

                for (int audioSoundbank = 0; audioSoundbank < this.audioList?.soundBankList.Count(); audioSoundbank++)
                {
                    treeViewAudio.Nodes.Add("Soundbank " + Convert.ToString(audioSoundbank, 10).ToUpper());
                    for (int j = 0; j < this.audioList?.soundBankList[audioSoundbank].songList?.Count; j++)
                        treeViewAudio.Nodes[audioSoundbank].Nodes.Add("Song " + (j + 1));
                    for (int j = 0; j < this.audioList?.soundBankList[audioSoundbank].soundList?.Count; j++)
                    {
                        TreeNode soundNode = new TreeNode("Sound " + (j + 1));
                        soundNode.Tag = j;
                        treeViewAudio.Nodes[audioSoundbank].Nodes.Add(soundNode);
                    }
                }
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
            for (int i = 0; i < ressourceList.Fib.Count(); i++)
            {
                for (int j = 0; j < ressourceList.Fib[i].Container.Count; j++)
                {
                    try
                    {
                        index++;
                        this.ressourceList.Fib[i].Container[j].Bff2.SaveTexture(i,j);
                        bWDecompress.ReportProgress(index);
                    }
                    catch { }
                }
            }
        }

        private void bWDecompress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var total = 0;
            foreach (var fib in ressourceList.Fib)
                total += fib.Container.Count();
            buttonExtractAllTextures.Text = e.ProgressPercentage + "/" + total;
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
                if (checkBoxLaunchEverdrive.Checked)
                    launchOnEverdrive();
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
                        if (checkBoxLaunchEverdrive.Checked)
                            launchOnEverdrive();
                    }
                }
            }
        }

        private void launchOnEverdrive()
        {
            //copy the file to output repertory (mandatory because usb64 doesn't seems support filepath)
            var fileIn = File.ReadAllBytes(textBoxPPLLocation.Text);
            File.WriteAllBytes(CGeneric.pathOtherContent + "out.z64", fileIn);

            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            p.StartInfo = info;
            p.Start();

            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("cd "+ CGeneric.pathOtherContent);
                    sw.WriteLine("usb64.exe -rom=out.z64 -start");
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

            if (treeViewSBF.SelectedNode.Level == 0)
            {
                drawScene1.BackColor = Color.Gray;
                return;
            }

            //scene display part
            var sbf = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index);
            var scene = sbf.GetScene(treeViewSBF.SelectedNode.Index);
            var ListTextureName = sbf.GetBifList();

            int nbItem = scene.GetTextureManagementCount() - 1;


            drawScene1.Init();
            for (int i = 0; i <= nbItem; i++)
            {
                string textureInsideSbfName = ListTextureName[scene.GetTextureManagementObject(i).getTextureIndex()]; //select good texture
                int indexData = this.ressourceList.Get3FIBIndexWithFIBName(textureInsideSbfName);
                if (indexData != -1)
                {
                    try
                    {
                        var bmp = this.ressourceList.Fib[indexData].Container[0].Bff2.GetBmpTexture();
                        var posY = scene.GetTextureManagementObject(i).Base.Y;
                        var posX = scene.GetTextureManagementObject(i).Base.X;
                        this.drawScene1.AddBmp(bmp, new Point(posX, posY));
                    }
                    catch { 
                    }
                }
            }
            this.drawScene1.Invalidate();

        }

        private void buttonHvqmPathOpen_Click(object sender, EventArgs e)
        {
            Process.Start(CGeneric.pathOtherContent);
        }

        private void buttonHVQMExtract_Click(object sender, EventArgs e)
        {
            foreach (CHVQM video in ressourceList.Hvqm)
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
                    this.ressourceList.Hvqm[treeViewHVQM.SelectedNode.Index].SetRawData(buffRom);
                    UpdateFreeSpaceLeft();
                }
            }
        }

        private void buttonHVQMRemove_Click(object sender, EventArgs e)
        {
            this.ressourceList.Hvqm.RemoveAt(treeViewHVQM.SelectedNode.Index);
            treeViewHVQM.Nodes.RemoveAt(treeViewHVQM.SelectedNode.Index);
            UpdateFreeSpaceLeft();
        }

        private void toolStripMenuItemReplaceAudioAllSoundBank_Click(object sender, EventArgs e)
        {
            byte[] PtrTable = OpenFileAndGetBytes("ptr file (*.ptr)|*.ptr");
            byte[] WaveTable = OpenFileAndGetBytes("wbk file (*.wbk)|*.wbk");
            byte[] SfxTable = OpenFileAndGetBytes("sfx file (*.bfx)|*.bfx");
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

        private byte[] OpenFileAndGetBytes(string filter= "bin file (*.bin)|*.bin")
        {
            byte[] outData = null;
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = filter;
                openFile.FilterIndex = 1;
                openFile.RestoreDirectory = true;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    outData = File.ReadAllBytes(openFile.FileName);
                }
            }
            return outData;
        }

        private void toolStripMenuItemReplacePointerTable_Click(object sender, EventArgs e)
        {
            byte[] newPtrTable = OpenFileAndGetBytes("ptr file (*.ptr)|*.ptr");
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
            byte[] newWaveTable = OpenFileAndGetBytes("wbk file (*.wbk)|*.wbk");
            int oldWaveTableLength = audioList.soundBankList[treeViewAudio.SelectedNode.Index].waveTable.Length;

            if (newWaveTable != null)
            {
                //if (newWaveTable.Length <= oldWaveTableLength || (newWaveTable.Length - oldWaveTableLength) < freeSpaceLeft)
                //{
                    audioList.ReplaceWaveTable(newWaveTable, treeViewAudio.SelectedNode.Index);
                    UpdateFreeSpaceLeft();
                //}
                //else
                //    MessageBox.Show("Not enought space in the rom.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Cancelled operation, No modification were made.", "PPL manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItemReplaceSfxTable_Click(object sender, EventArgs e)
        {
            byte[] newSfxTable = OpenFileAndGetBytes("sfx file (*.bfx)|*.bfx");
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
            toolStripMenuItemReplaceThisSong.Enabled = false;

            if (treeViewAudio.SelectedNode.Level == 1)
            {
                toolStripMenuItemReplaceThisSong.Enabled = true;
                toolStripMenuItemReplaceAudioAllSoundBank.Enabled = false;
                toolStripMenuItemReplacePointerTable.Enabled = false;
                toolStripMenuItemReplaceSfxTable.Enabled = false;
                toolStripMenuItemReplaceWaveTable.Enabled = false;
            }

            if(treeViewAudio.SelectedNode?.Index == 2)
                toolStripMenuItemReplaceSfxTable.Enabled = false;

        }

        private void treeViewAudio_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //small hack
            treeViewAudio.SelectedNode = e.Node;
        }


        private void buttonScenesTextureAdd_Click(object sender, EventArgs e)
        {
           


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
                ToolStripMenuItemReplaceScene.Enabled = false;

            }
            else
            {
                ToolStripMenuItemLoadSBF.Enabled = false;
                ToolStripMenuItemSaveSBF.Enabled = false;
                ToolStripMenuItemReplaceScene.Enabled = true;
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
            C3FIBObject fib;
            if (treeViewTextures.SelectedNode.Level == 0)
                fib = this.ressourceList.Fib[treeViewTextures.SelectedNode.Index];
            else
                fib = this.ressourceList.Fib[treeViewTextures.SelectedNode.Parent.Index];

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "3fib files (*.3fib)|*.3fib";
            saveFileDialog1.FileName = fib.NameString;
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    var b = fib.RecomposeRawData();
                    myStream.Write(b, 0, b.Length);
                    myStream.Close();
                }
            }
        }

        private void comboBoxRessourcesISO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (N64Game)
                return;
            //try
            //{
                LoadRessourcesList(isoRawData);
            //}
            //catch
            //{

            //}

            LoadTreeView();
            UpdateFreeSpaceLeft();
            tabControlTexMovSce.Enabled = true;
            buttonModifyRom.Enabled = true;
            buttonLoadRom.Enabled = false;
            buttonGetRomFolder.Enabled = false;
            buttonLoadRom.Text = "ROM Loaded";
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

        private void buttonConvertOldAVI_Click(object sender, EventArgs e)
        {
            if(!File.Exists(CGeneric.pathOtherContent + "hvqm2enc.exe"))
            {
                MessageBox.Show("Unable to find hvq2enc.exe, please move it in this folder : " + CGeneric.pathOtherContent, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (OpenFileDialog openRomFile = new OpenFileDialog())
            {
                openRomFile.Filter = "avi file (*.avi)|*.avi";
                openRomFile.FilterIndex = 1;
                openRomFile.RestoreDirectory = true;

                if (openRomFile.ShowDialog() == DialogResult.OK)
                     Process.Start("cmd.exe","/c start " + CGeneric.pathOtherContent + "hvqm2enc.exe " + openRomFile.FileName + " " + CGeneric.pathOtherContent + "converted.hvqm");
                                  
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(CGeneric.pathOtherContent);
        }

        private void buttonAudioExtractAllSounds_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < audioList.soundBankList.Count; i++)
            {
                for (int j = 0; j < audioList.soundBankList[i].nbInstruments; j++)
                {
                    File.WriteAllBytes(CGeneric.pathExtractedSound + "Soundbank "+ i.ToString("D" + 2) + @"\" + j.ToString("X" + 2) + ".wav", this.audioList.soundBankList[i].soundList[j].GetWave(i, j));
                }
            }
            Process.Start(CGeneric.pathExtractedSound);
        }

        private void ToolStripMenuItemReplaceThisSong_Click(object sender, EventArgs e)
        {
            audioList.ReplaceSong(OpenFileAndGetBytes(), treeViewAudio.SelectedNode.Parent.Index, treeViewAudio.SelectedNode.Index);
        }

        private void importNewSBFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openSbfFile = new OpenFileDialog())
            {
                openSbfFile.Filter = "sbf file (*.sbf)|*.sbf";
                openSbfFile.FilterIndex = 1;
                openSbfFile.RestoreDirectory = true;

                if (openSbfFile.ShowDialog() == DialogResult.OK)
                {
                    Byte[] buffRom = File.ReadAllBytes(openSbfFile.FileName);
                    var name = CGeneric.ConvertStringToByteArray(openSbfFile.SafeFileName);
                    this.ressourceList.Sbf1.Add(new CSBF1(buffRom,name));
                }
            }
            LoadTreeView();
            UpdateFreeSpaceLeft();
            
        }


        private void numericUpDownTextureTransparency_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Byte[] buffPalette = new byte[0] ;
            Byte[] buffTexture = new byte[0];
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.InitialDirectory = Application.StartupPath;
                openFile.RestoreDirectory = true;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    buffPalette = File.ReadAllBytes(openFile.FileName);

                    
                }
            }
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.InitialDirectory = Application.StartupPath;
                openFile.RestoreDirectory = true;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    buffTexture = File.ReadAllBytes(openFile.FileName);


                }
            }
            var outTex = CTextureManager.ConvertByteArrayToRGBA(buffTexture, Compression.trueColor16Bits, buffPalette);
            pictureBox1.Image = CTextureManager.ConvertRGBAByteArrayToBitmap(outTex,216,134);
        }

        private void extractBinToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "bin files (*.bin)|*.bin";
            saveFileDialog1.FileName = "raw sound";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    var b = audioList.soundBankList[treeViewAudio.SelectedNode.Parent.Index].soundList[treeViewAudio.SelectedNode.Index].rawData;
                    myStream.Write(b, 0, b.Length);
                    myStream.Close();
                }
            }
        }


        private void buttonAdd4thObj_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open4thFile = new OpenFileDialog())
            {
                open4thFile.Filter = "4th file (*.bin)|*.bin";
                open4thFile.FilterIndex = 1;
                open4thFile.RestoreDirectory = true;

                if (open4thFile.ShowDialog() == DialogResult.OK)
                {
                    Byte[] buffRom = File.ReadAllBytes(open4thFile.FileName);
                    ressourceList.Sbf1[treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index].AddNew4thObject(buffRom);
                }
            }

            UpdateFreeSpaceLeft();
        }

        private void button4thremove_Click(object sender, EventArgs e)
        {
            ressourceList.Sbf1[treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index].fourthObjectList = new List<CSBF1FourthObject>();
        }

        private void buttonDynamicObjectAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dynamicObjectFile = new OpenFileDialog())
            {
                dynamicObjectFile.Filter = "Dynamic object file (*.bin)|*.bin";
                dynamicObjectFile.FilterIndex = 1;
                dynamicObjectFile.RestoreDirectory = true;

                if (dynamicObjectFile.ShowDialog() == DialogResult.OK)
                {
                    Byte[] buffRom = File.ReadAllBytes(dynamicObjectFile.FileName);
                    ressourceList.Sbf1[treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index].AddNewDynamicObject(buffRom);
                }
            }

            UpdateFreeSpaceLeft();
        }

        private void buttonDynamicObjectRemove_Click(object sender, EventArgs e)
        {
            ressourceList.Sbf1[treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index].dynamicObjectList = new List<CSBF1DynamicObject>();
        }

        private void buttonDynamicObjectExport_Click(object sender, EventArgs e)
        {
            var DynamicList = this.ressourceList.Sbf1[this.treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index].dynamicObjectList;

            byte[] res = new byte[0];
            foreach (CSBF1DynamicObject dynObj in DynamicList)
                res = res.Concat(dynObj.GetRawData()).ToArray();

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "DynamicObject files (*.bin)|*.bin";
            saveFileDialog1.FileName = "DynamicObject1";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    myStream.Write(res, 0, res.Length);
                    myStream.Close();
                }
            }

           
        }

        private void ToolStripMenuItemReplaceScene_Click(object sender, EventArgs e)
        {
            var scene = this.ressourceList.Sbf1[this.treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index];


            using (OpenFileDialog openSceneFile = new OpenFileDialog())
            {
                openSceneFile.Filter = "scene file (*.scene)|*.scene";
                openSceneFile.FilterIndex = 1;
                openSceneFile.RestoreDirectory = true;

                if (openSceneFile.ShowDialog() == DialogResult.OK)
                {
                    Byte[] buffRaw = File.ReadAllBytes(openSceneFile.FileName);
                    this.ressourceList.Sbf1[this.treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index] = new CSBF1Scene(buffRaw);
                }
            }
            UpdateFreeSpaceLeft();
        }

        private void ToolStripMenuItemExportScene_Click(object sender, EventArgs e)
        {
            var scene = this.ressourceList.Sbf1[this.treeViewSBF.SelectedNode.Parent.Index].scenesList[treeViewSBF.SelectedNode.Index];

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Scene files (*.scene)|*.scene";
            saveFileDialog1.FileName = "SceneFile1";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    var rawData = scene.GetRawData();
                    myStream.Write(rawData, 0, rawData.Length);
                    myStream.Close();
                }
            }
        }

        private void buttonEditScene_Click(object sender, EventArgs e)
        {
            if (treeViewSBF.SelectedNode.Level == 0)
            {
                MessageBox.Show("Merci de sélectionner une scène.");
                return;
            }
            
            CSBF1 sbf = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index);
            
            var form2 = new SceneEdit(sbf,ressourceList, treeViewSBF.SelectedNode.Index);
            if (form2.ShowDialog() == DialogResult.OK)
            {
                //récupération des données en fin..
                sbf.SetScene(form2.Scene,treeViewSBF.SelectedNode.Index);
            }
        }

        private void toolStripMenuItemReplaceByWav_Click(object sender, EventArgs e)
        {
            
           
        }

        private void buttonReplaceByWav_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openWavFile = new OpenFileDialog())
            {
                openWavFile.Filter = "WAV files (*.wav;*.wave)|*.wav;*.wave";
                openWavFile.FilterIndex = 1;
                openWavFile.RestoreDirectory = true;

                if (openWavFile.ShowDialog() == DialogResult.OK)
                {
                    Byte[] buffWav = File.ReadAllBytes(openWavFile.FileName);
                    this.audioList.ReplaceSound(treeViewAudio.SelectedNode.Parent.Index, (int)treeViewAudio.SelectedNode.Tag, buffWav);
                }
            }
        }
    }
}
