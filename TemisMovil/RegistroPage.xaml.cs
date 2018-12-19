using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemisMovil.Model;
using TemisMovil.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TemisMovil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    {
        private RegistroViewModel registroViewModel;
        public RegistroPage()
        {
            InitializeComponent();
            registroViewModel = new RegistroViewModel();
            BindingContext = registroViewModel;
        }
    }
}