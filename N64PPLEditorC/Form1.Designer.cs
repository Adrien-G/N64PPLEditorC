namespace N64PPLEditorC
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonGetRomFolder = new System.Windows.Forms.Button();
            this.textBoxPPLLocation = new System.Windows.Forms.TextBox();
            this.buttonLoadRom = new System.Windows.Forms.Button();
            this.treeViewTextures = new System.Windows.Forms.TreeView();
            this.contextMenuStripTextureTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeThisTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ExtractBinaryTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.containerTypetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animatedBadgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textureScrollbluePokeballBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collpseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlTexMovSce = new System.Windows.Forms.TabControl();
            this.tabPageTextures = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelIsTextureContainer = new System.Windows.Forms.Label();
            this.pictureBoxTexture = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownTextureDisplayTime = new System.Windows.Forms.NumericUpDown();
            this.buttonExtractAllTextures = new System.Windows.Forms.Button();
            this.tabPageTexturesUncompressed = new System.Windows.Forms.TabPage();
            this.buttonUncompressedTextureReplace = new System.Windows.Forms.Button();
            this.buttonUncompressedTextureExtractAll = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.pictureBoxUncompressedTexture = new System.Windows.Forms.PictureBox();
            this.treeViewTexturesUncompressed = new System.Windows.Forms.TreeView();
            this.tabPageScenes = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.checkBoxScenesIsHidden = new System.Windows.Forms.CheckBox();
            this.checkBoxScenesWaitInput = new System.Windows.Forms.CheckBox();
            this.checkBoxScenesExtra4 = new System.Windows.Forms.CheckBox();
            this.checkBoxScenesForegroundText = new System.Windows.Forms.CheckBox();
            this.checkBoxScenesExtra3 = new System.Windows.Forms.CheckBox();
            this.checkBoxSceneCentered = new System.Windows.Forms.CheckBox();
            this.checkBoxScenesExtra1 = new System.Windows.Forms.CheckBox();
            this.checkBoxSceneScrolling = new System.Windows.Forms.CheckBox();
            this.groupBoxSceneText = new System.Windows.Forms.GroupBox();
            this.buttonScenesAddText = new System.Windows.Forms.Button();
            this.radioButtonSceneSameScene = new System.Windows.Forms.RadioButton();
            this.buttonSceneSuppressText = new System.Windows.Forms.Button();
            this.textBoxSceneText = new System.Windows.Forms.TextBox();
            this.radioButtonScenesNewScene = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownSceneText = new System.Windows.Forms.NumericUpDown();
            this.groupBoxSceneFontColor = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxSceneFontSize = new System.Windows.Forms.ComboBox();
            this.numericUpDownSceneTextPosX = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownSceneTextPosY = new System.Windows.Forms.NumericUpDown();
            this.buttonSceneBackColor = new System.Windows.Forms.Button();
            this.buttonSceneForeColor = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonScenesTextureAdd = new System.Windows.Forms.Button();
            this.groupBoxSceneTextureManagement = new System.Windows.Forms.GroupBox();
            this.buttonScenesTextureReplace = new System.Windows.Forms.Button();
            this.comboBoxSceneChangeTexture = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownSceneTexture = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSceneTexturePosX = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDownSceneTexturePosY = new System.Windows.Forms.NumericUpDown();
            this.comboBoxSceneAddTexture = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBoxTextureSBF = new System.Windows.Forms.GroupBox();
            this.treeViewSBF = new System.Windows.Forms.TreeView();
            this.contextMenuStripScenesTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemSaveSBF = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemLoadSBF = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageMovies = new System.Windows.Forms.TabPage();
            this.buttonHVQMRemove = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonHvqmPathOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonHVQMExtract = new System.Windows.Forms.Button();
            this.buttonHVQMReplace = new System.Windows.Forms.Button();
            this.treeViewHVQM = new System.Windows.Forms.TreeView();
            this.tabPageAudio = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.treeViewAudio = new System.Windows.Forms.TreeView();
            this.contextMenuStripAudioTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemReplaceAudioAllSoundBank = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemReplacePointerTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemReplaceWaveTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemReplaceSfxTable = new System.Windows.Forms.ToolStripMenuItem();
            this.TabPageMisc = new System.Windows.Forms.TabPage();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.MisclabelDifficultyLevel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.MiscTrackBarDifficultyLevel = new System.Windows.Forms.TrackBar();
            this.tabPageDebug = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonModifyRom = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.helpStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.bWDecompress = new System.ComponentModel.BackgroundWorker();
            this.labelFreeSpaceLeft = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelExtendedRom = new System.Windows.Forms.Label();
            this.comboBoxRessourcesISO = new System.Windows.Forms.ComboBox();
            this.drawScene1 = new N64PPLEditorC.TransparentPanel.DrawScene();
            this.groupBox1.SuspendLayout();
            this.contextMenuStripTextureTreeview.SuspendLayout();
            this.tabControlTexMovSce.SuspendLayout();
            this.tabPageTextures.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTexture)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTextureDisplayTime)).BeginInit();
            this.tabPageTexturesUncompressed.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUncompressedTexture)).BeginInit();
            this.tabPageScenes.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBoxSceneText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneText)).BeginInit();
            this.groupBoxSceneFontColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTextPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTextPosY)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBoxSceneTextureManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTexture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTexturePosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTexturePosY)).BeginInit();
            this.groupBoxTextureSBF.SuspendLayout();
            this.contextMenuStripScenesTreeView.SuspendLayout();
            this.tabPageMovies.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPageAudio.SuspendLayout();
            this.contextMenuStripAudioTreeview.SuspendLayout();
            this.TabPageMisc.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MiscTrackBarDifficultyLevel)).BeginInit();
            this.tabPageDebug.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonGetRomFolder);
            this.groupBox1.Controls.Add(this.textBoxPPLLocation);
            this.groupBox1.Controls.Add(this.buttonLoadRom);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 41);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PPL location : ";
            // 
            // buttonGetRomFolder
            // 
            this.buttonGetRomFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetRomFolder.Location = new System.Drawing.Point(498, 17);
            this.buttonGetRomFolder.Name = "buttonGetRomFolder";
            this.buttonGetRomFolder.Size = new System.Drawing.Size(34, 23);
            this.buttonGetRomFolder.TabIndex = 1;
            this.buttonGetRomFolder.Text = "...";
            this.buttonGetRomFolder.UseVisualStyleBackColor = true;
            this.buttonGetRomFolder.Click += new System.EventHandler(this.buttonGetRomFolder_Click);
            this.buttonGetRomFolder.MouseEnter += new System.EventHandler(this.buttonGetRomFolder_MouseEnter);
            this.buttonGetRomFolder.MouseLeave += new System.EventHandler(this.buttonGetRomFolder_MouseLeave);
            // 
            // textBoxPPLLocation
            // 
            this.textBoxPPLLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPPLLocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::N64PPLEditorC.Properties.Settings.Default, "txtPPLLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxPPLLocation.Enabled = false;
            this.textBoxPPLLocation.Location = new System.Drawing.Point(6, 19);
            this.textBoxPPLLocation.Name = "textBoxPPLLocation";
            this.textBoxPPLLocation.Size = new System.Drawing.Size(486, 20);
            this.textBoxPPLLocation.TabIndex = 1;
            this.textBoxPPLLocation.Text = global::N64PPLEditorC.Properties.Settings.Default.txtPPLLocation;
            // 
            // buttonLoadRom
            // 
            this.buttonLoadRom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadRom.Location = new System.Drawing.Point(538, 17);
            this.buttonLoadRom.Name = "buttonLoadRom";
            this.buttonLoadRom.Size = new System.Drawing.Size(82, 23);
            this.buttonLoadRom.TabIndex = 0;
            this.buttonLoadRom.Text = "Load rom";
            this.buttonLoadRom.UseVisualStyleBackColor = true;
            this.buttonLoadRom.Click += new System.EventHandler(this.buttonLoadRom_Click);
            this.buttonLoadRom.MouseEnter += new System.EventHandler(this.buttonLoadRom_MouseEnter);
            this.buttonLoadRom.MouseLeave += new System.EventHandler(this.buttonLoadRom_MouseLeave);
            // 
            // treeViewTextures
            // 
            this.treeViewTextures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewTextures.ContextMenuStrip = this.contextMenuStripTextureTreeview;
            this.treeViewTextures.Location = new System.Drawing.Point(0, 0);
            this.treeViewTextures.Name = "treeViewTextures";
            this.treeViewTextures.Size = new System.Drawing.Size(266, 624);
            this.treeViewTextures.TabIndex = 1;
            this.treeViewTextures.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTextures_AfterSelect);
            this.treeViewTextures.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewTextures_NodeMouseClick);
            this.treeViewTextures.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeViewTextures_KeyDown);
            this.treeViewTextures.MouseEnter += new System.EventHandler(this.treeViewTextures_MouseEnter);
            this.treeViewTextures.MouseLeave += new System.EventHandler(this.treeViewTextures_MouseLeave);
            // 
            // contextMenuStripTextureTreeview
            // 
            this.contextMenuStripTextureTreeview.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripTextureTreeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewTextureToolStripMenuItem,
            this.removeThisTextureToolStripMenuItem,
            this.toolStripSeparator4,
            this.ExtractBinaryTextureToolStripMenuItem,
            this.toolStripSeparator2,
            this.containerTypetoolStripMenuItem,
            this.toolStripSeparator1,
            this.expandAllToolStripMenuItem,
            this.collpseAllToolStripMenuItem});
            this.contextMenuStripTextureTreeview.Name = "contextMenuStrip1";
            this.contextMenuStripTextureTreeview.Size = new System.Drawing.Size(184, 178);
            this.contextMenuStripTextureTreeview.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripForTreeview_Opening);
            // 
            // addNewTextureToolStripMenuItem
            // 
            this.addNewTextureToolStripMenuItem.Image = global::N64PPLEditorC.Properties.Resources.AddImage_16x;
            this.addNewTextureToolStripMenuItem.Name = "addNewTextureToolStripMenuItem";
            this.addNewTextureToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.addNewTextureToolStripMenuItem.Text = "Add New texture(s)";
            this.addNewTextureToolStripMenuItem.Click += new System.EventHandler(this.addNewTextureToolStripMenuItem_Click);
            // 
            // removeThisTextureToolStripMenuItem
            // 
            this.removeThisTextureToolStripMenuItem.Enabled = false;
            this.removeThisTextureToolStripMenuItem.Image = global::N64PPLEditorC.Properties.Resources.RemoveGuide_16x;
            this.removeThisTextureToolStripMenuItem.Name = "removeThisTextureToolStripMenuItem";
            this.removeThisTextureToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.removeThisTextureToolStripMenuItem.Text = "Remove this texture";
            this.removeThisTextureToolStripMenuItem.Click += new System.EventHandler(this.removeThisTextureToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(180, 6);
            // 
            // ExtractBinaryTextureToolStripMenuItem
            // 
            this.ExtractBinaryTextureToolStripMenuItem.Name = "ExtractBinaryTextureToolStripMenuItem";
            this.ExtractBinaryTextureToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.ExtractBinaryTextureToolStripMenuItem.Text = "Extract binary 3FIB";
            this.ExtractBinaryTextureToolStripMenuItem.Click += new System.EventHandler(this.ExtractBinaryTextureToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // containerTypetoolStripMenuItem
            // 
            this.containerTypetoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixedToolStripMenuItem,
            this.animatedBadgesToolStripMenuItem,
            this.textureScrollbluePokeballBackgroundToolStripMenuItem});
            this.containerTypetoolStripMenuItem.Name = "containerTypetoolStripMenuItem";
            this.containerTypetoolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.containerTypetoolStripMenuItem.Text = "Container Type";
            // 
            // fixedToolStripMenuItem
            // 
            this.fixedToolStripMenuItem.Name = "fixedToolStripMenuItem";
            this.fixedToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.fixedToolStripMenuItem.Text = "Fixed";
            this.fixedToolStripMenuItem.Click += new System.EventHandler(this.fixedToolStripMenuItem_Click);
            // 
            // animatedBadgesToolStripMenuItem
            // 
            this.animatedBadgesToolStripMenuItem.Name = "animatedBadgesToolStripMenuItem";
            this.animatedBadgesToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.animatedBadgesToolStripMenuItem.Text = "Animated (Badges)";
            this.animatedBadgesToolStripMenuItem.Click += new System.EventHandler(this.animatedBadgesToolStripMenuItem_Click);
            // 
            // textureScrollbluePokeballBackgroundToolStripMenuItem
            // 
            this.textureScrollbluePokeballBackgroundToolStripMenuItem.Name = "textureScrollbluePokeballBackgroundToolStripMenuItem";
            this.textureScrollbluePokeballBackgroundToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.textureScrollbluePokeballBackgroundToolStripMenuItem.Text = "Texture scroll (blue pokeball background)";
            this.textureScrollbluePokeballBackgroundToolStripMenuItem.Click += new System.EventHandler(this.textureScrollbluePokeballBackgroundToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Image = global::N64PPLEditorC.Properties.Resources.ExpandAll_16x;
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.expandAllToolStripMenuItem.Text = "Expand all";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collpseAllToolStripMenuItem
            // 
            this.collpseAllToolStripMenuItem.Image = global::N64PPLEditorC.Properties.Resources.CollapseAll_16x;
            this.collpseAllToolStripMenuItem.Name = "collpseAllToolStripMenuItem";
            this.collpseAllToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.collpseAllToolStripMenuItem.Text = "Collapse all";
            this.collpseAllToolStripMenuItem.Click += new System.EventHandler(this.collpseAllToolStripMenuItem_Click);
            // 
            // tabControlTexMovSce
            // 
            this.tabControlTexMovSce.Controls.Add(this.tabPageTextures);
            this.tabControlTexMovSce.Controls.Add(this.tabPageTexturesUncompressed);
            this.tabControlTexMovSce.Controls.Add(this.tabPageScenes);
            this.tabControlTexMovSce.Controls.Add(this.tabPageMovies);
            this.tabControlTexMovSce.Controls.Add(this.tabPageAudio);
            this.tabControlTexMovSce.Controls.Add(this.TabPageMisc);
            this.tabControlTexMovSce.Controls.Add(this.tabPageDebug);
            this.tabControlTexMovSce.Enabled = false;
            this.tabControlTexMovSce.Location = new System.Drawing.Point(12, 69);
            this.tabControlTexMovSce.Name = "tabControlTexMovSce";
            this.tabControlTexMovSce.SelectedIndex = 0;
            this.tabControlTexMovSce.Size = new System.Drawing.Size(630, 650);
            this.tabControlTexMovSce.TabIndex = 4;
            // 
            // tabPageTextures
            // 
            this.tabPageTextures.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageTextures.Controls.Add(this.groupBox4);
            this.tabPageTextures.Controls.Add(this.groupBox2);
            this.tabPageTextures.Controls.Add(this.treeViewTextures);
            this.tabPageTextures.Controls.Add(this.buttonExtractAllTextures);
            this.tabPageTextures.Location = new System.Drawing.Point(4, 22);
            this.tabPageTextures.Name = "tabPageTextures";
            this.tabPageTextures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTextures.Size = new System.Drawing.Size(622, 624);
            this.tabPageTextures.TabIndex = 0;
            this.tabPageTextures.Text = "Textures";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.labelIsTextureContainer);
            this.groupBox4.Controls.Add(this.pictureBoxTexture);
            this.groupBox4.Location = new System.Drawing.Point(281, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(335, 267);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Render";
            // 
            // labelIsTextureContainer
            // 
            this.labelIsTextureContainer.AutoSize = true;
            this.labelIsTextureContainer.Location = new System.Drawing.Point(57, 129);
            this.labelIsTextureContainer.Name = "labelIsTextureContainer";
            this.labelIsTextureContainer.Size = new System.Drawing.Size(212, 26);
            this.labelIsTextureContainer.TabIndex = 1;
            this.labelIsTextureContainer.Text = "Please select a texture inside the container.\r\n    (Right click on the list for m" +
    "ore options)";
            this.labelIsTextureContainer.Visible = false;
            // 
            // pictureBoxTexture
            // 
            this.pictureBoxTexture.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxTexture.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxTexture.Name = "pictureBoxTexture";
            this.pictureBoxTexture.Size = new System.Drawing.Size(320, 240);
            this.pictureBoxTexture.TabIndex = 0;
            this.pictureBoxTexture.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericUpDownTextureDisplayTime);
            this.groupBox2.Location = new System.Drawing.Point(411, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 46);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Texture option";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Texture display time :";
            // 
            // numericUpDownTextureDisplayTime
            // 
            this.numericUpDownTextureDisplayTime.Location = new System.Drawing.Point(130, 19);
            this.numericUpDownTextureDisplayTime.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownTextureDisplayTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTextureDisplayTime.Name = "numericUpDownTextureDisplayTime";
            this.numericUpDownTextureDisplayTime.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownTextureDisplayTime.TabIndex = 13;
            this.numericUpDownTextureDisplayTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTextureDisplayTime.ValueChanged += new System.EventHandler(this.numericUpDownTextureDisplayTime_ValueChanged);
            // 
            // buttonExtractAllTextures
            // 
            this.buttonExtractAllTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExtractAllTextures.Location = new System.Drawing.Point(281, 279);
            this.buttonExtractAllTextures.Name = "buttonExtractAllTextures";
            this.buttonExtractAllTextures.Size = new System.Drawing.Size(124, 47);
            this.buttonExtractAllTextures.TabIndex = 7;
            this.buttonExtractAllTextures.Text = "Extract all textures";
            this.buttonExtractAllTextures.UseVisualStyleBackColor = true;
            this.buttonExtractAllTextures.Click += new System.EventHandler(this.buttonExtractAllTextures_Click);
            // 
            // tabPageTexturesUncompressed
            // 
            this.tabPageTexturesUncompressed.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageTexturesUncompressed.Controls.Add(this.buttonUncompressedTextureReplace);
            this.tabPageTexturesUncompressed.Controls.Add(this.buttonUncompressedTextureExtractAll);
            this.tabPageTexturesUncompressed.Controls.Add(this.groupBox10);
            this.tabPageTexturesUncompressed.Controls.Add(this.treeViewTexturesUncompressed);
            this.tabPageTexturesUncompressed.Location = new System.Drawing.Point(4, 22);
            this.tabPageTexturesUncompressed.Name = "tabPageTexturesUncompressed";
            this.tabPageTexturesUncompressed.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTexturesUncompressed.Size = new System.Drawing.Size(622, 624);
            this.tabPageTexturesUncompressed.TabIndex = 4;
            this.tabPageTexturesUncompressed.Text = "Textures (uncompressed)";
            // 
            // buttonUncompressedTextureReplace
            // 
            this.buttonUncompressedTextureReplace.Location = new System.Drawing.Point(353, 521);
            this.buttonUncompressedTextureReplace.Name = "buttonUncompressedTextureReplace";
            this.buttonUncompressedTextureReplace.Size = new System.Drawing.Size(98, 23);
            this.buttonUncompressedTextureReplace.TabIndex = 10;
            this.buttonUncompressedTextureReplace.Text = "Replace Texture";
            this.buttonUncompressedTextureReplace.UseVisualStyleBackColor = true;
            this.buttonUncompressedTextureReplace.Click += new System.EventHandler(this.buttonUncompressedTextureReplace_Click);
            // 
            // buttonUncompressedTextureExtractAll
            // 
            this.buttonUncompressedTextureExtractAll.Location = new System.Drawing.Point(272, 521);
            this.buttonUncompressedTextureExtractAll.Name = "buttonUncompressedTextureExtractAll";
            this.buttonUncompressedTextureExtractAll.Size = new System.Drawing.Size(75, 23);
            this.buttonUncompressedTextureExtractAll.TabIndex = 9;
            this.buttonUncompressedTextureExtractAll.Text = "Extract All";
            this.buttonUncompressedTextureExtractAll.UseVisualStyleBackColor = true;
            this.buttonUncompressedTextureExtractAll.Click += new System.EventHandler(this.buttonUncompressedTextureExtractAll_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.pictureBoxUncompressedTexture);
            this.groupBox10.Location = new System.Drawing.Point(272, 6);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(335, 509);
            this.groupBox10.TabIndex = 8;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Render";
            // 
            // pictureBoxUncompressedTexture
            // 
            this.pictureBoxUncompressedTexture.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxUncompressedTexture.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxUncompressedTexture.Name = "pictureBoxUncompressedTexture";
            this.pictureBoxUncompressedTexture.Size = new System.Drawing.Size(320, 482);
            this.pictureBoxUncompressedTexture.TabIndex = 0;
            this.pictureBoxUncompressedTexture.TabStop = false;
            // 
            // treeViewTexturesUncompressed
            // 
            this.treeViewTexturesUncompressed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewTexturesUncompressed.Location = new System.Drawing.Point(0, 0);
            this.treeViewTexturesUncompressed.Name = "treeViewTexturesUncompressed";
            this.treeViewTexturesUncompressed.Size = new System.Drawing.Size(266, 624);
            this.treeViewTexturesUncompressed.TabIndex = 2;
            this.treeViewTexturesUncompressed.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTexturesUncompressed_AfterSelect);
            // 
            // tabPageScenes
            // 
            this.tabPageScenes.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageScenes.Controls.Add(this.tabControl1);
            this.tabPageScenes.Controls.Add(this.groupBoxTextureSBF);
            this.tabPageScenes.Controls.Add(this.treeViewSBF);
            this.tabPageScenes.Location = new System.Drawing.Point(4, 22);
            this.tabPageScenes.Name = "tabPageScenes";
            this.tabPageScenes.Size = new System.Drawing.Size(622, 624);
            this.tabPageScenes.TabIndex = 2;
            this.tabPageScenes.Text = "Scenes";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(272, 273);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(342, 300);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox9);
            this.tabPage1.Controls.Add(this.groupBoxSceneText);
            this.tabPage1.Controls.Add(this.groupBoxSceneFontColor);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(334, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text edit";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.checkBoxScenesIsHidden);
            this.groupBox9.Controls.Add(this.checkBoxScenesWaitInput);
            this.groupBox9.Controls.Add(this.checkBoxScenesExtra4);
            this.groupBox9.Controls.Add(this.checkBoxScenesForegroundText);
            this.groupBox9.Controls.Add(this.checkBoxScenesExtra3);
            this.groupBox9.Controls.Add(this.checkBoxSceneCentered);
            this.groupBox9.Controls.Add(this.checkBoxScenesExtra1);
            this.groupBox9.Controls.Add(this.checkBoxSceneScrolling);
            this.groupBox9.Location = new System.Drawing.Point(6, 170);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(275, 98);
            this.groupBox9.TabIndex = 16;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Text Effects";
            // 
            // checkBoxScenesIsHidden
            // 
            this.checkBoxScenesIsHidden.AutoSize = true;
            this.checkBoxScenesIsHidden.Location = new System.Drawing.Point(102, 19);
            this.checkBoxScenesIsHidden.Name = "checkBoxScenesIsHidden";
            this.checkBoxScenesIsHidden.Size = new System.Drawing.Size(60, 17);
            this.checkBoxScenesIsHidden.TabIndex = 21;
            this.checkBoxScenesIsHidden.Text = "Hidden";
            this.checkBoxScenesIsHidden.UseVisualStyleBackColor = true;
            this.checkBoxScenesIsHidden.CheckedChanged += new System.EventHandler(this.checkBoxScenesIsHidden_CheckedChanged);
            // 
            // checkBoxScenesWaitInput
            // 
            this.checkBoxScenesWaitInput.AutoSize = true;
            this.checkBoxScenesWaitInput.Location = new System.Drawing.Point(6, 70);
            this.checkBoxScenesWaitInput.Name = "checkBoxScenesWaitInput";
            this.checkBoxScenesWaitInput.Size = new System.Drawing.Size(74, 17);
            this.checkBoxScenesWaitInput.TabIndex = 20;
            this.checkBoxScenesWaitInput.Text = "Wait input";
            this.checkBoxScenesWaitInput.UseVisualStyleBackColor = true;
            this.checkBoxScenesWaitInput.CheckedChanged += new System.EventHandler(this.checkBoxScenesWaitInput_CheckedChanged);
            // 
            // checkBoxScenesExtra4
            // 
            this.checkBoxScenesExtra4.AutoSize = true;
            this.checkBoxScenesExtra4.Location = new System.Drawing.Point(102, 70);
            this.checkBoxScenesExtra4.Name = "checkBoxScenesExtra4";
            this.checkBoxScenesExtra4.Size = new System.Drawing.Size(90, 17);
            this.checkBoxScenesExtra4.TabIndex = 19;
            this.checkBoxScenesExtra4.Text = "has font color";
            this.checkBoxScenesExtra4.UseVisualStyleBackColor = true;
            this.checkBoxScenesExtra4.CheckedChanged += new System.EventHandler(this.checkBoxScenesExtra4_CheckedChanged);
            // 
            // checkBoxScenesForegroundText
            // 
            this.checkBoxScenesForegroundText.AutoSize = true;
            this.checkBoxScenesForegroundText.Location = new System.Drawing.Point(5, 53);
            this.checkBoxScenesForegroundText.Name = "checkBoxScenesForegroundText";
            this.checkBoxScenesForegroundText.Size = new System.Drawing.Size(91, 17);
            this.checkBoxScenesForegroundText.TabIndex = 16;
            this.checkBoxScenesForegroundText.Text = "always on top";
            this.checkBoxScenesForegroundText.UseVisualStyleBackColor = true;
            this.checkBoxScenesForegroundText.CheckedChanged += new System.EventHandler(this.checkBoxScenesForegroundText_CheckedChanged);
            // 
            // checkBoxScenesExtra3
            // 
            this.checkBoxScenesExtra3.AutoSize = true;
            this.checkBoxScenesExtra3.Location = new System.Drawing.Point(102, 53);
            this.checkBoxScenesExtra3.Name = "checkBoxScenesExtra3";
            this.checkBoxScenesExtra3.Size = new System.Drawing.Size(171, 17);
            this.checkBoxScenesExtra3.TabIndex = 18;
            this.checkBoxScenesExtra3.Text = "Extra3 (personalized space 21)";
            this.checkBoxScenesExtra3.UseVisualStyleBackColor = true;
            this.checkBoxScenesExtra3.CheckedChanged += new System.EventHandler(this.checkBoxScenesExtra3_CheckedChanged);
            // 
            // checkBoxSceneCentered
            // 
            this.checkBoxSceneCentered.AutoSize = true;
            this.checkBoxSceneCentered.Location = new System.Drawing.Point(6, 36);
            this.checkBoxSceneCentered.Name = "checkBoxSceneCentered";
            this.checkBoxSceneCentered.Size = new System.Drawing.Size(69, 17);
            this.checkBoxSceneCentered.TabIndex = 15;
            this.checkBoxSceneCentered.Text = "Centered";
            this.checkBoxSceneCentered.UseVisualStyleBackColor = true;
            this.checkBoxSceneCentered.CheckedChanged += new System.EventHandler(this.checkBoxSceneCentered_CheckedChanged);
            // 
            // checkBoxScenesExtra1
            // 
            this.checkBoxScenesExtra1.AutoSize = true;
            this.checkBoxScenesExtra1.Location = new System.Drawing.Point(102, 36);
            this.checkBoxScenesExtra1.Name = "checkBoxScenesExtra1";
            this.checkBoxScenesExtra1.Size = new System.Drawing.Size(112, 17);
            this.checkBoxScenesExtra1.TabIndex = 17;
            this.checkBoxScenesExtra1.Text = "Extra1 (unknow 2)";
            this.checkBoxScenesExtra1.UseVisualStyleBackColor = true;
            this.checkBoxScenesExtra1.CheckedChanged += new System.EventHandler(this.checkBoxScenesExtra1_CheckedChanged);
            // 
            // checkBoxSceneScrolling
            // 
            this.checkBoxSceneScrolling.AutoSize = true;
            this.checkBoxSceneScrolling.Location = new System.Drawing.Point(6, 19);
            this.checkBoxSceneScrolling.Name = "checkBoxSceneScrolling";
            this.checkBoxSceneScrolling.Size = new System.Drawing.Size(66, 17);
            this.checkBoxSceneScrolling.TabIndex = 14;
            this.checkBoxSceneScrolling.Text = "Scrolling";
            this.checkBoxSceneScrolling.UseVisualStyleBackColor = true;
            this.checkBoxSceneScrolling.CheckedChanged += new System.EventHandler(this.checkBoxSceneTextScrolling_CheckedChanged);
            // 
            // groupBoxSceneText
            // 
            this.groupBoxSceneText.Controls.Add(this.buttonScenesAddText);
            this.groupBoxSceneText.Controls.Add(this.radioButtonSceneSameScene);
            this.groupBoxSceneText.Controls.Add(this.buttonSceneSuppressText);
            this.groupBoxSceneText.Controls.Add(this.textBoxSceneText);
            this.groupBoxSceneText.Controls.Add(this.radioButtonScenesNewScene);
            this.groupBoxSceneText.Controls.Add(this.label1);
            this.groupBoxSceneText.Controls.Add(this.numericUpDownSceneText);
            this.groupBoxSceneText.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSceneText.Name = "groupBoxSceneText";
            this.groupBoxSceneText.Size = new System.Drawing.Size(190, 161);
            this.groupBoxSceneText.TabIndex = 5;
            this.groupBoxSceneText.TabStop = false;
            this.groupBoxSceneText.Text = "Text management";
            // 
            // buttonScenesAddText
            // 
            this.buttonScenesAddText.Location = new System.Drawing.Point(3, 107);
            this.buttonScenesAddText.Name = "buttonScenesAddText";
            this.buttonScenesAddText.Size = new System.Drawing.Size(84, 23);
            this.buttonScenesAddText.TabIndex = 5;
            this.buttonScenesAddText.Text = "add new text";
            this.buttonScenesAddText.UseVisualStyleBackColor = true;
            this.buttonScenesAddText.Click += new System.EventHandler(this.buttonScenesAddText_Click);
            // 
            // radioButtonSceneSameScene
            // 
            this.radioButtonSceneSameScene.AutoSize = true;
            this.radioButtonSceneSameScene.Checked = true;
            this.radioButtonSceneSameScene.Location = new System.Drawing.Point(94, 106);
            this.radioButtonSceneSameScene.Name = "radioButtonSceneSameScene";
            this.radioButtonSceneSameScene.Size = new System.Drawing.Size(93, 17);
            this.radioButtonSceneSameScene.TabIndex = 14;
            this.radioButtonSceneSameScene.TabStop = true;
            this.radioButtonSceneSameScene.Text = "in same scene";
            this.radioButtonSceneSameScene.UseVisualStyleBackColor = true;
            // 
            // buttonSceneSuppressText
            // 
            this.buttonSceneSuppressText.Location = new System.Drawing.Point(3, 136);
            this.buttonSceneSuppressText.Name = "buttonSceneSuppressText";
            this.buttonSceneSuppressText.Size = new System.Drawing.Size(85, 23);
            this.buttonSceneSuppressText.TabIndex = 13;
            this.buttonSceneSuppressText.Text = "Remove text";
            this.buttonSceneSuppressText.UseVisualStyleBackColor = true;
            this.buttonSceneSuppressText.Click += new System.EventHandler(this.buttonSceneSuppressText_Click);
            // 
            // textBoxSceneText
            // 
            this.textBoxSceneText.Location = new System.Drawing.Point(3, 43);
            this.textBoxSceneText.Multiline = true;
            this.textBoxSceneText.Name = "textBoxSceneText";
            this.textBoxSceneText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSceneText.Size = new System.Drawing.Size(179, 58);
            this.textBoxSceneText.TabIndex = 3;
            this.textBoxSceneText.TextChanged += new System.EventHandler(this.textBoxSceneText_TextChanged);
            this.textBoxSceneText.Leave += new System.EventHandler(this.textBoxSceneText_Leave);
            // 
            // radioButtonScenesNewScene
            // 
            this.radioButtonScenesNewScene.AutoSize = true;
            this.radioButtonScenesNewScene.Location = new System.Drawing.Point(94, 123);
            this.radioButtonScenesNewScene.Name = "radioButtonScenesNewScene";
            this.radioButtonScenesNewScene.Size = new System.Drawing.Size(88, 17);
            this.radioButtonScenesNewScene.TabIndex = 15;
            this.radioButtonScenesNewScene.Text = "in new scene";
            this.radioButtonScenesNewScene.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text Selection :";
            // 
            // numericUpDownSceneText
            // 
            this.numericUpDownSceneText.Location = new System.Drawing.Point(93, 17);
            this.numericUpDownSceneText.Name = "numericUpDownSceneText";
            this.numericUpDownSceneText.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownSceneText.TabIndex = 1;
            this.numericUpDownSceneText.ValueChanged += new System.EventHandler(this.numericUpDownSceneText_ValueChanged);
            // 
            // groupBoxSceneFontColor
            // 
            this.groupBoxSceneFontColor.Controls.Add(this.label10);
            this.groupBoxSceneFontColor.Controls.Add(this.label9);
            this.groupBoxSceneFontColor.Controls.Add(this.comboBoxSceneFontSize);
            this.groupBoxSceneFontColor.Controls.Add(this.numericUpDownSceneTextPosX);
            this.groupBoxSceneFontColor.Controls.Add(this.label12);
            this.groupBoxSceneFontColor.Controls.Add(this.label8);
            this.groupBoxSceneFontColor.Controls.Add(this.label7);
            this.groupBoxSceneFontColor.Controls.Add(this.numericUpDownSceneTextPosY);
            this.groupBoxSceneFontColor.Controls.Add(this.buttonSceneBackColor);
            this.groupBoxSceneFontColor.Controls.Add(this.buttonSceneForeColor);
            this.groupBoxSceneFontColor.Location = new System.Drawing.Point(196, 3);
            this.groupBoxSceneFontColor.Name = "groupBoxSceneFontColor";
            this.groupBoxSceneFontColor.Size = new System.Drawing.Size(152, 161);
            this.groupBoxSceneFontColor.TabIndex = 12;
            this.groupBoxSceneFontColor.TabStop = false;
            this.groupBoxSceneFontColor.Text = "Font and position";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Size :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 134);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Position Y : ";
            // 
            // comboBoxSceneFontSize
            // 
            this.comboBoxSceneFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSceneFontSize.FormattingEnabled = true;
            this.comboBoxSceneFontSize.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Big"});
            this.comboBoxSceneFontSize.Location = new System.Drawing.Point(45, 82);
            this.comboBoxSceneFontSize.Name = "comboBoxSceneFontSize";
            this.comboBoxSceneFontSize.Size = new System.Drawing.Size(87, 21);
            this.comboBoxSceneFontSize.TabIndex = 16;
            this.comboBoxSceneFontSize.SelectedIndexChanged += new System.EventHandler(this.comboBoxSceneFontSize_SelectedIndexChanged);
            // 
            // numericUpDownSceneTextPosX
            // 
            this.numericUpDownSceneTextPosX.Location = new System.Drawing.Point(75, 110);
            this.numericUpDownSceneTextPosX.Maximum = new decimal(new int[] {
            320,
            0,
            0,
            0});
            this.numericUpDownSceneTextPosX.Name = "numericUpDownSceneTextPosX";
            this.numericUpDownSceneTextPosX.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownSceneTextPosX.TabIndex = 8;
            this.numericUpDownSceneTextPosX.ValueChanged += new System.EventHandler(this.numericUpDownScenePosX_ValueChanged);
            this.numericUpDownSceneTextPosX.Leave += new System.EventHandler(this.numericUpDownScenePosX_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "BackColor :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Position X : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "ForeColor :";
            // 
            // numericUpDownSceneTextPosY
            // 
            this.numericUpDownSceneTextPosY.Location = new System.Drawing.Point(75, 132);
            this.numericUpDownSceneTextPosY.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.numericUpDownSceneTextPosY.Name = "numericUpDownSceneTextPosY";
            this.numericUpDownSceneTextPosY.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownSceneTextPosY.TabIndex = 9;
            this.numericUpDownSceneTextPosY.ValueChanged += new System.EventHandler(this.numericUpDownScenePosY_ValueChanged);
            // 
            // buttonSceneBackColor
            // 
            this.buttonSceneBackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSceneBackColor.Location = new System.Drawing.Point(68, 47);
            this.buttonSceneBackColor.Name = "buttonSceneBackColor";
            this.buttonSceneBackColor.Size = new System.Drawing.Size(64, 26);
            this.buttonSceneBackColor.TabIndex = 11;
            this.buttonSceneBackColor.Text = "Set";
            this.buttonSceneBackColor.UseVisualStyleBackColor = true;
            this.buttonSceneBackColor.Click += new System.EventHandler(this.buttonSceneBackColor_Click);
            // 
            // buttonSceneForeColor
            // 
            this.buttonSceneForeColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSceneForeColor.Location = new System.Drawing.Point(70, 15);
            this.buttonSceneForeColor.Name = "buttonSceneForeColor";
            this.buttonSceneForeColor.Size = new System.Drawing.Size(62, 26);
            this.buttonSceneForeColor.TabIndex = 10;
            this.buttonSceneForeColor.Text = "Set";
            this.buttonSceneForeColor.UseVisualStyleBackColor = true;
            this.buttonSceneForeColor.Click += new System.EventHandler(this.buttonSceneForeColor_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.buttonScenesTextureAdd);
            this.tabPage2.Controls.Add(this.groupBoxSceneTextureManagement);
            this.tabPage2.Controls.Add(this.comboBoxSceneAddTexture);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(334, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "textures edit";
            // 
            // buttonScenesTextureAdd
            // 
            this.buttonScenesTextureAdd.Location = new System.Drawing.Point(182, 176);
            this.buttonScenesTextureAdd.Name = "buttonScenesTextureAdd";
            this.buttonScenesTextureAdd.Size = new System.Drawing.Size(35, 23);
            this.buttonScenesTextureAdd.TabIndex = 24;
            this.buttonScenesTextureAdd.Text = "add";
            this.buttonScenesTextureAdd.UseVisualStyleBackColor = true;
            this.buttonScenesTextureAdd.Click += new System.EventHandler(this.buttonScenesTextureAdd_Click);
            // 
            // groupBoxSceneTextureManagement
            // 
            this.groupBoxSceneTextureManagement.Controls.Add(this.buttonScenesTextureReplace);
            this.groupBoxSceneTextureManagement.Controls.Add(this.comboBoxSceneChangeTexture);
            this.groupBoxSceneTextureManagement.Controls.Add(this.label17);
            this.groupBoxSceneTextureManagement.Controls.Add(this.numericUpDownSceneTexture);
            this.groupBoxSceneTextureManagement.Controls.Add(this.numericUpDownSceneTexturePosX);
            this.groupBoxSceneTextureManagement.Controls.Add(this.label16);
            this.groupBoxSceneTextureManagement.Controls.Add(this.label18);
            this.groupBoxSceneTextureManagement.Controls.Add(this.numericUpDownSceneTexturePosY);
            this.groupBoxSceneTextureManagement.Enabled = false;
            this.groupBoxSceneTextureManagement.Location = new System.Drawing.Point(6, 6);
            this.groupBoxSceneTextureManagement.Name = "groupBoxSceneTextureManagement";
            this.groupBoxSceneTextureManagement.Size = new System.Drawing.Size(325, 100);
            this.groupBoxSceneTextureManagement.TabIndex = 22;
            this.groupBoxSceneTextureManagement.TabStop = false;
            this.groupBoxSceneTextureManagement.Text = "Texture Management";
            // 
            // buttonScenesTextureReplace
            // 
            this.buttonScenesTextureReplace.Location = new System.Drawing.Point(268, 11);
            this.buttonScenesTextureReplace.Name = "buttonScenesTextureReplace";
            this.buttonScenesTextureReplace.Size = new System.Drawing.Size(57, 23);
            this.buttonScenesTextureReplace.TabIndex = 26;
            this.buttonScenesTextureReplace.Text = "replace";
            this.buttonScenesTextureReplace.UseVisualStyleBackColor = true;
            this.buttonScenesTextureReplace.Click += new System.EventHandler(this.buttonScenesTextureReplace_Click);
            // 
            // comboBoxSceneChangeTexture
            // 
            this.comboBoxSceneChangeTexture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSceneChangeTexture.FormattingEnabled = true;
            this.comboBoxSceneChangeTexture.Location = new System.Drawing.Point(163, 13);
            this.comboBoxSceneChangeTexture.Name = "comboBoxSceneChangeTexture";
            this.comboBoxSceneChangeTexture.Size = new System.Drawing.Size(99, 21);
            this.comboBoxSceneChangeTexture.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 64);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "Position Y : ";
            // 
            // numericUpDownSceneTexture
            // 
            this.numericUpDownSceneTexture.Location = new System.Drawing.Point(108, 14);
            this.numericUpDownSceneTexture.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownSceneTexture.Name = "numericUpDownSceneTexture";
            this.numericUpDownSceneTexture.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownSceneTexture.TabIndex = 20;
            this.numericUpDownSceneTexture.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownSceneTexture.ValueChanged += new System.EventHandler(this.numericUpDownSceneTexture_ValueChanged);
            // 
            // numericUpDownSceneTexturePosX
            // 
            this.numericUpDownSceneTexturePosX.Location = new System.Drawing.Point(75, 40);
            this.numericUpDownSceneTexturePosX.Maximum = new decimal(new int[] {
            320,
            0,
            0,
            0});
            this.numericUpDownSceneTexturePosX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownSceneTexturePosX.Name = "numericUpDownSceneTexturePosX";
            this.numericUpDownSceneTexturePosX.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownSceneTexturePosX.TabIndex = 20;
            this.numericUpDownSceneTexturePosX.ValueChanged += new System.EventHandler(this.numericUpDownSceneTexturePosX_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "Texture Selection :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 42);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(63, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = "Position X : ";
            // 
            // numericUpDownSceneTexturePosY
            // 
            this.numericUpDownSceneTexturePosY.Location = new System.Drawing.Point(75, 62);
            this.numericUpDownSceneTexturePosY.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.numericUpDownSceneTexturePosY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownSceneTexturePosY.Name = "numericUpDownSceneTexturePosY";
            this.numericUpDownSceneTexturePosY.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownSceneTexturePosY.TabIndex = 21;
            this.numericUpDownSceneTexturePosY.ValueChanged += new System.EventHandler(this.numericUpDownSceneTexturePosY_ValueChanged);
            // 
            // comboBoxSceneAddTexture
            // 
            this.comboBoxSceneAddTexture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSceneAddTexture.FormattingEnabled = true;
            this.comboBoxSceneAddTexture.Location = new System.Drawing.Point(77, 176);
            this.comboBoxSceneAddTexture.Name = "comboBoxSceneAddTexture";
            this.comboBoxSceneAddTexture.Size = new System.Drawing.Size(99, 21);
            this.comboBoxSceneAddTexture.TabIndex = 23;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(38, 181);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 22;
            this.label19.Text = "add : ";
            // 
            // groupBoxTextureSBF
            // 
            this.groupBoxTextureSBF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTextureSBF.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxTextureSBF.Controls.Add(this.drawScene1);
            this.groupBoxTextureSBF.Location = new System.Drawing.Point(269, 3);
            this.groupBoxTextureSBF.Name = "groupBoxTextureSBF";
            this.groupBoxTextureSBF.Size = new System.Drawing.Size(350, 264);
            this.groupBoxTextureSBF.TabIndex = 1;
            this.groupBoxTextureSBF.TabStop = false;
            this.groupBoxTextureSBF.Text = "Scene Editor";
            // 
            // treeViewSBF
            // 
            this.treeViewSBF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewSBF.ContextMenuStrip = this.contextMenuStripScenesTreeView;
            this.treeViewSBF.Location = new System.Drawing.Point(0, 0);
            this.treeViewSBF.Name = "treeViewSBF";
            this.treeViewSBF.Size = new System.Drawing.Size(266, 624);
            this.treeViewSBF.TabIndex = 3;
            this.treeViewSBF.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSBF_AfterSelect);
            this.treeViewSBF.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewSBF_NodeMouseClick);
            // 
            // contextMenuStripScenesTreeView
            // 
            this.contextMenuStripScenesTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSaveSBF,
            this.ToolStripMenuItemLoadSBF});
            this.contextMenuStripScenesTreeView.Name = "contextMenuStripScenesTreeView";
            this.contextMenuStripScenesTreeView.Size = new System.Drawing.Size(126, 48);
            this.contextMenuStripScenesTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripScenesTreeView_Opening);
            // 
            // ToolStripMenuItemSaveSBF
            // 
            this.ToolStripMenuItemSaveSBF.Name = "ToolStripMenuItemSaveSBF";
            this.ToolStripMenuItemSaveSBF.Size = new System.Drawing.Size(125, 22);
            this.ToolStripMenuItemSaveSBF.Text = "Save .SBF";
            this.ToolStripMenuItemSaveSBF.Click += new System.EventHandler(this.saveSceneToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemLoadSBF
            // 
            this.ToolStripMenuItemLoadSBF.Name = "ToolStripMenuItemLoadSBF";
            this.ToolStripMenuItemLoadSBF.Size = new System.Drawing.Size(125, 22);
            this.ToolStripMenuItemLoadSBF.Text = "Load .SBF";
            this.ToolStripMenuItemLoadSBF.Click += new System.EventHandler(this.ToolStripMenuItemLoadSBF_Click);
            // 
            // tabPageMovies
            // 
            this.tabPageMovies.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMovies.Controls.Add(this.buttonHVQMRemove);
            this.tabPageMovies.Controls.Add(this.groupBox5);
            this.tabPageMovies.Controls.Add(this.buttonHVQMExtract);
            this.tabPageMovies.Controls.Add(this.buttonHVQMReplace);
            this.tabPageMovies.Controls.Add(this.treeViewHVQM);
            this.tabPageMovies.Location = new System.Drawing.Point(4, 22);
            this.tabPageMovies.Name = "tabPageMovies";
            this.tabPageMovies.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMovies.Size = new System.Drawing.Size(622, 624);
            this.tabPageMovies.TabIndex = 1;
            this.tabPageMovies.Text = "Movies";
            // 
            // buttonHVQMRemove
            // 
            this.buttonHVQMRemove.Location = new System.Drawing.Point(434, 6);
            this.buttonHVQMRemove.Name = "buttonHVQMRemove";
            this.buttonHVQMRemove.Size = new System.Drawing.Size(75, 35);
            this.buttonHVQMRemove.TabIndex = 12;
            this.buttonHVQMRemove.Text = "remove HVQM";
            this.buttonHVQMRemove.UseVisualStyleBackColor = true;
            this.buttonHVQMRemove.Click += new System.EventHandler(this.buttonHVQMRemove_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox8);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Location = new System.Drawing.Point(272, 47);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(342, 379);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Easy steps for converting AVI to HVQM video";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox8.Location = new System.Drawing.Point(6, 287);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(333, 83);
            this.groupBox8.TabIndex = 8;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Optimal Settings (Best compression)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(6, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(332, 52);
            this.label5.TabIndex = 6;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button3);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox7.Location = new System.Drawing.Point(6, 135);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(333, 146);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Step 2 : Convert your video to Good Avi Format";
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button3.Location = new System.Drawing.Point(111, 93);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 42);
            this.button3.TabIndex = 9;
            this.button3.Text = "Convert Old AVI format to HVQM";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(3, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(316, 65);
            this.label6.TabIndex = 7;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.buttonHvqmPathOpen);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox6.Location = new System.Drawing.Point(6, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(333, 110);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Step 1 : Grab the requirements";
            // 
            // buttonHvqmPathOpen
            // 
            this.buttonHvqmPathOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonHvqmPathOpen.Location = new System.Drawing.Point(189, 82);
            this.buttonHvqmPathOpen.Name = "buttonHvqmPathOpen";
            this.buttonHvqmPathOpen.Size = new System.Drawing.Size(62, 23);
            this.buttonHvqmPathOpen.TabIndex = 5;
            this.buttonHvqmPathOpen.Text = "hvq2enc";
            this.buttonHvqmPathOpen.UseVisualStyleBackColor = true;
            this.buttonHvqmPathOpen.Click += new System.EventHandler(this.buttonHvqmPathOpen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 78);
            this.label3.TabIndex = 4;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // buttonHVQMExtract
            // 
            this.buttonHVQMExtract.Location = new System.Drawing.Point(353, 6);
            this.buttonHVQMExtract.Name = "buttonHVQMExtract";
            this.buttonHVQMExtract.Size = new System.Drawing.Size(75, 35);
            this.buttonHVQMExtract.TabIndex = 8;
            this.buttonHVQMExtract.Text = "Extract HVQM";
            this.buttonHVQMExtract.UseVisualStyleBackColor = true;
            this.buttonHVQMExtract.Click += new System.EventHandler(this.buttonHVQMExtract_Click);
            // 
            // buttonHVQMReplace
            // 
            this.buttonHVQMReplace.Location = new System.Drawing.Point(272, 6);
            this.buttonHVQMReplace.Name = "buttonHVQMReplace";
            this.buttonHVQMReplace.Size = new System.Drawing.Size(75, 35);
            this.buttonHVQMReplace.TabIndex = 3;
            this.buttonHVQMReplace.Text = "Replace HVQM";
            this.buttonHVQMReplace.UseVisualStyleBackColor = true;
            this.buttonHVQMReplace.Click += new System.EventHandler(this.buttonHVQMReplace_Click);
            // 
            // treeViewHVQM
            // 
            this.treeViewHVQM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewHVQM.Location = new System.Drawing.Point(0, 0);
            this.treeViewHVQM.Name = "treeViewHVQM";
            this.treeViewHVQM.Size = new System.Drawing.Size(266, 624);
            this.treeViewHVQM.TabIndex = 2;
            // 
            // tabPageAudio
            // 
            this.tabPageAudio.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAudio.Controls.Add(this.label13);
            this.tabPageAudio.Controls.Add(this.treeViewAudio);
            this.tabPageAudio.Location = new System.Drawing.Point(4, 22);
            this.tabPageAudio.Name = "tabPageAudio";
            this.tabPageAudio.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAudio.Size = new System.Drawing.Size(622, 624);
            this.tabPageAudio.TabIndex = 5;
            this.tabPageAudio.Text = "Audio";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(341, 110);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(192, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Right click directly in the list for options.";
            // 
            // treeViewAudio
            // 
            this.treeViewAudio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewAudio.ContextMenuStrip = this.contextMenuStripAudioTreeview;
            this.treeViewAudio.Location = new System.Drawing.Point(0, 0);
            this.treeViewAudio.Name = "treeViewAudio";
            this.treeViewAudio.Size = new System.Drawing.Size(266, 624);
            this.treeViewAudio.TabIndex = 3;
            this.treeViewAudio.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewAudio_NodeMouseClick);
            // 
            // contextMenuStripAudioTreeview
            // 
            this.contextMenuStripAudioTreeview.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripAudioTreeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemReplaceAudioAllSoundBank,
            this.toolStripSeparator3,
            this.toolStripMenuItemReplacePointerTable,
            this.toolStripMenuItemReplaceWaveTable,
            this.toolStripMenuItemReplaceSfxTable});
            this.contextMenuStripAudioTreeview.Name = "contextMenuStripAudioTreeview";
            this.contextMenuStripAudioTreeview.Size = new System.Drawing.Size(201, 98);
            this.contextMenuStripAudioTreeview.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripAudioTreeview_Opening);
            // 
            // toolStripMenuItemReplaceAudioAllSoundBank
            // 
            this.toolStripMenuItemReplaceAudioAllSoundBank.Name = "toolStripMenuItemReplaceAudioAllSoundBank";
            this.toolStripMenuItemReplaceAudioAllSoundBank.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItemReplaceAudioAllSoundBank.Text = "Replace this SoundBank";
            this.toolStripMenuItemReplaceAudioAllSoundBank.Click += new System.EventHandler(this.toolStripMenuItemReplaceAudioAllSoundBank_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(197, 6);
            // 
            // toolStripMenuItemReplacePointerTable
            // 
            this.toolStripMenuItemReplacePointerTable.Name = "toolStripMenuItemReplacePointerTable";
            this.toolStripMenuItemReplacePointerTable.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItemReplacePointerTable.Text = "Replace Pointer Table";
            this.toolStripMenuItemReplacePointerTable.Click += new System.EventHandler(this.toolStripMenuItemReplacePointerTable_Click);
            // 
            // toolStripMenuItemReplaceWaveTable
            // 
            this.toolStripMenuItemReplaceWaveTable.Name = "toolStripMenuItemReplaceWaveTable";
            this.toolStripMenuItemReplaceWaveTable.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItemReplaceWaveTable.Text = "Replace WaveTable";
            this.toolStripMenuItemReplaceWaveTable.Click += new System.EventHandler(this.toolStripMenuItemReplaceWaveTable_Click);
            // 
            // toolStripMenuItemReplaceSfxTable
            // 
            this.toolStripMenuItemReplaceSfxTable.Name = "toolStripMenuItemReplaceSfxTable";
            this.toolStripMenuItemReplaceSfxTable.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItemReplaceSfxTable.Text = "Replace Sfx";
            this.toolStripMenuItemReplaceSfxTable.Click += new System.EventHandler(this.toolStripMenuItemReplaceSfxTable_Click);
            // 
            // TabPageMisc
            // 
            this.TabPageMisc.BackColor = System.Drawing.SystemColors.Control;
            this.TabPageMisc.Controls.Add(this.checkBox2);
            this.TabPageMisc.Controls.Add(this.checkBox1);
            this.TabPageMisc.Controls.Add(this.groupBox11);
            this.TabPageMisc.Location = new System.Drawing.Point(4, 22);
            this.TabPageMisc.Name = "TabPageMisc";
            this.TabPageMisc.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageMisc.Size = new System.Drawing.Size(622, 624);
            this.TabPageMisc.TabIndex = 6;
            this.TabPageMisc.Text = "Misc. (FR only)";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(140, 289);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(255, 17);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "No death (infinite game) ( 0x89F2C -> 00000000)";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(140, 266);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(268, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "eyecatch passable (adresse : 0x7224 -> 24051000)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.MisclabelDifficultyLevel);
            this.groupBox11.Controls.Add(this.label15);
            this.groupBox11.Controls.Add(this.label14);
            this.groupBox11.Controls.Add(this.label11);
            this.groupBox11.Controls.Add(this.MiscTrackBarDifficultyLevel);
            this.groupBox11.Location = new System.Drawing.Point(6, 6);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(240, 113);
            this.groupBox11.TabIndex = 0;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "CPU Difficulty (all levels)";
            // 
            // MisclabelDifficultyLevel
            // 
            this.MisclabelDifficultyLevel.AutoSize = true;
            this.MisclabelDifficultyLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MisclabelDifficultyLevel.Location = new System.Drawing.Point(201, 63);
            this.MisclabelDifficultyLevel.Name = "MisclabelDifficultyLevel";
            this.MisclabelDifficultyLevel.Size = new System.Drawing.Size(14, 13);
            this.MisclabelDifficultyLevel.TabIndex = 4;
            this.MisclabelDifficultyLevel.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(159, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Harder";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(82, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Medium";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.LimeGreen;
            this.label11.Location = new System.Drawing.Point(3, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Easier";
            // 
            // MiscTrackBarDifficultyLevel
            // 
            this.MiscTrackBarDifficultyLevel.Location = new System.Drawing.Point(6, 48);
            this.MiscTrackBarDifficultyLevel.Maximum = 7;
            this.MiscTrackBarDifficultyLevel.Minimum = -7;
            this.MiscTrackBarDifficultyLevel.Name = "MiscTrackBarDifficultyLevel";
            this.MiscTrackBarDifficultyLevel.Size = new System.Drawing.Size(189, 45);
            this.MiscTrackBarDifficultyLevel.TabIndex = 0;
            this.MiscTrackBarDifficultyLevel.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.MiscTrackBarDifficultyLevel.Value = -7;
            this.MiscTrackBarDifficultyLevel.Scroll += new System.EventHandler(this.MiscTrackBarDifficultyLevel_Scroll);
            // 
            // tabPageDebug
            // 
            this.tabPageDebug.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageDebug.Controls.Add(this.groupBox3);
            this.tabPageDebug.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebug.Name = "tabPageDebug";
            this.tabPageDebug.Size = new System.Drawing.Size(622, 624);
            this.tabPageDebug.TabIndex = 3;
            this.tabPageDebug.Text = "Debug";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(12, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(402, 222);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Debug";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(287, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(280, 172);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 16;
            this.button4.Text = "op";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(124, 159);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 16;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(124, 138);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 15;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(275, 111);
            this.textBox1.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonModifyRom
            // 
            this.buttonModifyRom.Enabled = false;
            this.buttonModifyRom.Location = new System.Drawing.Point(550, 670);
            this.buttonModifyRom.Name = "buttonModifyRom";
            this.buttonModifyRom.Size = new System.Drawing.Size(82, 40);
            this.buttonModifyRom.TabIndex = 12;
            this.buttonModifyRom.Text = "modify ROM";
            this.buttonModifyRom.UseVisualStyleBackColor = true;
            this.buttonModifyRom.Click += new System.EventHandler(this.buttonModifyRom_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 722);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(642, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // helpStatus
            // 
            this.helpStatus.Name = "helpStatus";
            this.helpStatus.Size = new System.Drawing.Size(627, 17);
            this.helpStatus.Spring = true;
            // 
            // bWDecompress
            // 
            this.bWDecompress.WorkerReportsProgress = true;
            this.bWDecompress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDecompress_DoWork);
            this.bWDecompress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWDecompress_ProgressChanged);
            this.bWDecompress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDecompress_RunWorkerCompleted);
            // 
            // labelFreeSpaceLeft
            // 
            this.labelFreeSpaceLeft.AutoSize = true;
            this.labelFreeSpaceLeft.Location = new System.Drawing.Point(425, 688);
            this.labelFreeSpaceLeft.Name = "labelFreeSpaceLeft";
            this.labelFreeSpaceLeft.Size = new System.Drawing.Size(13, 13);
            this.labelFreeSpaceLeft.TabIndex = 17;
            this.labelFreeSpaceLeft.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 688);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Free space left in the ROM :";
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // labelExtendedRom
            // 
            this.labelExtendedRom.AutoSize = true;
            this.labelExtendedRom.Location = new System.Drawing.Point(320, 700);
            this.labelExtendedRom.Name = "labelExtendedRom";
            this.labelExtendedRom.Size = new System.Drawing.Size(77, 13);
            this.labelExtendedRom.TabIndex = 18;
            this.labelExtendedRom.Text = "(extended rom)";
            this.labelExtendedRom.Visible = false;
            // 
            // comboBoxRessourcesISO
            // 
            this.comboBoxRessourcesISO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRessourcesISO.FormattingEnabled = true;
            this.comboBoxRessourcesISO.Items.AddRange(new object[] {
            "ressources 1",
            "ressources 2",
            "ressources 3",
            "ressources 4",
            "ressources 5",
            "ressources 6",
            "ressources 7"});
            this.comboBoxRessourcesISO.Location = new System.Drawing.Point(514, 58);
            this.comboBoxRessourcesISO.Name = "comboBoxRessourcesISO";
            this.comboBoxRessourcesISO.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRessourcesISO.TabIndex = 12;
            this.comboBoxRessourcesISO.Visible = false;
            this.comboBoxRessourcesISO.SelectedIndexChanged += new System.EventHandler(this.comboBoxRessourcesISO_SelectedIndexChanged);
            // 
            // drawScene1
            // 
            this.drawScene1.Location = new System.Drawing.Point(12, 16);
            this.drawScene1.Name = "drawScene1";
            this.drawScene1.Size = new System.Drawing.Size(320, 240);
            this.drawScene1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 744);
            this.Controls.Add(this.comboBoxRessourcesISO);
            this.Controls.Add(this.labelExtendedRom);
            this.Controls.Add(this.buttonModifyRom);
            this.Controls.Add(this.labelFreeSpaceLeft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControlTexMovSce);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "PPL - Rom Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStripTextureTreeview.ResumeLayout(false);
            this.tabControlTexMovSce.ResumeLayout(false);
            this.tabPageTextures.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTexture)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTextureDisplayTime)).EndInit();
            this.tabPageTexturesUncompressed.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUncompressedTexture)).EndInit();
            this.tabPageScenes.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBoxSceneText.ResumeLayout(false);
            this.groupBoxSceneText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneText)).EndInit();
            this.groupBoxSceneFontColor.ResumeLayout(false);
            this.groupBoxSceneFontColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTextPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTextPosY)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBoxSceneTextureManagement.ResumeLayout(false);
            this.groupBoxSceneTextureManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTexture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTexturePosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSceneTexturePosY)).EndInit();
            this.groupBoxTextureSBF.ResumeLayout(false);
            this.contextMenuStripScenesTreeView.ResumeLayout(false);
            this.tabPageMovies.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPageAudio.ResumeLayout(false);
            this.tabPageAudio.PerformLayout();
            this.contextMenuStripAudioTreeview.ResumeLayout(false);
            this.TabPageMisc.ResumeLayout(false);
            this.TabPageMisc.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MiscTrackBarDifficultyLevel)).EndInit();
            this.tabPageDebug.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonGetRomFolder;
        private System.Windows.Forms.TextBox textBoxPPLLocation;
        private System.Windows.Forms.Button buttonLoadRom;
        private System.Windows.Forms.TreeView treeViewTextures;
        private System.Windows.Forms.TabControl tabControlTexMovSce;
        private System.Windows.Forms.TabPage tabPageTextures;
        private System.Windows.Forms.TabPage tabPageMovies;
        private System.Windows.Forms.TabPage tabPageScenes;
        private System.Windows.Forms.TreeView treeViewHVQM;
        private System.Windows.Forms.TreeView treeViewSBF;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonExtractAllTextures;
        private System.Windows.Forms.PictureBox pictureBoxTexture;
        private System.Windows.Forms.NumericUpDown numericUpDownTextureDisplayTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel helpStatus;
        private System.ComponentModel.BackgroundWorker bWDecompress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTextureTreeview;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collpseAllToolStripMenuItem;
        private System.Windows.Forms.Label labelIsTextureContainer;
        private System.Windows.Forms.Button buttonModifyRom;
        private System.Windows.Forms.ToolStripMenuItem addNewTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeThisTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem containerTypetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animatedBadgesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textureScrollbluePokeballBackgroundToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxTextureSBF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownSceneText;
        private System.Windows.Forms.Button button1;
        private TransparentPanel.DrawScene drawScene1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelFreeSpaceLeft;
        private System.Windows.Forms.TabPage tabPageDebug;
        private System.Windows.Forms.Button buttonHVQMReplace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonHvqmPathOpen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonHVQMExtract;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBoxSceneText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownSceneTextPosY;
        private System.Windows.Forms.NumericUpDown numericUpDownSceneTextPosX;
        private System.Windows.Forms.TabPage tabPageTexturesUncompressed;
        private System.Windows.Forms.TreeView treeViewTexturesUncompressed;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.PictureBox pictureBoxUncompressedTexture;
        private System.Windows.Forms.Button buttonUncompressedTextureExtractAll;
        private System.Windows.Forms.Button buttonUncompressedTextureReplace;
        private System.Windows.Forms.Button buttonScenesAddText;
        private System.Windows.Forms.TextBox textBoxSceneText;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonSceneBackColor;
        private System.Windows.Forms.Button buttonSceneForeColor;
        private System.Windows.Forms.GroupBox groupBoxSceneFontColor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSceneSuppressText;
        private System.Windows.Forms.Button buttonHVQMRemove;
        private System.Windows.Forms.TabPage tabPageAudio;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TreeView treeViewAudio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAudioTreeview;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplacePointerTable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplaceWaveTable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplaceSfxTable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplaceAudioAllSoundBank;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.CheckBox checkBoxSceneScrolling;
        private System.Windows.Forms.CheckBox checkBoxSceneCentered;
        private System.Windows.Forms.ComboBox comboBoxSceneFontSize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton radioButtonSceneSameScene;
        private System.Windows.Forms.RadioButton radioButtonScenesNewScene;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox checkBoxScenesForegroundText;
        private System.Windows.Forms.CheckBox checkBoxScenesExtra1;
        private System.Windows.Forms.CheckBox checkBoxScenesExtra3;
        private System.Windows.Forms.CheckBox checkBoxScenesWaitInput;
        private System.Windows.Forms.CheckBox checkBoxScenesIsHidden;
        private System.Windows.Forms.TabPage TabPageMisc;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label MisclabelDifficultyLevel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar MiscTrackBarDifficultyLevel;
        private System.Windows.Forms.CheckBox checkBoxScenesExtra4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numericUpDownSceneTexture;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBoxSceneTextureManagement;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericUpDownSceneTexturePosX;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numericUpDownSceneTexturePosY;
        private System.Windows.Forms.ComboBox comboBoxSceneAddTexture;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonScenesTextureAdd;
        private System.Windows.Forms.Label labelExtendedRom;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox comboBoxSceneChangeTexture;
        private System.Windows.Forms.Button buttonScenesTextureReplace;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripScenesTreeView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveSBF;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemLoadSBF;
        private System.Windows.Forms.ToolStripMenuItem ExtractBinaryTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ComboBox comboBoxRessourcesISO;
    }
}

