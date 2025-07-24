using Scandit.DataCapture.Barcode.Data;
using Scandit.DataCapture.Barcode.Spark.Capture;
using Scandit.DataCapture.Barcode.Spark.UI;
using Scandit.DataCapture.Core.Capture;

namespace ScanditDemoApp.Model
{
    public interface IScanditModel
    {
        DataCaptureContext DataCaptureContext { get; }
        SparkScanViewSettings DefaultViewSettings { get; }
        SparkScan SparkScan { get; }
    }

    public class ScanditModel : IScanditModel
    {
        // Enter your Scandit License key here.
        // Your Scandit License key is available via your Scandit SDK web account.
        public const string SCANDIT_LICENSE_KEY = "PUT KEY HERE";

        public ScanditModel(DataCaptureContext dataCaptureContext)
        {
            // Create data capture context using your license key and set the camera as the frame source.
            this.DataCaptureContext = dataCaptureContext;

            SparkScanSettings settings = new();
            // The settings instance initially has all types of barcodes (symbologies) disabled.
            // For the purpose of this sample we enable a very generous set of symbologies.
            // In your own app ensure that you only enable the symbologies that your app requires as
            // every additional enabled symbology has an impact on processing times.
            HashSet<Symbology> symbologies = new()
            {
                Symbology.Aztec,
                Symbology.Codabar,
                Symbology.Code128,
                Symbology.Code39,
                Symbology.DataMatrix,
                Symbology.Ean13Upca,
                Symbology.Ean8,
                Symbology.Gs1Databar,
                Symbology.Gs1DatabarExpanded,
                Symbology.InterleavedTwoOfFive,
                Symbology.MaxiCode,
                Symbology.MatrixTwoOfFive,
                Symbology.Pdf417,
                Symbology.Qr,
                Symbology.Upce
            };

            settings.EnableSymbologies(symbologies);

            //// Some linear/1d barcode symbologies allow you to encode variable-length data.
            //// By default, the Scandit Data Capture SDK only scans barcodes in a certain length range.
            //// If your application requires scanning of one of these symbologies, and the length is
            //// falling outside the default range, you may need to adjust the "active symbol counts"
            //// for this symbology. This is shown in the following few lines of code for one of the
            //// variable-length symbologies.
            //settings.GetSymbologySettings(Symbology.Code39).ActiveSymbolCounts =
            //    new short[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            // Create new spark scan mode with the settings from above.
            this.SparkScan = new SparkScan(settings);
        }

        public DataCaptureContext DataCaptureContext { get; private set; }

        public SparkScan SparkScan { get; private set; }

        public SparkScanViewSettings DefaultViewSettings { get; private set; } = new SparkScanViewSettings()
        {
            HardwareTriggerEnabled = true,
            DefaultScanningMode = new SparkScanScanningModeTarget(SparkScanScanningBehavior.Continuous, SparkScanPreviewBehavior.Persistent)
        };
    };
}
