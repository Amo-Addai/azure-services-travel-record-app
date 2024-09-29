using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;

using travelrecordapp.Models;
using travelrecordapp.Pages;

namespace travelrecordapp
{
    public partial class App : Application
    {
        // todo: choose 1 (or configure both seamlessly), for entire app
        //  - Sqlite-db
        //  - Azure AppService Easy Tables

        public static string DatabaseLocation = string.Empty;

        // Azure AppService MobileServiceClient
        public static MobileServiceClient MobileService =
            new MobileServiceClient(
                    "https://travelrecordapp.azurewebsites.net"
                );

        public static User user = new User();

        public App ()
        {
            InitializeComponent();

            // MainPage = new MainPage(); // default 'this.'MainPage assignment

            MainPage = new NavigationPage(new LoginPage()); // NavigationPage() allows all page navigation
        }

        // constructor override can be used instead, in Android / iOS entry point
        public App(string dbLocation) // path to .sqlite db file
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());

            DatabaseLocation = dbLocation;
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

