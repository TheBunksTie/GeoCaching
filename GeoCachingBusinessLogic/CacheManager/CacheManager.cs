using System;
using System.Collections.Generic;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.CacheManager {
    public class CacheManager : AbstractManagerBase, ICacheManager {

        private readonly ICacheDao cacheDao = DalFactory.CreateCacheDao(database);
        private readonly ILogEntryDao logEntryDao = DalFactory.CreateLogEntryDao(database);
        private readonly IRatingDao ratingDao = DalFactory.CreateRatingDao(database);


        public List<string> GetCacheSizeList() {
            return cacheDao.GetAllCacheSizes();
        }

        public List<Cache> GetCacheList() {
            return cacheDao.GetAll();
        }

        public bool CreateNewDefaultCache(Cache c) {
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