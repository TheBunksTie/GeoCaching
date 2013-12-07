using System.Windows;
using System.Windows.Controls;

namespace Swk5.GeoCaching.Desktop {
    /// <summary>
    /// Interaction logic for UserEditor.xaml
    /// </summary>
    public partial class UserEditor : UserControl {
        public UserEditor ( ) {
            InitializeComponent();
        }

        // if a new dummy user was created and selected let him appear in the view
        private void OnSelectedUserChanged(object sender, SelectionChangedEventArgs e) {

            if (e.AddedItems.Count > 0) {
                (( ListBox ) sender).ScrollIntoView(e.AddedItems[0]);
            }
        }
    }
}
