using System;
using System.Windows.Input;

namespace TemisMovil.ViewModel.Commands
{
    public class RegistrarNavigationCommand : ICommand
    {
        private LoginViewModel loginViewModel;
        public event EventHandler CanExecuteChanged;

        public RegistrarNavigationCommand(LoginViewModel loginViewModel)
        {
            this.loginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            loginViewModel.RegistrarUsuarioNavigate();
        }
    }
}
