using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multi_File_Copy
{
    /// <summary>
    /// A file copy that uses the FileStream class, with selectable buffersize and PartcopyProgress.
    /// </summary>
    public class MyFileCopy
    {
        public static int bufferSizeInMBytes = 1;

        public static Action<int> CopyProgressChanged = null;

        public static void Copy(string srcFilePath, string destFilePath)
        {
            int bufferSize = 1024 * 1024 * bufferSizeInMBytes;

            using (System.IO.FileStream srcFileStream = new System.IO.FileStream(srcFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read, bufferSize),
                                        destFileStream = new System.IO.FileStream(destFilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.Read, bufferSize))
            {
                destFileStream.SetLength(srcFileStream.Length);
                int bytesRead = -1;
                byte[] bytes = new byte[bufferSize];

                while ((bytesRead = srcFileStream.Read(bytes, 0, bufferSize)) > 0)
                {
                    destFileStream.Write(bytes, 0, bytesRead);
                    if (CopyProgressChanged != null) CopyProgressChanged(bytesRead);
                }
            }
        }

        public static void Copy(System.IO.Stream inStream, string outputFilePath)
        {
            int bufferSize = 1024 * 1024 * bufferSizeInMBytes;

            using (System.IO.FileStream fileStream = new System.IO.FileStream(outputFilePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write))
            {
                fileStream.SetLength(inStream.Length);
                int bytesRead = -1;
                byte[] bytes = new byte[bufferSize];

                while ((bytesRead = inStream.Read(bytes, 0, bufferSize)) > 0)
                {
                    fileStream.Write(bytes, 0, bytesRead);
                }
            }
        }
    }
    public enum CopyResult
    {
        OK,
        FOLDER_NOT_COMPLETE,
        STARTED,
        SKIPPED,
        PATH_TO_LONG,
        ACCESS_DENIED,
        READ_ERROR
    }

    public class FileCopyStatus
    {
        public CopyResult Result;
        public int Index = 0;
        public string FilePath = string.Empty;

        public FileCopyStatus(int index, CopyResult result)
        {
            Index = index;
            Result = result;
        }
        public FileCopyStatus(int index, CopyResult result, string filePath)
        {
            Index = index;
            Result = result;
            FilePath = filePath;
        }
    }
    public class FolderCopyStatus
    {
        public CopyResult Result;
        public int Index = 0;

        public FolderCopyStatus(int index, CopyResult result)
        {
            Index = index;
            Result = result;
        }
    }
}
