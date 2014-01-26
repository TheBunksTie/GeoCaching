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
            Database = new Database(ConnectionString, "<image is not important here>");
            target = new LogEntryDao(Database);
        }

        [TestMethod]
        public void GetAllTest() {
            const int expected = 6956;
            IList<LogEntry> actual = target.GetAll();
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void GetByIdTest() {
            const int id = 3565;
            const int cacheId = 260;
            const int creatorId = 240;
            var expected = new LogEntry(id, cacheId, creatorId, new DateTime(2007, 1, 13), false, "einfach klasse");
            LogEntry actual = target.GetById(id);
            Assert.AreEqual(expected.CacheId, actual.CacheId);
            Assert.AreEqual(expected.CreatorId, actual.CreatorId);
            Assert.AreEqual(expected.CreationDate, actual.CreationDate);
            Assert.AreEqual(expected.IsFound, actual.IsFound);
            Assert.AreEqual(expected.Comment, actual.Comment);
        }

        [TestMethod]
        public void GetLogEntriesForCacheTest() {
            const int cacheId = 117;
            IList<LogEntry> expected = new List<LogEntry>();
            expected.Add(new LogEntry {Id = 1570});
            expected.Add(new LogEntry {Id = 1571});
            expected.Add(new LogEntry {Id = 1572});
            expected.Add(new LogEntry {Id = 1573});
            expected.Add(new LogEntry {Id = 1574});
            expected.Add(new LogEntry {Id = 1575});
            expected.Add(new LogEntry {Id = 1576});
            expected.Add(new LogEntry {Id = 1577});
            expected.Add(new LogEntry {Id = 1578});
            expected.Add(new LogEntry {Id = 1579});
            expected.Add(new LogEntry {Id = 1580});
            expected.Add(new LogEntry {Id = 1581});

            IList<LogEntry> actual = target.GetLogEntriesForCache(cacheId);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (LogEntry entry in expected) {
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void GetLogEntriesForCacheAndUserTest() {
            const int cacheId = 497;
            const int userId = 225;
            IList<LogEntry> expected = new List<LogEntry>();
            expected.Add(new LogEntry(6922, cacheId, userId, new DateTime(2007, 4, 10), false, "viel zu schwer, fast unmöglich"));
            expected.Add(new LogEntry(6926, cacheId, userId, new DateTime(2008, 1, 13), true, "diesmal gings besser"));

            IList<LogEntry> actual = target.GetLogEntriesForCacheAndUser(cacheId, userId);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (LogEntry entry in expected) {
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void GetLogentriesForUserTest() {
            const int userId = 199;
            IList<LogEntry> expected = new List<LogEntry>();
            expected.Add(new LogEntry {Id = 453});
            expected.Add(new LogEntry {Id = 1099});
            expected.Add(new LogEntry {Id = 1703});
            expected.Add(new LogEntry {Id = 1885});
            expected.Add(new LogEntry {Id = 2306});
            expected.Add(new LogEntry {Id = 2496});
            expected.Add(new LogEntry {Id = 2969});
            expected.Add(new LogEntry {Id = 2999});
            expected.Add(new LogEntry {Id = 3072});
            expected.Add(new LogEntry {Id = 3655});
            expected.Add(new LogEntry {Id = 4233});
            expected.Add(new LogEntry {Id = 4667});
            expected.Add(new LogEntry {Id = 4834});
            expected.Add(new LogEntry {Id = 4879});
            expected.Add(new LogEntry {Id = 4971});
            expected.Add(new LogEntry {Id = 5706});
            expected.Add(new LogEntry {Id = 6066});
            expected.Add(new LogEntry {Id = 6208});

            IList<LogEntry> actual = target.GetLogentriesForUser(userId);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (LogEntry entry in expected) {
                Assert.IsTrue(actual.Contains(entry));
            }
        }

        [TestMethod]
        public void InsertTest() {
            const int cacheId = 499;
            const int creatorId = 32;
            LogEntry toInsert = new LogEntry(-1, cacheId, creatorId, new DateTime(2009, 12, 6), false, "not so fantastic");

            target.Insert(toInsert);
            LogEntry expected = target.GetById(toInsert.Id);

            Assert.AreEqual(expected, toInsert);
            Assert.AreEqual(expected.CreationDate, toInsert.CreationDate);
            DeleteLogEntry(toInsert.Id); // internal method, not available in DaoInterface
            Assert.IsNull(target.GetById(toInsert.Id));
        }

        private bool DeleteLogEntry(int id) {
            IDbCommand cmd = Database.CreateCommand(
                "DELETE FROM cache_log " +
                "WHERE id = @id;");
            Database.DefineParameter(cmd, "id", DbType.Int32, id);

            return Database.ExecuteNonQuery(cmd) == 1;
        }
    }
}