using Microsoft.Extensions.Logging;
using myAmiibo.Services;   // <--- Agrega esto
using myAmiibo.ViewModels; // <--- Agrega esto

namespace myAmiibo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // --- AGREGA ESTAS 3 LÍNEAS AQUÍ ---

        // 1. El Servicio (Singleton: Una sola instancia para toda la app)
        builder.Services.AddSingleton<AmiiboService>();

        // 2. El ViewModel
        builder.Services.AddSingleton<AmiibosViewModel>();

        // 3. La Vista (Página principal)
        builder.Services.AddSingleton<MainPage>();

        // -----------------------------------

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}