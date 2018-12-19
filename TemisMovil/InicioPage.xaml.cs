using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TemisMovil.Model;
using TemisMovil.ViewModel;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace TemisMovil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioPage : ContentPage
    {
        public InicioViewModel inicioViewModel;
        public InicioPage()
        {
            InitializeComponent();
            inicioViewModel = new InicioViewModel();
            BindingContext = inicioViewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                /*var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                        await DisplayAlert("Need permission", "We will have to acces your camera", "Ok");

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }*/

                //if (status == PermissionStatus.Granted)
                await inicioViewModel.UpdateEventos();
                /*else
                    await DisplayAlert("Need permission", "We will have to acces your camera", "Ok");*/
            }
            catch (Exception)
            {

            }
        }
    }
}