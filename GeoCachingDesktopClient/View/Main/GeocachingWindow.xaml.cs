using Swk5.GeoCaching.BusinessLogic.AuthenticationManager;

namespace Swk5.GeoCaching.Desktop.View.Main {
    /// <summary>
    ///     Interaction logic for GeocachingWindow.xaml
    /// </summary>
    public partial class GeocachingWindow {
        private IAuthenticationManager authenticationManager;

        public GeocachingWindow(IAuthenticationManager authenticationManager) {
            InitializeComponent();
            this.authenticationManager = authenticationManager;
        }

        public GeocachingWindow() {
            InitializeComponent();
        }
    }
}