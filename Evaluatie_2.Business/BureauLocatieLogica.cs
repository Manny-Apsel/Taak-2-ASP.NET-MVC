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
    public class BureauLocatieLogica : IBureauLocatieLogica
    {
        private readonly DatabaseContext database;

        public BureauLocatieLogica(DatabaseContext _database)
        {
            database = _database;
        }

        public Task<BureauLocaties> BureauLocatie1Ophalen(int code)
        {
            return database.BureauLocaties.SingleOrDefaultAsync(x => x.Code == code);
        }

        public Task<List<BureauLocaties>> BureauLocatiesOphalen()
        {
            return database.BureauLocaties.ToListAsync();
        }
    }
}
