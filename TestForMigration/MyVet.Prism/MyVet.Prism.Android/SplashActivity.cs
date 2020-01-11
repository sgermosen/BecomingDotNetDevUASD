using Android.App;
using Android.OS;

namespace MyVet.Prism.Droid
{
    [Activity(
        Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            System.Threading.Thread.Sleep(1); //TODO: Set 1800
            StartActivity(typeof(MainActivity));
        }
    }
}
