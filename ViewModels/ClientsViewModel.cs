using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Maui.Models;
using GestionCommandes.Maui.Services;

namespace GestionCommandes.Maui.ViewModels;

public partial class ClientsViewModel : ObservableObject
{
    private readonly IDepotClients depotClients;

    // Le dépôt est injecté : le ViewModel ne sait pas d'où viennent les données
    public ClientsViewModel(IDepotClients depotClients)
    {
        this.depotClients = depotClients;
    }

    // Génère la propriété publique « Clients » et prévient la page à chaque changement
    [ObservableProperty]
    private ObservableCollection<Client> clients = new();

    // Vrai pendant un chargement : la page affiche un indicateur d'activité
    [ObservableProperty]
    private bool isBusy;

    // Génère la commande « ChargerClientsCommand », utilisable depuis la page
    [RelayCommand]
    private async Task ChargerClientsAsync()
    {
        if (IsBusy) return;          // évite deux chargements en même temps
        IsBusy = true;
        try
        {
            var liste = await depotClients.ListerAsync();
            Clients = new ObservableCollection<Client>(liste);
        }
        finally
        {
            IsBusy = false;          // toujours exécuté, même en cas d'erreur
        }
    }

    // Retour à la connexion : route absolue, la pile est vidée
    [RelayCommand]
    private async Task SeDeconnecterAsync()
    {
        await Shell.Current.GoToAsync("//connexion");
    }
}
