using CommandeGateau.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace CommandeGateau.ViewModel
{
    public partial class PatisserieViewModel : BaseViewModel
    {
        public ObservableCollection<PatisserieTemplate> Patisseries { get; set; } = new();

        private string _typeSelectionne;
        public string TypeSelectionne
        {
            get => _typeSelectionne;
            set
            {
                if (_typeSelectionne != value)
                {
                    _typeSelectionne = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(EstEnModeFormulaire));

                    // Mise à jour visibilité
                    OnPropertyChanged(nameof(IsGateauVisible));
                    OnPropertyChanged(nameof(IsBiscuitVisible));
                    OnPropertyChanged(nameof(IsCupcakeVisible));
                    OnPropertyChanged(nameof(IsMacaronVisible));
                    OnPropertyChanged(nameof(IsMagnumVisible));
                    OnPropertyChanged(nameof(IsPopCakeVisible));
                    OnPropertyChanged(nameof(IsVerrineVisible));
                }
            }
        }

        public bool EstEnModeFormulaire => !string.IsNullOrEmpty(TypeSelectionne);

        // Visibilité conditionnelle
        public bool IsGateauVisible => TypeSelectionne == "Gateau";
        public bool IsBiscuitVisible => TypeSelectionne == "Biscuit";
        public bool IsCupcakeVisible => TypeSelectionne == "Cupcake";
        public bool IsMacaronVisible => TypeSelectionne == "Macaron";
        public bool IsMagnumVisible => TypeSelectionne == "Magnum";
        public bool IsPopCakeVisible => TypeSelectionne == "PopCake";
        public bool IsVerrineVisible => TypeSelectionne == "Verrine";

        public ICommand SelectionnerTypeCommand => new Command<string>((type) =>
        {
            TypeSelectionne = type;
        });

        public ICommand ValiderSelectionCommand => new Command(async () =>
        {
            foreach (var patisserie in Patisseries)
            {
                Debug.WriteLine($"ID: {patisserie.Id}, Nom: {patisserie.Name}, Quantité: {patisserie.Quantity}, Prix: {patisserie.Price} €");
            }
            await Shell.Current.GoToAsync("..", true);
        });

        public string GateauNom { get; set; }
        public int GateauQuantite { get; set; }
        public int GateauEtage { get; set; }
        public string GateauGout { get; set; }
        public bool GateauFruite { get; set; }
        public bool GateauModelage { get; set; }

        public ICommand AjouterGateauCommand => new Command(() =>
        {
            var gateau = new Gateau
            {
                Id = Patisseries.Count + 1,
                Name = GateauNom,
                Quantity = GateauQuantite,
                Etage = GateauEtage,
                Gout = GateauGout,
                Fruite = GateauFruite,
                Modelage = GateauModelage
            };

            Patisseries.Add(gateau);
            TypeSelectionne = string.Empty;
        });

        public string BiscuitNom { get; set; }
        public int BiscuitQuantite { get; set; }
        public bool BiscuitTaille { get; set; }

        public ICommand AjouterBiscuitCommand => new Command(() =>
        {
            var biscuit = new Biscuit
            {
                Id = Patisseries.Count + 1,
                Name = BiscuitNom,
                Quantity = BiscuitQuantite,
                Taille = BiscuitTaille
            };

            Patisseries.Add(biscuit);
            TypeSelectionne = string.Empty;
        });

        public string CupcakeNom { get; set; }
        public int CupcakeQuantite { get; set; }

        public ICommand AjouterCupcakeCommand => new Command(() =>
        {
            var cupcake = new CupCake
            {
                Id = Patisseries.Count + 1,
                Name = CupcakeNom,
                Quantity = CupcakeQuantite
            };

            Patisseries.Add(cupcake);
            TypeSelectionne = string.Empty;
        });

        public string MacaronNom { get; set; }
        public int MacaronQuantite { get; set; }

        public ICommand AjouterMacaronCommand => new Command(() =>
        {
            var macaron = new Macaron
            {
                Id = Patisseries.Count + 1,
                Name = MacaronNom,
                Quantity = MacaronQuantite
            };

            Patisseries.Add(macaron);
            TypeSelectionne = string.Empty;
        });

        public string MagnumNom { get; set; }
        public int MagnumQuantite { get; set; }

        public ICommand AjouterMagnumCommand => new Command(() =>
        {
            var magnum = new MagnumCake
            {
                Id = Patisseries.Count + 1,
                Name = MagnumNom,
                Quantity = MagnumQuantite
            };

            Patisseries.Add(magnum);
            TypeSelectionne = string.Empty;
        });

        public string PopCakeNom { get; set; }
        public int PopCakeQuantite { get; set; }

        public ICommand AjouterPopCakeCommand => new Command(() =>
        {
            var popcake = new PopCake
            {
                Id = Patisseries.Count + 1,
                Name = PopCakeNom,
                Quantity = PopCakeQuantite
            };

            Patisseries.Add(popcake);
            TypeSelectionne = string.Empty;
        });

        public string VerrineNom { get; set; }
        public int VerrineQuantite { get; set; }

        public ICommand AjouterVerrineCommand => new Command(() =>
        {
            var verrine = new Verrine
            {
                Id = Patisseries.Count + 1,
                Name = VerrineNom,
                Quantity = VerrineQuantite
            };

            Patisseries.Add(verrine);
            TypeSelectionne = string.Empty;
        });
    }
}
