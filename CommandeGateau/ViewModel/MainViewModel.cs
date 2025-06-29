using CommandeGateau.Models;
using CommandeGateau.View;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CommandeGateau.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        Title = "Les délices de Christelle.";
    }
    public List<Commande> Commandes { get; set; }
    [RelayCommand]
    public async Task GoToNew()
    {
       await Shell.Current.GoToAsync(nameof(NewPage));
    }
    [RelayCommand] 
    public async Task GoToCommande()
    {
        await Shell.Current.GoToAsync(nameof(CommandePage));
    }
}
