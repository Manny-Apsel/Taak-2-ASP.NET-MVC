using Evaluatie_2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evaluatie_2.Business.Interfaces
{
    public interface IBureauTypeLogica
    {
        Task<List<BureauTypes>> BureauTypesOphalen();

        Task<BureauTypes> BureauType1Ophalen(int code);
    }
}
