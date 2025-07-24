using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using ScanditDemoApp.Model;
using ScanditDemoApp.Service;

namespace ScanditDemoApp.ViewModel
{
    public partial class MainPageViewModel : BarcodeEnabledViewModel
    {
        private readonly IPopupService _popupService;

        public MainPageViewModel(
            IAlertService alertService,
            IScanditModel scanditModel,
            IPopupService popupService) : base(alertService, scanditModel)
        {
            _popupService = popupService;
        }

        [RelayCommand]
        public async Task ShowPopupAsync()
        {
            await _popupService.ShowPopupAsync<ScanningPopupViewModel>(Shell.Current);
        }

        public override async Task<BarcodeScanResultModel?> OnBarcodeScannedAsync(BarcodeScanModel? barcodeScanModel)
        {
            if (barcodeScanModel == null || barcodeScanModel.Barcodes == null || barcodeScanModel.Barcodes.Count == 0)
            {
                return BarcodeScanResultModel.FailureBarcodeScanResultModel(barcodeScanModel?.Barcodes, "No barcodes.");
            }
            AlertService.ShowToast($"{barcodeScanModel.Barcodes.First().RawValue}", ToastDuration.Short, 14.0);
            return BarcodeScanResultModel.SuccessBarcodeScanResultModel(barcodeScanModel.Barcodes);
        }
    }
}
