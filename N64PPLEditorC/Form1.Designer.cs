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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonGetRomFolder = new System.Windows.Forms.Button();
            this.textBoxPPLLocation = new System.Windows.Forms.TextBox();
            this.buttonLoadRom = new System.Windows.Forms.Button();
            this.treeViewTextures = new System.Windows.Forms.TreeView();
            this.contextMenuStripForTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collpseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTextures = new System.Windows.Forms.TabPage();
            this.tabPageMovies = new System.Windows.Forms.TabPage();
            this.treeViewHVQM = new System.Windows.Forms.TreeView();
            this.tabPageScenes = new System.Windows.Forms.TabPage();
            this.treeViewSBF = new System.Windows.Forms.TreeView();
            this.groupBoxTextureContainer = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonShowTexture = new System.Windows.Forms.Button();
            this.groupBoxTextures = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBoxTexture = new System.Windows.Forms.PictureBox();
            this.buttonExtractAllTextures = new System.Windows.Forms.Button();
            this.checkBoxAlwaysShowTexture = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.helpStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerExtract = new System.Windows.Forms.Timer(this.components);
            this.bWDecompress = new System.ComponentModel.BackgroundWorker();
            this.labelIsTextureContainer = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.contextMenuStripForTreeview.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageTextures.SuspendLayout();
            this.tabPageMovies.SuspendLayout();
            this.tabPageScenes.SuspendLayout();
            this.groupBoxTextureContainer.SuspendLayout();
            this.groupBoxTextures.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTexture)).BeginInit();
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
            this.groupBox1.Size = new System.Drawing.Size(622, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PPL location : ";
            // 
            // buttonGetRomFolder
            // 
            this.buttonGetRomFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetRomFolder.Location = new System.Drawing.Point(494, 17);
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
            this.textBoxPPLLocation.Size = new System.Drawing.Size(482, 20);
            this.textBoxPPLLocation.TabIndex = 1;
            this.textBoxPPLLocation.Text = global::N64PPLEditorC.Properties.Settings.Default.txtPPLLocation;
            // 
            // buttonLoadRom
            // 
            this.buttonLoadRom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadRom.Location = new System.Drawing.Point(534, 17);
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
            this.treeViewTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewTextures.ContextMenuStrip = this.contextMenuStripForTreeview;
            this.treeViewTextures.Location = new System.Drawing.Point(0, 0);
            this.treeViewTextures.Name = "treeViewTextures";
            this.treeViewTextures.Size = new System.Drawing.Size(257, 578);
            this.treeViewTextures.TabIndex = 1;
            this.treeViewTextures.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTextures_AfterSelect);
            // 
            // contextMenuStripForTreeview
            // 
            this.contextMenuStripForTreeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandAllToolStripMenuItem,
            this.collpseAllToolStripMenuItem});
            this.contextMenuStripForTreeview.Name = "contextMenuStrip1";
            this.contextMenuStripForTreeview.Size = new System.Drawing.Size(135, 48);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.expandAllToolStripMenuItem.Text = "Expand all";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collpseAllToolStripMenuItem
            // 
            this.collpseAllToolStripMenuItem.Name = "collpseAllToolStripMenuItem";
            this.collpseAllToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.collpseAllToolStripMenuItem.Text = "Collapse all";
            this.collpseAllToolStripMenuItem.Click += new System.EventHandler(this.collpseAllToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTextures);
            this.tabControl1.Controls.Add(this.tabPageMovies);
            this.tabControl1.Controls.Add(this.tabPageScenes);
            this.tabControl1.Location = new System.Drawing.Point(12, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(265, 604);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPageTextures
            // 
            this.tabPageTextures.Controls.Add(this.treeViewTextures);
            this.tabPageTextures.Location = new System.Drawing.Point(4, 22);
            this.tabPageTextures.Name = "tabPageTextures";
            this.tabPageTextures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTextures.Size = new System.Drawing.Size(257, 578);
            this.tabPageTextures.TabIndex = 0;
            this.tabPageTextures.Text = "Textures";
            this.tabPageTextures.UseVisualStyleBackColor = true;
            // 
            // tabPageMovies
            // 
            this.tabPageMovies.Controls.Add(this.treeViewHVQM);
            this.tabPageMovies.Location = new System.Drawing.Point(4, 22);
            this.tabPageMovies.Name = "tabPageMovies";
            this.tabPageMovies.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMovies.Size = new System.Drawing.Size(257, 578);
            this.tabPageMovies.TabIndex = 1;
            this.tabPageMovies.Text = "Movies";
            this.tabPageMovies.UseVisualStyleBackColor = true;
            // 
            // treeViewHVQM
            // 
            this.treeViewHVQM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewHVQM.Location = new System.Drawing.Point(0, 0);
            this.treeViewHVQM.Name = "treeViewHVQM";
            this.treeViewHVQM.Size = new System.Drawing.Size(347, 678);
            this.treeViewHVQM.TabIndex = 2;
            // 
            // tabPageScenes
            // 
            this.tabPageScenes.Controls.Add(this.treeViewSBF);
            this.tabPageScenes.Location = new System.Drawing.Point(4, 22);
            this.tabPageScenes.Name = "tabPageScenes";
            this.tabPageScenes.Size = new System.Drawing.Size(257, 578);
            this.tabPageScenes.TabIndex = 2;
            this.tabPageScenes.Text = "Scenes";
            this.tabPageScenes.UseVisualStyleBackColor = true;
            // 
            // treeViewSBF
            // 
            this.treeViewSBF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSBF.Location = new System.Drawing.Point(0, 0);
            this.treeViewSBF.Name = "treeViewSBF";
            this.treeViewSBF.Size = new System.Drawing.Size(347, 678);
            this.treeViewSBF.TabIndex = 3;
            // 
            // groupBoxTextureContainer
            // 
            this.groupBoxTextureContainer.Controls.Add(this.radioButton3);
            this.groupBoxTextureContainer.Controls.Add(this.radioButton2);
            this.groupBoxTextureContainer.Controls.Add(this.radioButton1);
            this.groupBoxTextureContainer.Controls.Add(this.label3);
            this.groupBoxTextureContainer.Controls.Add(this.button1);
            this.groupBoxTextureContainer.Location = new System.Drawing.Point(6, 186);
            this.groupBoxTextureContainer.Name = "groupBoxTextureContainer";
            this.groupBoxTextureContainer.Size = new System.Drawing.Size(253, 137);
            this.groupBoxTextureContainer.TabIndex = 5;
            this.groupBoxTextureContainer.TabStop = false;
            this.groupBoxTextureContainer.Text = "Manage Container";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(12, 113);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(234, 17);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "image scroll ( like blue pokeball background)";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 90);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(134, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "animated ( like badges)";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 67);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 17);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Fixed";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Image type :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Remove this container";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonShowTexture
            // 
            this.buttonShowTexture.Enabled = false;
            this.buttonShowTexture.Location = new System.Drawing.Point(6, 19);
            this.buttonShowTexture.Name = "buttonShowTexture";
            this.buttonShowTexture.Size = new System.Drawing.Size(116, 23);
            this.buttonShowTexture.TabIndex = 1;
            this.buttonShowTexture.Text = "preview texture";
            this.buttonShowTexture.UseVisualStyleBackColor = true;
            this.buttonShowTexture.Click += new System.EventHandler(this.buttonShowTexture_Click);
            // 
            // groupBoxTextures
            // 
            this.groupBoxTextures.Controls.Add(this.button5);
            this.groupBoxTextures.Controls.Add(this.button4);
            this.groupBoxTextures.Controls.Add(this.groupBox2);
            this.groupBoxTextures.Controls.Add(this.groupBox4);
            this.groupBoxTextures.Controls.Add(this.groupBoxTextureContainer);
            this.groupBoxTextures.Controls.Add(this.buttonExtractAllTextures);
            this.groupBoxTextures.Controls.Add(this.checkBoxAlwaysShowTexture);
            this.groupBoxTextures.Controls.Add(this.buttonShowTexture);
            this.groupBoxTextures.Location = new System.Drawing.Point(283, 69);
            this.groupBoxTextures.Name = "groupBoxTextures";
            this.groupBoxTextures.Size = new System.Drawing.Size(352, 604);
            this.groupBoxTextures.TabIndex = 6;
            this.groupBoxTextures.TabStop = false;
            this.groupBoxTextures.Text = "Rom Management";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 106);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(116, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "Replace texture";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 77);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(116, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "insert new texture";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Location = new System.Drawing.Point(6, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 45);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Texture option";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Texture showed length :";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(133, 14);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown1.TabIndex = 13;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labelIsTextureContainer);
            this.groupBox4.Controls.Add(this.pictureBoxTexture);
            this.groupBox4.Location = new System.Drawing.Point(6, 329);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(335, 267);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Texture";
            // 
            // pictureBoxTexture
            // 
            this.pictureBoxTexture.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxTexture.Name = "pictureBoxTexture";
            this.pictureBoxTexture.Size = new System.Drawing.Size(320, 240);
            this.pictureBoxTexture.TabIndex = 0;
            this.pictureBoxTexture.TabStop = false;
            // 
            // buttonExtractAllTextures
            // 
            this.buttonExtractAllTextures.Location = new System.Drawing.Point(6, 48);
            this.buttonExtractAllTextures.Name = "buttonExtractAllTextures";
            this.buttonExtractAllTextures.Size = new System.Drawing.Size(116, 23);
            this.buttonExtractAllTextures.TabIndex = 7;
            this.buttonExtractAllTextures.Text = "Extract all textures";
            this.buttonExtractAllTextures.UseVisualStyleBackColor = true;
            this.buttonExtractAllTextures.Click += new System.EventHandler(this.buttonExtractAllTextures_Click);
            // 
            // checkBoxAlwaysShowTexture
            // 
            this.checkBoxAlwaysShowTexture.AutoSize = true;
            this.checkBoxAlwaysShowTexture.Checked = true;
            this.checkBoxAlwaysShowTexture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAlwaysShowTexture.Location = new System.Drawing.Point(128, 23);
            this.checkBoxAlwaysShowTexture.Name = "checkBoxAlwaysShowTexture";
            this.checkBoxAlwaysShowTexture.Size = new System.Drawing.Size(98, 17);
            this.checkBoxAlwaysShowTexture.TabIndex = 10;
            this.checkBoxAlwaysShowTexture.Text = "always preview";
            this.checkBoxAlwaysShowTexture.UseVisualStyleBackColor = true;
            this.checkBoxAlwaysShowTexture.CheckedChanged += new System.EventHandler(this.checkBoxAlwaysShowTexture_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 680);
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
            // timerExtract
            // 
            this.timerExtract.Interval = 1;
            // 
            // bWDecompress
            // 
            this.bWDecompress.WorkerReportsProgress = true;
            this.bWDecompress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDecompress_DoWork);
            this.bWDecompress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWDecompress_ProgressChanged);
            this.bWDecompress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDecompress_RunWorkerCompleted);
            // 
            // labelIsTextureContainer
            // 
            this.labelIsTextureContainer.AutoSize = true;
            this.labelIsTextureContainer.Location = new System.Drawing.Point(57, 129);
            this.labelIsTextureContainer.Name = "labelIsTextureContainer";
            this.labelIsTextureContainer.Size = new System.Drawing.Size(212, 13);
            this.labelIsTextureContainer.TabIndex = 1;
            this.labelIsTextureContainer.Text = "Please select a texture inside the container.";
            this.labelIsTextureContainer.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 702);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxTextures);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "PPL - Rom Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStripForTreeview.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageTextures.ResumeLayout(false);
            this.tabPageMovies.ResumeLayout(false);
            this.tabPageScenes.ResumeLayout(false);
            this.groupBoxTextureContainer.ResumeLayout(false);
            this.groupBoxTextureContainer.PerformLayout();
            this.groupBoxTextures.ResumeLayout(false);
            this.groupBoxTextures.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTexture)).EndInit();
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTextures;
        private System.Windows.Forms.TabPage tabPageMovies;
        private System.Windows.Forms.TabPage tabPageScenes;
        private System.Windows.Forms.TreeView treeViewHVQM;
        private System.Windows.Forms.TreeView treeViewSBF;
        private System.Windows.Forms.GroupBox groupBoxTextureContainer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonShowTexture;
        private System.Windows.Forms.GroupBox groupBoxTextures;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonExtractAllTextures;
        private System.Windows.Forms.PictureBox pictureBoxTexture;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox checkBoxAlwaysShowTexture;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel helpStatus;
        private System.Windows.Forms.Timer timerExtract;
        private System.ComponentModel.BackgroundWorker bWDecompress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForTreeview;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collpseAllToolStripMenuItem;
        private System.Windows.Forms.Label labelIsTextureContainer;
    }
}

