using N64PPLEditorC.TransparentPanel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static N64PPLEditorC.CSBF1TextVisualStyle;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace N64PPLEditorC
{
    public partial class SceneEdit : Form
    {
        bool InitStep = true;
        public CSBF1 Sbf { get; set; }
        public CSBF1Scene Scene { get; set; }
        public CRessourceList RessourceList { get; set; }
        List<TextBox> txtBox = new List<TextBox>();
        public SceneEdit()
        {
            InitializeComponent();
        }

        public SceneEdit(CSBF1 sbf, CRessourceList ressourceList, int indexScene) : this()
        {
            this.Sbf = sbf;
            Scene = sbf.GetScene(indexScene);
            this.RessourceList = ressourceList;
            this.Text = "éditeur de scene : " + Scene.GetSceneName();
            InitializeForm();
            InitStep = false;

        }

        public void InitializeForm()
        {
            //list the texture (combobox) present in the scene
            comboBoxSceneChangeTexture.Items.Clear();
            for (int i = 0; i < Sbf.GetBifList().Count(); i++)
                comboBoxSceneChangeTexture.Items.Add(Sbf.GetBifName(i).ToLower().Replace(".bif", ""));

            //list all the textures present in the rom (pretty name)
            int indexData = 0;
            comboBoxSceneAddTexture.Items.Clear();
            for (int i = 0; i < RessourceList.fibList.Count; i++)
            {
                indexData = RessourceList.Get3FIBIndexWithFIBName(RessourceList.fibList[i].GetRessourceName());
                comboBoxSceneAddTexture.Items.Add(RessourceList.fibList[indexData].GetFIBName());
            }

            //text objects
            int nbTextObject = Scene.GetTextObjectCount();
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

            //textures object
            int nbTextureObject = Scene.GetTextureManagementCount();
            if (nbTextureObject == 0)
                numericUpDownSceneTexture.Maximum = 0;
            else
                numericUpDownSceneTexture.Maximum = nbTextureObject - 1;

            //4th object
            int nb4thObject = Scene.Get4thObjCount();
            label4thCount.Text = nb4thObject.ToString();

            //dynamic object
            int nbDynamicObject = Scene.GetDynamicObjectCount();
            labelDynamicObjCount.Text = nbDynamicObject.ToString();

            groupBoxSceneText.Enabled = true;
            groupBoxSceneTextureManagement.Enabled = true;
            launchSceneDisplay();
        }

        private void launchSceneDisplay()
        {
            launchGraphicDisplayPart();
            LaunchTextDisplayGroup();
        }

        private void launchGraphicDisplayPart(int displaySpecificTexture = -1)
        {
            var ListTextureName = Sbf.GetBifList();

            int nbItem = Scene.GetTextureManagementCount() - 1;


            drawScene1.Init();
            for (int i = 0; i <= nbItem; i++)
            {
                string textureInsideSbfName = ListTextureName[Scene.GetTextureManagementObject(i).getTextureIndex()]; //select good texture
                int indexData = RessourceList.Get3FIBIndexWithFIBName(textureInsideSbfName);
                if (indexData != -1 && Scene.GetTextureManagementObject(i).isCompressedTexture && (displaySpecificTexture == -1 || displaySpecificTexture == i))
                {
                    try
                    {
                        var bmp = RessourceList.fibList[indexData].GetBmpTexture(0);
                        var posY = Scene.GetTextureManagementObject(i).posY;
                        var posX = Scene.GetTextureManagementObject(i).posX;
                        this.drawScene1.AddBmp(bmp, new Point(posX, posY));
                    }
                    catch { }
                }
                if (displaySpecificTexture != -1)
                {
                    numericUpDownSceneTexturePosX.Value = Scene.GetTextureManagementObject(displaySpecificTexture).posX;
                    numericUpDownSceneTexturePosY.Value = Scene.GetTextureManagementObject(displaySpecificTexture).posY;
                }
            }
            this.drawScene1.Invalidate();
        }
        private void LaunchTextDisplayGroup()
        {
            //clear all textbox
            foreach (TextBox txtObj in txtBox)
                drawScene1.Controls.Remove(txtObj);
            txtBox.Clear();

            //get object count and verify > 0
            int nbTextObject = Scene.GetTextObjectCount();

            if (nbTextObject > 0)
            {
                var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);

                //Check base
                numericUpDownSceneTextPosX.Value = textObject.Base.X;
                numericUpDownSceneTextPosY.Value = textObject.Base.Y;
                labelTextId.Text = "Id : " + textObject.Base.Id.ToString();

                //Check animation
                checkBoxSceneProgressiveDisplay.Checked = textObject.Animation.ProgressiveDisplay;
                numericUpDownTextProgressiveSound.Value = textObject.Animation.ProgressiveSoundStyle;
                numericUpDownTextLinkedTexture.Value = textObject.Animation.LinkedTextureId;

                //check visual styles
                if(textObject.VisualStyle.PrimaryColor.Count > 0)
                {
                    buttonScenePrimaryColor.BackColor = textObject.VisualStyle.PrimaryColor[0];
                    buttonSceneSecondaryColor.BackColor = textObject.VisualStyle.SecondaryColor[0];
                }
                else
                {
                    buttonScenePrimaryColor.BackColor = Color.WhiteSmoke;
                    buttonSceneSecondaryColor.BackColor = Color.WhiteSmoke;
                }

                    comboBoxSceneFontSize.SelectedIndex = (int)textObject.VisualStyle.FontSize;

                //check text output
                textBoxTextOutput.Text = textObject.Text.GetAsciiText();







                //add text options (posX,posY, backColor, forecolor)
                checkBoxScenesForegroundText.Checked = textObject.isForegroundText;
                checkBoxScenesIsHidden.Checked = textObject.isHidden;


                //text object
                var sceneTxt = Scene.GetTextObjectGroup(textObject.group);

                int index = 0;
                foreach (CSBF1TextObject txtObj in sceneTxt)
                {
                    var txtBoxTmp = new TextBox();
                    txtBoxTmp.Multiline = true;
                    txtBoxTmp.ReadOnly = true;
                    txtBoxTmp.BorderStyle = BorderStyle.None;
                    txtBox.Add(txtBoxTmp);

                    txtBox[index].Text = txtObj.Text.GetAsciiText();
                    txtBox[index].Top = txtObj.Base.Y;
                    txtBox[index].Left = txtObj.Base.X;

                    Size size = TextRenderer.MeasureText(txtBox[index].Text, txtBox[index].Font);
                    txtBox[index].ClientSize = new Size(size.Width, size.Height);

                    if (textBoxTextOutput.Text == txtBox[index].Text && sceneTxt.Count >= 1)
                    {
                        txtBox[index].BackColor = Color.LightGreen;
                        txtBox[index].BringToFront();
                    }

                    drawScene1.Controls.Add(txtBox[index]);
                    index++;
                }
            }
        }

        private void SceneEdit_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDownSceneText_ValueChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            LaunchTextDisplayGroup();
        }

        private void numericUpDownSceneTextPosX_ValueChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObj.Base.X = Convert.ToInt32(numericUpDownSceneTextPosX.Value);
            LaunchTextDisplayGroup();
        }

        private void numericUpDownSceneTextPosY_ValueChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObj.Base.Y = Convert.ToInt32(numericUpDownSceneTextPosY.Value);
            LaunchTextDisplayGroup();
        }




        private void buttonSceneSuppressText_Click(object sender, EventArgs e)
        {
            //a adapter.
           
            if (Scene.GetTextObjectCount() > 0)
            {
                Scene.RemoveText((int)numericUpDownSceneText.Value);
                if (numericUpDownSceneText.Value > 0)
                    numericUpDownSceneText.Maximum -= 1;
                else
                    numericUpDownSceneText.Maximum = 0;
            }
        }


        private void buttonScenesAddText_Click(object sender, EventArgs e)
        {
           Scene.AddNewTextObject(radioButtonScenesNewScene.Checked);
            numericUpDownSceneText.Maximum += 1;
            numericUpDownSceneText.Value = numericUpDownSceneText.Maximum;
            groupBoxSceneText.Text = "Text Edit (" + Convert.ToInt32(numericUpDownSceneText.Value + 1) + " Text(s))";
            textBoxTextOutput.Text = "(set new text here)";
        }

        private void checkBoxSceneScrolling_CheckedChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.Animation.ProgressiveDisplay = checkBoxSceneProgressiveDisplay.Checked;
        }

        private void checkBoxSceneCentered_CheckedChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);
        }

        private void comboBoxSceneFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.VisualStyle.FontSize = (FontMode)comboBoxSceneFontSize.SelectedIndex;
        }

        private void SceneEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonScenePrimaryColor_Click(object sender, EventArgs e)
        {
            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            colorDialog1.Color = textObj.VisualStyle.PrimaryColor[0];
            colorDialog1.CustomColors = new int[] { 0xb17941, 0xffaf55, 0xef9542, 0xd9be8d, 0x36cfff, 0x4254ef, 0xc955ff, 0xef42bb, 0xffce65, 0xf7b982, 0xf18379, 0x66ff83, 0x84dffc, 0x8085f3, 0xd176ff, 0xc074b9, 0x57ccd6 };

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {

                textObj.VisualStyle.PrimaryColor[0] = colorDialog1.Color;
                buttonScenePrimaryColor.BackColor = textObj.VisualStyle.PrimaryColor[0];
                //launchTextDisplayText();
            }
        }

        private void buttonSceneSecondaryColor_Click(object sender, EventArgs e)
        {
            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            colorDialog1.Color = textObj.VisualStyle.SecondaryColor[0];
            colorDialog1.CustomColors = new int[] { 0xe4b88d, 0xe4b88d, 0xbc5746, 0x6d128, 0xb42ff0, 0x3362f, 0x431c19, 0 };

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textObj.VisualStyle.SecondaryColor[0] = colorDialog1.Color;
                buttonSceneSecondaryColor.BackColor = textObj.VisualStyle.SecondaryColor[0];
                //launchTextDisplayText();
            }
        }

        private void textBoxTextOutput_TextChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            Scene.GetTextObject((int)numericUpDownSceneText.Value).Text.SetAsciiText(textBoxTextOutput.Text);
        }
    }
}
