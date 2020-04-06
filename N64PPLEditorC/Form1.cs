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
                try
                {
                    FileStream fstream = File.Open(textBoxPPLLocation.Text, FileMode.Open, FileAccess.ReadWrite);
                    fstream.Close();
                    buttonLoadRom.Enabled = false;
                    buttonLoadRom.Text = "ROM Loaded";
                    LoadRessourcesList();
                    LoadTreeView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening rom..." + Environment.NewLine + "error details : " + ex.Message, "PPL Rom management error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                treeViewTextures.Nodes.Add(fib + 1 + ", " + ressourceList.GetBIFName(fib));
                for (int bff = 0; bff < this.ressourceList.GetBFFCount(fib); bff++)
                    treeViewTextures.Nodes[fib].Nodes.Add(bff + 1 +", " + this.ressourceList.GetBFFName(fib,bff));
            }

            for (int i = 0; i < this.ressourceList.GetHVQMCount(); i++)
                treeViewHVQM.Nodes.Add(i + 1 + ", " + ressourceList.GetHVQMName(i));

            for (int i = 0; i < this.ressourceList.GetSBFCount(); i++)
                treeViewSBF.Nodes.Add(i + 1 + ", " + ressourceList.GetSBFName(i));

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

        private void buttonShowTexture_Click(object sender, EventArgs e)
        {
            if(treeViewTextures.SelectedNode.Level == 1)
                this.ressourceList.ShowTexture(pictureBoxTexture,treeViewTextures.SelectedNode.Parent.Index,treeViewTextures.SelectedNode.Index);
        }

        private void treeViewTextures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int level = treeViewTextures.SelectedNode.Level;
            if (checkBoxAlwaysShowTexture.Checked && level != 0)
            {
                labelIsTextureContainer.Hide();
                 this.ressourceList.ShowTexture(pictureBoxTexture, treeViewTextures.SelectedNode.Parent.Index, treeViewTextures.SelectedNode.Index);
                pictureBoxTexture.Show();
            }
            if (level == 0)
            {
                labelIsTextureContainer.Show();
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
                for (int j = 0; j < ressourceList.GetBFFCount(i); j++)
                {
                    try
                    {
                        index++;
                        this.ressourceList.SaveTexture(i, j);
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

        private void checkBoxAlwaysShowTexture_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAlwaysShowTexture.Checked)
                buttonShowTexture.Enabled = false;
            else
                buttonShowTexture.Enabled = true;
        }

        private void buttonModifyRom_Click(object sender, EventArgs e)
        {
            ressourceList.WriteAllData(textBoxPPLLocation.Text);
            buttonModifyRom.BackColor = Color.LimeGreen;
        }

        //part helping on the form..
        private void buttonLoadRom_MouseEnter(object sender, EventArgs e) { helpStatus.Text = "load PPL rom for editing content"; }
        private void buttonGetRomFolder_MouseEnter(object sender, EventArgs e) { helpStatus.Text = "open PPL rom, can only take .z64 file."; }

        private void buttonLoadRom_MouseLeave(object sender, EventArgs e) { helpStatus.Text = ""; }
        private void buttonGetRomFolder_MouseLeave(object sender, EventArgs e) { helpStatus.Text = ""; }

        private void button4_Click(object sender, EventArgs e)
        {


            //CompressTexture formCT = new CompressTexture();
            //formCT.Show();
        }
    }
}
