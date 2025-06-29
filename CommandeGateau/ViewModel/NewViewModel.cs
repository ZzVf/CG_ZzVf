using CommandeGateau.Models;
using CommandeGateau.Services;
using CommandeGateau.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CommandeGateau.ViewModel
{
    public partial class NewViewModel : BaseViewModel
    {
        private readonly CommandeService _commandeService;
        [ObservableProperty]
        private string nameClient;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int? age;

        [ObservableProperty]
        private DateTime dateLivraison = DateTime.Today.AddDays(1);

        [ObservableProperty]
        private string note;

        [ObservableProperty]
        private ObservableCollection<PatisserieTemplate> patisseries = new();

        [ObservableProperty]
        private double total;

        public NewViewModel(CommandeService commandeService)
        {
            _commandeService = commandeService;
            Patisseries.CollectionChanged += (_, _) => RecalculerTotal();
        }

        private void RecalculerTotal()
        {
            Total = Patisseries.Sum(p => (p.Price) * p.Quantity);
            foreach (var item in Patisseries)
            {
                if (item is Gateau gateau && gateau.Modelage)
                    Total += 4.00;
            }
        }
        [RelayCommand]
        public async Task ValiderCommande()
        {
            var nouvelleCommande = new Commande
            {
                NameClient = NameClient,
                Name = Name,
                Age = Age,
                DateLivraison = DateLivraison,
                Note = Note,
                Patisseries = Patisseries.ToList(),
                Total = Total
            };

            await _commandeService.AddCommande(nouvelleCommande);

            await Shell.Current.DisplayAlert(
                "Succès", $"Commande enregistrée\nTotal : {Total:F2} €", "OK");
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task GoToPatisserie()
        {
            await Shell.Current.GoToAsync(
                nameof(PatisseriePage),
                new Dictionary<string, object>
                {
                    { "Patisseries", Patisseries }
                });
        }
    }
}
