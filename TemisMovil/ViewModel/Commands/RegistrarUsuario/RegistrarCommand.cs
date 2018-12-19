using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TemisMovil.Model;

namespace TemisMovil.ViewModel.Commands
{
    public class RegistrarCommand : ICommand
    {
        public RegistroViewModel RegistroViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public RegistrarCommand(RegistroViewModel registroViewModel)
        {
            RegistroViewModel = registroViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var user = (User)parameter;

            if (user != null)
                if (user.Password == user.ConfirmPassword)
                    if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Password))
                        return true;

            return false;
        }

        public void Execute(object parameter)
        {
            User user = (User)parameter;
            RegistroViewModel.RegistrarUsuario(user);
        }
    }
}
