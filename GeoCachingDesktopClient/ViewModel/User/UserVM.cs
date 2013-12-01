using System;
using Swk5.GeoCaching.BusinessLogic;
using Swk5.GeoCaching.DomainModel;


namespace Swk5.GeoCaching.Desktop.ViewModel.User {
    public class UserVM : ViewModelBase<UserVM> {
        private const double TOLERANCE = 0.000001;

        private readonly DomainModel.User user;
        private string passwordRepition;


        private RelayCommand createCommand;
        private RelayCommand deleteCommand;
        private RelayCommand updateCommand;
        private IUserManager userManager;

        public UserVM(IUserManager userManager, DomainModel.User user) {
            this.userManager = userManager;
            this.user = user;
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

        public UserRole Role {
            get { return user.Role; }
            set {
                if (user.Role != value) {
                    user.Role = value;

                    RaisePropertyChangedEvent(vm => vm.Role);
                }
            }
        }

        public double Latitude {
            get {
                return user.Position.Latitude;
            }
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
            get { return user.Password; }
            set {
                if (user.Password != value) {
                    user.Password = value;

                    RaisePropertyChangedEvent(vm => vm.Password);
                }
            }
        }

        public string RegistrationDate {
            get { return user.RegistrationDate.ToShortDateString(); }
            set {
                DateTime registrationDate = DateTime.Parse(value);
            }

        }
    }
}