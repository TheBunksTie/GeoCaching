﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.BusinessLogic.StatisticsManager;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Statistic {
    public class StatisticsCollectionVM : AbstractViewModelBase<StatisticsCollectionVM> {
        private readonly FilterVM filterVM;
        private readonly IStatisticsManager statisticsManager;
        private StatisticVM currentStatistic;
        private bool dateFilterRequested;
        private Filter defaultFilter;

        private ICommand getStatisticsCommand;

        private bool regionFilterRequested;
        private List<StatisticData> statisticsData;

        public StatisticsCollectionVM(IStatisticsManager statisticsManager) {
            this.statisticsManager = statisticsManager;
            defaultFilter = LoadDefaultFilter();

            statisticsData = new List<StatisticData>();

            // initialise filter with default values
            filterVM = new FilterVM(defaultFilter);
            LoadAvailableStatistics();
        }

        public ObservableCollection<StatisticVM> AvailableStatistics { get; private set; }

        public StatisticVM CurrentStatistic {
            get { return currentStatistic; }
            set {
                if (currentStatistic != value) {
                    currentStatistic = value;
                    RaisePropertyChangedEvent(vm => vm.CurrentStatistic);
                }
            }
        }

        public bool RegionFilterRequested {
            get { return regionFilterRequested; }
            set {
                if (regionFilterRequested != value) {
                    regionFilterRequested = value;
                    RaisePropertyChangedEvent(vm => vm.RegionFilterRequested);
                }
            }
        }

        public bool DateFilterRequested {
            get { return dateFilterRequested; }
            set {
                if (dateFilterRequested != value) {
                    dateFilterRequested = value;
                    RaisePropertyChangedEvent(vm => vm.DateFilterRequested);
                }
            }
        }

        public List<StatisticData> StatisticsData {
            get { return statisticsData; }
        }

        public ICommand GetStatisticsCommand {
            get { return getStatisticsCommand ?? (getStatisticsCommand = new RelayCommand(param => GetStatistics())); }
        }

        public FilterVM Filter {
            get { return filterVM; }
        }

        private void LoadAvailableStatistics() {            
            AvailableStatistics = new ObservableCollection<StatisticVM> {
                new StatisticVM(StatisticalOperation.CachesFoundByUser),
                new StatisticVM(StatisticalOperation.CachesHiddenByUser),
                new StatisticVM(StatisticalOperation.CacheDistributionBySize),
                new StatisticVM(StatisticalOperation.CacheDistributionByCacheDifficulty),
                new StatisticVM(StatisticalOperation.CacheDistributionByTerrainDifficulty)
            };

            CurrentStatistic = AvailableStatistics.First();
        }

        private Filter LoadDefaultFilter() {
            return new Filter {
                FromDate = statisticsManager.GetEarliestCacheCreationDate(),
                ToDate = statisticsManager.GetLatestCacheCreationDate(),
                FromPosition = statisticsManager.GetLowestCachePosition(),
                ToPosition = statisticsManager.GetHighestCachePosition()
            };
        }

        private void GetStatistics() {
            // prepare filter object depending on user-selected options
            PrepareFilter();

            switch (CurrentStatistic.Operation) {
                case StatisticalOperation.CachesFoundByUser: statisticsData = statisticsManager.GetFoundCachesPerUser(Filter.Current);
                    break;
                case StatisticalOperation.CachesHiddenByUser: statisticsData = statisticsManager.GetHiddenCachesPerUser(Filter.Current);
                    break;
                case StatisticalOperation.CacheDistributionBySize: statisticsData = statisticsManager.GetCacheDistributionBySize(Filter.Current);
                    break;
                case StatisticalOperation.CacheDistributionByCacheDifficulty: statisticsData = statisticsManager.GetCacheDistributionByCacheDifficulty(Filter.Current);
                    break;
                case StatisticalOperation.CacheDistributionByTerrainDifficulty: statisticsData = statisticsManager.GetCacheDistributionByTerrainDifficulty(Filter.Current);
                    break;
            }
            RaisePropertyChangedEvent(vm => vm.StatisticsData);
        }

        private void PrepareFilter() {
            if (!RegionFilterRequested) {
                Filter.FromLatitude = defaultFilter.FromPosition.Latitude;
                Filter.FromLongitude = defaultFilter.FromPosition.Longitude;

                Filter.ToLatitude = defaultFilter.ToPosition.Latitude;
                Filter.ToLongitude = defaultFilter.ToPosition.Longitude;
            }

            if (!DateFilterRequested) {
                Filter.FromDate = defaultFilter.FromDate;
                Filter.ToDate = defaultFilter.ToDate;
            }
        }
    }
}