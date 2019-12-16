using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Evaluatie_2.Model;

namespace Evaluatie_2.Business.Interfaces
{
    public interface IBureauLogica
    {
        Task<List<Bureaus>> BureausOphalen();

        Task<Bureaus> BureauOphalen(int code);

        Task BureauToevoegen(Bureaus bureau);

        Task BureauWijzigen(Bureaus bureau);

        Task BureauVerwijderen(Bureaus bureau);

        List<string> QrCodesOphalen();
    }
}