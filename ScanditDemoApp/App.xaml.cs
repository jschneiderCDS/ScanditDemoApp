using CommunityToolkit.Mvvm.Messaging;
using ScanditDemoApp.Message;

namespace ScanditDemoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override void OnStart()
        {
            WeakReferenceMessenger.Default.Send(new StartMessage());
            base.OnStart();
        }

        protected override void OnSleep()
        {
            WeakReferenceMessenger.Default.Send(new SleepMessage());
            base.OnSleep();
        }

        protected override void OnResume()
        {
            WeakReferenceMessenger.Default.Send(new ResumeMessage());
            base.OnResume();
        }
    }
}