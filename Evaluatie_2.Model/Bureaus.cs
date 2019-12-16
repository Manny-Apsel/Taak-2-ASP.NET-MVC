using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluatie_2.Model
{
    public class Bureaus
    {
        public int Code { get; set; }
        public string Omschrijving { get; set; }
        public string Identificatie { get; set; }
        public string QrCode { get; set; }
        public bool Gereserveerd { get; set; }
        public BureauTypes Type { get; set; }
        public BureauLocaties Locatie { get; set; }

        public override string ToString()
        {
            if (Gereserveerd == true)
            {
                return $"({Identificatie}) {Omschrijving} [Gereserveerd] ({QrCode})";
            }
            else if (Gereserveerd == false)
            {
                return $"({Identificatie}) {Omschrijving} [Vrij] ({QrCode})";
            }
            else
            {
                return $"({Identificatie}) {Omschrijving} [Probleem met de databank] ({QrCode})";
            }
        }
    }
}