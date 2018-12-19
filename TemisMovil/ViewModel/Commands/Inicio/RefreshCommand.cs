using System;
using System.Windows.Input;
using TemisMovil.Model;

namespace TemisMovil.ViewModel.Commands
{
    public class RefreshCommand : ICommand
    {
        public InicioViewModel InicioViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public RefreshCommand(InicioViewModel inicioViewModel)
        {
            InicioViewModel = inicioViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await InicioViewModel.UpdateEventos();
        }
    }
}
