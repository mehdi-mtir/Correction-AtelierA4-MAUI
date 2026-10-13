using GestionCommandes.Maui.Services;
using GestionCommandes.Maui.ViewModels;
using GestionCommandes.Maui.Views;
using Microsoft.Extensions.Logging;

namespace GestionCommandes.Maui;

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

        // ---------- Injection de dépendances ----------
        // Services (une instance pour toute l'application)
        builder.Services.AddSingleton<IDepotClients, DepotClientsFactice>();
        builder.Services.AddSingleton<IDepotCommandes, DepotCommandesFactice>();

        // ViewModels et pages (une instance à chaque affichage)
        builder.Services.AddTransient<ConnexionViewModel>();
        builder.Services.AddTransient<ConnexionPage>();
        builder.Services.AddTransient<ClientsViewModel>();
        builder.Services.AddTransient<ClientsPage>();
        builder.Services.AddTransient<CommandesViewModel>();
        builder.Services.AddTransient<CommandesPage>();
        builder.Services.AddTransient<DetailCommandeViewModel>();
        builder.Services.AddTransient<DetailCommandePage>();
        builder.Services.AddTransient<CommandeViewModel>();
        builder.Services.AddTransient<CommandePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
