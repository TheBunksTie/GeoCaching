using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.StatisticsManager;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    
    [TestClass]
    public class IStatisticsManagerTest {

        private IStatisticsManager statisticsManager;

        [TestInitialize]
        public void Initialize ( ) {
            statisticsManager = GeoCachingBLFactory.GetStatisticsManager();
        }
        
        [TestMethod]
        public void GetCacheDistributionByCacheDifficultyTest() {
            DataFilter filter = statisticsManager.GetDefaultFilter();
            
            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "2", Value = "26,2000"},
                new StatisticData { Name = "1,5", Value = "22,2000"}
            };
            
            List<StatisticData> actual = statisticsManager.GetCacheDistributionByCacheDifficulty(filter).Data;
            
            Assert.AreEqual(9, actual.Count);
            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }
        }
        
        [TestMethod]
        public void GetCacheDistributionBySizeTest() {
            DataFilter filter = statisticsManager.GetDefaultFilter();

            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "Regular", Value = "39,6000"},
                new StatisticData { Name = "None", Value = "6,2000"}
            };

            List<StatisticData> actual = statisticsManager.GetCacheDistributionBySize(filter).Data;
            Assert.AreEqual(6, actual.Count);

            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }
        }

        [TestMethod]
        public void GetCacheDistributionByTerrainDifficultyTest() {
            DataFilter filter = statisticsManager.GetDefaultFilter();

            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "1,5", Value = "18,4000"},
                new StatisticData { Name = "1", Value = "11,6000"}
            };

            List<StatisticData> actual = statisticsManager.GetCacheDistributionByTerrainDifficulty(filter).Data;
            Assert.AreEqual(9, actual.Count);

            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }                       
        }

        [TestMethod]
        public void GetFoundCachesPerUserTest() {
            DataFilter filter = statisticsManager.GetDefaultFilter();
            
            // set unbound limit
            filter.Limit = 500;
            
            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "Armin295", Value = "30"},
                new StatisticData { Name = "Sandra359", Value = "27"}
            };

            List<StatisticData> actual = statisticsManager.GetUsersByFoundCaches(filter).Data;
            Assert.AreEqual(215, actual.Count);

            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }                       
        }

        [TestMethod]
        public void GetHiddenCachesPerUserTest() {
            DataFilter filter = statisticsManager.GetDefaultFilter();

            // set unbound limit
            filter.Limit = 500;

            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "Andreas872", Value = "20"},
                new StatisticData { Name = "Barbara921", Value = "13"}
            };

            List<StatisticData> actual = statisticsManager.GetUsersByHiddenCaches(filter).Data;
            Assert.AreEqual(40, actual.Count);

            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }                       
        }

        [TestMethod]
        public void GetDefaultFilterTest ( ) {
            DataFilter expected = new DataFilter {
                FromDate = new DateTime(2001, 1, 20),
                ToDate = new DateTime(2031, 12, 20),
                FromPosition = new GeoPosition(46.966667, 12.1913),
                ToPosition = new GeoPosition(48.4719, 14.399167),
                FromCacheDifficulty = 1.0,
                ToCacheDifficulty = 5.0,
                FromTerrainDifficulty = 1.0,
                ToTerrainDifficulty = 5.0,
                FromCacheSize = 1,
                ToCacheSize = 6,
                CacheName = "*",                
                Limit = 10
            };
            DataFilter actual = statisticsManager.GetDefaultFilter();
            
            Assert.AreEqual(expected.FromDate, actual.FromDate);
            Assert.AreEqual(expected.ToDate, actual.ToDate);
            Assert.AreEqual(expected.FromPosition, actual.FromPosition);
            Assert.AreEqual(expected.ToPosition, actual.ToPosition);
        }
    }
}