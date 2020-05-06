using Android.Content;
using Android.Provider;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using PitedaBarcodes;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace Piteda
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        PitedaBarcodes.ReadBarcodes api = new PitedaBarcodes.ReadBarcodes();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSwitchCameraClicked(object sender, EventArgs e)
        {
            if(CameraPrevRear.IsVisible == true)
            {
                CameraPrevRear.IsVisible = false;
                CameraPrevFront.IsVisible = true;
            }
            else
            {
                statusLabel.Text = "Here!";
                CameraPrevRear.IsVisible = true;
                CameraPrevFront.IsVisible = false;
            }
        }

        private async void OnCaptureClicked(object sender, EventArgs e)
        {
            // Intent intent = new Intent(MediaStore.ActionImageCapture); # call intent start event in MainActivity
            await CrossMedia.Current.Initialize();

            if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "Camera isn't available in your device", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    Directory = "Captures",
                    Name = "Scanned_" + DateTime.Now + ".jpg",
                    SaveToAlbum = true
                });

            if (file == null) return;

            /*
            myCapture.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            */
        }
    }
}
