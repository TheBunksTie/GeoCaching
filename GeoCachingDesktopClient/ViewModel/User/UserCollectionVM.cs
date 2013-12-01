using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swk5.GeoCaching.BusinessLogic;
using  Swk5.GeoCaching.DomainModel;


namespace Swk5.GeoCaching.Desktop.ViewModel.User {
    
    // represent a list of all users
    public class UserCollectionVM : ViewModelBase<UserCollectionVM> {
        private IUserManager userManager;
        private UserVM currentUser;

        public UserCollectionVM(IUserManager userManager) {
            this.userManager = userManager;
            Users = new ObservableCollection<UserVM>();
            LoadUsers();
        }

        public ObservableCollection<UserVM> Users { get; set; }

        public UserVM CurrentUser {
            get {
                return currentUser;
            }
            set {
                // only change current if a another user is selected
                if (currentUser != value) {
                    currentUser = value;
                    RaisePropertyChangedEvent(vm => vm.CurrentUser);
                }
            }
        }

        private async void LoadUsers() {
            Users.Clear();

            IEnumerator<DomainModel.User> e = userManager.GetUsers().GetEnumerator();
            while (await Task.Factory.StartNew(() => e.MoveNext())) {
                Users.Add(new UserVM(userManager, e.Current));    
            }
        }
    }
}