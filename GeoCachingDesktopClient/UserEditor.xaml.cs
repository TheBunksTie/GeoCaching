using System.Windows.Controls;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.UserManager;
using Swk5.GeoCaching.Desktop.ViewModel.User;

namespace Swk5.GeoCaching.Desktop {
    /// <summary>
    ///     Interaction logic for UserEditor.xaml
    /// </summary>
    public partial class UserEditor : UserControl {
        private readonly IUserManager userManager = GeoCachingBLFactory.GetUserManager();

        public UserEditor() {
            InitializeComponent();

            Loaded += (s, e) => { DataContext = new UserCollectionVM(userManager); };
        }

        // if a new dummy user was created and selected let him appear in the view
        private void OnSelectedUserChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0) {
                (( ListBox ) sender).ScrollIntoView(e.AddedItems[0]);
            }
        }
    }
}