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
            this.treeViewMovies = new System.Windows.Forms.TreeView();
            this.tabPageScenes = new System.Windows.Forms.TabPage();
            this.treeViewScenes = new System.Windows.Forms.TreeView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageTextures.SuspendLayout();
            this.tabPageMovies.SuspendLayout();
            this.tabPageScenes.SuspendLayout();
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
            this.buttonGetRomFolder.Location = new System.Drawing.Point(668, 16);
            this.buttonGetRomFolder.Name = "buttonGetRomFolder";
            this.buttonGetRomFolder.Size = new System.Drawing.Size(34, 23);
            this.buttonGetRomFolder.TabIndex = 1;
            this.buttonGetRomFolder.Text = "...";
            this.buttonGetRomFolder.UseVisualStyleBackColor = true;
            this.buttonGetRomFolder.Click += new System.EventHandler(this.buttonGetRomFolder_Click);
            // 
            // textBoxPPLLocation
            // 
            this.textBoxPPLLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPPLLocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::N64PPLEditorC.Properties.Settings.Default, "txtPPLLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxPPLLocation.Location = new System.Drawing.Point(6, 19);
            this.textBoxPPLLocation.Name = "textBoxPPLLocation";
            this.textBoxPPLLocation.Size = new System.Drawing.Size(656, 20);
            this.textBoxPPLLocation.TabIndex = 1;
            this.textBoxPPLLocation.Text = global::N64PPLEditorC.Properties.Settings.Default.txtPPLLocation;
            // 
            // buttonLoadRom
            // 
            this.buttonLoadRom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadRom.Location = new System.Drawing.Point(708, 17);
            this.buttonLoadRom.Name = "buttonLoadRom";
            this.buttonLoadRom.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadRom.TabIndex = 0;
            this.buttonLoadRom.Text = "Load rom";
            this.buttonLoadRom.UseVisualStyleBackColor = true;
            this.buttonLoadRom.Click += new System.EventHandler(this.buttonLoadRom_Click);
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
            this.groupBox2.Location = new System.Drawing.Point(373, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 108);
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
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.tabPageMovies.Controls.Add(this.treeViewMovies);
            this.tabPageMovies.Location = new System.Drawing.Point(4, 22);
            this.tabPageMovies.Name = "tabPageMovies";
            this.tabPageMovies.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMovies.Size = new System.Drawing.Size(347, 678);
            this.tabPageMovies.TabIndex = 1;
            this.tabPageMovies.Text = "Movies";
            this.tabPageMovies.UseVisualStyleBackColor = true;
            // 
            // treeViewMovies
            // 
            this.treeViewMovies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewMovies.Location = new System.Drawing.Point(0, 0);
            this.treeViewMovies.Name = "treeViewMovies";
            this.treeViewMovies.Size = new System.Drawing.Size(347, 678);
            this.treeViewMovies.TabIndex = 2;
            // 
            // tabPageScenes
            // 
            this.tabPageScenes.Controls.Add(this.treeViewScenes);
            this.tabPageScenes.Location = new System.Drawing.Point(4, 22);
            this.tabPageScenes.Name = "tabPageScenes";
            this.tabPageScenes.Size = new System.Drawing.Size(347, 678);
            this.tabPageScenes.TabIndex = 2;
            this.tabPageScenes.Text = "Scenes";
            this.tabPageScenes.UseVisualStyleBackColor = true;
            // 
            // treeViewScenes
            // 
            this.treeViewScenes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewScenes.Location = new System.Drawing.Point(0, 0);
            this.treeViewScenes.Name = "treeViewScenes";
            this.treeViewScenes.Size = new System.Drawing.Size(347, 678);
            this.treeViewScenes.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 785);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "PPL - Rom Management";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageTextures.ResumeLayout(false);
            this.tabPageMovies.ResumeLayout(false);
            this.tabPageScenes.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TreeView treeViewMovies;
        private System.Windows.Forms.TreeView treeViewScenes;
    }
}

