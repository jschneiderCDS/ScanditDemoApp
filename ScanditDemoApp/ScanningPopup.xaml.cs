using ScanditDemoApp.ViewModel;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;

namespace ScanditDemoApp;

public partial class ScanningPopup : ContentView
{
    private ScanningPopupViewModel ViewModel => (ScanningPopupViewModel)this.BindingContext;

    public ScanningPopup(ScanningPopupViewModel viewModel) : base()
    {
        InitializeComponent();
        BindingContext = viewModel;
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

    private async void Page_Loaded(object sender, EventArgs e)
    {
        this.SparkScanView.OnAppearing();
    }

    private void Page_Unloaded(object sender, EventArgs e)
    {
        this.SparkScanView.OnDisappearing();
    }
}