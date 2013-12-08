using System.Windows;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;

namespace Swk5.GeoCaching.Desktop.Views.Cache {
    /// <summary>
    ///     Interaction logic for CacheDetailsWindow.xaml
    /// </summary>
    public partial class CacheDetailsWindow : Window {
        public CacheDetailsWindow() {
            InitializeComponent();
        }

        private void OnPinClicked ( object sender, MouseEventArgs mouseEventArgs ) {
            mouseEventArgs.Handled = true;
            MessageBox.Show(( ( Pushpin ) sender ).Tag.ToString(), "Information");
        }

        private void OnCacheMapClicked ( object sender, MouseButtonEventArgs e ) {
            e.Handled = true;
            // just do nothing
        }
    }
}