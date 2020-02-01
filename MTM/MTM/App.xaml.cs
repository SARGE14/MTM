﻿using Xamarin.Forms;

namespace MTM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Get<INotificationManager>().Initialize();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
