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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            try
            {
                FileStream fstream = File.Open(textBoxPPLLocation.Text, FileMode.Open, FileAccess.ReadWrite);
                fstream.Close();
                buttonLoadRom.Enabled = false;
                buttonLoadRom.Text = "ROM Loaded";
                LoadTreeViewRessources();
                //timerSearchContent.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening rom..." + Environment.NewLine + "error details : " + ex.Message, "PPL Rom management error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void LoadTreeViewRessources()
        {
            Byte[] buffRom = File.ReadAllBytes(textBoxPPLLocation.Text);

            // search for "ABRA.BIF" pattern (start of array ressources location)
            Byte[] patternAbraBif = { 65, 66, 82, 65, 46, 66, 73, 70 };
            int indexRessourcesArrayStart = CGenericFunctions.SearchBytesInArray(buffRom, patternAbraBif);
            labelStartingData.Text = indexRessourcesArrayStart.ToString("X");

            // search for "N64 PtrTablesV2" pattern (end of ressources location)
            Byte[] patternN64WaveTable = { 78, 54, 52, 32, 80, 116, 114, 84, 97, 98, 108, 101, 115, 86, 50 };
            int indexRessourcesEnd = CGenericFunctions.SearchBytesInArray(buffRom, patternN64WaveTable);
            labelEndingData.Text = indexRessourcesEnd.ToString("X");


            //read header of table data
            Byte[] nbElements = new Byte[3];
            Array.Copy(buffRom, indexRessourcesArrayStart, nbElements, 0, nbElements.Length);
            
            //send array to the CressourcesListClass between starting and ending data




            if (treeView1.Nodes.Count > 0)
                treeView1.SelectedNode = treeView1.Nodes[0];
            else
                treeView1.Nodes.Add("No data were found :(");

        }
    }
}
