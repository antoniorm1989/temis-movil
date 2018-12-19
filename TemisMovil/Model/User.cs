using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TemisMovil.Model
{
    public class User : INotifyPropertyChanged
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

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static async void RegistrarUsuarioAsync(User user)
        {
            await App.MobileService.GetTable<User>().InsertAsync(user);
        }
        public static async Task<bool> LoginAsync(string email, string password)
        {
            var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();
            if (user != null)
            {
                if (user.Password == password)
                {
                    App.user = user;
                    return true;
                }
            }

            return false;
        }
    }
}
