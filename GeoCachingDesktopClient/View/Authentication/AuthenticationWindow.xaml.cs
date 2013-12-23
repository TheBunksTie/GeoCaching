using System.Windows;
using Swk5.GeoCaching.BusinessLogic.AuthenticationManager;
using Swk5.GeoCaching.Desktop.View.Main;

namespace Swk5.GeoCaching.Desktop.View.Authentication {
    /// <summary>
    ///     Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow {
        private readonly IAuthenticationManager authenticationManager = new AuthenticationManager();

        public AuthenticationWindow() {
            InitializeComponent();
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e) {

            if (authenticationManager.AuthenticateUser(UsernameBox.Text, PasswordBox.Password, true) != null) {
                new GeocachingWindow(authenticationManager).Show();
                Close();
            }
            else {
                MessageBox.Show("Error: Provided user credentials are invalid.", "Invalid user credentials", MessageBoxButton.OK , MessageBoxImage.Error);
                
                // reset password input
                PasswordBox.Password = "";
            }                     
        }

        private void QuitButton_OnClick(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}