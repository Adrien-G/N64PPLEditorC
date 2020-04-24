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

namespace N64PPLEditorC
{
    public partial class Form1 : Form
    {
        CRessourceList ressourceList;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            CGeneric.VerifyExistingPath();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.txtPPLLocation = textBoxPPLLocation.Text;
            Properties.Settings.Default.Save();
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
                MessageBox.Show("Please select a file...", "N64 PPL Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //try
                //{
                    FileStream fstream = File.Open(textBoxPPLLocation.Text, FileMode.Open, FileAccess.ReadWrite);
                    fstream.Close();
                    buttonLoadRom.Enabled = false;
                    buttonLoadRom.Text = "ROM Loaded";
                    LoadRessourcesList();
                    LoadTreeView();
                    groupBoxTextures.Enabled = true;
                    tabControlTexMovSce.Enabled = true;
                    buttonModifyRom.Enabled = true;
               // }
               //catch (Exception ex)
               // {
               //     MessageBox.Show("Error opening rom..." + Environment.NewLine + "error details : " + ex.Message, "PPL Rom management error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // }
            }
        }

        private void LoadTreeView()
        {
            //add treeview items' (BIF,HVQM and SBF)
            treeViewTextures.BeginUpdate();
            treeViewHVQM.BeginUpdate();
            treeViewSBF.BeginUpdate();
            for (int fib = 0; fib < this.ressourceList.GetFIBCount(); fib++)
            {
                treeViewTextures.Nodes.Add(fib + 1 + ", " + ressourceList.Get3FIB(fib).GetFIBName());
                for (int bff = 0; bff < this.ressourceList.Get3FIB(fib).GetBFFCount(); bff++)
                    treeViewTextures.Nodes[fib].Nodes.Add(bff + 1 +", " + this.ressourceList.Get3FIB(fib).GetBFFName(bff));
            }

            for (int i = 0; i < this.ressourceList.GetHVQMCount(); i++)
                treeViewHVQM.Nodes.Add(i + 1 + ", " + ressourceList.GetHVQM(i).GetRessourceName());

            for (int sbf = 0; sbf < this.ressourceList.GetSBFCount(); sbf++) 
            {
                treeViewSBF.Nodes.Add(sbf + 1 + ", " + ressourceList.GetSBF1(sbf).GetRessourceName());

                for (int scene = 0; scene < this.ressourceList.GetSBF1(sbf).GetSceneCount(); scene++)
                    treeViewSBF.Nodes[sbf].Nodes.Add(scene + 1 + ", " + this.ressourceList.GetSBF1(sbf).GetScene(scene).GetSceneName());
            }

            if (treeViewTextures.Nodes.Count > 0)
                treeViewTextures.SelectedNode = treeViewTextures.Nodes[0];
            else
                treeViewTextures.Nodes.Add("No data were found :(");

            if (treeViewHVQM.Nodes.Count > 0)
                treeViewHVQM.SelectedNode = treeViewHVQM.Nodes[0];
            else
                treeViewHVQM.Nodes.Add("No data were found :(");

            if (treeViewSBF.Nodes.Count > 0)
                treeViewSBF.SelectedNode = treeViewSBF.Nodes[0];
            else
                treeViewSBF.Nodes.Add("No data were found :(");

            treeViewTextures.EndUpdate();
            treeViewHVQM.EndUpdate();
            treeViewSBF.EndUpdate();

        }

        private void LoadRessourcesList()
        {
            Byte[] buffRom = File.ReadAllBytes(textBoxPPLLocation.Text);

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

        private void treeViewTextures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int level = treeViewTextures.SelectedNode.Level;
            if (level != 0)
            {
                labelIsTextureContainer.Hide();
                 this.ressourceList.Get3FIB(treeViewTextures.SelectedNode.Parent.Index).GetTexture(pictureBoxTexture, treeViewTextures.SelectedNode.Index);
                pictureBoxTexture.Show();
                numericUpDownTextureDisplayTime.Value = this.ressourceList.Get3FIB(treeViewTextures.SelectedNode.Parent.Index).GetBFF2(treeViewTextures.SelectedNode.Index).GetTextureDisplayTime();
            }
            if (level == 0)
            {
                labelIsTextureContainer.Show();
                pictureBoxTexture.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBoxTexture.Hide();
            }
        }

        //decompress all texture task..
        private void buttonExtractAllTextures_Click(object sender, EventArgs e)
        {
            if (!bWDecompress.IsBusy)
                bWDecompress.RunWorkerAsync();
        }

        private void bWDecompress_DoWork(object sender, DoWorkEventArgs e)
        {
            int index = 1;
            for (int i = 0; i < ressourceList.GetFIBCount(); i++)
            {
                for (int j = 0; j < ressourceList.Get3FIB(i).GetBFFCount(); j++)
                {
                    try
                    {
                        index++;
                        this.ressourceList.Get3FIB(i).SaveTexture(i, j);
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

        private void buttonModifyRom_Click(object sender, EventArgs e)
        {
            ressourceList.WriteAllData(textBoxPPLLocation.Text);
            buttonModifyRom.BackColor = Color.LimeGreen;
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
                        foreach (String inputTexture in openTexture.FileNames) { 

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

                                //add header and generate the bff2
                                string safeFileName = Path.GetFileNameWithoutExtension(inputTexture);
                                //check if the name has same naming convention than the extract (remove index before coma)
                                if (safeFileName.Split(',').Count() == 2)
                                    safeFileName = safeFileName.Split(',')[1].Trim();

                                byte[] finalBFF2 = CBFF2.GenerateBFF2(palette, compressedData, compressionMethod, img.Width, img.Height, safeFileName);

                                //add texture to the bff2 files and update treeview
                                if (treeViewTextures.SelectedNode.Level == 0)
                                {
                                    ressourceList.Get3FIB(treeViewTextures.SelectedNode.Index).AddBFF2Child(finalBFF2);
                                    treeViewTextures.SelectedNode.Nodes.Add("[added] , " + safeFileName);
                                    treeViewTextures.SelectedNode = treeViewTextures.Nodes[treeViewTextures.SelectedNode.Index].LastNode;
                                }
                                else
                                {
                                    ressourceList.Get3FIB(treeViewTextures.SelectedNode.Parent.Index).AddBFF2Child(finalBFF2);
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
                }
            }
        }

        private void removeThisTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeViewTextures.SelectedNode.Level == 1)
            {
                ressourceList.Get3FIB(treeViewTextures.SelectedNode.Parent.Index).RemoveBFF2Child(treeViewTextures.SelectedNode.Index);
                treeViewTextures.SelectedNode.Remove();
            }
            
        }

        #region helping form
        //part helping on the form..
        private void buttonLoadRom_MouseEnter(object sender, EventArgs e) { helpStatus.Text = "load PPL rom for editing content"; }
        private void buttonGetRomFolder_MouseEnter(object sender, EventArgs e) { helpStatus.Text = "open PPL rom, can only take .z64 file."; }
        private void treeViewTextures_MouseEnter(object sender, EventArgs e) { helpStatus.Text = "Texture management, right click for options (add, remove texture, etc...)"; }
        private void buttonLoadRom_MouseLeave(object sender, EventArgs e) { helpStatus.Text = ""; }
        private void buttonGetRomFolder_MouseLeave(object sender, EventArgs e) { helpStatus.Text = ""; }
        private void treeViewTextures_MouseLeave(object sender, EventArgs e) { helpStatus.Text = ""; }

        #endregion

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
            }
                

        }

        private void treeViewTextures_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeViewTextures.SelectedNode = e.Node;
        }

        private void treeViewTextures_KeyDown(object sender, KeyEventArgs e)
        {
            if (treeViewTextures.SelectedNode.Level == 1)
                if (e.KeyCode == Keys.Delete && treeViewTextures.SelectedNode != null)
                {
                    ressourceList.Get3FIB(treeViewTextures.SelectedNode.Parent.Index).RemoveBFF2Child(treeViewTextures.SelectedNode.Index);
                    treeViewTextures.SelectedNode.Remove();
                }
        }

        private void numericUpDownTextureDisplayTime_ValueChanged(object sender, EventArgs e)
        {
            this.ressourceList.Get3FIB(treeViewTextures.SelectedNode.Parent.Index).GetBFF2(treeViewTextures.SelectedNode.Index).SetTextureDisplayTime((byte)numericUpDownTextureDisplayTime.Value);
        }

        private void treeViewSBF_AfterSelect(object sender, TreeViewEventArgs e)
        {
            numericUpDownSceneText.Value = 0;
            textBoxSceneText.Text = "";
            if (treeViewSBF.SelectedNode.Level == 1)
            {
                int nbTextObject = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObjectCount();
                groupBoxSceneText.Text = nbTextObject + " Text(s)";
                numericUpDownSceneText.Maximum = nbTextObject;
                if (nbTextObject > 0)
                {
                    textBoxSceneText.Text = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value).GetText();
                    numericUpDownSceneText.Maximum -= 1;
                }
            }
        }

        private void numericUpDownSceneText_ValueChanged(object sender, EventArgs e)
        {
            textBoxSceneText.Text = "";
            if (treeViewSBF.SelectedNode.Level == 1)
            {
                int nbTextObject = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObjectCount();
            if(nbTextObject > 0)
                textBoxSceneText.Text = this.ressourceList.GetSBF1(treeViewSBF.SelectedNode.Parent.Index).GetScene(treeViewSBF.SelectedNode.Index).GetTextObject((int)numericUpDownSceneText.Value).GetText();
            }
        }
    }
}
