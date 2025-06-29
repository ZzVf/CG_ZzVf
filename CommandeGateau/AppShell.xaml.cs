using CommandeGateau.View;

namespace CommandeGateau;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(NewPage), typeof(NewPage));
		Routing.RegisterRoute(nameof(PatisseriePage), typeof(PatisseriePage));
		Routing.RegisterRoute(nameof(CommandePage), typeof(CommandePage));
		Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
    }
}
