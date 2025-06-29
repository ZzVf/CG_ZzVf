using CommandeGateau.Services;
using CommandeGateau.View;
using CommandeGateau.ViewModel;
using Microsoft.Extensions.Logging;

namespace CommandeGateau;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
        builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<NewPage>();
        builder.Services.AddTransient<NewViewModel>();
		builder.Services.AddTransient<PatisseriePage>();
        builder.Services.AddTransient<PatisserieViewModel>();
		builder.Services.AddTransient<CommandePage>();
		builder.Services.AddTransient<CommandeViewModel>();
        builder.Services.AddSingleton<CommandeService>();
		builder.Services.AddTransient<DetailPage>();
		builder.Services.AddTransient<DetailViewModel>();


        return builder.Build();
	}
}
