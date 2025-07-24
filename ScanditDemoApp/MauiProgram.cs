using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Scandit.DataCapture.Barcode.Spark.UI.Maui;
using Scandit.DataCapture.Core.Capture;
using ScanditDemoApp.Model;
using ScanditDemoApp.Service;
using ScanditDemoApp.ViewModel;

namespace ScanditDemoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit(static options =>
                {
                    options.SetPopupDefaults(new DefaultPopupSettings
                    {
                        CanBeDismissedByTappingOutsideOfPopup = false,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        Margin = 0,
                        Padding = 0,
                    });
                    options.SetPopupOptionsDefaults(new DefaultPopupOptionsSettings
                    {
                        CanBeDismissedByTappingOutsideOfPopup = false,
                        Shadow = null,
                        Shape = null
                    });
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers(handler =>
                {
                    handler.AddHandler(typeof(SparkScanView), typeof(SparkScanViewHandler));
                }); 

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(DataCaptureContext.ForLicenseKey(ScanditModel.SCANDIT_LICENSE_KEY));
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<IPopupService, PopupService>();
            builder.Services.AddTransient<IScanditModel, ScanditModel>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingletonPopup<ScanningPopup, ScanningPopupViewModel>();

            return builder.Build();
        }
    }
}
