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
        public void RecalculerTotal()
        {
            if (Patisseries == null)
            {
                Total = 0;
                return;
            }
            Total = Math.Round(Patisseries.Sum(p => p.Price * p.Quantity), 2);
            foreach (var item in Patisseries)
            {
                if (item is Gateau gateau && gateau.Modelage)
                    Total += 4.00;
            }
        }
    }
}

