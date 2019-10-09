using System;
using System.Collections.Generic;

namespace Puutarha.Models
{
    public partial class Kasvit
    {
        public Kasvit()
        {
            Istutukset = new HashSet<Istutukset>();
            Satotiedot = new HashSet<Satotiedot>();
        }

        public int KasviId { get; set; }
        public string Nimi { get; set; }
        public string Lajike { get; set; }
        public string TieteellinenNimi { get; set; }
        public byte[] Kuva { get; set; }
        public bool Monivuotinen { get; set; }
        public bool Hyötykasvi { get; set; }
        public bool Poistettu { get; set; }

        public virtual ICollection<Istutukset> Istutukset { get; set; }
        public virtual ICollection<Satotiedot> Satotiedot { get; set; }
    }
}
