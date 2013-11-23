using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.DAL.Common.DaoInterface;
using Swk5.GeoCaching.DAL.MySQLServer;
using Swk5.GeoCaching.DAL.MySQLServer.Dao;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    [TestClass]
    public class LogEntryDaoTest : AbstractTest {
        private ILogEntryDao target;

        [TestInitialize]
        public void Initialize() {
            Database = new Database(ConnectionString);
            target = new LogEntryDao(Database);
        }

        [TestMethod]
        public void GetAllTest() {
            const int expected = 6951;
            IList<LogEntry> actual = target.GetAll();
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void GetByPrimaryKeyTest() {
            const int cacheId = 260;
            const string creatorName = "Zelda553";
            var expected = new LogEntry(cacheId, creatorName, new DateTime(2007, 1, 13), false, "einfach klasse");
            LogEntry actual = target.GetByPrimaryKey(cacheId, creatorName);
            Assert.AreEqual(expected.CacheId, actual.CacheId);
            Assert.AreEqual(expected.Creator, actual.Creator);
            Assert.AreEqual(expected.CreationDate, actual.CreationDate);
            Assert.AreEqual(expected.IsFound, actual.IsFound);
            Assert.AreEqual(expected.Comment, actual.Comment);
        }

        [TestMethod]
        public void GetLogEntriesForCacheTest() {
            const int cacheId = 117;
            IList<LogEntry> expected = new List<LogEntry>();
            expected.Add(new LogEntry {Creator = "Luke746"});
            expected.Add(new LogEntry {Creator = "Konrad286"});
            expected.Add(new LogEntry {Creator = "John822"});
            expected.Add(new LogEntry {Creator = "Jimmy468"});
            expected.Add(new LogEntry {Creator = "Jason935"});
            expected.Add(new LogEntry {Creator = "Dominik372"});
            expected.Add(new LogEntry {Creator = "Charlie220"});
            expected.Add(new LogEntry {Creator = "Bunk417"});
            expected.Add(new LogEntry {Creator = "Benno235"});
            expected.Add(new LogEntry {Creator = "Benjamin699"});
            expected.Add(new LogEntry {Creator = "Bart467"});
            expected.Add(new LogEntry {Creator = "Arabella372"});

            IList<LogEntry> actual = target.GetLogEntriesForCache(cacheId);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (LogEntry entry in expected) {
                entry.CacheId = cacheId;
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void GetLogentriesForUserTest() {
            const string userName = "Rosa334";
            IList<LogEntry> expected = new List<LogEntry>();
            expected.Add(new LogEntry {CacheId = 32});
            expected.Add(new LogEntry {CacheId = 80});
            expected.Add(new LogEntry {CacheId = 126});
            expected.Add(new LogEntry {CacheId = 138});
            expected.Add(new LogEntry {CacheId = 169});
            expected.Add(new LogEntry {CacheId = 183});
            expected.Add(new LogEntry {CacheId = 217});
            expected.Add(new LogEntry {CacheId = 219});
            expected.Add(new LogEntry {CacheId = 225});
            expected.Add(new LogEntry {CacheId = 266});
            expected.Add(new LogEntry {CacheId = 308});
            expected.Add(new LogEntry {CacheId = 339});
            expected.Add(new LogEntry {CacheId = 351});
            expected.Add(new LogEntry {CacheId = 354});
            expected.Add(new LogEntry {CacheId = 359});
            expected.Add(new LogEntry {CacheId = 412});
            expected.Add(new LogEntry {CacheId = 437});
            expected.Add(new LogEntry {CacheId = 446});

            IList<LogEntry> actual = target.GetLogentriesForUser(userName);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (LogEntry entry in expected) {
                entry.Creator = userName;
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void InsertTest() {
            const int cacheId = 499;
            const string creatorName = "Bunk417";
            var toInsert = new LogEntry(cacheId, creatorName, new DateTime(2009, 12, 6), false, "not so fantastic");

            Assert.IsTrue(target.Insert(toInsert));
            LogEntry expected = target.GetByPrimaryKey(cacheId, creatorName);

            Assert.AreEqual(expected, toInsert);
            Assert.AreEqual(expected.CreationDate, toInsert.CreationDate);
            DeleteLogEntry(cacheId, creatorName); // internal method, not available in DaoInterface
            Assert.IsNull(target.GetByPrimaryKey(cacheId, creatorName));
        }

        [TestMethod]
        public void UpdateTest() {
            const int cacheId = 275;
            const string creatorName = "Benjamin699";
            LogEntry entry = target.GetByPrimaryKey(cacheId, creatorName);
            string initialComment = entry.Comment;
            string updateComment = initialComment + " - some additional comment stuff";
            entry.Comment = updateComment;

            Assert.IsTrue(target.Update(entry));

            entry = target.GetByPrimaryKey(cacheId, creatorName);
            Assert.AreEqual(updateComment, entry.Comment);

            entry.Comment = initialComment;
            Assert.IsTrue(target.Update(entry));

            entry = target.GetByPrimaryKey(cacheId, creatorName);
            Assert.AreEqual(initialComment, entry.Comment);
        }

        private bool DeleteLogEntry(int cacheId, string creatorName) {
            IDbCommand cmd = Database.CreateCommand(
                "DELETE FROM cache_log " +
                "WHERE cacheId = @cacheId AND creatorName = @creatorName;");
            Database.DefineParameter(cmd, "cacheId", DbType.Int32, cacheId);
            Database.DefineParameter(cmd, "creatorName", DbType.String, creatorName);

            return Database.ExecuteNonQuery(cmd) == 1;
        }
    }
}