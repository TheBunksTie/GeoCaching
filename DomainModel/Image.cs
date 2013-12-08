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

        public void LoadImage(string localImageDirectory) {
            if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(localImageDirectory)) {
                // initialize a file stream to read the image file
                using (var imageStream = new FileStream(localImageDirectory + FileName, FileMode.Open, FileAccess.Read)) {
                    ImageData = new byte[imageStream.Length];

                    // read the data from the file stream and store them
                    imageStream.Read(ImageData, 0, Convert.ToInt32(imageStream.Length));
                    imageStream.Close();
                }
            }
        }

        public void SaveImage(string localImageDirectory) {
            if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(localImageDirectory)) {
                // initialize a file stream to write the image file
                using (var imageStream = new FileStream(localImageDirectory + FileName, FileMode.CreateNew, FileAccess.Write)) {
                    // read the data from the file stream and store them
                    imageStream.Write(ImageData, 0, Convert.ToInt32(imageStream.Length));
                    imageStream.Close();
                }
            }
        }
    }
}