using System;
using System.Windows.Input;
using TemisMovil.Model;

namespace TemisMovil.ViewModel.Commands.CrearEditarEvento
{
    public class GuardarCommand : ICommand
    {
        public CrearEditarEventoViewModel crearEditarEventoViewModel;
        public event EventHandler CanExecuteChanged;

        public GuardarCommand(CrearEditarEventoViewModel crearEditarEventoViewModel)
        {
            this.crearEditarEventoViewModel = crearEditarEventoViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var evento = (Evento)parameter;

            if (evento != null)
                if (!string.IsNullOrEmpty(evento.Nombre))
                    return true;

            return false;
        }

        public void Execute(object parameter)
        {
            var evento = (Evento)parameter;
            crearEditarEventoViewModel.GuardarEvento(evento);
        }
    }
}
