using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Swk5.GeoCaching.BusinessLogic.StatisticsManager;
using Swk5.GeoCaching.Desktop.ViewModel.Filter;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Statistic {
    public class StatisticsCollectionVM : AbstractViewModelBase<StatisticsCollectionVM> {
        private readonly DataFilter defaultFilter;
        private readonly FilterVM filterVM;
        private readonly IStatisticsManager statisticsManager;
        private StatisticDataset currentStatisticData;

        private bool dateFilterRequested;

        private ICommand getStatisticsCommand;

        private bool regionFilterRequested;
        private StatisticVM requestedStatistic;

        public StatisticsCollectionVM(IStatisticsManager statisticsManager) {
            this.statisticsManager = statisticsManager;
            defaultFilter = statisticsManager.GetDefaultFilter();

            // initialise filter with default values
            filterVM = new FilterVM(defaultFilter);
            LoadAvailableStatistics();
        }

        public ObservableCollection<StatisticVM> AvailableStatistics { get; private set; }

        public StatisticVM RequestedStatistic {
            get { return requestedStatistic; }
            set {
                if (requestedStatistic != value) {
                    requestedStatistic = value;
                    RaisePropertyChangedEvent(vm => vm.RequestedStatistic);
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

        public StatisticDataset CurrentStatisticData {
            get { return currentStatisticData; }
            private set {
                if (currentStatisticData != value) {
                    currentStatisticData = value;
                    RaisePropertyChangedEvent(vm => vm.CurrentStatisticData);
                }
            }
        }

        public ICommand GetStatisticsCommand {
            get { return getStatisticsCommand ?? (getStatisticsCommand = new RelayCommand(param => GetStatistics())); }
        }

        public FilterVM Filter {
            get { return filterVM; }
        }

        private void LoadAvailableStatistics() {
            AvailableStatistics = new ObservableCollection<StatisticVM> {
                new StatisticVM(StatisticalOperation.UsersByFoundCaches),
                new StatisticVM(StatisticalOperation.UsersByHiddenCaches),
                new StatisticVM(StatisticalOperation.CachesByRating),
                new StatisticVM(StatisticalOperation.CachesByLogEntrNr),
                new StatisticVM(StatisticalOperation.CacheDistributionBySize),
                new StatisticVM(StatisticalOperation.CacheDistributionByCacheDifficulty),
                new StatisticVM(StatisticalOperation.CacheDistributionByTerrainDifficulty)                
            };

            RequestedStatistic = AvailableStatistics.First();
        }

        private void GetStatistics() {
            // prepare filter object depending on user-selected options
            PrepareFilter();

            switch (RequestedStatistic.Operation) {
                case StatisticalOperation.UsersByFoundCaches:
                    CurrentStatisticData = statisticsManager.GetUsersByFoundCaches(Filter.Current);
                    break;
                case StatisticalOperation.UsersByHiddenCaches:
                    CurrentStatisticData = statisticsManager.GetUsersByHiddenCaches(Filter.Current);
                    break;
                case StatisticalOperation.CachesByRating:
                    CurrentStatisticData = statisticsManager.GetCachesByRating(Filter.Current);
                    break;
                case StatisticalOperation.CachesByLogEntrNr:
                    CurrentStatisticData = statisticsManager.GetCachesByLogCount(Filter.Current);
                    break;
                case StatisticalOperation.CacheDistributionBySize:
                    CurrentStatisticData = statisticsManager.GetCacheDistributionBySize(Filter.Current);
                    break;
                case StatisticalOperation.CacheDistributionByCacheDifficulty:
                    CurrentStatisticData = statisticsManager.GetCacheDistributionByCacheDifficulty(Filter.Current);
                    break;
                case StatisticalOperation.CacheDistributionByTerrainDifficulty:
                    CurrentStatisticData = statisticsManager.GetCacheDistributionByTerrainDifficulty(Filter.Current);
                    break;
            }
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