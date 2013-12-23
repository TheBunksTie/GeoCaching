using System.Windows;
using System.Windows.Controls;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.UserManager;
using Swk5.GeoCaching.Desktop.ViewModel.User;

namespace Swk5.GeoCaching.Desktop.View.User {
    /// <summary>
    ///     Interaction logic for UserEditor.xaml
    /// </summary>
    public partial class UserEditor {
        private readonly IUserManager userManager = GeoCachingBLFactory.GetUserManager();
        private UserCollectionVM userCollectionVm;
        
        public UserEditor() {
            InitializeComponent();

            Loaded += (s, e) => {
                userCollectionVm = new UserCollectionVM(userManager);
                DataContext = userCollectionVm;
            };
        }

        // if a new dummy user was created and selected let him appear in the view
        private void OnSelectedUserChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0) {
                (( ListBox ) sender).ScrollIntoView(e.AddedItems[0]);
            }
        }

        // manual handling necessary, because no databinding for password box allowed
        private void OnPasswordLostFocus(object sender, RoutedEventArgs e) {
            userCollectionVm.CurrentUser.Password = (( PasswordBox ) sender).Password;
        }

        private void OnPasswordRepitionLostFocus(object sender, RoutedEventArgs e) {
            userCollectionVm.CurrentUser.PasswordRepetition = ( ( PasswordBox ) sender ).Password;
        }
    }
}