using System;
using System.Collections.Generic;

namespace Puutarha.Models
{
    public partial class Satotiedot
    {
        public int SatoId { get; set; }
        public int KasviId { get; set; }
        public DateTime SatoPvm { get; set; }
        public decimal? Lämpötila { get; set; }
        public decimal? Määrä { get; set; }
        public string Yksikkö { get; set; }
        public string Lisätieto { get; set; }

        public virtual Kasvit Kasvi { get; set; }
    }
}
