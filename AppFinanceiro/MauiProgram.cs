using AppFinanceiro.Services;
using AppFinanceiro.ViewModels;
using AppFinanceiro.Views;
using LiteDB;
using Microsoft.Extensions.Logging;
using Repositories.Repositories;

namespace AppFinanceiro
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
                })
                .RegisterDatabaseAndRepositories();

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<HomeVM>();
            builder.Services.AddSingleton<AddTransactionPage>();
            builder.Services.AddSingleton<AddTransactionVM>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
           

            RegisterForRoute<AddTransactionPage>();

            static void RegisterForRoute<T>()
            {
                Routing.RegisterRoute(typeof(T).Name, typeof(T));
            }
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterDatabaseAndRepositories(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<LiteDatabase>(
                options =>
                {
                    return new LiteDatabase($"Filename={AppSettings.DatabasePath};Connection=Shared");
                }
            );
            mauiAppBuilder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
            return mauiAppBuilder;
        }
    }
}
