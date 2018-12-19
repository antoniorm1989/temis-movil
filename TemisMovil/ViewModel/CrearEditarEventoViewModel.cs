using System;
using System.ComponentModel;
using TemisMovil.Model;
using TemisMovil.ViewModel.Commands.CrearEditarEvento;

namespace TemisMovil.ViewModel
{
    public class CrearEditarEventoViewModel : INotifyPropertyChanged
    {
        public GuardarCommand GuardarCommand { get; set; }
        private Evento evento;
        public Evento Evento
        {
            get { return evento; }
            set
            {
                evento = value;
                Nombre = evento.Nombre;
                FechaInicio = evento.FechaInicio;
                FechaFin = evento.FechaFin;
                OnPropertyChanged("Evento");
            }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                Evento.Nombre = Nombre;
                /*Evento = new Evento()
                {
                    Nombre = Nombre,
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                    UserId = App.user.Id
                };*/
                OnPropertyChanged("Nombre");
            }
        }

        private DateTime fechaInicio;
        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set
            {
                fechaInicio = value;
                Evento.FechaInicio = FechaInicio;
                /*Evento = new Evento()
                {
                    Nombre = Nombre,
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                    UserId = App.user.Id
                };*/
                OnPropertyChanged("FechaInicio");
            }
        }

        private DateTime fechaFin;
        public DateTime FechaFin
        {
            get { return fechaFin; }
            set
            {
                fechaFin = value;
                Evento.FechaFin = FechaFin;
                /*Evento = new Evento()
                {
                    Nombre = Nombre,
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                    UserId = App.user.Id
                };*/
                OnPropertyChanged("FechaFin");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public CrearEditarEventoViewModel()
        {
            GuardarCommand = new GuardarCommand(this);
            Evento = new Evento();
        }
        public CrearEditarEventoViewModel(string eventoId)
        {
            GuardarCommand = new GuardarCommand(this);
            Evento = new Evento
            {
                Id = eventoId
            };
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void EstablcerEventoEditar()
        {
            if (!string.IsNullOrEmpty(evento.Id))
            {
                Evento = await Evento.GetByIdAsync(Evento.Id);
            }
        }

        public async void GuardarEvento(Evento evento)
        {
            try
            {
                Evento.InsertOrUpdate(evento);
                await App.Current.MainPage.DisplayAlert("Success", "El evento " + evento.Nombre + " ha sido creado.", "Aceptar");
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "El evento no pudo ser creado", "Aceptar");
            }
        }
    }
}
