using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using TemisMovil.Model;
using TemisMovil.ViewModel.Commands;
using Xamarin.Essentials;

namespace TemisMovil.ViewModel
{
    public class RegistroViewModel : INotifyPropertyChanged
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
                    Password = Password,
                    ConfirmPassword = confirmPassword
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
                    Password = Password,
                    ConfirmPassword = confirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User()
                {
                    Email = Email,
                    Password = Password,
                    ConfirmPassword = confirmPassword
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public RegistrarCommand RegistrarCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public RegistroViewModel()
        {
            RegistrarCommand = new RegistrarCommand(this);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public async void RegistrarUsuario(User user)
        {
            User.RegistrarUsuarioAsync(user);
            await SendEmail("Prueba", "<h1 style='color:red'>Hola mundo<h1>", new List<string>() { "arosales@bts.com.mx" });
        }

        private async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };

                
                await Xamarin.Essentials.Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }
    }
}
