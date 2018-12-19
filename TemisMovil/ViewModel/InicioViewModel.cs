using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using TemisMovil.helpers;
using TemisMovil.Model;
using TemisMovil.ViewModel.Commands;
using Xamarin.Forms;

namespace TemisMovil.ViewModel
{
    public class InicioViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Evento> Eventos { get; set; }
        private Evento evento;
        public Evento Evento
        {
            get { return evento; }
            set
            {
                evento = value;
                OnPropertyChanged("Evento");
            }
        }
        public NavigationCommand NavigationCommand { get; set; }
        public BorrarEventoCommand BorrarEventoCommand { get; set; }
        public RefreshCommand RefreshCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public InicioViewModel()
        {
            Evento = new Evento();
            Eventos = new ObservableCollection<Evento>();
            NavigationCommand = new NavigationCommand(this);
            BorrarEventoCommand = new BorrarEventoCommand(this);
            RefreshCommand = new RefreshCommand(this);
            CrearEvento();
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task<bool> UpdateEventos()
        {
            try
            {
                var eventos = await Evento.ReadAsync();
                if (eventos != null)
                {
                    Eventos.Clear();
                    foreach (var evento in eventos)
                        Eventos.Add(evento);
                }

                await BackendNotification.SendNotificationAsync("hola", null);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async void CrearEditarEventoNavigate()
        {
            if (!string.IsNullOrEmpty(evento.Id))
                await App.Current.MainPage.Navigation.PushAsync(new CrearEditarEventoPage(Evento.Id));
            else
                await App.Current.MainPage.Navigation.PushAsync(new CrearEditarEventoPage());
        }
        public async void BorrarEvento(Evento evento)
        {
            await Evento.DeletePostAsync(evento);
            await UpdateEventos();
        }
        public async void RefreshEventos()
        {
            await UpdateEventos();
            // es nececiario poner a la lista isRefreshing = false;
        }

        public async void CrearEvento()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Calendar);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Calendar))
                    await App.Current.MainPage.DisplayAlert("Need permission", "We will have to acces your calendar", "Ok");

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Calendar);
                if (results.ContainsKey(Permission.Calendar))
                    status = results[Permission.Calendar];
            }

            if (status == PermissionStatus.Granted)
            {
                IPlatform service = DependencyService.Get<IPlatform>();
                service.SaveEvent(new DateTime(), new DateTime(), "Planning todo el dia", "elemento trabajo infos", "Sala movil", false);
            }
                
        }
    }
}
