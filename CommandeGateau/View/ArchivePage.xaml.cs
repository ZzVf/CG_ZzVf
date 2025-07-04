using CommandeGateau.ViewModel;

namespace CommandeGateau.View;

public partial class ArchivePage : ContentPage
{
    ArchiveViewModel _viewModel;
    public ArchivePage(ArchiveViewModel cvm)
    {
        InitializeComponent();
        _viewModel = cvm;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.RefreshCommandes();
    }
}