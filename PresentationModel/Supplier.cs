﻿
using Interfaces;

namespace PresentationModel
{
   public class Supplier:ISupplier
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Kunde_Nummer { get; set; }
        public string Inkjøps_Nummer { get; set; }
        public string Løpe_Nummer { get; set; }
        public string Attesterad_Av { get; set; }
        public string Eventuella_Kommentarer { get; set; }
    }
}
