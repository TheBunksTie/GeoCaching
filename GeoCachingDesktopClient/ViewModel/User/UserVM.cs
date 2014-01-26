using System;
using System.Windows;
using System.Windows.Input;
using Swk5.GeoCaching.BusinessLogic.UserManager;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.User {
    public class UserVM : AbstractViewModelBase<UserVM> {
        private const double TOLERANCE = 0.000001;

        private readonly DomainModel.User user;
        private string passwordRepitition;

        private readonly IUserManager userManager;
        private ICommand updateCommand;
        private bool passwordChanged = false;

        public UserVM(IUserManager userManager, DomainModel.User user) {
            this.userManager = userManager;
            this.user = user;
            passwordRepitition = user.PasswordHash;
        }

        // TODO bug when modifying name of existing user to another existing name
        // check for name duplicates also when just updating

        public int Id {
            get { return user.Id; }
        }

        public string Name {
            get { return user.Name; }
            set {
                if (user.Name != value) {
                    user.Name = value;
                    RaisePropertyChangedEvent(vm => vm.Name);
                }                
            }
        }

        public string Email {
            get { return user.Email; }
            set {
                if (user.Email != value) {
                    user.Email = value;
                    RaisePropertyChangedEvent(vm => vm.Email);
                }
            }
        }

        public string Role {
            get { return user.Role; }
            set {
                if (user.Role != value) {
                    user.Role = value;

                    RaisePropertyChangedEvent(vm => vm.Role);
                }
            }
        }

        public double Latitude {
            get { return user.Position.Latitude; }
            set {
                if (Math.Abs(user.Position.Latitude - value) > TOLERANCE) {
                    GeoPosition currentPosition = user.Position;
                    currentPosition.Latitude = value;
                    user.Position = currentPosition;
                    RaisePropertyChangedEvent(vm => vm.Latitude);
                }
            }
        }

        public double Longitude {
            get { return user.Position.Longitude; }
            set {
                if (Math.Abs(user.Position.Longitude - value) > TOLERANCE) {
                    GeoPosition currentPosition = user.Position;
                    currentPosition.Longitude = value;
                    user.Position = currentPosition;
                    RaisePropertyChangedEvent(vm => vm.Longitude);
                }
            }
        }

        public string Password {
            get { return user.PasswordHash; }
            set {
                if (user.PasswordHash != value ) {
                    user.PasswordHash = value;
                    passwordChanged = true;
                    RaisePropertyChangedEvent(vm => vm.Password);
                }
            }
        }

        public string PasswordRepetition {
            get { return passwordRepitition; }
            set {
                if (passwordRepitition != value ) {
                    passwordRepitition = value;
                    passwordChanged = true;
                    RaisePropertyChangedEvent(vm => vm.PasswordRepetition);
                }
            }
        }

        public ICommand UpdateCommand {
            get { return updateCommand ?? (updateCommand = new RelayCommand(param => UpdateUser())); }
        }

        private void UpdateUser() {

            if (!passwordChanged || Password.Equals(PasswordRepetition)) {
                try {
                    userManager.UpdateExistingUser(user, passwordChanged);
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message, "User manager error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else {
                MessageBox.Show("Error: Provided passwords do not match", "User manager error", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
        }
    }
}