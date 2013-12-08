using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.CacheManager {
    public class CacheManager : AbstractManagerBase, ICacheManager {
        private readonly ICacheDao cacheDao = DalFactory.CreateCacheDao(database);
        private readonly IImageDao imageDao = DalFactory.CreateImageDao(database);
        private readonly ILogEntryDao logEntryDao = DalFactory.CreateLogEntryDao(database);
        private readonly IRatingDao ratingDao = DalFactory.CreateRatingDao(database);
        private readonly IUserDao userDao = DalFactory.CreateUserDao(database);

        public List<string> GetCacheSizeList() {
            return cacheDao.GetAllCacheSizes();
        }

        public User GetCacheOwner(Cache c) {
            return userDao.GetById(c.OwnerId);
        }

        public List<Image> GetImagesForCache(int cacheId) {
            return imageDao.GetAllForCache(cacheId);
        }

        public bool AssignImageToCache(Image image) {
            // TODO check if name of image is not already in db, else rename? 
            return imageDao.Insert(image);
        }

        public List<Cache> GetCacheList() {
            return cacheDao.GetAll();
        }

        public bool CreateNewDefaultCache() {
            throw new NotImplementedException();
        }

        public bool CreateNewCacheFromData(Cache c) {
            throw new NotImplementedException();
        }

        public bool UpdateExisitingCache(Cache c) {
            throw new NotImplementedException();
        }

        public bool DeleteCache(Cache c) {
            throw new NotImplementedException();
        }
    }
}