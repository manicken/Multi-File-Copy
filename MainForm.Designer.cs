namespace Multi_File_Copy
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.btnCopyFiles = new System.Windows.Forms.Button();
            this.txtBoxSrcRoot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxDestRoot = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvFolders = new System.Windows.Forms.MyDataGridView();
            this.dgvFiles = new System.Windows.Forms.MyDataGridView();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnSelectSrcDir = new System.Windows.Forms.Button();
            this.btnSelectDestDir = new System.Windows.Forms.Button();
            this.chkUseFS_copy = new System.Windows.Forms.CheckBox();
            this.nudBufferSizeMB = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlFS_copy = new System.Windows.Forms.Panel();
            this.lblTimeLeft = new System.Windows.Forms.Label();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFolders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeMB)).BeginInit();
            this.pnlFS_copy.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtLog
            // 
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtLog.Location = new System.Drawing.Point(0, 0);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(1172, 108);
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            this.rtxtLog.VisibleChanged += new System.EventHandler(this.rtxtLog_VisibleChanged);
            // 
            // btnCopyFiles
            // 
            this.btnCopyFiles.Enabled = false;
            this.btnCopyFiles.Location = new System.Drawing.Point(477, 7);
            this.btnCopyFiles.Name = "btnCopyFiles";
            this.btnCopyFiles.Size = new System.Drawing.Size(84, 46);
            this.btnCopyFiles.TabIndex = 1;
            this.btnCopyFiles.Text = "Copy files";
            this.btnCopyFiles.UseVisualStyleBackColor = true;
            this.btnCopyFiles.Click += new System.EventHandler(this.btnCopyFiles_Click);
            // 
            // txtBoxSrcRoot
            // 
            this.txtBoxSrcRoot.Location = new System.Drawing.Point(12, 27);
            this.txtBoxSrcRoot.Name = "txtBoxSrcRoot";
            this.txtBoxSrcRoot.Size = new System.Drawing.Size(279, 20);
            this.txtBoxSrcRoot.TabIndex = 2;
            this.txtBoxSrcRoot.Text = "G:\\!!various";
            this.txtBoxSrcRoot.TextChanged += new System.EventHandler(this.txtBoxSrcRoot_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Src root:";
            // 
            // txtBoxDestRoot
            // 
            this.txtBoxDestRoot.Location = new System.Drawing.Point(567, 28);
            this.txtBoxDestRoot.Name = "txtBoxDestRoot";
            this.txtBoxDestRoot.Size = new System.Drawing.Size(279, 20);
            this.txtBoxDestRoot.TabIndex = 2;
            this.txtBoxDestRoot.Text = "H:\\!!various";
            this.txtBoxDestRoot.TextChanged += new System.EventHandler(this.txtBoxDestRoot_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(564, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Dest root";
            // 
            // pbar
            // 
            this.pbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbar.Location = new System.Drawing.Point(12, 480);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(1176, 29);
            this.pbar.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(900, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 46);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(323, 7);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(91, 46);
            this.btnAnalyze.TabIndex = 5;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtxtLog);
            this.splitContainer1.Size = new System.Drawing.Size(1172, 390);
            this.splitContainer1.SplitterDistance = 278;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvFolders);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvFiles);
            this.splitContainer2.Size = new System.Drawing.Size(1172, 278);
            this.splitContainer2.SplitterDistance = 586;
            this.splitContainer2.TabIndex = 1;
            this.splitContainer2.SizeChanged += new System.EventHandler(this.splitContainer2_SizeChanged);
            // 
            // dgvFolders
            // 
            this.dgvFolders.AllowUserToAddRows = false;
            this.dgvFolders.AllowUserToDeleteRows = false;
            this.dgvFolders.AllowUserToResizeRows = false;
            this.dgvFolders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFolders.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFolders.Location = new System.Drawing.Point(0, 0);
            this.dgvFolders.MultiSelect = false;
            this.dgvFolders.Name = "dgvFolders";
            this.dgvFolders.ReadOnly = true;
            this.dgvFolders.RowHeadersVisible = false;
            this.dgvFolders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFolders.Size = new System.Drawing.Size(586, 278);
            this.dgvFolders.TabIndex = 0;
            this.dgvFolders.DataSourceChanged += new System.EventHandler(this.dgv_DataSourceChanged);
            this.dgvFolders.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvFolders_DataError);
            this.dgvFolders.SelectionChanged += new System.EventHandler(this.dgvFolders_SelectionChanged);
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AllowUserToResizeRows = false;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFiles.Location = new System.Drawing.Point(0, 0);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.Size = new System.Drawing.Size(582, 278);
            this.dgvFiles.TabIndex = 1;
            this.dgvFiles.DataSourceChanged += new System.EventHandler(this.dgvFiles_DataSourceChanged);
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(11, 457);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(51, 20);
            this.lblProgress.TabIndex = 8;
            this.lblProgress.Text = "label3";
            // 
            // btnSelectSrcDir
            // 
            this.btnSelectSrcDir.Location = new System.Drawing.Point(293, 27);
            this.btnSelectSrcDir.Name = "btnSelectSrcDir";
            this.btnSelectSrcDir.Size = new System.Drawing.Size(24, 21);
            this.btnSelectSrcDir.TabIndex = 9;
            this.btnSelectSrcDir.Text = "...";
            this.btnSelectSrcDir.UseVisualStyleBackColor = true;
            this.btnSelectSrcDir.Click += new System.EventHandler(this.btnSelectSrcDir_Click);
            // 
            // btnSelectDestDir
            // 
            this.btnSelectDestDir.Location = new System.Drawing.Point(852, 27);
            this.btnSelectDestDir.Name = "btnSelectDestDir";
            this.btnSelectDestDir.Size = new System.Drawing.Size(24, 21);
            this.btnSelectDestDir.TabIndex = 9;
            this.btnSelectDestDir.Text = "...";
            this.btnSelectDestDir.UseVisualStyleBackColor = true;
            this.btnSelectDestDir.Click += new System.EventHandler(this.btnSelectDestDir_Click);
            // 
            // chkUseFS_copy
            // 
            this.chkUseFS_copy.AutoSize = true;
            this.chkUseFS_copy.Location = new System.Drawing.Point(3, 4);
            this.chkUseFS_copy.Name = "chkUseFS_copy";
            this.chkUseFS_copy.Size = new System.Drawing.Size(153, 17);
            this.chkUseFS_copy.TabIndex = 10;
            this.chkUseFS_copy.Text = "use FileStream based copy";
            this.chkUseFS_copy.UseVisualStyleBackColor = true;
            // 
            // nudBufferSizeMB
            // 
            this.nudBufferSizeMB.Location = new System.Drawing.Point(116, 27);
            this.nudBufferSizeMB.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudBufferSizeMB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBufferSizeMB.Name = "nudBufferSizeMB";
            this.nudBufferSizeMB.Size = new System.Drawing.Size(40, 20);
            this.nudBufferSizeMB.TabIndex = 11;
            this.nudBufferSizeMB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "buffersize (MB)";
            // 
            // pnlFS_copy
            // 
            this.pnlFS_copy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFS_copy.Controls.Add(this.chkUseFS_copy);
            this.pnlFS_copy.Controls.Add(this.label3);
            this.pnlFS_copy.Controls.Add(this.nudBufferSizeMB);
            this.pnlFS_copy.Location = new System.Drawing.Point(990, 6);
            this.pnlFS_copy.Name = "pnlFS_copy";
            this.pnlFS_copy.Size = new System.Drawing.Size(194, 53);
            this.pnlFS_copy.TabIndex = 13;
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeLeft.AutoSize = true;
            this.lblTimeLeft.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeLeft.Location = new System.Drawing.Point(960, 461);
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(224, 16);
            this.lblTimeLeft.TabIndex = 14;
            this.lblTimeLeft.Text = "Time Remaining: 00h:00m:00s";
            // 
            // toolTipMain
            // 
            this.toolTipMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 511);
            this.Controls.Add(this.lblTimeLeft);
            this.Controls.Add(this.pnlFS_copy);
            this.Controls.Add(this.btnSelectDestDir);
            this.Controls.Add(this.btnSelectSrcDir);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxDestRoot);
            this.Controls.Add(this.txtBoxSrcRoot);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCopyFiles);
            this.Name = "MainForm";
            this.Text = "Multi file Copy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFolders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeMB)).EndInit();
            this.pnlFS_copy.ResumeLayout(false);
            this.pnlFS_copy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Button btnCopyFiles;
        private System.Windows.Forms.TextBox txtBoxSrcRoot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxDestRoot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MyDataGridView dgvFolders;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.MyDataGridView dgvFiles;
        private System.Windows.Forms.Button btnSelectSrcDir;
        private System.Windows.Forms.Button btnSelectDestDir;
        private System.Windows.Forms.CheckBox chkUseFS_copy;
        private System.Windows.Forms.NumericUpDown nudBufferSizeMB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlFS_copy;
        private System.Windows.Forms.Label lblTimeLeft;
        private System.Windows.Forms.ToolTip toolTipMain;
    }
}

