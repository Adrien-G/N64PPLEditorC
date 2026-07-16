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
            this.CreateNewContainertoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.labelTextureCompression = new System.Windows.Forms.Label();
            this.checkBoxKeepSameCompression = new System.Windows.Forms.CheckBox();
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
            this.buttonEditScene = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonDynamicObjectExport = new System.Windows.Forms.Button();
            this.labelDynamicObjCount = new System.Windows.Forms.Label();
            this.buttonDynamicObjectRemove = new System.Windows.Forms.Button();
            this.buttonDynamicObjectAdd = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonScenesTextureAddNew = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button4thremove = new System.Windows.Forms.Button();
            this.buttonAdd4thObj = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label4thCount = new System.Windows.Forms.Label();
            this.label4thObjectData = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBoxTextureSBF = new System.Windows.Forms.GroupBox();
            this.drawScene1 = new N64PPLEditorC.TransparentPanel.DrawScene();
            this.treeViewSBF = new System.Windows.Forms.TreeView();
            this.contextMenuStripScenesTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemSaveSBF = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemLoadSBF = new System.Windows.Forms.ToolStripMenuItem();
            this.importNewSBFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemReplaceScene = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExportScene = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageMovies = new System.Windows.Forms.TabPage();
            this.buttonHVQMRemove = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.buttonConvertOldAVI = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonHvqmPathOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonHVQMExtract = new System.Windows.Forms.Button();
            this.buttonHVQMReplace = new System.Windows.Forms.Button();
            this.treeViewHVQM = new System.Windows.Forms.TreeView();
            this.tabPageAudio = new System.Windows.Forms.TabPage();
            this.buttonAudioExtractAllSounds = new System.Windows.Forms.Button();
            this.treeViewAudio = new System.Windows.Forms.TreeView();
            this.contextMenuStripAudioTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemReplaceAudioAllSoundBank = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemReplaceThisSong = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemReplacePointerTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemReplaceWaveTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemReplaceSfxTable = new System.Windows.Forms.ToolStripMenuItem();
            this.extractBinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabPageMisc = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxLaunchEverdrive = new System.Windows.Forms.CheckBox();
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
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.CreateNewContainertoolStripMenuItem,
            this.containerTypetoolStripMenuItem,
            this.toolStripSeparator1,
            this.expandAllToolStripMenuItem,
            this.collpseAllToolStripMenuItem});
            this.contextMenuStripTextureTreeview.Name = "contextMenuStrip1";
            this.contextMenuStripTextureTreeview.Size = new System.Drawing.Size(191, 204);
            this.contextMenuStripTextureTreeview.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripForTreeview_Opening);
            // 
            // addNewTextureToolStripMenuItem
            // 
            this.addNewTextureToolStripMenuItem.Image = global::N64PPLEditorC.Properties.Resources.AddImage_16x;
            this.addNewTextureToolStripMenuItem.Name = "addNewTextureToolStripMenuItem";
            this.addNewTextureToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.addNewTextureToolStripMenuItem.Text = "Add New texture(s)";
            this.addNewTextureToolStripMenuItem.Click += new System.EventHandler(this.addNewTextureToolStripMenuItem_Click);
            // 
            // removeThisTextureToolStripMenuItem
            // 
            this.removeThisTextureToolStripMenuItem.Enabled = false;
            this.removeThisTextureToolStripMenuItem.Image = global::N64PPLEditorC.Properties.Resources.RemoveGuide_16x;
            this.removeThisTextureToolStripMenuItem.Name = "removeThisTextureToolStripMenuItem";
            this.removeThisTextureToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.removeThisTextureToolStripMenuItem.Text = "Remove this texture";
            this.removeThisTextureToolStripMenuItem.Click += new System.EventHandler(this.removeThisTextureToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(187, 6);
            // 
            // ExtractBinaryTextureToolStripMenuItem
            // 
            this.ExtractBinaryTextureToolStripMenuItem.Name = "ExtractBinaryTextureToolStripMenuItem";
            this.ExtractBinaryTextureToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.ExtractBinaryTextureToolStripMenuItem.Text = "Extract binary 3FIB";
            this.ExtractBinaryTextureToolStripMenuItem.Click += new System.EventHandler(this.ExtractBinaryTextureToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(187, 6);
            // 
            // CreateNewContainertoolStripMenuItem
            // 
            this.CreateNewContainertoolStripMenuItem.Name = "CreateNewContainertoolStripMenuItem";
            this.CreateNewContainertoolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.CreateNewContainertoolStripMenuItem.Text = "Create new container";
            this.CreateNewContainertoolStripMenuItem.Click += new System.EventHandler(this.CreateNewContainertoolStripMenuItem_Click);
            // 
            // containerTypetoolStripMenuItem
            // 
            this.containerTypetoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixedToolStripMenuItem,
            this.animatedBadgesToolStripMenuItem,
            this.textureScrollbluePokeballBackgroundToolStripMenuItem});
            this.containerTypetoolStripMenuItem.Name = "containerTypetoolStripMenuItem";
            this.containerTypetoolStripMenuItem.Size = new System.Drawing.Size(190, 26);
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
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Image = global::N64PPLEditorC.Properties.Resources.ExpandAll_16x;
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.expandAllToolStripMenuItem.Text = "Expand all";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collpseAllToolStripMenuItem
            // 
            this.collpseAllToolStripMenuItem.Image = global::N64PPLEditorC.Properties.Resources.CollapseAll_16x;
            this.collpseAllToolStripMenuItem.Name = "collpseAllToolStripMenuItem";
            this.collpseAllToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
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
            this.groupBox2.Controls.Add(this.labelTextureCompression);
            this.groupBox2.Controls.Add(this.checkBoxKeepSameCompression);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericUpDownTextureDisplayTime);
            this.groupBox2.Location = new System.Drawing.Point(411, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 95);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Texture option";
            // 
            // labelTextureCompression
            // 
            this.labelTextureCompression.AutoSize = true;
            this.labelTextureCompression.Location = new System.Drawing.Point(6, 70);
            this.labelTextureCompression.Name = "labelTextureCompression";
            this.labelTextureCompression.Size = new System.Drawing.Size(76, 13);
            this.labelTextureCompression.TabIndex = 12;
            this.labelTextureCompression.Text = "Compression : ";
            // 
            // checkBoxKeepSameCompression
            // 
            this.checkBoxKeepSameCompression.AutoSize = true;
            this.checkBoxKeepSameCompression.Location = new System.Drawing.Point(9, 50);
            this.checkBoxKeepSameCompression.Name = "checkBoxKeepSameCompression";
            this.checkBoxKeepSameCompression.Size = new System.Drawing.Size(142, 17);
            this.checkBoxKeepSameCompression.TabIndex = 15;
            this.checkBoxKeepSameCompression.Text = "Keep same Compression";
            this.checkBoxKeepSameCompression.UseVisualStyleBackColor = true;
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
            this.tabPageScenes.Controls.Add(this.buttonEditScene);
            this.tabPageScenes.Controls.Add(this.tabControl1);
            this.tabPageScenes.Controls.Add(this.groupBoxTextureSBF);
            this.tabPageScenes.Controls.Add(this.treeViewSBF);
            this.tabPageScenes.Location = new System.Drawing.Point(4, 22);
            this.tabPageScenes.Name = "tabPageScenes";
            this.tabPageScenes.Size = new System.Drawing.Size(622, 624);
            this.tabPageScenes.TabIndex = 2;
            this.tabPageScenes.Text = "Scenes";
            // 
            // buttonEditScene
            // 
            this.buttonEditScene.Location = new System.Drawing.Point(520, 265);
            this.buttonEditScene.Name = "buttonEditScene";
            this.buttonEditScene.Size = new System.Drawing.Size(92, 39);
            this.buttonEditScene.TabIndex = 36;
            this.buttonEditScene.Text = "Edit Scene";
            this.buttonEditScene.UseVisualStyleBackColor = true;
            this.buttonEditScene.Click += new System.EventHandler(this.buttonEditScene_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(272, 273);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(342, 300);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonDynamicObjectExport);
            this.tabPage4.Controls.Add(this.labelDynamicObjCount);
            this.tabPage4.Controls.Add(this.buttonDynamicObjectRemove);
            this.tabPage4.Controls.Add(this.buttonDynamicObjectAdd);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(334, 274);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Dynamic Object";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // buttonDynamicObjectExport
            // 
            this.buttonDynamicObjectExport.Location = new System.Drawing.Point(66, 69);
            this.buttonDynamicObjectExport.Name = "buttonDynamicObjectExport";
            this.buttonDynamicObjectExport.Size = new System.Drawing.Size(51, 23);
            this.buttonDynamicObjectExport.TabIndex = 35;
            this.buttonDynamicObjectExport.Text = "Export";
            this.buttonDynamicObjectExport.UseVisualStyleBackColor = true;
            this.buttonDynamicObjectExport.Click += new System.EventHandler(this.buttonDynamicObjectExport_Click);
            // 
            // labelDynamicObjCount
            // 
            this.labelDynamicObjCount.AutoSize = true;
            this.labelDynamicObjCount.Location = new System.Drawing.Point(88, 24);
            this.labelDynamicObjCount.Name = "labelDynamicObjCount";
            this.labelDynamicObjCount.Size = new System.Drawing.Size(10, 13);
            this.labelDynamicObjCount.TabIndex = 34;
            this.labelDynamicObjCount.Text = "-";
            // 
            // buttonDynamicObjectRemove
            // 
            this.buttonDynamicObjectRemove.Location = new System.Drawing.Point(9, 69);
            this.buttonDynamicObjectRemove.Name = "buttonDynamicObjectRemove";
            this.buttonDynamicObjectRemove.Size = new System.Drawing.Size(51, 23);
            this.buttonDynamicObjectRemove.TabIndex = 33;
            this.buttonDynamicObjectRemove.Text = "Remove";
            this.buttonDynamicObjectRemove.UseVisualStyleBackColor = true;
            this.buttonDynamicObjectRemove.Click += new System.EventHandler(this.buttonDynamicObjectRemove_Click);
            // 
            // buttonDynamicObjectAdd
            // 
            this.buttonDynamicObjectAdd.Location = new System.Drawing.Point(8, 40);
            this.buttonDynamicObjectAdd.Name = "buttonDynamicObjectAdd";
            this.buttonDynamicObjectAdd.Size = new System.Drawing.Size(51, 23);
            this.buttonDynamicObjectAdd.TabIndex = 32;
            this.buttonDynamicObjectAdd.Text = "Add";
            this.buttonDynamicObjectAdd.UseVisualStyleBackColor = true;
            this.buttonDynamicObjectAdd.Click += new System.EventHandler(this.buttonDynamicObjectAdd_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 31;
            this.label14.Text = "Count";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(88, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(28, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "data";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(5, 11);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 13);
            this.label20.TabIndex = 29;
            this.label20.Text = "Dynamic Object";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(334, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "textures edit";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonScenesTextureAddNew);
            this.groupBox3.Location = new System.Drawing.Point(9, 156);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(325, 50);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Add new texture to the scene";
            // 
            // buttonScenesTextureAddNew
            // 
            this.buttonScenesTextureAddNew.Location = new System.Drawing.Point(6, 19);
            this.buttonScenesTextureAddNew.Name = "buttonScenesTextureAddNew";
            this.buttonScenesTextureAddNew.Size = new System.Drawing.Size(35, 23);
            this.buttonScenesTextureAddNew.TabIndex = 24;
            this.buttonScenesTextureAddNew.Text = "add";
            this.buttonScenesTextureAddNew.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button4thremove);
            this.tabPage3.Controls.Add(this.buttonAdd4thObj);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label4thCount);
            this.tabPage3.Controls.Add(this.label4thObjectData);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(334, 274);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "4thObject";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button4thremove
            // 
            this.button4thremove.Location = new System.Drawing.Point(9, 68);
            this.button4thremove.Name = "button4thremove";
            this.button4thremove.Size = new System.Drawing.Size(51, 23);
            this.button4thremove.TabIndex = 28;
            this.button4thremove.Text = "Remove";
            this.button4thremove.UseVisualStyleBackColor = true;
            this.button4thremove.Click += new System.EventHandler(this.button4thremove_Click);
            // 
            // buttonAdd4thObj
            // 
            this.buttonAdd4thObj.Location = new System.Drawing.Point(8, 39);
            this.buttonAdd4thObj.Name = "buttonAdd4thObj";
            this.buttonAdd4thObj.Size = new System.Drawing.Size(51, 23);
            this.buttonAdd4thObj.TabIndex = 27;
            this.buttonAdd4thObj.Text = "Add";
            this.buttonAdd4thObj.UseVisualStyleBackColor = true;
            this.buttonAdd4thObj.Click += new System.EventHandler(this.buttonAdd4thObj_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Count";
            // 
            // label4thCount
            // 
            this.label4thCount.AutoSize = true;
            this.label4thCount.Location = new System.Drawing.Point(66, 23);
            this.label4thCount.Name = "label4thCount";
            this.label4thCount.Size = new System.Drawing.Size(10, 13);
            this.label4thCount.TabIndex = 25;
            this.label4thCount.Text = "-";
            // 
            // label4thObjectData
            // 
            this.label4thObjectData.AutoSize = true;
            this.label4thObjectData.Location = new System.Drawing.Point(66, 10);
            this.label4thObjectData.Name = "label4thObjectData";
            this.label4thObjectData.Size = new System.Drawing.Size(28, 13);
            this.label4thObjectData.TabIndex = 24;
            this.label4thObjectData.Text = "data";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "4th object : ";
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
            // drawScene1
            // 
            this.drawScene1.Location = new System.Drawing.Point(12, 16);
            this.drawScene1.Name = "drawScene1";
            this.drawScene1.Size = new System.Drawing.Size(320, 240);
            this.drawScene1.TabIndex = 0;
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
            this.contextMenuStripScenesTreeView.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripScenesTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSaveSBF,
            this.ToolStripMenuItemLoadSBF,
            this.importNewSBFToolStripMenuItem,
            this.ToolStripMenuItemReplaceScene,
            this.ToolStripMenuItemExportScene});
            this.contextMenuStripScenesTreeView.Name = "contextMenuStripScenesTreeView";
            this.contextMenuStripScenesTreeView.Size = new System.Drawing.Size(158, 114);
            this.contextMenuStripScenesTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripScenesTreeView_Opening);
            // 
            // ToolStripMenuItemSaveSBF
            // 
            this.ToolStripMenuItemSaveSBF.Name = "ToolStripMenuItemSaveSBF";
            this.ToolStripMenuItemSaveSBF.Size = new System.Drawing.Size(157, 22);
            this.ToolStripMenuItemSaveSBF.Text = "Save .SBF";
            this.ToolStripMenuItemSaveSBF.Click += new System.EventHandler(this.saveSceneToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemLoadSBF
            // 
            this.ToolStripMenuItemLoadSBF.Name = "ToolStripMenuItemLoadSBF";
            this.ToolStripMenuItemLoadSBF.Size = new System.Drawing.Size(157, 22);
            this.ToolStripMenuItemLoadSBF.Text = "Load .SBF";
            this.ToolStripMenuItemLoadSBF.Click += new System.EventHandler(this.ToolStripMenuItemLoadSBF_Click);
            // 
            // importNewSBFToolStripMenuItem
            // 
            this.importNewSBFToolStripMenuItem.Name = "importNewSBFToolStripMenuItem";
            this.importNewSBFToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.importNewSBFToolStripMenuItem.Text = "Import new SBF";
            this.importNewSBFToolStripMenuItem.Click += new System.EventHandler(this.importNewSBFToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemReplaceScene
            // 
            this.ToolStripMenuItemReplaceScene.Name = "ToolStripMenuItemReplaceScene";
            this.ToolStripMenuItemReplaceScene.Size = new System.Drawing.Size(157, 22);
            this.ToolStripMenuItemReplaceScene.Text = "Replace Scene";
            this.ToolStripMenuItemReplaceScene.Click += new System.EventHandler(this.ToolStripMenuItemReplaceScene_Click);
            // 
            // ToolStripMenuItemExportScene
            // 
            this.ToolStripMenuItemExportScene.Name = "ToolStripMenuItemExportScene";
            this.ToolStripMenuItemExportScene.Size = new System.Drawing.Size(157, 22);
            this.ToolStripMenuItemExportScene.Text = "ExportScene";
            this.ToolStripMenuItemExportScene.Click += new System.EventHandler(this.ToolStripMenuItemExportScene_Click);
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
            this.groupBox5.Size = new System.Drawing.Size(342, 424);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Easy steps for converting AVI to HVQM video";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox8.Location = new System.Drawing.Point(6, 315);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(333, 101);
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
            this.label5.Size = new System.Drawing.Size(332, 65);
            this.label5.TabIndex = 6;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button3);
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.buttonConvertOldAVI);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox7.Location = new System.Drawing.Point(6, 135);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(333, 174);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Step 2 : Convert your video to Good Avi Format";
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button3.Location = new System.Drawing.Point(220, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "converted";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label19.Location = new System.Drawing.Point(6, 138);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(218, 26);
            this.label19.TabIndex = 10;
            this.label19.Text = "- When finished, the file will be placed here : \r\n- And named \"converted.hvqm\"";
            // 
            // buttonConvertOldAVI
            // 
            this.buttonConvertOldAVI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonConvertOldAVI.Location = new System.Drawing.Point(111, 93);
            this.buttonConvertOldAVI.Name = "buttonConvertOldAVI";
            this.buttonConvertOldAVI.Size = new System.Drawing.Size(104, 42);
            this.buttonConvertOldAVI.TabIndex = 9;
            this.buttonConvertOldAVI.Text = "Convert Old AVI format to HVQM";
            this.buttonConvertOldAVI.UseVisualStyleBackColor = true;
            this.buttonConvertOldAVI.Click += new System.EventHandler(this.buttonConvertOldAVI_Click);
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
            this.buttonHvqmPathOpen.Location = new System.Drawing.Point(198, 81);
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
            this.tabPageAudio.Controls.Add(this.buttonAudioExtractAllSounds);
            this.tabPageAudio.Controls.Add(this.treeViewAudio);
            this.tabPageAudio.Location = new System.Drawing.Point(4, 22);
            this.tabPageAudio.Name = "tabPageAudio";
            this.tabPageAudio.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAudio.Size = new System.Drawing.Size(622, 624);
            this.tabPageAudio.TabIndex = 5;
            this.tabPageAudio.Text = "Audio";
            // 
            // buttonAudioExtractAllSounds
            // 
            this.buttonAudioExtractAllSounds.Location = new System.Drawing.Point(272, 24);
            this.buttonAudioExtractAllSounds.Name = "buttonAudioExtractAllSounds";
            this.buttonAudioExtractAllSounds.Size = new System.Drawing.Size(84, 41);
            this.buttonAudioExtractAllSounds.TabIndex = 5;
            this.buttonAudioExtractAllSounds.Text = "Extract all sounds";
            this.buttonAudioExtractAllSounds.UseVisualStyleBackColor = true;
            this.buttonAudioExtractAllSounds.Click += new System.EventHandler(this.buttonAudioExtractAllSounds_Click);
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
            this.toolStripMenuItemReplaceThisSong,
            this.toolStripSeparator3,
            this.toolStripMenuItemReplacePointerTable,
            this.toolStripMenuItemReplaceWaveTable,
            this.toolStripMenuItemReplaceSfxTable,
            this.extractBinToolStripMenuItem});
            this.contextMenuStripAudioTreeview.Name = "contextMenuStripAudioTreeview";
            this.contextMenuStripAudioTreeview.Size = new System.Drawing.Size(201, 142);
            this.contextMenuStripAudioTreeview.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripAudioTreeview_Opening);
            // 
            // toolStripMenuItemReplaceAudioAllSoundBank
            // 
            this.toolStripMenuItemReplaceAudioAllSoundBank.Name = "toolStripMenuItemReplaceAudioAllSoundBank";
            this.toolStripMenuItemReplaceAudioAllSoundBank.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItemReplaceAudioAllSoundBank.Text = "Replace this SoundBank";
            this.toolStripMenuItemReplaceAudioAllSoundBank.Click += new System.EventHandler(this.toolStripMenuItemReplaceAudioAllSoundBank_Click);
            // 
            // toolStripMenuItemReplaceThisSong
            // 
            this.toolStripMenuItemReplaceThisSong.Name = "toolStripMenuItemReplaceThisSong";
            this.toolStripMenuItemReplaceThisSong.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItemReplaceThisSong.Text = "Replace this Song";
            this.toolStripMenuItemReplaceThisSong.Click += new System.EventHandler(this.ToolStripMenuItemReplaceThisSong_Click);
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
            // extractBinToolStripMenuItem
            // 
            this.extractBinToolStripMenuItem.Name = "extractBinToolStripMenuItem";
            this.extractBinToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.extractBinToolStripMenuItem.Text = "Extract Bin";
            this.extractBinToolStripMenuItem.Click += new System.EventHandler(this.extractBinToolStripMenuItem_Click);
            // 
            // TabPageMisc
            // 
            this.TabPageMisc.BackColor = System.Drawing.SystemColors.Control;
            this.TabPageMisc.Controls.Add(this.pictureBox1);
            this.TabPageMisc.Controls.Add(this.button1);
            this.TabPageMisc.Controls.Add(this.checkBox2);
            this.TabPageMisc.Controls.Add(this.checkBox1);
            this.TabPageMisc.Location = new System.Drawing.Point(4, 22);
            this.TabPageMisc.Name = "TabPageMisc";
            this.TabPageMisc.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageMisc.Size = new System.Drawing.Size(622, 624);
            this.TabPageMisc.TabIndex = 6;
            this.TabPageMisc.Text = "Misc. (FR only)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(174, 115);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(314, 195);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 43);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(255, 17);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "No death (infinite game) ( 0x89F2C -> 00000000)";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(268, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "eyecatch passable (adresse : 0x7224 -> 24051000)";
            this.checkBox1.UseVisualStyleBackColor = true;
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 26);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // checkBoxLaunchEverdrive
            // 
            this.checkBoxLaunchEverdrive.AutoSize = true;
            this.checkBoxLaunchEverdrive.Location = new System.Drawing.Point(515, 650);
            this.checkBoxLaunchEverdrive.Name = "checkBoxLaunchEverdrive";
            this.checkBoxLaunchEverdrive.Size = new System.Drawing.Size(124, 17);
            this.checkBoxLaunchEverdrive.TabIndex = 12;
            this.checkBoxLaunchEverdrive.Text = "Launch on everdrive";
            this.checkBoxLaunchEverdrive.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 744);
            this.Controls.Add(this.checkBoxLaunchEverdrive);
            this.Controls.Add(this.comboBoxRessourcesISO);
            this.Controls.Add(this.labelExtendedRom);
            this.Controls.Add(this.buttonModifyRom);
            this.Controls.Add(this.labelFreeSpaceLeft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControlTexMovSce);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "N64 PPL Editor";
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
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
            this.contextMenuStripAudioTreeview.ResumeLayout(false);
            this.TabPageMisc.ResumeLayout(false);
            this.TabPageMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private TransparentPanel.DrawScene drawScene1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelFreeSpaceLeft;
        private System.Windows.Forms.Button buttonHVQMReplace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonHvqmPathOpen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonHVQMExtract;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonConvertOldAVI;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TabPage tabPageTexturesUncompressed;
        private System.Windows.Forms.TreeView treeViewTexturesUncompressed;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.PictureBox pictureBoxUncompressedTexture;
        private System.Windows.Forms.Button buttonUncompressedTextureExtractAll;
        private System.Windows.Forms.Button buttonUncompressedTextureReplace;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonHVQMRemove;
        private System.Windows.Forms.TabPage tabPageAudio;
        private System.Windows.Forms.TreeView treeViewAudio;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAudioTreeview;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplacePointerTable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplaceWaveTable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplaceSfxTable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplaceAudioAllSoundBank;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabPage TabPageMisc;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelExtendedRom;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripScenesTreeView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveSBF;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemLoadSBF;
        private System.Windows.Forms.ToolStripMenuItem ExtractBinaryTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ComboBox comboBoxRessourcesISO;
        private System.Windows.Forms.ToolStripMenuItem CreateNewContainertoolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxKeepSameCompression;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label labelTextureCompression;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonAudioExtractAllSounds;
        private System.Windows.Forms.CheckBox checkBoxLaunchEverdrive;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplaceThisSong;
        private System.Windows.Forms.ToolStripMenuItem importNewSBFToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonScenesTextureAddNew;
        private System.Windows.Forms.ToolStripMenuItem extractBinToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4thObjectData;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label4thCount;
        private System.Windows.Forms.Button buttonAdd4thObj;
        private System.Windows.Forms.Button button4thremove;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label labelDynamicObjCount;
        private System.Windows.Forms.Button buttonDynamicObjectRemove;
        private System.Windows.Forms.Button buttonDynamicObjectAdd;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button buttonDynamicObjectExport;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemReplaceScene;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExportScene;
        private System.Windows.Forms.Button buttonEditScene;
    }
}

