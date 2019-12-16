using Evaluatie_2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evaluatie_2.Business.Interfaces
{
    public interface IBureauLocatieLogica
    {
        Task<List<BureauLocaties>> BureauLocatiesOphalen();

        Task<BureauLocaties> BureauLocatie1Ophalen(int code);
    }
}
