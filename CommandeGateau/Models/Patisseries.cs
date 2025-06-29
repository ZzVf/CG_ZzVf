namespace CommandeGateau.Models
{
    public class PatisserieTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public virtual double Price => 0;

        public virtual string PatisserieType => "Patisserie";
    }

    public class Biscuit : PatisserieTemplate
    {
        public bool Taille { get; set; }
        public override double Price => Taille ? 3.00 : 1.50;
        public override string PatisserieType => "Biscuit";
    }

    public class Gateau : PatisserieTemplate
    {
        public int Etage { get; set; }
        public string Gout { get; set; }
        public bool Fruite { get; set; }
        public bool Modelage { get; set; }

        public override double Price => Fruite ? 4.00 : 3.50;
        public override string PatisserieType => "Gateau";
    }

    public class CupCake : PatisserieTemplate
    {
        public override double Price => 3;
        public override string PatisserieType => "CupCake";
    }

    public class Macaron : PatisserieTemplate
    {
        public override double Price => 1.50;
        public override string PatisserieType => "Macaron";
    }

    public class MagnumCake : PatisserieTemplate
    {
        public override double Price => 2.25;
        public override string PatisserieType => "MagnumCake";
    }

    public class PopCake : PatisserieTemplate
    {
        public override double Price => 1.50;
        public override string PatisserieType => "PopCake";
    }

    public class Verrine : PatisserieTemplate
    {
        public override double Price => 3;
        public override string PatisserieType => "Verrine";
    }
}
