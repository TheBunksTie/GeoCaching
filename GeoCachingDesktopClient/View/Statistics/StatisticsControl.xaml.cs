using System.Windows.Controls;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.StatisticsManager;
using Swk5.GeoCaching.Desktop.ViewModel.Statistic;

namespace Swk5.GeoCaching.Desktop.View.Statistics {
    /// <summary>
    /// Interaction logic for StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl {

        private readonly IStatisticsManager statisticsManager = GeoCachingBLFactory.GetStatisticsManager();
        
        public StatisticsControl ( ) {
            InitializeComponent();

            Loaded += (s, e) => {
                DataContext = new StatisticsCollectionVM(statisticsManager);
            };

        }
    }
}
