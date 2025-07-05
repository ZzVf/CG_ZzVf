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
        public string GateauQuantite { get; set; }
        public string GateauEtage { get; set; }
        public string GateauGout { get; set; }
        public bool GateauFruite { get; set; }
        public bool GateauModelage { get; set; }

        public ICommand AjouterGateauCommand => new Command(() =>
        {
            int.TryParse(GateauQuantite, out int quantite);
            int.TryParse(GateauEtage, out int etage);

            var gateau = new Gateau
            {
                Id = Patisseries.Count + 1,
                Name = GateauNom,
                Quantity = quantite,
                Etage = etage,
                Gout = GateauGout,
                Fruite = GateauFruite,
                Modelage = GateauModelage
            };

            Patisseries.Add(gateau);
            TypeSelectionne = string.Empty;
        });

        public string BiscuitNom { get; set; }
        public string BiscuitQuantite { get; set; }
        public bool BiscuitTaille { get; set; }

        public ICommand AjouterBiscuitCommand => new Command(() =>
        {
            int.TryParse(BiscuitQuantite, out int quantite);

            var biscuit = new Biscuit
            {
                Id = Patisseries.Count + 1,
                Name = BiscuitNom,
                Quantity = quantite,
                Taille = BiscuitTaille
            };

            Patisseries.Add(biscuit);
            TypeSelectionne = string.Empty;
        });

        public string CupcakeNom { get; set; }
        public string CupcakeQuantite { get; set; }

        public ICommand AjouterCupcakeCommand => new Command(() =>
        {
            int.TryParse(CupcakeQuantite, out int quantite);

            var cupcake = new CupCake
            {
                Id = Patisseries.Count + 1,
                Name = CupcakeNom,
                Quantity = quantite
            };

            Patisseries.Add(cupcake);
            TypeSelectionne = string.Empty;
        });

        public string MacaronNom { get; set; }
        public string MacaronQuantite { get; set; }

        public ICommand AjouterMacaronCommand => new Command(() =>
        {
            int.TryParse(MacaronQuantite, out int quantite);

            var macaron = new Macaron
            {
                Id = Patisseries.Count + 1,
                Name = MacaronNom,
                Quantity = quantite
            };

            Patisseries.Add(macaron);
            TypeSelectionne = string.Empty;
        });

        public string MagnumNom { get; set; }
        public string MagnumQuantite { get; set; }

        public ICommand AjouterMagnumCommand => new Command(() =>
        {
            int.TryParse(MagnumQuantite, out int quantite);

            var magnum = new MagnumCake
            {
                Id = Patisseries.Count + 1,
                Name = MagnumNom,
                Quantity = quantite
            };

            Patisseries.Add(magnum);
            TypeSelectionne = string.Empty;
        });

        public string PopCakeNom { get; set; }
        public string PopCakeQuantite { get; set; }

        public ICommand AjouterPopCakeCommand => new Command(() =>
        {
            int.TryParse(PopCakeQuantite, out int quantite);

            var popcake = new PopCake
            {
                Id = Patisseries.Count + 1,
                Name = PopCakeNom,
                Quantity = quantite
            };

            Patisseries.Add(popcake);
            TypeSelectionne = string.Empty;
        });

        public string VerrineNom { get; set; }
        public string VerrineQuantite { get; set; }

        public ICommand AjouterVerrineCommand => new Command(() =>
        {
            int.TryParse(VerrineQuantite, out int quantite);

            var verrine = new Verrine
            {
                Id = Patisseries.Count + 1,
                Name = VerrineNom,
                Quantity = quantite
            };

            Patisseries.Add(verrine);
            TypeSelectionne = string.Empty;
        });
    }
}