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
            textBoxPPLLocation.Text = Properties.Settings.Default.txtPPLLocation.ToString();
        }

        private void buttonGetRomFolder_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openRomFile = new OpenFileDialog())
            {
                openRomFile.InitialDirectory = Application.StartupPath;
                openRomFile.Filter = "rom file (*.n64)|*.n64| rom file (*.z64)|*.z64";
                openRomFile.FilterIndex = 1;
                openRomFile.RestoreDirectory = true;

                if(openRomFile.ShowDialog() == DialogResult.OK)
                {
                    textBoxPPLLocation.Text = openRomFile.FileName;
                }
            }

        }

        private void buttonLoadRom_Click(object sender, EventArgs e)
        {
            //try
            //{
                FileStream fstream = File.Open(textBoxPPLLocation.Text, FileMode.Open, FileAccess.ReadWrite);
                fstream.Close();
                buttonLoadRom.Enabled = false;
                buttonLoadRom.Text = "ROM Loaded";
                LoadTreeViewAndRessourcesList();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error opening rom..." + Environment.NewLine + "error details : " + ex.Message, "PPL Rom management error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }
        
        private void LoadTreeViewAndRessourcesList()
        {
            Byte[] buffRom = File.ReadAllBytes(textBoxPPLLocation.Text);

            // search for "ABRA.BIF" pattern (start of array ressources location)
            int indexRessourcesArrayStart = CGeneric.SearchBytesInArray(buffRom, CGeneric.patternAbraBif) - 12;
            labelStartingData.Text = indexRessourcesArrayStart.ToString("X");

            // search for "N64 PtrTablesV2" pattern (end of ressources location)
            int indexRessourcesEnd = CGeneric.SearchBytesInArray(buffRom, CGeneric.patternN64WaveTable);
            labelEndingData.Text = indexRessourcesEnd.ToString("X");


            //read header of table data
            Byte[] nbElementsTable = new Byte[4];
            Array.Copy(buffRom, indexRessourcesArrayStart, nbElementsTable, 0, nbElementsTable.Length);
            int nbElementsInTable = CGeneric.ConvertByteArrayToInt(nbElementsTable);


            //read data table for getting data information location and length
            Byte[] dataTable = new Byte[CGeneric.sizeOfElementTable * nbElementsInTable];
            Array.Copy(buffRom, indexRessourcesArrayStart+4,dataTable,0,dataTable.Length);

            //get array between load and end data
            Byte[] ressourcesData = new Byte[indexRessourcesEnd - indexRessourcesArrayStart - dataTable.Length-4];
            Array.Copy(buffRom, indexRessourcesArrayStart + dataTable.Length+4, ressourcesData, 0, ressourcesData.Length);


            //init the ressources list and data associated
            this.ressourceList = new CRessourceList();
            this.ressourceList.Init(nbElementsInTable, dataTable, ressourcesData);

            if (treeViewTextures.Nodes.Count > 0)
                treeViewTextures.SelectedNode = treeViewTextures.Nodes[0];
            else
                treeViewTextures.Nodes.Add("No data were found :(");

        }
    }
}
