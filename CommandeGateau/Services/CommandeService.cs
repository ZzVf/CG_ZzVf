using CommandeGateau.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
#if ANDROID
using Android.Content;
using Android.Provider;
using Microsoft.Maui.ApplicationModel;
#endif

namespace CommandeGateau.Services
{
    public class CommandeService
    {
        private readonly string _filePath;
        private readonly string _archivePath;
        private readonly JsonSerializerOptions _options;

        public CommandeService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "commandes.json");
            _archivePath = Path.Combine(FileSystem.AppDataDirectory, "archives.json");

            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() },
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
                {
                    typeInfo =>
                    {
                        if (typeInfo.Type == typeof(PatisserieTemplate))
                        {
                            typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                            {
                                TypeDiscriminatorPropertyName = "$type"
                            };

                            typeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(Gateau), "Gateau"));
                            typeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(Biscuit), "Biscuit"));
                            typeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(CupCake), "CupCake"));
                            typeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(Macaron), "Macaron"));
                            typeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(MagnumCake), "MagnumCake"));
                            typeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(PopCake), "PopCake"));
                            typeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(Verrine), "Verrine"));
                        }
                    }
                }
                }
            };
        }

        public async Task AddCommande(Commande nouvelleCommande)
        {
            List<Commande> commandes;

            if (File.Exists(_filePath))
            {
                var json = await File.ReadAllTextAsync(_filePath);
                commandes = JsonSerializer.Deserialize<List<Commande>>(json, _options) ?? new List<Commande>();
            }
            else
            {
                commandes = new List<Commande>();
            }

            int maxId = commandes.Count > 0 ? commandes.Max(c => c.Id) : 0;
            nouvelleCommande.Id = maxId + 1;

            commandes.Add(nouvelleCommande);

            var updatedJson = JsonSerializer.Serialize(commandes, _options);
            await File.WriteAllTextAsync(_filePath, updatedJson);
            AjouterEvenementAgenda(nouvelleCommande);
        }

        public async Task<List<Commande>> GetCommandes()
        {
            if (!File.Exists(_filePath))
                return new List<Commande>();

            using var reader = new StreamReader(_filePath);
            var content = await reader.ReadToEndAsync();

            var commandes = JsonSerializer.Deserialize<List<Commande>>(content, _options)
                            ?? new List<Commande>();

            return commandes;
        }

        public async Task DeleteCommande(Commande commandeASupprimer)
        {
            if (!File.Exists(_filePath))
                return;

            var json = await File.ReadAllTextAsync(_filePath);
            var commandes = JsonSerializer.Deserialize<List<Commande>>(json, _options) ?? new();

            commandes.RemoveAll(c => c.Id == commandeASupprimer.Id);

            var updatedJson = JsonSerializer.Serialize(commandes, _options);
            await File.WriteAllTextAsync(_filePath, updatedJson);
        }
        //
        //Archive
        //
        public async Task ArchiveCommande(Commande commandeArchiver)
        {
            List<Commande> commandesArchivees;

            if (File.Exists(_archivePath))
            {
                var archiveJson = await File.ReadAllTextAsync(_archivePath);
                commandesArchivees = JsonSerializer.Deserialize<List<Commande>>(archiveJson, _options) ?? new();
            }
            else
            {
                commandesArchivees = new();
            }

            int maxId = commandesArchivees.Count > 0 ? commandesArchivees.Max(c => c.Id) : 0;
            commandeArchiver.Id = maxId + 1;

            commandesArchivees.Add(commandeArchiver);

            var updatedArchiveJson = JsonSerializer.Serialize(commandesArchivees, _options);
            await File.WriteAllTextAsync(_archivePath, updatedArchiveJson);

            await DeleteCommande(commandeArchiver);
        }

        public async Task<List<Commande>> GetArchive()
        {
            if (!File.Exists(_archivePath))
                return new List<Commande>();

            using var reader = new StreamReader(_archivePath);
            var content = await reader.ReadToEndAsync();

            var commandes = JsonSerializer.Deserialize<List<Commande>>(content, _options)
                            ?? new List<Commande>();

            return commandes;
        }

        public async Task DeleteArchive(Commande commandeASupprimer)
        {
            if (!File.Exists(_archivePath))
                return;

            var json = await File.ReadAllTextAsync(_archivePath);
            var commandes = JsonSerializer.Deserialize<List<Commande>>(json, _options) ?? new();

            commandes.RemoveAll(c => c.Id == commandeASupprimer.Id);

            var updatedJson = JsonSerializer.Serialize(commandes, _options);
            await File.WriteAllTextAsync(_archivePath, updatedJson);
        }
        public async Task UpdateCommande(Commande commandeModifiee)
        {
            if (!File.Exists(_filePath))
                return;

            var json = await File.ReadAllTextAsync(_filePath);
            var commandes = JsonSerializer.Deserialize<List<Commande>>(json, _options) ?? new();

            var index = commandes.FindIndex(c => c.Id == commandeModifiee.Id);
            if (index != -1)
            {
                commandeModifiee.RecalculerTotal();
                commandes[index] = commandeModifiee;

                var updatedJson = JsonSerializer.Serialize(commandes, _options);
                await File.WriteAllTextAsync(_filePath, updatedJson);
            }
        }

        public void AjouterEvenementAgenda(Commande commande)
        {
#if ANDROID
            var context = Android.App.Application.Context;
            var intent = new Intent(Intent.ActionInsert);
            intent.SetData(CalendarContract.Events.ContentUri);
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Title, $"Commande - {commande.NameClient}");
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Description, "");
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.EventLocation, "");

            var debut = new Java.Util.Date(commande.DateLivraison.Year - 1900, commande.DateLivraison.Month - 1, commande.DateLivraison.Day);
            var fin = new Java.Util.Date(commande.DateLivraison.Year - 1900, commande.DateLivraison.Month - 1, commande.DateLivraison.Day + 1);

            intent.PutExtra(CalendarContract.ExtraEventBeginTime, debut.Time);
            intent.PutExtra(CalendarContract.ExtraEventEndTime, fin.Time);

            intent.PutExtra(CalendarContract.Events.InterfaceConsts.AllDay, true);
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.HasAlarm, false);

            intent.SetFlags(ActivityFlags.NewTask);
            context.StartActivity(intent);
#endif
        }

    }
}
