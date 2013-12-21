using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Swk5.GeoCaching.BusinessLogic.UserManager;

namespace Swk5.GeoCaching.Desktop.ViewModel.User {
    // represent a list of all users
    public class UserCollectionVM : AbstractViewModelBase<UserCollectionVM> {
        private readonly IUserManager userManager;

        private ICommand createCommand;
        private ICommand deleteCommand;

        private UserVM currentUser;

        public UserCollectionVM(IUserManager userManager) {
            this.userManager = userManager;
            Users = new ObservableCollection<UserVM>();
            LoadUsers();

            RoleList = new List<string>();
            LoadUserRoleList();
        }

        public ObservableCollection<UserVM> Users { get; private set; }

        public UserVM CurrentUser {
            get { return currentUser; }
            set {
                // only change current if a another user is selected
                if (currentUser != value) {
                    currentUser = value;
                    RaisePropertyChangedEvent(vm => vm.CurrentUser);
                }
            }
        }

        public List<string> RoleList { get; private set; }

        public ICommand CreateCommand {
            get { return createCommand ?? (createCommand = new RelayCommand(param => NewUser())); }
        }

        public ICommand DeleteCommand {
            get { return deleteCommand ?? (deleteCommand = new RelayCommand(param => DeleteUser())); }
        }

        private void NewUser() {
            try {
                // create new dummy user and set him as current one
                CurrentUser = new UserVM(userManager, userManager.CreateNewDefaultUser());
                Users.Add(CurrentUser);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "User manager error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteUser() {
            userManager.DeleteUser(CurrentUser.Id);
            Users.Remove(CurrentUser);
            CurrentUser = null;
        }

        private async void LoadUsers() {
            Users.Clear();

            IEnumerator<DomainModel.User> e = userManager.GetUserList().GetEnumerator();
            while (await Task.Factory.StartNew(() => e.MoveNext())) {
                Users.Add(new UserVM(userManager, e.Current));
            }
        }

        private async void LoadUserRoleList() {
            RoleList.Clear();

            IEnumerator<string> e = userManager.GetUserRoleList().GetEnumerator();
            while (await Task.Factory.StartNew(() => e.MoveNext())) {
                RoleList.Add(e.Current);
            }
        }
    }
}