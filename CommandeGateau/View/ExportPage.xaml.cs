using CommandeGateau.ViewModel;

namespace CommandeGateau.View;

public partial class ExportPage : ContentPage
{
    public ExportPage(ExportViewModel evm)
    {
        InitializeComponent();
        BindingContext = evm;
    }
}