using System;
using System.Collections.Generic;
using System.Linq;
using UserNotifications;
using Foundation;
using UIKit;

namespace MTM.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            UNUserNotificationCenter.Current.Delegate = new iOSNotificationReceiver();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
