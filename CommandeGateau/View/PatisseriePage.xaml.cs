using CommandeGateau.Models;
using CommandeGateau.ViewModel;
using System.Collections.ObjectModel;

namespace CommandeGateau.View;

public partial class PatisseriePage : ContentPage, IQueryAttributable
{
    private readonly PatisserieViewModel _viewModel;

    public PatisseriePage(PatisserieViewModel pvm)
    {
        InitializeComponent();
        _viewModel = pvm;
        BindingContext = _viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Patisseries", out var value)
            && value is ObservableCollection<PatisserieTemplate> patisseries)
        {
            _viewModel.Patisseries = patisseries;
        }
    }
}