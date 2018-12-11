using System;
using System.Collections.Generic;

namespace Strongpoint.Models
{
    public class Leverendør
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string KundeNummer { get; set; }
        public string InkjøpsNummer { get; set; }
        public string LøpeNummer { get; set; }
        public string AttesteradAv { get; set; }
        public string EventuellaKommentarer { get; set; }
    }
}
