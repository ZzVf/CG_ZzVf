using CommandeGateau.ViewModel;

namespace CommandeGateau;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mvm)
	{
		InitializeComponent();
		BindingContext = mvm;
	}
}
