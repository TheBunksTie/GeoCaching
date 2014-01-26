using System.Windows;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.AuthenticationManager;
using Swk5.GeoCaching.Desktop.View.Main;

namespace Swk5.GeoCaching.Desktop.View.Authentication {
    /// <summary>
    ///     Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow {
        private readonly IAuthenticationManager authenticationManager;

        public AuthenticationWindow() {
            InitializeComponent();
            try {
                authenticationManager = GeoCachingBLFactory.GetAuthenticationManager();
            }
            catch {
                // any exception at this point means configuration for dal-assemblies do no work correctly --> end application with error message
                MessageBox.Show("Fatal Error: Could not load required program components.", "Fatal eror", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }


        private void LoginButton_OnClick(object sender, RoutedEventArgs e) {
            try {
                if (authenticationManager.AuthenticateUser(UsernameBox.Text, PasswordBox.Password, LoginMode.PrivilegeRequired) != null) {

                    new GeocachingWindow().Show();
                    Close();
                }
                else {
                    MessageBox.Show("Error: Provided user credentials are invalid.", "Invalid user credentials", MessageBoxButton.OK, MessageBoxImage.Error);

                    // reset password input
                    PasswordBox.Password = "";
                }
            }
            catch {
                // any exception at this point means there is no connection to database --> end application with error message
                MessageBox.Show("Fatal Error: Could not connect to database.", "Fatal eror", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }     
        
        private void QuitButton_OnClick(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}