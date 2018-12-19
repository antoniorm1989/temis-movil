using System;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using TemisMovil.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TemisMovil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearEditarEventoPage : ContentPage
    {
        public CrearEditarEventoViewModel crearEditarEventoViewModel;
        public CrearEditarEventoPage()
        {
            InitializeComponent();
            crearEditarEventoViewModel = new CrearEditarEventoViewModel();
            BindingContext = crearEditarEventoViewModel;
        }
        public CrearEditarEventoPage(string eventoId)
        {
            InitializeComponent();
            crearEditarEventoViewModel = new CrearEditarEventoViewModel(eventoId);
            BindingContext = crearEditarEventoViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            crearEditarEventoViewModel.EstablcerEventoEditar();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is no supported on your device", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

           if(selectedImageFile == null)
            {
                await DisplayAlert("Error", "there was an aerro when trying to get your image", "Ok");
                return;
            }

            selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());

            UploadImage(selectedImageFile.GetStream());
        }

        private async void UploadImage(Stream stream)
        {
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=imagestorageantonio;AccountKey=Yvo6OveoZWzrPpbYFMdLmLVsbFbmypSDAGT12dCn4yIDTku3PLLhy6sK2ViVccXTK3mVQZH1iVLL0VlEJBmYug==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("imagecontainer");
            await container.CreateIfNotExistsAsync();

            var name = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{name}.jpg");
            await blockBlob.UploadFromStreamAsync(stream);

            string url = blockBlob.Uri.OriginalString;
        }
    }
}