using CommandeGateau.Models;
using CommandeGateau.Services;
using CommandeGateau.View;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandeGateau.ViewModel
{
    public partial class CommandeViewModel : BaseViewModel
    {
        private readonly CommandeService _service;
        public ObservableCollection<Commande> Commandes { get; }

        public CommandeViewModel(CommandeService service)
        {
            _service = service;
            Commandes = new ObservableCollection<Commande>();
            GetCommandeAsync();
        }

        public async Task GetCommandeAsync()
        {
            if (IsBusy) return;

            IsBusy = true;
            await Task.Delay(2000);

            try
            {
                var commandes = await _service.GetCommandes();

                var commandesTriees = commandes
                    .OrderBy(c => c.DateLivraison)
                    .ToList();

                Commandes.Clear();
                foreach (var commande in commandesTriees)
                {
                    Commandes.Add(commande);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Erreur", "Une erreur s'est produite lors de la récupération des commandes.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async Task GoToDetails(Commande commandeToDetail)
        {
            await Shell.Current.GoToAsync(
    nameof(DetailPage),
    new Dictionary<string, object>
    {
        { "Commande", commandeToDetail },
        { "IsFromArchive", false }
    });

        }
        [RelayCommand]
        public static async Task GoToArchive()
        {
            await Shell.Current.GoToAsync(nameof(ArchivePage));
        }
        public async Task RefreshCommandes()
        {
            await GetCommandeAsync();
        }
    }
}
