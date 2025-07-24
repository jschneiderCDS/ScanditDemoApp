namespace ScanditDemoApp.Model
{
    public class BarcodeScanResultModel
    {
        public List<BarcodeInfo>? Barcodes { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsDuplicate { get; set; }

        public static BarcodeScanResultModel NullBarcodeScanResultModel => new BarcodeScanResultModel() { Barcodes = null, Message = "No barcode scanned.", IsDuplicate = false, IsSuccess = false };
        public static BarcodeScanResultModel SuccessBarcodeScanResultModel(List<BarcodeInfo> barcodes) => new BarcodeScanResultModel() { Barcodes = barcodes, Message = null, IsDuplicate = false, IsSuccess = true };

        public static BarcodeScanResultModel FailureBarcodeScanResultModel(List<BarcodeInfo> barcodes, string message) => new BarcodeScanResultModel() { Barcodes = barcodes, Message = message, IsDuplicate = false, IsSuccess = false };
        public static BarcodeScanResultModel DuplicateBarcodeScanResultModel(List<BarcodeInfo> barcodes, string message = "Barcode was already scanned.", bool isSuccess = false) => new BarcodeScanResultModel() { Barcodes = barcodes, Message = message, IsDuplicate = true, IsSuccess = isSuccess };
    }

}
