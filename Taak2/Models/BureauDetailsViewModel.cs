using Evaluatie_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taak2.Models
{
    public class BureauDetailsViewModel
    {
        public string Omschrijving { get; set; }
        public string Identificatie { get; set; }
        public string QrCode { get; set; }
        public bool Gereserveerd { get; set; }
        public int GeselecteerdBureauType { get; set; }
        public int GeselecteerdBureauLocatie { get; set; }
        public List<BureauTypes> Type { get; set; }
        public List<BureauLocaties> Locatie { get; set; }
        public List<string> Foutboodschap { get; set; } = new List<string>();
    }
}
