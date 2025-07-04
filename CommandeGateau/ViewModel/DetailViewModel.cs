﻿using CommandeGateau.Models;
using CommandeGateau.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CommandeGateau.ViewModel
{
    public partial class DetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotFromArchive))]
        private bool isFromArchive;
        public bool IsNotFromArchive => !IsFromArchive;


        readonly CommandeService _service;
        public DetailViewModel(CommandeService service)
        {
            _service = service;
        }
        [ObservableProperty]
        private Commande commande;

        public void SetCommande(Commande commande, bool fromArchive = false)
        {
            Commande = commande;
            IsFromArchive = fromArchive;
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
        [RelayCommand]
        public async Task ArchiveCommande()
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Archiver cette commande ?",
                "Oui",
                "Non");

            if (!confirm)
                return;

            await _service.ArchiveCommande(Commande);

            await Shell.Current.DisplayAlert("Archivé", "La commande a été archivée.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task DeleteArchive()
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Supprimer cette archive ?",
                "Oui",
                "Non");

            if (!confirm)
                return;

            await _service.DeleteArchive(Commande);

            await Shell.Current.DisplayAlert("Supprimé", "L'archive a été supprimée.", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}
