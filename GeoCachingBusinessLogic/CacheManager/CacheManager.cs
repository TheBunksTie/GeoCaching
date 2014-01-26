using System;
using System.Collections.Generic;
using System.IO;
using Swk5.GeoCaching.BusinessLogic.FilterManager;
using Swk5.GeoCaching.DAL.Common;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.CacheManager {
    public class CacheManager : AbstractManagerBase, ICacheManager {
        // TODO set readonly again
        private User authenticatedUser;
        private readonly ICacheDao cacheDao = DalFactory.CreateCacheDao(database);
        private readonly IFilterManager filterManager = GeoCachingBLFactory.GetFilterManager();
        private readonly IImageDao imageDao = DalFactory.CreateImageDao(database);
        private readonly ILogEntryDao logEntryDao = DalFactory.CreateLogEntryDao(database);
        private readonly IRatingDao ratingDao = DalFactory.CreateRatingDao(database);
        private readonly IUserDao userDao = DalFactory.CreateUserDao(database);

        public CacheManager() {
            authenticatedUser = GeoCachingBLFactory.GetAuthenticationManager().AuthenticatedUser;
        }

        public User GetAuthenticatedUser() {
            return authenticatedUser;
        }

        // TODO only for unit testing purposes
        public void SetAuthenticatedUser(User u) {
            authenticatedUser = u;
        }

        public List<Cache> GetFilteredCacheList(DataFilter filter) {
            if (filter != null) {
                try {
                    return cacheDao.GetCachesMatchingFilter(filter);
                }
                catch {
                    throw new Exception("Error: Unable to connect to database.");   
                }
                
            }
            throw new Exception("Error: The provided data filter is invalid.");
        }

        public DataFilter GetDefaultFilter() {
            return filterManager.GetDefaultFilter();
        }

        public Cache GetCacheById(int cacheId) {
            ValidateCacheId(cacheId);

            try {
                return cacheDao.GetById(cacheId);
            }
            catch {
                throw new Exception("Error: Unable to connect to database.");
            }
        }

        public List<string> GetCacheSizeList() {
            return cacheDao.GetAllCacheSizes();
        }

        public User GetCacheOwner(Cache c) {
            ValidateCache(c);
            return userDao.GetById(c.OwnerId);
        }

        public List<Image> GetImagesForCache(int cacheId) {
            ValidateCacheId(cacheId);
            return imageDao.GetAllForCache(cacheId);
        }

        public List<LogEntry> GetLogEntriesforCache(int cacheId) {
            ValidateCacheId(cacheId);
            return logEntryDao.GetLogEntriesForCache(cacheId);
        }

        public double GetAverageRatingForCache(int cacheId) {
            ValidateCacheId(cacheId);
            return ratingDao.GetAverageCacheRating(cacheId);
        }

        public Cache CreateNewPositionedCache(int ownerId, double latitude, double longitude) {
            var defaultCache = new Cache {
                Id = -1,
                Name = "<default cache>",
                CreationDate = DateTime.Now,
                CacheDifficulty = 1,
                TerrainDifficulty = 1,
                Size = "Regular",
                OwnerId = ownerId,
                Position = new GeoPosition(latitude, longitude),
                Description = "put a short description here"
            };

            // just for owner id and longitude/latitude
            ValidateCache(defaultCache);
            cacheDao.Insert(defaultCache);
            return defaultCache;
        }

        public bool UpdateExisitingCache(Cache c) {
            ValidateCache(c);

            // in addition check if cache owner is authenticated user
            if (c.OwnerId == authenticatedUser.Id) {
                return cacheDao.Update(c);
            }
            throw new Exception("Error: You are not the cache owner and therefor not permitted to change any details.");
        }

        public bool AddLogEntryForCache(LogEntry entry) {
            ValidateLogEntry(entry);
            return (logEntryDao.Insert(entry) > 0);
        }

        public bool AddRatingForCache(Rating rating) {
            ValidateRating(rating);
            return (ratingDao.Insert(rating) > 0);
        }

        public bool DeleteCache(int cacheId) {
            ValidateCacheOwner(cacheId);

            // check if there are no assigned log entries or ratings
            if (logEntryDao.GetLogEntriesForCache(cacheId).Count == 0 ||
                ratingDao.GetRatingsForCache(cacheId).Count == 0) {
                
                imageDao.DeleteAllForCache(cacheId);
                return cacheDao.Delete(cacheId);
            }
            throw new Exception("Error: Unable to delete cache due to assigned log entries/ratings.");
        }

        public Image UploadImage(int cacheId, Stream imageStream, string fileExtension) {
            ValidateCacheOwner(cacheId);
            
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
            ValidateCacheOwner(image.CacheId);
            
            if (!imageDao.Delete(image)) {
                throw new Exception("Error: Unable to delete image");
            }
            return true;
        }

        private void ValidateCache(Cache c) {
            if (c == null) {
                throw new Exception("Error: The requested operation cannot be performed as the provided data are corrupt.");
            }

            if (string.IsNullOrEmpty(c.Name)) {
                throw new Exception("Error: The provided cache name is too short.");
            }

            if (c.OwnerId < 1) {
                throw new Exception("Error: The provided cache owner is invalid-");
            }

            if ( string.IsNullOrEmpty(c.Description)) {
                throw new Exception("Error: The provided cache description is empty-");
            }

            if (c.Position.Latitude <= 0 || c.Position.Longitude <= 0) {
                throw new Exception("Error: The provided cache position of is invalid.");
            }

            if ( string.IsNullOrEmpty(c.Size)) {
                throw new Exception("Error: The provided cache size is invalid.");
            }

            if (c.CacheDifficulty < 1 || c.CacheDifficulty > 5) {
                throw new Exception("Error: The provided cache difficulty is invalid. (Must be between 1 and 5)");
            }

            if (c.TerrainDifficulty < 1 || c.TerrainDifficulty > 5) {
                throw new Exception("Error: The provided terrain difficulty is invalid. (Must be between 1 and 5)");
            }
        }

        private void ValidateLogEntry(LogEntry e) {
            if (e == null) {
                throw new Exception(
                    "Error: The requested operation cannot be performed as the provided data are corrupt.");
            }

            if (e.CacheId < 1) {
                throw new Exception("Error: The provided cache to log is invalid.");
            }

            if (e.CreatorId < 1) {
                throw new Exception("Error: The provided creator of the log entry is invalid");
            }
        }

        private void ValidateRating(Rating r) {
            if (r == null) {
                throw new Exception(
                    "Error: The requested operation cannot be performed as the provided data are corrupt.");
            }

            if (r.CacheId < 1) {
                throw new Exception("Error: The provided cache to be rated is invalid.");
            }

            if (r.CreatorId < 1) {
                throw new Exception("Error: The provided creator of the rating is invalid.");
            }

            if (r.Grade < 1 || r.Grade > 10) {
                throw new Exception("Error: The provided cache rating is invalid. (Must be between 1 and 10)");
            }
        }

        private void ValidateCacheId(int cacheId) {                              
            if ( cacheId < 1 ) {
                throw new Exception("Error: The provided cache id was invalid.");
            }
        }

        private void ValidateCacheOwner(int cacheId) {
            if (cacheDao.GetById(cacheId).OwnerId != authenticatedUser.Id) {
                throw new Exception("Error: Only the cache owner is allowed to modify/delete this cache.");
            }
        }
    }
}