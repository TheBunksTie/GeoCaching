using System;

namespace Swk5.GeoCaching.DomainModel {
    public class Rating {
        
        public Rating(int cacheId, string creator, DateTime creationDate, int grade) {
            CacheId = cacheId;
            Creator = creator;
            CreationDate = creationDate;
            Grade = grade;
        }

        public int CacheId { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public int Grade { get; set; }

    }
}
