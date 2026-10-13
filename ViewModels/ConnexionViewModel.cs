using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GestionCommandes.Maui.ViewModels;

public partial class ConnexionViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SeConnecterCommand))]
    private string identifiant = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SeConnecterCommand))]
    private string motDePasse = string.Empty;

    [ObservableProperty]
    private string erreur = string.Empty;

    // Le bouton n'est actif que si les deux champs sont remplis
    private bool PeutSeConnecter() =>
        !string.IsNullOrWhiteSpace(Identifiant) && !string.IsNullOrWhiteSpace(MotDePasse);

    [RelayCommand(CanExecute = nameof(PeutSeConnecter))]
    private async Task SeConnecterAsync()
    {
        Erreur = string.Empty;

        // Simulation : la vérification par l'API (JWT) arrive à l'atelier A5
        if (Identifiant == "admin" && MotDePasse == "admin123")
        {
            // Route ABSOLUE (//) : on remplace toute la pile de navigation
            await Shell.Current.GoToAsync("//clients");
        }
        else
        {
            Erreur = "Identifiant ou mot de passe incorrect.";
        }
    }
}
