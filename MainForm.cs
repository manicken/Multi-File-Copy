using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Multi_File_Copy
{
    public partial class MainForm : Form
    {
        private MultiFileCopier mfc;
        private bool pendingProgramClose = false;

        public MainForm()
        {
            InitializeComponent();

            mfc = new MultiFileCopier(MultiFileCopier_ProgressChanged);
            mfc.ProgressCompleted = MultiFileCopier_ProgressCompleted;

            dgvFolders.DataSource = null;
        }

        private void MultiFileCopier_ProgressChanged(object state)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { ProgressChanged(state); });
            }
            else
            {
                ProgressChanged(state);
            }
        }

        private void MultiFileCopier_ProgressCompleted()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(ProgressCompleted));
            }
            else
            {
                ProgressCompleted();
            }
        }

        private void LockUI_TaskRunning()
        {
            btnAnalyze.Enabled = false;
            btnCopyFiles.Enabled = false;
            pnlFS_copy.Enabled = false;
            btnCopyFiles.Enabled = false;
            dgvFolders.PreventUserClick = true;
            dgvFiles.PreventUserClick = true;
            btnCancel.Enabled = true;
        }
        private void UnlockUI_TaskComplete()
        {
            btnAnalyze.Enabled = true;
            btnCopyFiles.Enabled = true;
            pnlFS_copy.Enabled = true;
            btnCopyFiles.Enabled = true;
            dgvFolders.PreventUserClick = false;
            dgvFiles.PreventUserClick = false;
            btnCancel.Enabled = false;
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            LockUI_TaskRunning();
            mfc.StartAnalyze();
        }

        private void btnCopyFiles_Click(object sender, EventArgs e)
        {
            MyFileCopy.bufferSizeInMBytes = Convert.ToInt32(nudBufferSizeMB.Value);
            mfc.useFS_copy = chkUseFS_copy.Checked;
            LockUI_TaskRunning();
            mfc.StartCopy();
        }

        private void ProgressCompleted()
        {
            UnlockUI_TaskComplete();
 
            rtxtLog.AppendTextLine("\nExeption count: " + mfc.exCount, MultiFileCopier.COLOR_ERROR);
            rtxtLog.AppendTextLine("Warning count: " + mfc.waCount, MultiFileCopier.COLOR_WARNING);
            rtxtLog.SaveFile(mfc.multiFileCopyLogFilePath);
            mfc.SaveStatusFile();

            if (pendingProgramClose)
            {
                this.Close();
            }
        }

        private void ProgressChanged(object state)
        {
            if (state != null)
            {
                if (state.GetType() == typeof(TimeSpan))
                {
                    TimeSpan ts = (TimeSpan)state;
                    string timeLeft = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                    ts.Hours,
                                    ts.Minutes,
                                    ts.Seconds);
                    lblTimeLeft.Text = "Time Remaining: " + timeLeft;
                }
                else if (state.GetType() == typeof(RichTextAppendArguments))
                {
                    RichTextAppendArguments userState = (RichTextAppendArguments)state;
                    rtxtLog.AppendText(userState);

                    if (userState.Message.StartsWith("[EX]"))
                        mfc.exCount++;
                    else if (userState.Message.StartsWith("[WA]"))
                        mfc.waCount++;

                    /*if (ea.ProgressPercentage != -1)
                    {
                        lblProgress.Text = ea.ProgressPercentage + "/" + (int)(totalSizeInBytes / (1024 * 1024));
                    }*/
                }
                else if (state.GetType() == typeof(FileCopyStatus))
                {
                    FileCopyStatus fcs = (FileCopyStatus)state;
                    if (dgvFiles.Rows.Count == 0) return;
                    if (fcs.Result == CopyResult.STARTED)
                    {
                        //bgw.ReportProgress(-1, new RichTextAppendArguments("[Copying] " + currentFiles[i], false));
                        
                        dgvFiles.Rows[fcs.Index].Selected = true;
                        dgvFiles.Rows[fcs.Index].DefaultCellStyle.BackColor = MultiFileCopier.COLOR_STARTED;
                        dgvFiles.ScrollToSelectedRow();

                    }
                    else if (fcs.Result == CopyResult.OK)
                    {
                        //bgw.ReportProgress(-1, new RichTextAppendArguments("[OK] ", true, COLOR_OK));
                        dgvFiles.Rows[fcs.Index].Selected = false;
                        dgvFiles.Rows[fcs.Index].DefaultCellStyle.BackColor = MultiFileCopier.COLOR_OK;
                    }
                    else if (fcs.Result == CopyResult.SKIPPED)
                    {
                        //bgw.ReportProgress(-1, new RichTextAppendArguments("[Skipping] " + currentFiles[fi], true, COLOR_WARNING));
                        dgvFiles.Rows[fcs.Index].Selected = false;
                        dgvFiles.Rows[fcs.Index].DefaultCellStyle.BackColor = MultiFileCopier.COLOR_WARNING;
                    }
                    else if (fcs.Result == CopyResult.ACCESS_DENIED)
                    {
                        dgvFiles.Rows[fcs.Index].Selected = false;
                        dgvFiles.Rows[fcs.Index].DefaultCellStyle.BackColor = MultiFileCopier.COLOR_ERROR;

                        rtxtLog.AppendTextLine("[ACCESS_DENIED] @ " + fcs.FilePath, MultiFileCopier.COLOR_ERROR);
                        rtxtLog.SaveFile(mfc.multiFileCopyLogFilePath);
                    }
                    else if (fcs.Result == CopyResult.PATH_TO_LONG)
                    {
                        dgvFiles.Rows[fcs.Index].Selected = false;
                        dgvFiles.Rows[fcs.Index].DefaultCellStyle.BackColor = MultiFileCopier.COLOR_ERROR;

                        rtxtLog.AppendTextLine("[PATH_TO_LONG] @ " + fcs.FilePath, MultiFileCopier.COLOR_ERROR);
                        rtxtLog.SaveFile(mfc.multiFileCopyLogFilePath);
                    }
                    else if (fcs.Result == CopyResult.READ_ERROR)
                    {
                        dgvFiles.Rows[fcs.Index].Selected = false;
                        dgvFiles.Rows[fcs.Index].DefaultCellStyle.BackColor = MultiFileCopier.COLOR_ERROR;

                        rtxtLog.AppendTextLine("[READ_ERROR] @ " + fcs.FilePath, MultiFileCopier.COLOR_ERROR);
                        rtxtLog.SaveFile(mfc.multiFileCopyLogFilePath);
                    }
                }
                else if (state.GetType() == typeof(FolderCopyStatus))
                {
                    FolderCopyStatus fcs = (FolderCopyStatus)state;
                    if (fcs.Result == CopyResult.STARTED)
                    {
                        dgvFolders.Rows[fcs.Index].Selected = true;
                        dgvFolders.Rows[fcs.Index].DefaultCellStyle.BackColor = MultiFileCopier.COLOR_STARTED;
                        dgvFolders.ScrollToSelectedRow();

                        dgvFiles.DataSource = mfc.dtCurrDirFiles;
                    }
                    else if (fcs.Result == CopyResult.OK)
                    {
                        dgvFolders.Rows[fcs.Index].Selected = false;
                        dgvFolders.Rows[fcs.Index].DefaultCellStyle.BackColor = MultiFileCopier.COLOR_OK;
                    }
                    else if (fcs.Result == CopyResult.FOLDER_NOT_COMPLETE) // when a file in a folder cannot be copied
                    {
                        dgvFolders.Rows[fcs.Index].Selected = false;
                        dgvFolders.Rows[fcs.Index].DefaultCellStyle.BackColor = MultiFileCopier.COLOR_WARNING;
                    }
                }
            }
            /*else
            {
                if (ea.ProgressPercentage != -1)
                {
                    pbar.Maximum = ea.ProgressPercentage;
                    pbar.Minimum = 0;
                }
                else
                {
                    dgvFolders.DataSource = null;
                    dgvFolders.DataSource = dtDir;
                }
            }*/
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mfc.AbortCurrentTask();
        }

        private void dgv_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgvFolders.DataSource == null)
                return;
            dgvFolders.Columns[0].Width = 640;
            dgvFolders.Columns[1].Width = 96;
            dgvFolders.Columns[2].Width = 64;
            dgvFolders.Columns[3].Width = 64;
            dgvFolders.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvFolders.Columns[4].Width = 64;

            dgvFolders.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgvFolders.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgvFolders.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgvFolders.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgvFolders.Columns[4].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        public void LoadStatusFile()
        {
            mfc.LoadStatusFile();
            lblProgress.Text = "0/" + (int)(mfc.totalSizeInBytes / (1024 * 1024));
            txtBoxSrcRoot.Text = (string)mfc.dtDir.Rows[1][0];

            dgvFolders.DataSource = null;
            dgvFolders.DataSource = mfc.dtDir;
            btnCopyFiles.Enabled = true;
            
        }

        private void btnSelectSrcDir_Click(object sender, EventArgs e)
        {
            string path = "";
            if (!SelectFolder(out path))
                return;
            txtBoxSrcRoot.Text = path;
            mfc.SavePaths();
        }

        private void btnSelectDestDir_Click(object sender, EventArgs e)
        {
            string path = "";
            if (!SelectFolder(out path))
                return;
            txtBoxDestRoot.Text = path;
            mfc.SavePaths();
        }

        public bool SelectFolder(out string folder)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    folder = "";
                    return false;
                }
                if (!Directory.Exists(fbd.SelectedPath))
                {
                    folder = "";
                    return false;
                }
                folder = fbd.SelectedPath;
                return true;
            }
        }

        private void dgvFolders_SelectionChanged(object sender, EventArgs e)
        {
            if (mfc.IsRunning) return;

            if (dgvFolders.SelectedRows.Count != 1) return;

            dgvFiles.DataSource = mfc.GetTable(dgvFolders.SelectedRows[0].Index);
        }

        private void dgvFiles_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgvFiles.DataSource == null)
                return;
            dgvFiles.Columns[0].Width = 640;
            dgvFiles.Columns[1].Width = 96;
            dgvFiles.Columns[2].Width = 64;
            dgvFiles.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvFiles.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgvFiles.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgvFiles.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;

        }

        private void txtBoxSrcRoot_TextChanged(object sender, EventArgs e)
        {
            mfc.srcRootDir = txtBoxSrcRoot.Text;
        }

        private void txtBoxDestRoot_TextChanged(object sender, EventArgs e)
        {
            mfc.destRootDir = txtBoxDestRoot.Text;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mfc.IsRunning)
            {
                e.Cancel = true;
                DialogResult dr = MessageBox.Show("A task is running, do you want to abort the task and quit?", "Task running", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    pendingProgramClose = true;
                    mfc.AbortCurrentTask();
                }
            }
            else
            {
                mfc.SavePaths();
                rtxtLog.SaveFile(mfc.multiFileCopyLogFilePath, RichTextBoxStreamType.RichText);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            mfc.LoadPaths();
            txtBoxSrcRoot.Text = mfc.srcRootDir;
            txtBoxDestRoot.Text = mfc.destRootDir;
            LoadStatusFile();
            rtxtLog.LoadFile(mfc.multiFileCopyLogFilePath, RichTextBoxStreamType.RichText);
            System.Threading.Thread.CurrentThread.Name = "MainForm_Thread";
        }

        private void splitContainer2_SizeChanged(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer2.Width / 2;
        }

        private void clearTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtLog.SaveToDateTimeNamedFile("log_");
            rtxtLog.Clear();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtLog.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtLog.Paste();
        }

        private void rtxtLog_VisibleChanged(object sender, EventArgs e)
        {
            rtxtLog.SetStandardRightClickMenu();
        }

        private void dgvFolders_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            rtxtLog.AppendTextLine("dgv wtf=" + e.Exception, MultiFileCopier.COLOR_ERROR);
        }
    }
}
