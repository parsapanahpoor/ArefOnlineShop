using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application.Convertors
{
    public class FileMerger
    {
        public string FileName { get; set; }
        public string TempFolder { get; set; }
        public int MaxFileSizeMB { get; set; }
        public List<String> FileParts { get; set; }

        public FileMerger()
        {
            FileParts = new List<string>();
        }

        public string MergeFile(string fileName, string targetPath)
        {
            bool rslt = false;
            string partToken = ".part_";

            string baseFileName = fileName.Substring(0, fileName.IndexOf(partToken));
            string trailingTokens = fileName.Substring(fileName.IndexOf(partToken) + partToken.Length);

            int FileIndex = 0;
            int FileCount = 0;

            int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out FileIndex);
            int.TryParse(trailingTokens.Substring(trailingTokens.IndexOf(".") + 1), out FileCount);
            string Searchpattern = Path.GetFileName(baseFileName) + partToken + "*";
            string[] FilesList = Directory.GetFiles(Path.GetDirectoryName(fileName), Searchpattern);

            if (FilesList.Count() == FileCount)
            {
                if (!MergeFileManager.Instance.InUse(baseFileName))
                {
                    MergeFileManager.Instance.AddFile(baseFileName);

                    if (File.Exists(baseFileName))
                        File.Delete(baseFileName);

                    List<SortedFile> MergeList = new List<SortedFile>();

                    foreach (string File in FilesList)
                    {
                        SortedFile sFile = new SortedFile();
                        sFile.FileName = File;
                        baseFileName = File.Substring(0, File.IndexOf(partToken));
                        trailingTokens = File.Substring(File.IndexOf(partToken) + partToken.Length);
                        int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out FileIndex);
                        sFile.FileOrder = FileIndex;
                        MergeList.Add(sFile);
                    }

                    var MergeOrder = MergeList.OrderBy(s => s.FileOrder).ToList();

                    var newFileName = Guid.NewGuid() + Path.GetExtension(baseFileName);

                    if (!Directory.Exists(targetPath))
                    {
                        Directory.CreateDirectory(targetPath);
                    }

                    using (FileStream FS = new FileStream(targetPath + newFileName, FileMode.Create))
                    {
                        foreach (var chunk in MergeOrder)
                        {
                            try
                            {
                                using (FileStream fileChunk = new FileStream(chunk.FileName, FileMode.Open))
                                {
                                    fileChunk.CopyTo(FS);
                                }
                            }
                            catch (IOException)
                            {
                                throw;
                            }

                        }
                    }

                    foreach (var chunk in MergeOrder)
                    {
                        File.Delete(chunk.FileName);
                    }

                    MergeFileManager.Instance.RemoveFile(baseFileName);

                    return newFileName;
                }
            }
            return "";
        }
    }

    public struct SortedFile
    {
        public int FileOrder { get; set; }
        public String FileName { get; set; }
    }

    public class MergeFileManager
    {
        private static MergeFileManager instance;

        private List<string> MergeFileList;

        private MergeFileManager()
        {
            try
            {
                MergeFileList = new List<string>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static MergeFileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MergeFileManager();
                }

                return instance;
            }
        }

        public void AddFile(string BaseFileName)
        {
            MergeFileList.Add(BaseFileName);
        }

        public bool InUse(string BaseFileName)
        {
            return MergeFileList.Contains(BaseFileName);
        }

        public bool RemoveFile(string BaseFileName)
        {
            return MergeFileList.Remove(BaseFileName);
        }
    }
}