using CommandeGateau.Models;
using CommandeGateau.ViewModel;

namespace CommandeGateau.View;

public partial class DetailPage : ContentPage, IQueryAttributable
{
    private readonly DetailViewModel _viewModel;

    public DetailPage(DetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Commande", out var value) && value is Commande commande)
        {
            _viewModel.SetCommande(commande);
        }

        if (query.TryGetValue("IsFromArchive", out var isFromArchiveObj) && isFromArchiveObj is bool isFromArchive)
        {
            _viewModel.IsFromArchive = isFromArchive;
        }
    }
}
