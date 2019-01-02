using System;
namespace Interfaces
{
    public interface IInvoice
    {
        int? Faktura_Nummer { get; set; }
        int? Leverendør_Id { get; set; }
        ISupplier Leverendør { get; set; }
        DateTime? Datum_Intervall { get; set; }
        DateTime? FraDato { get; set; }
        DateTime? TillDato { get; set; }
        int? CurrentPage { get; set; }
        int? PageSize { get; set; }
    }
}
