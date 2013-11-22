using System;
using System.Configuration;
using System.Reflection;
using Swk5.GeoCaching.DAL.Common.DaoInterface;

namespace Swk5.GeoCaching.DAL.Common {
    public static class DALFactory {
        private const string DaoLocation = ".Dao.";
        private static readonly string assemblyName;
        private static readonly Assembly dalAssembly;

        static DALFactory() {
            // TODO: add error handling

            assemblyName = ConfigurationManager.AppSettings["DALAssembly"];
            dalAssembly = Assembly.Load(assemblyName);
        }

        public static IDatabase CreateDatabase() {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        public static IDatabase CreateDatabase(string connectionString) {
            // fully qualified database class name
            string databaseClassName = assemblyName + ".Database";
            Type dbClass = dalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass,
                new object[] {connectionString}) as IDatabase;
        }

        public static ICacheDao CreateCacheDao(IDatabase database) {
            Type cacheType = dalAssembly.GetType(assemblyName + DaoLocation + "CacheDao");
            return Activator.CreateInstance(cacheType, new object[] {database})
                as ICacheDao;
        }

        public static IUserDao CreateUserDao(IDatabase database) {
            Type userType = dalAssembly.GetType(assemblyName + DaoLocation + "UserDao");
            return Activator.CreateInstance(userType, new object[] {database})
                as IUserDao;
        }

        public static ILogEntryDao CreateLogEntryDao(IDatabase database) {
            Type logEntryType = dalAssembly.GetType(assemblyName + DaoLocation + "LogEntryDao");
            return Activator.CreateInstance(logEntryType, new object[] {database})
                as ILogEntryDao;
        }

        public static IRatingDao CreateRatingDao(IDatabase database) {
            Type ratingType = dalAssembly.GetType(assemblyName + DaoLocation + "RatingDao");
            return Activator.CreateInstance(ratingType, new object[] {database})
                as IRatingDao;
        }

        public static IImageDao CreateImageDao(IDatabase database) {
            Type imageType = dalAssembly.GetType(assemblyName + DaoLocation + "ImageDao");
            return Activator.CreateInstance(imageType, new object[] {database})
                as IImageDao;
        }
    }
}