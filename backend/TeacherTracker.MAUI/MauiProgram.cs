using Microsoft.Extensions.Logging;
using TeacherTracker.MAUI.Services;

namespace TeacherTracker.MAUI
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

            var handler = new HttpClientHandler();


            handler.ServerCertificateCustomValidationCallback =
                (request, certificate, chain, errors) =>
                {
                    // Samo lokalni API iz Android emulatora.
                    if (request.RequestUri?.Host == "10.0.2.2" &&
                        request.RequestUri.Port == 7025 &&
                        certificate?.Issuer == "CN=localhost")
                    {
                        return true;
                    }

                    return errors == System.Net.Security.SslPolicyErrors.None;
                };
#endif

            builder.Services.AddSingleton(new HttpClient(handler)
            {
                BaseAddress = new Uri("https://10.0.2.2:7025/")
            });

            builder.Services.AddSingleton<SchoolApiService>();

            return builder.Build();
        }
    }
}
