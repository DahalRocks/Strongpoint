using Interfaces;
using System;

namespace DomainModel
{
    public class Invoice: IInvoice
    {
        public int? Faktura_Nummer { get; set; }
        public int? Leverendør_Id { get; set; }
        public virtual ISupplier Leverendør { get; set; }
        public DateTime? Datum_Intervall { get; set; }
        public DateTime? FraDato { get; set; }
        public DateTime? TillDato { get; set; }
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
    }
}
