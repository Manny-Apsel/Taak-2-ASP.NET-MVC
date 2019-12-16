using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Evaluatie_2.Business.Interfaces;
using Evaluatie_2.DataAccess;
using Evaluatie_2.Model;
using Microsoft.EntityFrameworkCore;

namespace Evaluatie_2.Business
{
    public class BureauTypeLogica : IBureauTypeLogica
    {
        private readonly DatabaseContext database;

        public BureauTypeLogica(DatabaseContext _database)
        {
            database = _database;
        }

        public Task<BureauTypes> BureauType1Ophalen(int code)
        {
            return database.BureauTypes.SingleOrDefaultAsync(x => x.Code == code);
        }

        public Task<List<BureauTypes>> BureauTypesOphalen()
        {
            return database.BureauTypes.ToListAsync();
        }
    }
}
