using System;
using System.Windows.Input;
using TemisMovil.Model;

namespace TemisMovil.ViewModel.Commands
{
    public class BorrarEventoCommand : ICommand
    {
        public InicioViewModel InicioViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public BorrarEventoCommand(InicioViewModel inicioViewModel)
        {
            InicioViewModel = inicioViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var evento = (Evento)parameter;
            InicioViewModel.BorrarEvento(evento);
        }
    }
}
