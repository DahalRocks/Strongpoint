using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strongpoint.Models
{
    public class Faktura
    {
        public int? FakturaNummer { get; set; }
        [Display(Name ="Leverendør")]
        public int? LeverendørId { get; set; }
        [ForeignKey("LeverendørId")]
        public virtual Leverendør Leverendør { get; set; }

        public DateTime? DatumIntervall { get; set; }
        [NotMapped]
        public DateTime? FraDato { get; set; }
        [NotMapped]
        public DateTime? TillDato { get; set; }
    }
}
