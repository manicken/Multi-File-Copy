using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics; // Stopwatch;
using System.Data;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Multi_File_Copy
{
    public class MultiFileCopier
    {
        public Action<object> ReportProgress;
        public Action ProgressCompleted;

        private Thread AnalyzeSrc_Thread;
        private Thread CopyFiles_Thread;
        private bool running;

        public bool IsRunning
        {
            get { return running; }
        }

        public DataTable GetTable(int dirIndex)
        {
            if (ds.Tables.Contains("DTF" + dirIndex))
                return ds.Tables["DTF" + dirIndex];
            return dtFilesTemplate;
        }

        private Stopwatch stopWatch;

        public DataTable dtDir;
        public DataTable dtCurrDirFiles;
        public DataTable dtFilesTemplate;
        public DataSet ds;

        public string TOTAL_SIZE_STR = "[TOTAL SIZE]";

        public string multiFileCopyStatusFilePath = Directory.GetCurrentDirectory() + "\\" + "MultiFileCopyStatus.xml";
        public string multiFileCopyPathsFilePath = Directory.GetCurrentDirectory() + "\\" + "MultiFileCopyPaths.txt";
        public string multiFileCopyLogFilePath = Directory.GetCurrentDirectory() + "\\" + "MultiFileCopyLog.rtf";

        private readonly string COL_NAME = "Name";
        private readonly string COL_PATH = "Path";
        private readonly string COL_BYTES = "bytes";
        private readonly string COL_FILE_COUNT = "FileCount";
        private readonly string COL_MB = "MB";

        public static Color COLOR_STARTED = Color.White;
        public static Color COLOR_OK = Color.LightGreen;
        public static Color COLOR_WARNING = Color.LightYellow;
        public static Color COLOR_ERROR = Color.Pink;

        public string srcRootDir = "";
        public string destRootDir = "";

        public int exCount = 0;
        public int waCount = 0;

        public int totalDirectories = 0;
        public long totalSizeInBytes = 0;
        public long totalFileCount = 0;

        /// <summary>
        /// used to calculate average value of MB per sec
        /// </summary>
        public decimal totalMBs = 0;
        public int foldersCopied = 0;

        public bool useFS_copy = false;

        public MultiFileCopier(Action<object> ReportProgress_Handler)
        {
            ReportProgress = ReportProgress_Handler;

            running = false;

            stopWatch = new Stopwatch();

            ds = new DataSet("DS");
            dtDir = new DataTable("DTD");
            dtDir.Columns.Add(COL_PATH, typeof(string));
            dtDir.Columns.Add(COL_BYTES, typeof(long));
            dtDir.Columns.Add(COL_MB, typeof(string));
            dtDir.Columns.Add("FileCount", typeof(long));
            dtDir.Columns.Add("MB/s", typeof(string));
            ds.Tables.Add(dtDir);

            dtFilesTemplate = new DataTable("DTF");
            dtFilesTemplate.Columns.Add(COL_NAME, typeof(string));
            dtFilesTemplate.Columns.Add(COL_BYTES, typeof(long));
            dtFilesTemplate.Columns.Add(COL_MB, typeof(string));
        }

        public void StartAnalyze()
        {
            if (running) return;
            running = true;

            if (dtDir.Rows.Count != 0)
            {
                DialogResult dr = MessageBox.Show("Do you want to clear the previous list?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ds.Clear();
                    ds.Tables.Clear();
                    ds.Tables.Add(dtDir);
                    dtDir.Rows.Clear();
                    totalDirectories = 0;
                    totalSizeInBytes = 0;
                    totalFileCount = 0;
                }
                else if (dr == DialogResult.Cancel)
                {
                    if (ProgressCompleted != null)
                        ProgressCompleted();
                    running = false;
                    return;
                }
            }
            exCount = 0;
            waCount = 0;
            totalDirectories = 0;
            totalSizeInBytes = 0;
            totalFileCount = 0;

            AnalyzeSrc_Thread = new Thread(new ThreadStart(AnalyzeSrc_Task));
            AnalyzeSrc_Thread.Name = "AnalyzeSrc_Thread";
            AnalyzeSrc_Thread.Start();
        }

        public void StartCopy()
        {
            if (running) return;
            running = true;

            exCount = 0;
            waCount = 0;

            CopyFiles_Thread = new Thread(new ThreadStart(CopyFiles_Task));
            CopyFiles_Thread.Name = "CopyFiles_Thread";
            CopyFiles_Thread.Start();
        }

        public void AbortCurrentTask()
        {
            running = false;
        }

        public void SavePaths()
        {
            if (File.Exists(multiFileCopyPathsFilePath))
                File.Delete(multiFileCopyPathsFilePath);

            File.WriteAllText(multiFileCopyPathsFilePath,
                srcRootDir + "\r\n" +
                destRootDir);
        }

        public void LoadPaths()
        {
            if (!File.Exists(multiFileCopyPathsFilePath))
                return;

            string[] lines = File.ReadAllLines(multiFileCopyPathsFilePath);
            if (lines.Length < 2)
                return;
            srcRootDir = lines[0];
            destRootDir = lines[1];
        }
        public void LoadStatusFile()
        {
            if (File.Exists(multiFileCopyStatusFilePath))
            {
                ds.ReadXml(multiFileCopyStatusFilePath, XmlReadMode.InferSchema);
                dtDir = ds.Tables["DTD"];
                totalSizeInBytes = (long)dtDir.Rows[0][COL_BYTES];
                totalFileCount = (long)dtDir.Rows[0][COL_FILE_COUNT];
                totalDirectories = dtDir.Rows.Count;
                
            }
        }
        public void SaveStatusFile()
        {
            ds.WriteXml(multiFileCopyStatusFilePath);
        }
        // #######################################################################################################
        // #######################################################################################################
        // #######################################################################################################
        // #######################################################################################################
        #region **** ANALYZE SOURCE DIR ****
        private void AnalyzeSrc_Task()
        {
            SavePaths();

            stopWatch.Start();
            AnalyzeFilesInDirectory(srcRootDir);
            Analyze(srcRootDir);
            stopWatch.Stop();

            decimal folders_sec = ((decimal)totalDirectories / (decimal)stopWatch.ElapsedMilliseconds) * 1000;


            ReportProgress(new RichTextAppendArguments("\nTotal time for analyze: " + stopWatch.ElapsedMilliseconds + " mS", true));
            ReportProgress(new RichTextAppendArguments("\nTotal folders: " + totalDirectories, true));
            ReportProgress(new RichTextAppendArguments("\nFolder analyze rate: " + folders_sec + " folders/sec", true));


            string sizeInMB = string.Format("{0,8:####0.00}", (decimal)totalSizeInBytes / (decimal)(1024 * 1024));
            if (dtDir.Rows[0][0].ToString() == TOTAL_SIZE_STR)
            {
                dtDir.Rows[0][1] = totalSizeInBytes;
                dtDir.Rows[0][2] = sizeInMB;
                dtDir.Rows[0][3] = totalFileCount;
            }
            else
            {
                DataRow dr = dtDir.NewRow(); // we need DataRow when inserting rows
                dr.ItemArray = new object[] { TOTAL_SIZE_STR, totalSizeInBytes, sizeInMB, totalFileCount };
                dtDir.Rows.InsertAt(dr, 0);
            }
            if (ProgressCompleted != null)
                ProgressCompleted();
            running = false;
        }

        private void Analyze(string directoryPath)
        {
            string[] directories = Directory.GetDirectories(directoryPath);
            if (directories.Length == 0) return;

            totalDirectories += directories.Length;

            ReportProgress(new RichTextAppendArguments("[Analyzing] " + directoryPath, true));
            for (int di = 0; di < directories.Length && running; di++)
            {
                if (AnalyzeFilesInDirectory(directories[di]))
                {
                    Analyze(directories[di]);
                }
            }
        }

        private bool AnalyzeFilesInDirectory(string directoryPath)
        {
            long directorySizeInBytes = 0, directoryFileCount = 0;

            DirectoryInfo dir = new DirectoryInfo(directoryPath);

            try
            {
                FileInfo[] files = dir.GetFiles();
                directoryFileCount = files.Length;
                if (directoryFileCount == 0)
                {
                    dtDir.Rows.Add(directoryPath, 0, 0, 0);
                    return true;
                }
                DataTable newDtFiles = dtFilesTemplate.Clone();
                newDtFiles.TableName = "DTF" + (dtDir.Rows.Count + 1);
                ds.Tables.Add(newDtFiles);
                for (int fi = 0; fi < directoryFileCount && running; fi++)
                {
                    directorySizeInBytes += files[fi].Length;
                    string fileSizeInMB = string.Format("{0,8:####0.00}", (decimal)files[fi].Length / (decimal)(1024 * 1024));

                    newDtFiles.Rows.Add(files[fi].Name, files[fi].Length, fileSizeInMB);
                }
                string sizeInMB = string.Format("{0,8:####0.00}", (decimal)directorySizeInBytes / (decimal)(1024 * 1024));

                dtDir.Rows.Add(directoryPath, directorySizeInBytes, sizeInMB, directoryFileCount);

                totalSizeInBytes += directorySizeInBytes;
                totalFileCount += directoryFileCount;
                return true;
            }
            catch (Exception ex)
            {
                ReportProgress(new RichTextAppendArguments("[EX] " + ex.ToString(), true, COLOR_ERROR));
                dtDir.Rows.Add(directoryPath, -1, "access denied", -1, -1);
                return false;
            }
        }
        #endregion
        // #######################################################################################################
        // #######################################################################################################
        // #######################################################################################################
        // #######################################################################################################
        #region **** COPY FILES ****
        private void CopyFiles_Task()
        {
            SavePaths();
            int remaningSizeInMBytesToCopy = (int)(totalSizeInBytes / (1024 * 1024));
            int currentFolderSizeInMBytes = 0;
            foldersCopied = 0;
            totalMBs = 0;
            for (int i = 1; i < dtDir.Rows.Count && running; i++)
            {
                dtCurrDirFiles = ds.Tables["DTF" + i];
                if (dtCurrDirFiles == null) continue;

                ReportProgress(new FolderCopyStatus(i, CopyResult.STARTED));

                stopWatch.Restart();
                if (CopyFiles(i))
                    ReportProgress(new FolderCopyStatus(i, CopyResult.OK));
                else
                    ReportProgress(new FolderCopyStatus(i, CopyResult.FOLDER_NOT_COMPLETE));

                stopWatch.Stop();

                currentFolderSizeInMBytes = (int)(((long)dtDir.Rows[i][COL_BYTES]) / (1024 * 1024));

                if ((currentFolderSizeInMBytes != 0) && (stopWatch.ElapsedMilliseconds != 0))
                {
                    remaningSizeInMBytesToCopy -= currentFolderSizeInMBytes;

                    decimal MByte_sec = 0;
                    string prevMByte_sec = dtDir.Rows[i][4].ToString();
                    if (prevMByte_sec == "")
                    {
                        MByte_sec = ((decimal)currentFolderSizeInMBytes / (decimal)stopWatch.ElapsedMilliseconds) * 1000;
                        string megabytesSec = string.Format("{0,8:####0.00}", MByte_sec);
                        dtDir.Rows[i][4] = megabytesSec;
                    }
                    else
                    {
                        MByte_sec = decimal.Parse(prevMByte_sec);
                    }

                    //int copiedMB = (int)(totalSizeInBytes / (1024 * 1024)) - remaningSizeInMBytesToCopy;
                    if (MByte_sec == 0)
                    {
                        MByte_sec = ((decimal)currentFolderSizeInMBytes / (decimal)stopWatch.ElapsedMilliseconds) * 1000;
                        string megabytesSec = string.Format("{0,8:####0.00}", MByte_sec);
                        dtDir.Rows[i][4] = megabytesSec;
                    }
                    totalMBs += MByte_sec;
                        MByte_sec = totalMBs / (++foldersCopied); // average MB/s
                        TimeSpan t = TimeSpan.FromSeconds((int)((decimal)remaningSizeInMBytesToCopy / MByte_sec));

                        ReportProgress(t);// "MB/S, Calculated time left: " + timeLeft, true, COLOR_OK));
                    
                }
            }
            if (ProgressCompleted != null)
                ProgressCompleted();
            running = false;
        }
        private bool CopyFiles(int dirIndex)
        {
            string srcDir = dtDir.Rows[dirIndex][0].ToString();
            string destDir = destRootDir + srcDir.Substring(srcRootDir.Length);
            CreateDirectoryIfNeeded(destDir);

            destDir += "\\";
            srcDir += "\\";
            string fileName = "";

            string destFile = "";
            bool fileCopyOk = true;
            //MyFileCopy.CopyProgressChanged = MyCopyFile_ProgressChanged;
            //MyFileCopy.bufferSizeInMBytes = 4;
            for (int fi = 0; fi < dtCurrDirFiles.Rows.Count && running; fi++)
            {
                fileName = dtCurrDirFiles.Rows[fi][0].ToString();
                destFile = destDir + fileName;

                if (File.Exists(destFile))
                {
                    //ReportProgress(new RichTextAppendArguments("[Skipping] " + currentFiles[i], true, COLOR_WARNING));
                    ReportProgress(new FileCopyStatus(fi, CopyResult.SKIPPED));
                    continue;
                }

                //bgw.ReportProgress(new RichTextAppendArguments("[Copying] " + currentFiles[i], false));
                ReportProgress(new FileCopyStatus(fi, CopyResult.STARTED));

                try
                {
                    if (useFS_copy) MyFileCopy.Copy(srcDir + fileName, destFile);
                    else File.Copy(srcDir + fileName, destFile);

                    ReportProgress(new FileCopyStatus(fi, CopyResult.OK));
                }
                catch (UnauthorizedAccessException)
                {
                    ReportProgress(new FileCopyStatus(fi, CopyResult.ACCESS_DENIED, srcDir + fileName));
                    fileCopyOk = false;
                }
                catch (PathTooLongException)
                {
                    ReportProgress(new FileCopyStatus(fi, CopyResult.PATH_TO_LONG, srcDir + fileName));
                    fileCopyOk = false;
                }
                catch (IOException ex)
                {
                    ReportProgress(new RichTextAppendArguments("[EX] " + ex.ToString(), true, COLOR_ERROR));
                    ReportProgress(new FileCopyStatus(fi, CopyResult.READ_ERROR, srcDir + fileName));
                    fileCopyOk = false;
                }
            }
            return fileCopyOk;

        }

        private void CreateDirectoryIfNeeded(string directoryPath)
        {
            try { Directory.CreateDirectory(directoryPath); }
            catch (Exception ex)
            {
                ReportProgress(new RichTextAppendArguments("[DIR_CREATE_ERROR] " + directoryPath + "\n" + ex.ToString(), true, COLOR_ERROR));
            }
        }
        private void MyCopyFile_ProgressChanged(int bytesCopied)
        {
            ReportProgress(new RichTextAppendArguments("[file progress] " + bytesCopied, true, Color.AliceBlue));
        }
        #endregion
    }
}
