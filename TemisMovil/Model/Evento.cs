using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;

namespace TemisMovil.Model
{
    public class Evento : INotifyPropertyChanged
    {
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
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
                OnPropertyChanged("FechaInicio");
            }
        }


        private DateTime fechaFin;
        public DateTime FechaFin
        {
            get { return fechaInicio; }
            set
            {
                fechaFin = value;
                OnPropertyChanged("FechaFin");
            }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static async Task<List<Evento>> ReadAsync()
        {
            var eventos = await App.MobileService.GetTable<Evento>().ToListAsync();
            return eventos;
        }
        public static async Task<Evento> GetByIdAsync(string eventoId)
        {
            var evento = (await App.MobileService.GetTable<Evento>().Where(e => e.Id == eventoId).ToListAsync()).FirstOrDefault();
            return evento;
        }
        public static async void InsertOrUpdate(Evento evento)
        {
            if (string.IsNullOrEmpty(evento.Id))
                await App.MobileService.GetTable<Evento>().InsertAsync(evento);
            else
                await App.MobileService.GetTable<Evento>().UpdateAsync(evento);
        }
        public static async Task<bool> DeletePostAsync(Evento evento)
        {
            try
            {
                await App.MobileService.GetTable<Evento>().DeleteAsync(evento);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
