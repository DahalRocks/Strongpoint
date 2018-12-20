using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strongpoint.Models
{
    public class Faktura
    {
        public int? Faktura_Nummer { get; set; }
        [Display(Name ="Leverendør")]
        public int? Leverendør_Id { get; set; }
        [ForeignKey("Leverendør_Id")]
        public virtual Leverendør Leverendør { get; set; }
        public DateTime? Datum_Intervall { get; set; }
        [NotMapped]
        public DateTime? FraDato { get; set; }
        [NotMapped]
        public DateTime? TillDato { get; set; }
        [NotMapped]
        public int? CurrentPage { get; set; }
        [NotMapped]
        public int? PageSize { get; set; }
    }
}
