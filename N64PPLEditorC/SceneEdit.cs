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
            for (int i = 0; i < RessourceList.Fib.Count; i++)
            {
                indexData = RessourceList.Get3FIBIndexWithFIBName(RessourceList.Fib[i].RessourceNameString);
                comboBoxSceneAddTexture.Items.Add(RessourceList.Fib[indexData].NameString);
            }

            //text objects
            int nbTextObject = Scene.GetTextObjectCount();
            if (nbTextObject == 0)
            {
                numericUpDownSceneText.Maximum = 0;
            }
            else
            {
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
                if (indexData != -1 /*&& Scene.GetTextureManagementObject(i).isCompressedTexture*/ && (displaySpecificTexture == -1 || displaySpecificTexture == i))
                {
                    try
                    {
                        var bmp = RessourceList.Fib[indexData].Container[0].Bff2.GetBmpTexture();
                        var posY = Scene.GetTextureManagementObject(i).Base.Y;
                        var posX = Scene.GetTextureManagementObject(i).Base.X;
                        this.drawScene1.AddBmp(bmp, new Point(posX, posY));
                    }
                    catch { }
                }
                if (displaySpecificTexture != -1)
                {
                    numericUpDownSceneTexturePosX.Value = Scene.GetTextureManagementObject(displaySpecificTexture).Base.X;
                    numericUpDownSceneTexturePosY.Value = Scene.GetTextureManagementObject(displaySpecificTexture).Base.Y;
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

            labelTextCount.Text = " / " + Scene.GetTextObjectCount().ToString();
            if (nbTextObject == 0)
                return;

            var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);

            
            //Check base
            numericUpDownSceneTextPosX.Value = textObject.Base.X;
            numericUpDownSceneTextPosY.Value = textObject.Base.Y;
            numericUpDownTextId.Value = textObject.Base.Id;

            //Check animation
            checkBoxSceneProgressiveDisplay.Checked = textObject.Flags.IsProgressiveDisplay;
            numericUpDownTextProgressiveSound.Value = textObject.Animation.ProgressiveSound;
            numericUpDownTextLinkedTexture.Value = textObject.Animation.LinkedTextureId;

            //check visual styles
            buttonSceneColor1Primary.BackColor = Color.WhiteSmoke;
            buttonSceneColor2Primary.BackColor = Color.WhiteSmoke;
            buttonSceneColor3Primary.BackColor = Color.WhiteSmoke;
            buttonSceneColor1Secondary.BackColor = Color.WhiteSmoke;
            buttonSceneColor2Secondary.BackColor = Color.WhiteSmoke;
            buttonSceneColor3Secondary.BackColor = Color.WhiteSmoke;
            checkBoxColor1.Checked = false;
            checkBoxColor2.Checked = false;
            checkBoxColor3.Checked = false;

            if (textObject.VisualStyle.PrimaryColor.Count > 0)
            {
                checkBoxColor1.Checked = true;
                buttonSceneColor1Primary.BackColor = textObject.VisualStyle.PrimaryColor[0];
                buttonSceneColor1Secondary.BackColor = textObject.VisualStyle.SecondaryColor[0];
            }
            if (textObject.VisualStyle.PrimaryColor.Count > 1)
            {
                checkBoxColor2.Checked = true;
                buttonSceneColor2Primary.BackColor = textObject.VisualStyle.PrimaryColor[1];
                buttonSceneColor2Secondary.BackColor = textObject.VisualStyle.SecondaryColor[1];
            }
            if (textObject.VisualStyle.PrimaryColor.Count > 2)
            {
                checkBoxColor3.Checked = true;
                buttonSceneColor3Primary.BackColor = textObject.VisualStyle.PrimaryColor[2];
                buttonSceneColor3Secondary.BackColor = textObject.VisualStyle.SecondaryColor[2];
            }

            //check layout
            checkBoxFixedGlyphAdvance.Checked = textObject.Flags.IsFixedGlyphAdvance;
            numericUpDownFixedGlyphAdvance.Value = textObject.Layout.FixedGlyphAdvance;

            checkBoxBoundedLayout.Checked = textObject.Flags.IsBoundedLayout;
            numericUpDownWrapWidth.Value = textObject.Layout.WrapWidth;
            numericUpDownLayoutHeight.Value = textObject.Layout.LayoutHeight;

            //check text output
            textBoxTextOutput.Text = textObject.Text.GetAsciiText();

            //update flags
            checkBoxSceneTextHidden.Checked = textObject.Flags.IsTextHidden;
                
            checkBoxProgressiveSound.Checked = textObject.Flags.IsProgressiveSound;
                
            checkBoxCenterVertically.Checked = textObject.Flags.IsCenterVertically;
            checkBoxUseFixedColor.Checked = textObject.Flags.IsUseFixedColors;
            checkBoxProgressiveSound.Checked = textObject.Flags.IsProgressiveSound;
            checkBoxCenterLineHorizontally.Checked = textObject.Flags.IsCenterHorizontally;
            checkBoxAdditionalGlyphAdvance.Checked = textObject.Flags.IsAdditionalGlyphAdvance;
            checkBoxRuntimeGlyphState.Checked = textObject.Flags.IsRuntimeGlyphState1;
            checkBoxUnk00000002.Checked = textObject.Flags.IsUnknown00000002;
            checkBoxAlternateGlyphRendering.Checked = textObject.Flags.IsAlternateGlyphRendering;
            checkBoxDynamicLayoutRuntimeState.Checked = textObject.Flags.IsDynamicLayoutRuntimeState;
            checkBoxDynamicLayoutText.Checked = textObject.Flags.IsDynamicLayoutText;
            checkBoxAlternateRevealControl.Checked = textObject.Flags.IsAlternateRevealControl;
            checkBoxAlternateTextMode.Checked = textObject.Flags.IsAlternateTextMode;

            //font
            if (textObject.Flags.IsSmallFont)
                comboBoxSceneFontSize.SelectedIndex = 2;
            else if (textObject.Flags.IsNormalFont)
                comboBoxSceneFontSize.SelectedIndex = 1;
            else
                comboBoxSceneFontSize.SelectedIndex = 0;

            //render
            if (textObject.Flags.IsRenderLatePass)
                comboBoxRenderingPass.SelectedIndex = 2;
            else if (textObject.Flags.IsRenderMiddlePass)
                comboBoxRenderingPass.SelectedIndex = 1;
            else
                comboBoxRenderingPass.SelectedIndex = 0;





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
                if (txtObj.Flags.IsCenterHorizontally)
                    txtBox[index].Left += 320/2-size.Width/2;

                if (txtObj.Flags.IsCenterVertically)
                    txtBox[index].Top += 240 / 2 - size.Height / 2;

                if (textBoxTextOutput.Text == txtBox[index].Text && sceneTxt.Count >= 1)
                {
                    txtBox[index].BackColor = Color.LightGreen;
                    txtBox[index].BringToFront();
                }

                drawScene1.Controls.Add(txtBox[index]);
                index++;
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
           //Scene.AddNewTextObject(radioButtonScenesNewScene.Checked);
           // numericUpDownSceneText.Maximum += 1;
           // numericUpDownSceneText.Value = numericUpDownSceneText.Maximum;
           // groupBoxSceneText.Text = "Text Edit (" + Convert.ToInt32(numericUpDownSceneText.Value + 1) + " Text(s))";
           // textBoxTextOutput.Text = "(set new text here)";
        }

        private void checkBoxSceneScrolling_CheckedChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.Flags.IsProgressiveDisplay = checkBoxSceneProgressiveDisplay.Checked;
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
            if (comboBoxSceneFontSize.SelectedIndex == 0)
            {
                textObject.Flags.IsSmallFont = false;
                textObject.Flags.IsNormalFont = false;
            }
            else if(comboBoxSceneFontSize.SelectedIndex == 1)
            {
                textObject.Flags.IsSmallFont = false;
                textObject.Flags.IsNormalFont = true;
            }
            else
            {
                textObject.Flags.IsSmallFont = true;
                textObject.Flags.IsNormalFont = false;
            }
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
                buttonSceneColor1Primary.BackColor = textObj.VisualStyle.PrimaryColor[0];
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
                buttonSceneColor1Secondary.BackColor = textObj.VisualStyle.SecondaryColor[0];
                //launchTextDisplayText();
            }
        }

        private void textBoxTextOutput_TextChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            Scene.GetTextObject((int)numericUpDownSceneText.Value).Text.SetAsciiText(textBoxTextOutput.Text);
        }

        private void numericUpDownTextProgressiveSound_ValueChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.Animation.ProgressiveSound = (ushort)numericUpDownTextProgressiveSound.Value;
        }

        private void numericUpDownTextLinkedTexture_ValueChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.Animation.LinkedTextureId = (ushort)numericUpDownTextLinkedTexture.Value;
        }

        private void comboBoxRenderingPass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            if (comboBoxRenderingPass.SelectedIndex == 0)
            {
                textObject.Flags.IsRenderMiddlePass = false;
                textObject.Flags.IsRenderLatePass = false;
            }
            else if (comboBoxSceneFontSize.SelectedIndex == 1)
            {
                textObject.Flags.IsRenderMiddlePass = true;
                textObject.Flags.IsRenderLatePass = false;
            }
            else
            {
                textObject.Flags.IsRenderMiddlePass = false;
                textObject.Flags.IsRenderLatePass = true;
            }
        }

        private void numericUpDownTextId_ValueChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObject = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObject.Base.Id = (int)numericUpDownTextId.Value;
        }

        private void checkBoxCenterLineHorizontally_CheckedChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObj.Flags.IsCenterHorizontally = checkBoxCenterLineHorizontally.Checked;
            LaunchTextDisplayGroup();
        }

        private void checkBoxCenterVertically_CheckedChanged(object sender, EventArgs e)
        {
            if (InitStep)
                return;

            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObj.Flags.IsCenterVertically = checkBoxCenterVertically.Checked;
            LaunchTextDisplayGroup();
        }

        private void buttonSceneColor2Primary_Click(object sender, EventArgs e)
        {
            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            if(textObj.VisualStyle.PrimaryColor.Count >= 2)
                colorDialog1.Color = textObj.VisualStyle.PrimaryColor[1];
            colorDialog1.CustomColors = new int[] { 0xb17941, 0xffaf55, 0xef9542, 0xd9be8d, 0x36cfff, 0x4254ef, 0xc955ff, 0xef42bb, 0xffce65, 0xf7b982, 0xf18379, 0x66ff83, 0x84dffc, 0x8085f3, 0xd176ff, 0xc074b9, 0x57ccd6 };

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (textObj.VisualStyle.PrimaryColor.Count < 2)
                {
                    textObj.VisualStyle.PrimaryColor.Add(colorDialog1.Color);
                    checkBoxColor2.Checked = true;
                }
                else
                    textObj.VisualStyle.PrimaryColor[1] = colorDialog1.Color;
                buttonSceneColor2Primary.BackColor = textObj.VisualStyle.PrimaryColor[1];
            }
        }

        private void buttonSceneColor2Secondary_Click(object sender, EventArgs e)
        {
            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            if (textObj.VisualStyle.SecondaryColor.Count >= 2)
                colorDialog1.Color = textObj.VisualStyle.SecondaryColor[1];
            colorDialog1.CustomColors = new int[] { 0xb17941, 0xffaf55, 0xef9542, 0xd9be8d, 0x36cfff, 0x4254ef, 0xc955ff, 0xef42bb, 0xffce65, 0xf7b982, 0xf18379, 0x66ff83, 0x84dffc, 0x8085f3, 0xd176ff, 0xc074b9, 0x57ccd6 };

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (textObj.VisualStyle.SecondaryColor.Count < 2)
                {
                    textObj.VisualStyle.SecondaryColor.Add(colorDialog1.Color);
                    checkBoxColor2.Checked = true;
                }
                else
                    textObj.VisualStyle.SecondaryColor[1] = colorDialog1.Color;
                buttonSceneColor2Secondary.BackColor = textObj.VisualStyle.SecondaryColor[1];
            }
        }

        private void buttonSetOnCursorColor2_Click(object sender, EventArgs e)
        {
            textBoxTextOutput.SelectedText = "[9801]";
        }

        private void buttonSetOnCursorColor1_Click(object sender, EventArgs e)
        {
            textBoxTextOutput.SelectedText = "[9800]";
        }

        private void buttonSetOnCursorColor3_Click(object sender, EventArgs e)
        {
            textBoxTextOutput.SelectedText = "[9802]";
        }

        private void buttonSceneColor3Primary_Click(object sender, EventArgs e)
        {
            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            if (textObj.VisualStyle.PrimaryColor.Count >= 3)
                colorDialog1.Color = textObj.VisualStyle.PrimaryColor[2];
            colorDialog1.CustomColors = new int[] { 0xb17941, 0xffaf55, 0xef9542, 0xd9be8d, 0x36cfff, 0x4254ef, 0xc955ff, 0xef42bb, 0xffce65, 0xf7b982, 0xf18379, 0x66ff83, 0x84dffc, 0x8085f3, 0xd176ff, 0xc074b9, 0x57ccd6 };

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (textObj.VisualStyle.PrimaryColor.Count < 3)
                {
                    textObj.VisualStyle.PrimaryColor.Add(colorDialog1.Color);
                    checkBoxColor3.Checked = true;
                }
                else
                    textObj.VisualStyle.PrimaryColor[2] = colorDialog1.Color;
                buttonSceneColor3Primary.BackColor = textObj.VisualStyle.PrimaryColor[2];
            }
        }

        private void buttonSceneColor3Secondary_Click(object sender, EventArgs e)
        {
            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            if (textObj.VisualStyle.SecondaryColor.Count >= 3)
                colorDialog1.Color = textObj.VisualStyle.SecondaryColor[2];
            colorDialog1.CustomColors = new int[] { 0xb17941, 0xffaf55, 0xef9542, 0xd9be8d, 0x36cfff, 0x4254ef, 0xc955ff, 0xef42bb, 0xffce65, 0xf7b982, 0xf18379, 0x66ff83, 0x84dffc, 0x8085f3, 0xd176ff, 0xc074b9, 0x57ccd6 };

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (textObj.VisualStyle.SecondaryColor.Count < 3)
                {
                    textObj.VisualStyle.SecondaryColor.Add(colorDialog1.Color);
                    checkBoxColor3.Checked = true;
                }
                else
                    textObj.VisualStyle.SecondaryColor[2] = colorDialog1.Color;
                buttonSceneColor3Secondary.BackColor = textObj.VisualStyle.SecondaryColor[2];
            }
        }

        private void checkBoxBoundedLayout_CheckedChanged(object sender, EventArgs e)
        {
            var textObj = Scene.GetTextObject((int)numericUpDownSceneText.Value);
            textObj.Flags.IsBoundedLayout = checkBoxBoundedLayout.Checked;
        }

        private void numericUpDownSceneTexturePosX_ValueChanged(object sender, EventArgs e)
        {
            Scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value).Base.X = (int)numericUpDownSceneTexturePosX.Value;
            launchGraphicDisplayPart((int)numericUpDownSceneTexture.Value);
        }

        private void numericUpDownSceneTexturePosY_ValueChanged(object sender, EventArgs e)
        {
            Scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value).Base.Y = (int)numericUpDownSceneTexturePosY.Value;
            launchGraphicDisplayPart((int)numericUpDownSceneTexture.Value);
        }

        private void numericUpDownSceneTexture_ValueChanged(object sender, EventArgs e)
        {
           
            launchGraphicDisplayPart((int)numericUpDownSceneTexture.Value);
            if ((int)numericUpDownSceneTexture.Value == -1)
            {
                numericUpDownSceneTexturePosX.Enabled = false;
                numericUpDownSceneTexturePosY.Enabled = false;
                comboBoxSceneChangeTexture.SelectedIndex = -1;
            }
            else
            {
                //grab index of the selected texture
                var textureSbf = Scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value);
                comboBoxSceneChangeTexture.SelectedIndex = textureSbf.getTextureIndex();
                numericUpDownSceneTexturePosX.Enabled = true;
                numericUpDownSceneTexturePosY.Enabled = true;
                if (textureSbf.transparencybit)
                {
                    checkBoxTexturesExtra1.Checked = true;
                    numericUpDownTextureTransparency.Value = textureSbf.transparency;
                }
                else
                    checkBoxTexturesExtra1.Checked = false;

                //set flags
                checkBoxIsHidden.Checked = textureSbf.Flags.IsHidden;
                checkBoxIsRenderedLate.Checked = textureSbf.Flags.IsRenderedLate;
                checkBoxIsFramePreservedOnShow.Checked = textureSbf.Flags.IsFramePreservedOnShow;
                checkBoxIsCallbackSuppressed.Checked = textureSbf.Flags.IsCallbackSuppressed;
                checkBoxIsCallbackObject.Checked = textureSbf.Flags.IsCallbackObject;
                checkBoxIsNormalBifRenderingSkipped.Checked = textureSbf.Flags.IsNormalBifRenderingSkipped;
                checkBoxHasBifBlendAlphaOverride.Checked = textureSbf.Flags.HasBifBlendAlphaOverride;
                checkBoxHasBifFields94And98.Checked = textureSbf.Flags.HasBifFields94And98;
                checkBoxIsBifBlendAlphaForcedToZero.Checked = textureSbf.Flags.IsBifBlendAlphaForcedToZero;
                checkBoxIsBifPingPongAnimationEnabled.Checked = textureSbf.Flags.IsBifPingPongAnimationEnabled;
                checkBoxUsesCenteredBifAnchor.Checked = textureSbf.Flags.UsesCenteredBifAnchor;
                checkBoxIsBifFlag1000Enabled.Checked = textureSbf.Flags.IsBifFlag1000Enabled;


            }
        }

        private void buttonScenesTextureReplace_Click(object sender, EventArgs e)
        {
            byte indexNewTexture = (byte)comboBoxSceneChangeTexture.SelectedIndex;
            Scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value).BifRessourceIndex = indexNewTexture;
            launchGraphicDisplayPart((int)numericUpDownSceneTexture.Value);
        }

        private void checkBoxTexturesExtra1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxTexturesExtra1.Checked)
            {
              
                var textureObj = Scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value);
                textureObj.transparencybit = false;
            }
        }

        private void numericUpDownTextureTransparency_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxTexturesExtra1.Checked)
            {
              
                var textureObj = Scene.GetTextureManagementObject((int)numericUpDownSceneTexture.Value);
                textureObj.SetTransparencyValue(true, (byte)numericUpDownTextureTransparency.Value);
            }
        }

        private void buttonScenesTextureAdd_Click(object sender, EventArgs e)
        {
            string fibName = this.RessourceList.Fib[comboBoxSceneAddTexture.SelectedIndex].RessourceNameString;

            Sbf.AddBifToSbf(fibName);

            //update the texture (combobox) present in the scene
            comboBoxSceneChangeTexture.Items.Clear();
            for (int i = 0; i < Sbf.GetBifList().Count(); i++)
                comboBoxSceneChangeTexture.Items.Add(Sbf.GetBifName(i).ToLower().Replace(".bif", ""));
        }
    }
}
