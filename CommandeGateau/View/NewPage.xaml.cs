using CommandeGateau.ViewModel;

namespace CommandeGateau.View;

public partial class NewPage : ContentPage
{
	public NewPage(NewViewModel nvm)
	{
		InitializeComponent();
		BindingContext=nvm;
	}
}