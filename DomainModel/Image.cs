using System;
using System.IO;

namespace Swk5.GeoCaching.DomainModel {
    public class Image : IEquatable<Image> {
        public byte[] ImageData { get; set; }

        public int Id { get; set; }
        public int CacheId { get; set; }
        public string FileName { get; set; }

        public bool Equals(Image other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return Id == other.Id;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals(( Image ) obj);
        }

        public override int GetHashCode() {
            return Id;
        }

        public void LoadImageData(string localImageRepository) {
            if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(localImageRepository)) {
                string qualifiedDirectory = localImageRepository + @"\" + CacheId + @"\";

                ImageData = File.ReadAllBytes(qualifiedDirectory + FileName);
            }
        }

        public void SaveImageData(string localImageRepository) {
            if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(localImageRepository)) {
                string qualifiedDirectory = localImageRepository + @"\" + CacheId + @"\";

                // check for the directories itself to exist
                if (!Directory.Exists(qualifiedDirectory)) {
                    Directory.CreateDirectory(qualifiedDirectory);
                }

                File.WriteAllBytes(qualifiedDirectory + FileName, ImageData);
            }
        }

        public void Delete(string localImageRepository) {
            string qualifiedDirectory = localImageRepository + @"\" + CacheId + @"\";
            File.Delete(qualifiedDirectory + FileName);
        }
    }
}