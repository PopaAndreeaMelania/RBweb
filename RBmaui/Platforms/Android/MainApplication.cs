using Android.App;
using Android.Runtime;

namespace RBmaui
{
    [Application]
    [assembly: UsesPermission(Android.Manifest.Permission.Internet)]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
