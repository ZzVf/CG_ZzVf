using CommandeGateau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CommandeGateau.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public string NameClient { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Note { get; set; }
        public DateTime DateCommande { get; set; } = DateTime.Now;
        public DateTime DateLivraison { get; set; }
        public List<PatisserieTemplate> Patisseries { get; set; }
        public double Total { get; set; }

    }
}

