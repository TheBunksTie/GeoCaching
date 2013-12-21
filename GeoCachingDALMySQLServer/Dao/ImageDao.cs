using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class ImageDao : AbstractDaoBase, IImageDao {
        public ImageDao(IDatabase database) : base(database) {}

        public List<Image> GetAllForCache(int cacheId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT id, cacheId, fileName FROM cache_image " +
                "WHERE cacheId = @cacheId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return GetImagesForCache(cmd);
        }

        public bool Insert(Image image) {
            bool success = false;

            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO cache_image (cacheId, fileName) " +
                "VALUES (@cacheId, @fileName);");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, image.CacheId);
            database.DefineParameter(cmd, "fileName", DbType.String, image.FileName);

            success = database.ExecuteNonQuery(cmd) == 1;

            if (success) {
                // if database entry was inserted succesfully, store image data in local image dir
                image.SaveImage(database.LocalImageRepository);
            }

            return success;
        }

        public bool Delete(int id) {
            IDbCommand cmd = database.CreateCommand(
                "DELETE FROM cache_image " +
                "WHERE id = @id;");
            database.DefineParameter(cmd, "id", DbType.Int32, id);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool DeleteAllForCache(int cacheId) {
            foreach (var image in GetAllForCache(cacheId)) {
                image.Delete(database.LocalImageRepository);
            }

            IDbCommand cmd = database.CreateCommand(
               "DELETE FROM cache_image " +
               "WHERE cacheId = @cacheId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return database.ExecuteNonQuery(cmd) >= 0;
        }

        private List<Image> GetImagesForCache(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                var images = new List<Image>();


                // while reading add app-wise configured local image storage to pathname
                while (reader.Read()) {
                    images.Add(new Image(
                        ( int ) reader["id"],
                        ( int ) reader["cacheId"],
                        ( string ) reader["fileName"]));
                }

                // explicitly load each image (for use in async methods, when reader is already closed)
                foreach (Image image in images) {
                    image.LoadImage(database.LocalImageRepository + @"\");
                }

                return images;
            }
        }
    }
}