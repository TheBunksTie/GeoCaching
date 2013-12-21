using System;
using System.IO;

namespace Swk5.GeoCaching.DomainModel {
    public class Image {
        public Image(int id, int cacheId, string fileName) {
            Id = id;
            CacheId = cacheId;
            FileName = fileName;
        }

        public byte[] ImageData { get; private set; }

        public int Id { get; set; }
        public int CacheId { get; set; }
        public string FileName { get; set; }

        public void LoadImage(string localImageRepository) {
            if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(localImageRepository)) {
                string qualifiedDirectory = localImageRepository + @"\" + CacheId + @"\";

                // initialize a file stream to read the image file
                using (var imageStream = new FileStream(qualifiedDirectory + FileName, FileMode.Open, FileAccess.Read)) {
                    ImageData = new byte[imageStream.Length];

                    // read the data from the file stream and store them
                    imageStream.Read(ImageData, 0, Convert.ToInt32(imageStream.Length));
                    imageStream.Close();
                }
            }
        }

        public void SaveImage(string localImageRepository) {
            if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(localImageRepository)) {
                string qualifiedDirectory = localImageRepository + @"\" + CacheId + @"\";

                // check for the directories itself to exist
                if (!Directory.Exists(qualifiedDirectory)) {
                    Directory.CreateDirectory(qualifiedDirectory);
                }

                // initialize a file stream to write the image file
                using (var imageStream = new FileStream(qualifiedDirectory + FileName, FileMode.CreateNew, FileAccess.Write)) {
                    // read the data from the file stream and store them
                    imageStream.Write(ImageData, 0, Convert.ToInt32(imageStream.Length));
                    imageStream.Close();
                }
            }
        }

        public void Delete(string localImageRepository) {
            string qualifiedDirectory = localImageRepository + @"\" + CacheId + @"\";
            File.Delete(qualifiedDirectory + FileName);
        }
    }
}