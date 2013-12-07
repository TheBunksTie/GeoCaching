using System.Windows.Controls;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.Desktop.ViewModel.Cache;

namespace Swk5.GeoCaching.Desktop {
    /// <summary>
    ///     Interaction logic for CacheEditor.xaml
    /// </summary>
    public partial class CacheEditor : UserControl {

        private readonly ICacheManager cacheManager = GeoCachingBLFactory.GetCacheManager();

        public CacheEditor() {
            InitializeComponent();

            Loaded += ( s, e ) => { DataContext = new CacheCollectionVM(cacheManager); };
        }

        private void OnSelectedCacheChanged(object sender, SelectionChangedEventArgs e) {
            // TODO
        }
    }
}