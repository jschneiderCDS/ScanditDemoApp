using Scandit.DataCapture.Barcode.Data;
using Scandit.DataCapture.Barcode.Spark.Capture;
using Scandit.DataCapture.Barcode.Spark.Feedback;
using Scandit.DataCapture.Barcode.Spark.UI;
using Scandit.DataCapture.Core.Capture;
using ScanditDemoApp.Model;
using ScanditDemoApp.Service;

namespace ScanditDemoApp.ViewModel
{
    public abstract partial class BarcodeEnabledViewModel : BaseViewModel, ISparkScanFeedbackDelegate
    {

        public virtual DataCaptureContext? DataCaptureContext
        {
            get
            {
                return _scanditModel?.DataCaptureContext;
            }
        }
        public virtual SparkScan? SparkScan
        {
            get
            {
                return _scanditModel?.SparkScan;
            }
        }
        public virtual SparkScanViewSettings? ViewSettings
        {
            get
            {
                return _scanditModel?.DefaultViewSettings;
            }
        }

        private readonly IScanditModel _scanditModel;


        public BarcodeEnabledViewModel(
           
            IAlertService alertService,
            IScanditModel scanditModel) : base(alertService)
        {
            _scanditModel = scanditModel;
        }

        public override void OnStart()
        {
            base.OnStart();
            Application.Current.Dispatcher.Dispatch(async () =>
            {
                await CheckCameraPermissionAsync();
            });
        }

        public override void OnResume()
        {
            base.OnResume();
            Application.Current.Dispatcher.Dispatch(async () =>
            {
                await CheckCameraPermissionAsync();
            });
        }

        private async Task CheckCameraPermissionAsync()
        {
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (permissionStatus != PermissionStatus.Granted)
            {
                permissionStatus = await Permissions.RequestAsync<Permissions.Camera>();
                if (permissionStatus == PermissionStatus.Granted)
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        public abstract Task<BarcodeScanResultModel?> OnBarcodeScannedAsync(BarcodeScanModel? barcodeScanModel);

        public SparkScanBarcodeFeedback? GetFeedbackForBarcode(Barcode barcode)
        {
            var barcodeScanModel = new BarcodeScanModel
            {
                Barcodes = new List<BarcodeInfo>() { new BarcodeInfo
                        {
                            RawValue = barcode.Data,
                            Symbology = new SymbologyDescription(barcode.Symbology).ReadableName,
                            Typed = false
                        }
                    }
            };

            SparkScanBarcodeFeedback? feedback = null;
            var barcodeTask = Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                var scanResult = await this.OnBarcodeScannedAsync(barcodeScanModel);
                if (scanResult == null)
                {
                    return;
                }
                feedback = scanResult.IsSuccess ? new SparkScanBarcodeSuccessFeedback() : new SparkScanBarcodeErrorFeedback(scanResult?.Message ?? string.Empty, TimeSpan.FromSeconds(0));
            });

            barcodeTask.Wait();
            return feedback;
        }
    }
}
