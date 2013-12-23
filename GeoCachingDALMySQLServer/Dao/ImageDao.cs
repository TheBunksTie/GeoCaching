using System.Data;
using System.Collections.Generic;
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
            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO cache_image (cacheId, fileName) " +
                "VALUES (@cacheId, @fileName);");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, image.CacheId);
            database.DefineParameter(cmd, "fileName", DbType.String, image.FileName);

            if ( database.ExecuteNonQuery(cmd) == 1 ) {
                // retrieve id of just generated database entry and store in in cache
                IDbCommand idCmd = database.CreateCommand("SELECT last_insert_id();");
                image.Id = ( int ) database.ExecuteScalarQuery<long>(idCmd);

                // if database entry was inserted succesfully, store image data in local image dir
                image.SaveImageData(database.LocalImageRepository);
            }
            else {
                image.Id = -1;
            }
            return image.Id > 0;
        }

        public bool Delete(Image image) {
            bool success = false;

            IDbCommand cmd = database.CreateCommand(
                "DELETE FROM cache_image " +
                "WHERE id = @id;");
            database.DefineParameter(cmd, "id", DbType.Int32, image.Id);

            success = database.ExecuteNonQuery(cmd) == 1;

            if (success) {
                // delete phsysical represenation of file
                image.Delete(database.LocalImageRepository);
            }

            return success;
        }

        public bool DeleteAllForCache(int cacheId) {
            foreach (var image in GetAllForCache(cacheId)) {
                image.Delete(database.LocalImageRepository);
            }

            IDbCommand cmd = database.CreateCommand(
               "DELETE FROM cache_image " +
               "WHERE cacheId = @cacheId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return database.ExecuteNonQuery(cmd) > 0;
        }

        private List<Image> GetImagesForCache(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                var images = new List<Image>();

                // while reading add app-wise configured local image storage to pathname
                while (reader.Read()) {
                    images.Add(new Image{
                        Id = ( int ) reader["id"],
                        CacheId = ( int ) reader["cacheId"],
                        FileName = reader["fileName"].ToString()});
                }

                // explicitly load each image (for use in async methods and when reader is already closed)
                foreach (Image image in images) {
                    image.LoadImageData(database.LocalImageRepository + @"\");
                }

                return images;
            }
        }
    }
}