using System.Windows;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.BusinessLogic.UserManager;
using Swk5.GeoCaching.Desktop.ViewModel.User;

namespace Swk5.GeoCaching.Desktop {
    /// <summary>
    /// Interaction logic for GeocachingWindow.xaml
    /// </summary>
    public partial class GeocachingWindow : Window {

        private IUserManager userManager = GeoCachingBLFactory.GetUserManager();

        public GeocachingWindow ( ) {
            InitializeComponent();

            this.Loaded += ( s, e ) => {
                this.DataContext = new UserCollectionVM(userManager);
            };

        }
    }
}
