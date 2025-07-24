namespace ScanditDemoApp.Model
{
    public class BarcodeScanModel
    {
        public List<BarcodeInfo>? Barcodes { get; set; }
    }

    public class BarcodeInfo
    {
        public string? RawValue { get; set; }
        public string? Symbology { get; set; }
        public bool Typed { get; set; } = false;
    }
}