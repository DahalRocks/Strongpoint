namespace Interfaces
{
    public interface ISupplier
    {
         int Id { get; set; }
         string Navn { get; set; }
         string Kunde_Nummer { get; set; }
         string Inkjøps_Nummer { get; set; }
         string Løpe_Nummer { get; set; }
         string Attesterad_Av { get; set; }
         string Eventuella_Kommentarer { get; set; }
    }
}