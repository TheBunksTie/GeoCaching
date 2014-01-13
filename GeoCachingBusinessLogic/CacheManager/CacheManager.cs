using System;
using System.Collections.Generic;
using System.IO;
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

        public List<LogEntry> GetLogEntriesforCache(int cacheId) {
            return logEntryDao.GetLogEntriesForCache(cacheId);
        }

        public double GetAverageRatingForCache(int cacheId) {
            return ratingDao.GetAverageCacheRating(cacheId);
        }

        public List<Cache> GetCacheList() {
            return cacheDao.GetAll();
        }

        public Cache CreateNewPositionedCache(int ownerId, double latitude, double longitude) {
            var defaultCache = new Cache{ Id = -1,
                Name = "<default cache>", CreationDate = DateTime.Now, CacheDifficulty = 1, TerrainDifficulty = 1, Size = "Regular",
                OwnerId = ownerId, Position = new GeoPosition(latitude, longitude), Description = "put a short description here"};
            cacheDao.Insert(defaultCache);
            return defaultCache;
        }

        public bool UpdateExisitingCache(Cache c) {
            return cacheDao.Update(c);
        }

        public bool AddLogEntryForCache(LogEntry entry) {
            // TODO validation of entry
            return (logEntryDao.Insert(entry) > 0);
        }

        public bool AddRatingForCache(Rating rating) {
            return (ratingDao.Insert(rating) > 0);
        }

        public bool DeleteCache(int cacheId) {
            // check if there are no assigned log entries or ratings
            if (logEntryDao.GetLogEntriesForCache(cacheId).Count == 0 ||
                ratingDao.GetRatingsForCache(cacheId).Count == 0) {
                
                imageDao.DeleteAllForCache(cacheId);
                return cacheDao.Delete(cacheId);
            }
            throw new Exception("Error: Unable to delete cache due to assigned log entries/ratings.");
        }

        public Image UploadImage(int cacheId, Stream imageStream, string fileExtension) {
            var image = new Image {
                Id = -1,
                CacheId = cacheId,
                FileName = imageStream.GetHashCode() + DateTime.Now.GetHashCode() + fileExtension,
            };

            using (var memoryStream = new MemoryStream()) {
                imageStream.CopyTo(memoryStream);
                image.ImageData = memoryStream.ToArray();
            }

            // put into database
            if (imageDao.Insert(image)) {
                return image;
            }
            throw new Exception("Error: Unable to upload image.");
        }

        public bool DeleteImage(Image image) {
            if (!imageDao.Delete(image)) {
                throw new Exception("Error: Unable to delete image");
            }
            return true;
        }

        public List<Cache> GetFilteredCacheList(FilterCriterium criterium = FilterCriterium.Size,
            FilterOperation operation = FilterOperation.AboveEquals,
            string filterValue = "1") {
            try {
                return cacheDao.GetCachesByCriterium(criterium, operation, filterValue);
            }
            catch {
                throw new Exception("Error: Unable to resolve filter criterium.");
            }
        }
    }
}