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
    
        public List<Cache> GetCacheList() {
            return cacheDao.GetAll();
        }

        public bool CreateNewDefaultCache() {
            throw new NotImplementedException();
        }

        public Cache CreateNewPositionedCache(int ownerId, double latitude, double longitude) {
            var defaultCache = new Cache(-1,
                "<default cache>",
                DateTime.Now,
                1,
                1,
                "Regular",
                ownerId,
                new GeoPosition(latitude, longitude),
                "put a short description here");
            cacheDao.Insert(defaultCache);
            return defaultCache;
        }

        public bool CreateNewCacheFromData(Cache c) {
            throw new NotImplementedException();
        }

        public bool UpdateExisitingCache(Cache c) {
            return cacheDao.Update(c);
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

        public void DeleteImage(Image image) {
            if (!imageDao.Delete(image)) {
                throw new Exception("Error: Unable to delete image");
            }
        }

        public List<Cache> GetFilteredCacheList(FilterCriterium criterium = FilterCriterium.Size,
            FilterOperation operation = FilterOperation.AboveEquals,
            string filterValue = "1") {
            try {
                return cacheDao.GetCacheByCriterium(criterium, operation, filterValue);
            }
            catch {
                throw new Exception("Error: Unable to resolve filter criterium.");
            }
        }
    }
}