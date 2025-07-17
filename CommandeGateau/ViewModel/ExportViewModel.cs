using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CommandeGateau.ViewModel
{
    public partial class ExportViewModel : BaseViewModel
    {
        [RelayCommand]
        public async Task Export()
        {
            await ExportFile("commandes.json");
        }

        [RelayCommand]
        public async Task ExportArchive()
        {
            await ExportFile("archives.json");
        }

        [RelayCommand]
        public async Task Import()
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Importer les commandes ?",
                "Oui",
                "Non");
            if (!confirm)
                return;
            await ImportFile("commandes.json");
        }

        [RelayCommand]
        public async Task ImportArchive()
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Importer les archives ?",
                "Oui",
                "Non");
            if (!confirm)
                return;
            await ImportFile("archives.json");
        }

        // --- Méthode Générique Export ---
        private static async Task ExportFile(string fileName)
        {
            try
            {
                string sourceFilePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                if (!File.Exists(sourceFilePath))
                {
                    await Shell.Current.DisplayAlert("Erreur", $"Fichier {fileName} introuvable !", "OK");
                    return;
                }

#if ANDROID
                string downloadsPath = "/storage/emulated/0/Download";
                string destFilePath = Path.Combine(downloadsPath, fileName);
                File.Copy(sourceFilePath, destFilePath, true);
#endif

                await Shell.Current.DisplayAlert("Succès", $"Fichier exporté vers les téléchargements", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", $"Export échoué :\n{ex.Message}", "OK");
            }
        }

        private static async Task ImportFile(string fileName)
        {
            try
            {
#if ANDROID
                string downloadsPath = "/storage/emulated/0/Download";
                string sourceFilePath = Path.Combine(downloadsPath, fileName);
                string destFilePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                if (!File.Exists(sourceFilePath))
                {
                    await Shell.Current.DisplayAlert("Erreur", $"Fichier {fileName} non trouvé dans Téléchargements !", "OK");
                    return;
                }

                File.Copy(sourceFilePath, destFilePath, true);

                await Shell.Current.DisplayAlert("Succès", $"Fichier importé depuis les téléchargements", "OK");
#else
                await Shell.Current.DisplayAlert("Erreur", $"Import non supporté sur cette plateforme.", "OK");
#endif
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", $"Import échoué :\n{ex.Message}", "OK");
            }
        }
    }
}
