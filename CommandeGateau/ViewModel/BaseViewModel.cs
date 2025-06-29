using CommunityToolkit.Mvvm.ComponentModel;
namespace CommandeGateau.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty] 
    string title;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(isNotBusy))]
    public bool isBusy;
    public bool isNotBusy => !IsBusy;

}
