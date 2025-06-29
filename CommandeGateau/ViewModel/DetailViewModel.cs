using CommandeGateau.Models;
using CommandeGateau.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CommandeGateau.ViewModel
{
    public partial class DetailViewModel : BaseViewModel
    {
        readonly  CommandeService _service;
        public DetailViewModel(CommandeService service)
        {
            _service = service;
        }
        [ObservableProperty]
        private Commande commande;

        public void SetCommande(Commande commande)
        {
            Commande = commande;
            OnPropertyChanged(nameof(Patisseries));
        }

        public ObservableCollection<PatisserieTemplate> Patisseries =>
            new(commande?.Patisseries ?? new());

        [RelayCommand]
        public async Task DeleteCommande()
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirmation",                    
                "Supprimer cette commande ?",      
                "Oui",                             
                "Non");                            

            if (!confirm)
                return;

            await _service.DeleteCommande(Commande);

            await Shell.Current.DisplayAlert("Supprimé", "La commande a été supprimée.", "OK");
            await Shell.Current.GoToAsync("..");
        }

    }
}
