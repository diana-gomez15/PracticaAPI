using Microsoft.Extensions.Logging;
using RawPostres.Views;  // ✅ Cambia RawGames a RawPostres
using RawPostres.Services;
using RawPostres.ViewModel;

namespace PracticaAPI
{
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

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Registrar servicios
            builder.Services.AddSingleton<PostresApiService>();

            // Registrar ViewModels
            builder.Services.AddSingleton<PostreListViewModel>();

            // Registrar Views - ✅ CAMBIA ESTA LÍNEA
            builder.Services.AddSingleton<PostreListPage>();

            return builder.Build();
        }
    }
}
