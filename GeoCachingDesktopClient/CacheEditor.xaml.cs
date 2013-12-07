using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.Desktop.ViewModel.Cache;

namespace Swk5.GeoCaching.Desktop {
    /// <summary>
    ///     Interaction logic for CacheEditor.xaml
    /// </summary>
    public partial class CacheEditor : UserControl {

        private readonly ICacheManager cacheManager = GeoCachingBLFactory.GetCacheManager();
        private readonly CacheCollectionVM cacheCollectionVm; 

        public CacheEditor() {
            InitializeComponent();
            cacheCollectionVm = new CacheCollectionVM(cacheManager);
            Loaded += ( s, e ) => { DataContext = cacheCollectionVm; };
        }

        private void OnSelectedCacheChanged(object sender, SelectionChangedEventArgs e) {
            // TODO
        }

        private void OnPinClicked(object sender, MouseEventArgs mouseEventArgs) {
            MessageBox.Show((( Pushpin ) sender).Location.ToString(), "Information");            
        }
    }
}