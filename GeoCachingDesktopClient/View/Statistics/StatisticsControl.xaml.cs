using System.Windows.Controls;
using Swk5.GeoCaching.BusinessLogic.StatisticsManager;
using Swk5.GeoCaching.Desktop.ViewModel.Statistic;

namespace Swk5.GeoCaching.Desktop.View.Statistics {
    /// <summary>
    /// Interaction logic for StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl : UserControl {
        
        private readonly IStatisticsManager statisticsManager = new StatisticsManager();
        
        public StatisticsControl ( ) {
            InitializeComponent();

            Loaded += (s, e) => {
                DataContext = new StatisticsCollectionVM(statisticsManager);
            };

        }
    }
}
