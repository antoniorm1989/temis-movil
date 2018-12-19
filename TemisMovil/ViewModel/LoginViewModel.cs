using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TemisMovil.Model;
using TemisMovil.ViewModel.Commands;

namespace TemisMovil.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                User = new User()
                {
                    Email = Email,
                    Password = Password
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new User()
                {
                    Email = Email,
                    Password = Password
                };
                OnPropertyChanged("Password");
            }
        }

        public LoginCommand LoginCommand { get; set; }
        public RegistrarNavigationCommand RegistrarNavigationCommand {get; set;}
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public LoginViewModel()
        {
            User = new User();
            LoginCommand = new LoginCommand(this);
            RegistrarNavigationCommand = new RegistrarNavigationCommand(this);
        }
        public async void LoginAsync()
        {
            var canLogin = await User.LoginAsync(User.Email, User.Password);
            if (canLogin)
                await App.Current.MainPage.Navigation.PushAsync(new InicioPage());
            else
                await App.Current.MainPage.DisplayAlert("Error", "Try again", "Ok");
        }
        public async void RegistrarUsuarioNavigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegistroPage());
        }
    }
}
