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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonGetRomFolder = new System.Windows.Forms.Button();
            this.textBoxPPLLocation = new System.Windows.Forms.TextBox();
            this.buttonLoadRom = new System.Windows.Forms.Button();
            this.treeViewTextures = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelEndingData = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStartingData = new System.Windows.Forms.Label();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBoxAlwaysShowTexture = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.helpStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageTextures.SuspendLayout();
            this.tabPageMovies.SuspendLayout();
            this.tabPageScenes.SuspendLayout();
            this.groupBoxTextureContainer.SuspendLayout();
            this.groupBoxTextures.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
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
            this.groupBox1.Size = new System.Drawing.Size(789, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PPL location : ";
            // 
            // buttonGetRomFolder
            // 
            this.buttonGetRomFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetRomFolder.Location = new System.Drawing.Point(661, 17);
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
            this.textBoxPPLLocation.Location = new System.Drawing.Point(6, 19);
            this.textBoxPPLLocation.Name = "textBoxPPLLocation";
            this.textBoxPPLLocation.Size = new System.Drawing.Size(649, 20);
            this.textBoxPPLLocation.TabIndex = 1;
            this.textBoxPPLLocation.Text = global::N64PPLEditorC.Properties.Settings.Default.txtPPLLocation;
            // 
            // buttonLoadRom
            // 
            this.buttonLoadRom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadRom.Location = new System.Drawing.Point(701, 17);
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
            this.treeViewTextures.Location = new System.Drawing.Point(0, 0);
            this.treeViewTextures.Name = "treeViewTextures";
            this.treeViewTextures.Size = new System.Drawing.Size(347, 678);
            this.treeViewTextures.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Starting data : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelEndingData);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.labelStartingData);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(253, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 180);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Debug Info";
            // 
            // labelEndingData
            // 
            this.labelEndingData.AutoSize = true;
            this.labelEndingData.Location = new System.Drawing.Point(88, 29);
            this.labelEndingData.Name = "labelEndingData";
            this.labelEndingData.Size = new System.Drawing.Size(13, 13);
            this.labelEndingData.TabIndex = 5;
            this.labelEndingData.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ending data : ";
            // 
            // labelStartingData
            // 
            this.labelStartingData.AutoSize = true;
            this.labelStartingData.Location = new System.Drawing.Point(88, 16);
            this.labelStartingData.Name = "labelStartingData";
            this.labelStartingData.Size = new System.Drawing.Size(13, 13);
            this.labelStartingData.TabIndex = 3;
            this.labelStartingData.Text = "0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTextures);
            this.tabControl1.Controls.Add(this.tabPageMovies);
            this.tabControl1.Controls.Add(this.tabPageScenes);
            this.tabControl1.Location = new System.Drawing.Point(12, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(355, 704);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPageTextures
            // 
            this.tabPageTextures.Controls.Add(this.treeViewTextures);
            this.tabPageTextures.Location = new System.Drawing.Point(4, 22);
            this.tabPageTextures.Name = "tabPageTextures";
            this.tabPageTextures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTextures.Size = new System.Drawing.Size(347, 678);
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
            this.tabPageMovies.Size = new System.Drawing.Size(347, 678);
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
            this.tabPageScenes.Size = new System.Drawing.Size(347, 678);
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
            this.groupBoxTextureContainer.Location = new System.Drawing.Point(6, 284);
            this.groupBoxTextureContainer.Name = "groupBoxTextureContainer";
            this.groupBoxTextureContainer.Size = new System.Drawing.Size(306, 137);
            this.groupBoxTextureContainer.TabIndex = 5;
            this.groupBoxTextureContainer.TabStop = false;
            this.groupBoxTextureContainer.Text = "Manage Container (can hold multiple textures)";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(37, 113);
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
            this.radioButton2.Location = new System.Drawing.Point(37, 90);
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
            this.radioButton1.Location = new System.Drawing.Point(37, 67);
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
            this.buttonShowTexture.Location = new System.Drawing.Point(6, 19);
            this.buttonShowTexture.Name = "buttonShowTexture";
            this.buttonShowTexture.Size = new System.Drawing.Size(116, 23);
            this.buttonShowTexture.TabIndex = 1;
            this.buttonShowTexture.Text = "show texture";
            this.buttonShowTexture.UseVisualStyleBackColor = true;
            // 
            // groupBoxTextures
            // 
            this.groupBoxTextures.Controls.Add(this.groupBox4);
            this.groupBoxTextures.Controls.Add(this.groupBox3);
            this.groupBoxTextures.Controls.Add(this.groupBoxTextureContainer);
            this.groupBoxTextures.Controls.Add(this.groupBox2);
            this.groupBoxTextures.Controls.Add(this.button3);
            this.groupBoxTextures.Controls.Add(this.checkBoxAlwaysShowTexture);
            this.groupBoxTextures.Controls.Add(this.buttonShowTexture);
            this.groupBoxTextures.Location = new System.Drawing.Point(369, 69);
            this.groupBoxTextures.Name = "groupBoxTextures";
            this.groupBoxTextures.Size = new System.Drawing.Size(432, 700);
            this.groupBoxTextures.TabIndex = 6;
            this.groupBoxTextures.TabStop = false;
            this.groupBoxTextures.Text = "Rom Management";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Location = new System.Drawing.Point(6, 427);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(335, 267);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Texture";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 240);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Location = new System.Drawing.Point(6, 149);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(239, 120);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Texture Options";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Texture showed length :";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(132, 19);
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 37);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(116, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Add new texture";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(8, 66);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(116, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "Replace texture";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Extract all textures";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // checkBoxAlwaysShowTexture
            // 
            this.checkBoxAlwaysShowTexture.AutoSize = true;
            this.checkBoxAlwaysShowTexture.Location = new System.Drawing.Point(128, 23);
            this.checkBoxAlwaysShowTexture.Name = "checkBoxAlwaysShowTexture";
            this.checkBoxAlwaysShowTexture.Size = new System.Drawing.Size(86, 17);
            this.checkBoxAlwaysShowTexture.TabIndex = 10;
            this.checkBoxAlwaysShowTexture.Text = "always show";
            this.checkBoxAlwaysShowTexture.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 775);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(813, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // helpStatus
            // 
            this.helpStatus.Name = "helpStatus";
            this.helpStatus.Size = new System.Drawing.Size(798, 17);
            this.helpStatus.Spring = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 797);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageTextures.ResumeLayout(false);
            this.tabPageMovies.ResumeLayout(false);
            this.tabPageScenes.ResumeLayout(false);
            this.groupBoxTextureContainer.ResumeLayout(false);
            this.groupBoxTextureContainer.PerformLayout();
            this.groupBoxTextures.ResumeLayout(false);
            this.groupBoxTextures.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelStartingData;
        private System.Windows.Forms.Label labelEndingData;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
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
    }
}

