using System.Windows.Controls;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.Desktop.ViewModel.Cache;

namespace Swk5.GeoCaching.Desktop.View.Cache {
    /// <summary>
    ///     Interaction logic for CacheEditor.xaml
    /// </summary>
    public partial class CacheEditor {
        private readonly ICacheManager cacheManager = GeoCachingBLFactory.GetCacheManager();

        public CacheEditor() {
            InitializeComponent();
            CacheCollectionVM cacheCollectionVm;
            
            Loaded += (s, e) => {
                cacheCollectionVm = new CacheCollectionVM(cacheManager);
                DataContext = cacheCollectionVm;
            };
        }

        private void OnSelectedCacheChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0) {
                (( ListBox ) sender).ScrollIntoView(e.AddedItems[0]);
            }
        }
    }
}