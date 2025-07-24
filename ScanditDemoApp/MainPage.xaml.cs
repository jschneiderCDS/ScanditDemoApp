using ScanditDemoApp.ViewModel;

namespace ScanditDemoApp
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel ViewModel => (MainPageViewModel)this.BindingContext;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
            SubscribeToViewModelEvents();
            this.SparkScanView.Loaded += SparkScanView_Loaded;
        }

        private void SparkScanView_Loaded(object sender, EventArgs e)
        {
            this.SparkScanView.Feedback = this.ViewModel;
        }

        private void SubscribeToViewModelEvents()
        {
            this.ViewModel.Sleep += (object sender, EventArgs args) =>
            {
                this.SparkScanView.PauseScanning();
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.SparkScanView.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.SparkScanView.OnDisappearing();
        }
    }

}
