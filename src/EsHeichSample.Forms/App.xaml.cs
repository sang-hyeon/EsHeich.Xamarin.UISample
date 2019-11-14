
namespace EsHeichSample
{
    using System;
    using Autofac;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        public App()
        {
            InitializeComponent();
            Container = Forms.Initializer.BuildContainer();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
