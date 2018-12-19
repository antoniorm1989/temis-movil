using System;
using System.Linq;
using TemisMovil.Model;
using TemisMovil.ViewModel;
using Xamarin.Forms;

namespace TemisMovil
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel loginViewModel;
        public LoginPage()
        {
            InitializeComponent();

            loginViewModel = new LoginViewModel();
            BindingContext = loginViewModel;

            var assembly = typeof(LoginPage);
            logoImage.Source = ImageSource.FromResource("TemisMovil.Assets.Images.logo.png", assembly);
        }
    }
}
