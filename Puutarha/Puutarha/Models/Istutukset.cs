using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Puutarha.Models
{
    public partial class Istutukset
    {
        public int IstutusId { get; set; }
        public int KasviId { get; set; }

        [DataType(DataType.Date)]
        public DateTime IstutusPvm { get; set; }
        public decimal? Määrä { get; set; }
        public string Yksikkö { get; set; }
        public string Istutuspaikka { get; set; }
        public decimal? Lämpötila { get; set; }
        public string Lisätieto { get; set; }
        public bool Poistettu { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Poistopvm { get; set; }

        public virtual Kasvit Kasvi { get; set; }
    }
}
