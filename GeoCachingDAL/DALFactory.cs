using System;
using System.Configuration;
using System.Reflection;
using Swk5.GeoCaching.DAL.Common.DaoInterface;

namespace Swk5.GeoCaching.DAL.Common {
    public static class DalFactory {
        private const string DaoLocation = ".Dao.";
        private static readonly string AssemblyName;
        private static readonly Assembly DalAssembly;

        static DalFactory() {
            // TODO: add error handling
            try {
                AssemblyName = ConfigurationManager.AppSettings["DALAssembly"];
                DalAssembly = Assembly.Load(AssemblyName);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public static IDatabase CreateDatabase() {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        public static IDatabase CreateDatabase(string connectionString) {
            // fully qualified database class name
            string databaseClassName = AssemblyName + ".Database";
            Type dbClass = DalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass,
                new object[] {connectionString}) as IDatabase;
        }

        public static ICacheDao CreateCacheDao(IDatabase database) {
            Type cacheType = DalAssembly.GetType(AssemblyName + DaoLocation + "CacheDao");
            return Activator.CreateInstance(cacheType, new object[] {database})
                as ICacheDao;
        }

        public static IUserDao CreateUserDao(IDatabase database) {
            Type userType = DalAssembly.GetType(AssemblyName + DaoLocation + "UserDao");
            return Activator.CreateInstance(userType, new object[] {database})
                as IUserDao;
        }

        public static ILogEntryDao CreateLogEntryDao(IDatabase database) {
            Type logEntryType = DalAssembly.GetType(AssemblyName + DaoLocation + "LogEntryDao");
            return Activator.CreateInstance(logEntryType, new object[] {database})
                as ILogEntryDao;
        }

        public static IRatingDao CreateRatingDao(IDatabase database) {
            Type ratingType = DalAssembly.GetType(AssemblyName + DaoLocation + "RatingDao");
            return Activator.CreateInstance(ratingType, new object[] {database})
                as IRatingDao;
        }

        public static IImageDao CreateImageDao(IDatabase database) {
            Type imageType = DalAssembly.GetType(AssemblyName + DaoLocation + "ImageDao");
            return Activator.CreateInstance(imageType, new object[] {database})
                as IImageDao;
        }
    }
}