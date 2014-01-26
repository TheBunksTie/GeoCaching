using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.Desktop.ViewModel.Cache;

namespace Swk5.GeoCaching.Desktop.View.Cache {
    /// <summary>
    ///     Interaction logic for CacheMapEditor.xaml
    /// </summary>
    public partial class CacheMapEditor {
        private readonly ICacheManager cacheManager = GeoCachingBLFactory.GetCacheManager();
        private CacheCollectionVM cacheCollectionVm;        
        private readonly DomainModel.User authenticatedUser;


        public CacheMapEditor() {
            InitializeComponent();

            Loaded += (s, e) => {
                cacheCollectionVm = new CacheCollectionVM(cacheManager);
                DataContext = cacheCollectionVm;
            };
            authenticatedUser = GeoCachingBLFactory.GetAuthenticationManager().AuthenticatedUser;

            CacheMap.Loaded += (s, e) => { CacheMap.Center = new Location(authenticatedUser.Position.Latitude, authenticatedUser.Position.Longitude); };
        }

        private void OnPushPinDoubleClick(object sender, MouseButtonEventArgs e) {
            try {
                // get cache id information from property tag of clicked pin
                int clickedCacheId = Int32.Parse((sender as Pushpin).Tag.ToString());
                cacheCollectionVm.SetCurrentCacheById(clickedCacheId);
            }
            catch {
                MessageBox.Show("Error: Can't find cache details for clicked pin.",
                    "Pin Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void OnCacheMapClicked(object sender, MouseButtonEventArgs e) {
            e.Handled = true;
            // just do nothing
        }

        private void OnCacheMapRightClicked(object sender, MouseButtonEventArgs e) {
            // transform clicked coordinated to geo location
            Location clickedLocation =
                (( Map ) sender).ViewportPointToLocation(new Point(e.GetPosition(CacheMap).X, e.GetPosition(CacheMap).Y));
                       
            cacheCollectionVm.CurrentCache = new CacheVM(cacheManager, cacheManager.CreateNewPositionedCache(authenticatedUser.Id, clickedLocation.Latitude, clickedLocation.Longitude));
            cacheCollectionVm.Caches.Add(cacheCollectionVm.CurrentCache);
        }
    }
}