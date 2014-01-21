using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.BusinessLogic.StatisticsManager;
using Swk5.GeoCaching.DomainModel;

namespace GeoCachingTest {
    
    [TestClass]
    public class IStatisticsManagerTest {

        private IStatisticsManager statManager;

        [TestInitialize]
        public void Initialize ( ) {
            statManager = GeoCachingBLFactory.GetStatisticsManager();
        }
        
        [TestMethod]
        public void GetCacheDistributionByCacheDifficultyTest() {
            DataFilter limitation = new DataFilter {
                FromCreationDate = new DateTime(2001, 1,20),
                ToCreationDate = new DateTime(2031, 12, 20),
                FromPosition = new GeoPosition(46.966667, 12.1913),
                ToPosition = new GeoPosition(48.4719, 14.399167)
            };
            
            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "2", Value = "26,2000"},
                new StatisticData { Name = "1,5", Value = "22,2000"}
            };
            
            List<StatisticData> actual = statManager.GetCacheDistributionByCacheDifficulty(limitation).Data;
            
            Assert.AreEqual(9, actual.Count);
            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }
        }
        
        [TestMethod]
        public void GetCacheDistributionBySizeTest() {
            DataFilter limitation = new DataFilter {
                FromCreationDate = new DateTime(2001, 1, 20),
                ToCreationDate = new DateTime(2031, 12, 20),
                FromPosition = new GeoPosition(46.966667, 12.1913),
                ToPosition = new GeoPosition(48.4719, 14.399167)
            };

            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "Regular", Value = "39,6000"},
                new StatisticData { Name = "None", Value = "6,2000"}
            };

            List<StatisticData> actual = statManager.GetCacheDistributionBySize(limitation).Data;
            Assert.AreEqual(6, actual.Count);

            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }
        }

        [TestMethod]
        public void GetCacheDistributionByTerrainDifficultyTest() {
            DataFilter limitation = new DataFilter {
                FromCreationDate = new DateTime(2001, 1, 20),
                ToCreationDate = new DateTime(2031, 12, 20),
                FromPosition = new GeoPosition(46.966667, 12.1913),
                ToPosition = new GeoPosition(48.4719, 14.399167)
            };

            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "1,5", Value = "18,4000"},
                new StatisticData { Name = "1", Value = "11,6000"}
            };

            List<StatisticData> actual = statManager.GetCacheDistributionByTerrainDifficulty(limitation).Data;
            Assert.AreEqual(9, actual.Count);

            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }                       
        }

        [TestMethod]
        public void GetFoundCachesPerUserTest() {
            DataFilter limitation = new DataFilter {
                FromCreationDate = new DateTime(2001, 1, 20),
                ToCreationDate = new DateTime(2031, 12, 20),
                FromPosition = new GeoPosition(46.966667, 12.1913),
                ToPosition = new GeoPosition(48.4719, 14.399167)
            };


            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "Armin295", Value = "30"},
                new StatisticData { Name = "Sandra359", Value = "27"}
            };

            List<StatisticData> actual = statManager.GetFoundCachesByUser(limitation).Data;
            Assert.AreEqual(214, actual.Count);

            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }                       
        }

        [TestMethod]
        public void GetHiddenCachesPerUserTest() {
            DataFilter limitation = new DataFilter {
                FromCreationDate = new DateTime(2001, 1, 20),
                ToCreationDate = new DateTime(2031, 12, 20),
                FromPosition = new GeoPosition(46.966667, 12.1913),
                ToPosition = new GeoPosition(48.4719, 14.399167)
            };

            List<StatisticData> expected = new List<StatisticData> {
                new StatisticData { Name = "Andreas872", Value = "20"},
                new StatisticData { Name = "Barbara921", Value = "13"}
            };

            List<StatisticData> actual = statManager.GetHiddenCachesByUser(limitation).Data;
            Assert.AreEqual(40, actual.Count);

            foreach ( var data in expected ) {
                Assert.IsTrue(actual.Contains(data));
            }                       
        }

        //[TestMethod]
        //public void GetEarliestCacheCreationDateTest ( ) {
        //    var expected = new DateTime(2001, 1, 20);
        //    DateTime actual = statManager.GetEarliestCacheCreationDate();
        //    Assert.AreEqual(expected, actual);
        //}
        
        //[TestMethod]
        //public void GetHighestCachePositionTest() {
        //    var expected = new GeoPosition(48.4719, 14.399167);
        //    GeoPosition actual = statManager.GetHighestCachePosition();
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod]
        //public void GetLatestCacheCreationDateTest() {
        //    var expected = new DateTime(2031,12,20); 
        //    DateTime actual = statManager.GetLatestCacheCreationDate();
        //    Assert.AreEqual(expected, actual);
        //}
        
        //[TestMethod]
        //public void GetLowestCachePositionTest() {
        //    var expected = new GeoPosition(46.966667, 12.1913);
        //    GeoPosition actual = statManager.GetLowestCachePosition();
        //    Assert.AreEqual(expected, actual);
        //}
    }
}