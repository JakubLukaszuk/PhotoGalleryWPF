using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace PhotoGalleryWPF.Data
{
    public class DataProvider
    {
        public String[] getPathsFilesFromFolder(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            if (searchFolder != "")
            {
                var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                foreach (var filter in filters)
                {
                    filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
                }
            }

            return filesFound.ToArray();
        }

        public BitmapImage getBitmapFromPath(string path)
        {
            return new BitmapImage(new Uri(path));
        }

        public long getFileSizeMb(string path)
        {
            return new System.IO.FileInfo(path).Length;
        }

        public DateTime getFileCreationTime(string path)
        {
            return File.GetCreationTime(path);
        }

        public DateTime getFileLastModificationTime(string path)
        {
            return File.GetLastWriteTime(path);
        }

        public string getFileName(string path)
        {
            return Path.GetFileName(path);
        }
    }
}
