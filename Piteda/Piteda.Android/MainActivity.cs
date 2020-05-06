using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Android.Graphics;
using System.IO;
using Android.Content;

namespace Piteda.Droid
{
    [Activity(Label = "Piteda", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static PitedaBarcodes.ReadBarcodes api = new PitedaBarcodes.ReadBarcodes();
        static PitedaBarcodes.LOG log = new PitedaBarcodes.LOG();

        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            RequestPermissions(permissionGroup, 0);
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap androidBitmap = (Bitmap)data.Extras.Get("data");
            MemoryStream stream = new MemoryStream();
            androidBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(stream);

            // @TODO: Find a way to pass the current fragment to MainActivity.xaml.cs and call StartActivityForResult(intent, 0); (In order to call this function)

            try
            {
                api.ReadSingleBarcode(bitmap);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, log.PACK_SIZE_ERROR, ToastLength.Long);
            }
        }
    }
}