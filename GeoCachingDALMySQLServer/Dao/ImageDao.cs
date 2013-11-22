using System;
using System.Collections.Generic;
using System.Data;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;

namespace Swk5.GeoCaching.DAL.MySQLServer.Dao {
    public class ImageDao : IImageDao {
        private readonly IDatabase database;

        public ImageDao(IDatabase database) {
            this.database = database;
        }

        public IList<String> GetAllForCache(int cacheId) {
            IDbCommand cmd = database.CreateCommand(
                "SELECT fileName FROM cache_image " +
                "WHERE cacheId = @cacheId;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);

            return GetImagesForCache(cmd);
        }

        public bool Insert(int cacheId, string fileName) {
            IDbCommand cmd = database.CreateCommand(
                "INSERT INTO cache_image (cacheId, fileName) " +
                "VALUES (@cacheId, @fileName)");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            database.DefineParameter(cmd, "fileName", DbType.String, fileName);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        public bool Delete(int cacheId, string fileName) {
            IDbCommand cmd = database.CreateCommand(
                "DELETE FROM cache_image " +
                "WHERE cacheId = @cacheId AND fileName = @fileName;");
            database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            database.DefineParameter(cmd, "fileName", DbType.String, fileName);

            return database.ExecuteNonQuery(cmd) == 1;
        }

        private IList<String> GetImagesForCache(IDbCommand cmd) {
            using (IDataReader reader = database.ExecuteReader(cmd)) {
                IList<string> images = new List<string>();

                while (reader.Read()) {
                    images.Add(( string ) reader["fileName"]);
                }
                return images;
            }
        }
    }
}