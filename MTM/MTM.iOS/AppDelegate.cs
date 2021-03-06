﻿using Foundation;
using System;
using UIKit;
using UserNotifications;

namespace MTM.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);
            
            UNUserNotificationCenter.Current.Delegate = new iOSNotificationReceiver();
           
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            completionHandler(UIBackgroundFetchResult.NewData);
        }
    }
}
