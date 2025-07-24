using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScanditDemoApp.Model;
using ScanditDemoApp.Service;

namespace ScanditDemoApp.ViewModel
{
    public partial class ScanningPopupViewModel : BarcodeEnabledViewModel
    {
        private readonly IPopupService _popupService;

        public ScanningPopupViewModel(
            IAlertService alertService, 
            IScanditModel scanditModel,
            IPopupService popupService) : base(alertService, scanditModel)
        {
            _popupService = popupService;
        }

        [ObservableProperty]
        private string _lastScannedBarcode = string.Empty;

        [RelayCommand]
        public async Task ClosePopupAsync()
        {
            await _popupService.ClosePopupAsync(Shell.Current);
        }

        public override async Task<BarcodeScanResultModel?> OnBarcodeScannedAsync(BarcodeScanModel? barcodeScanModel)
        {
            if (barcodeScanModel == null || barcodeScanModel.Barcodes == null || barcodeScanModel.Barcodes.Count == 0)
            {
                return BarcodeScanResultModel.FailureBarcodeScanResultModel(barcodeScanModel?.Barcodes, "No barcodes.");
            }
            LastScannedBarcode = barcodeScanModel.Barcodes.First().RawValue;
            return BarcodeScanResultModel.SuccessBarcodeScanResultModel(barcodeScanModel.Barcodes);
        }
    }
}
