using CommandeGateau.ViewModel;

namespace CommandeGateau.View;

public partial class CommandePage : ContentPage
{
    CommandeViewModel _viewModel;
    public CommandePage(CommandeViewModel cvm)
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