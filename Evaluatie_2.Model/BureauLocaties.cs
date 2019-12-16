using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluatie_2.Model
{
    public class BureauLocaties
    {
        public int Code { get; set; }
        public string Identificatie { get; set; }
        public string Omschrijving { get; set; }

        public override string ToString()
        {
            return $"{Omschrijving}";
        }
    }
}