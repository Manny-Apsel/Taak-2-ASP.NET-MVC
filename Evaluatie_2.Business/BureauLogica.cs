using System;
using System.Collections.Generic;
using Evaluatie_2.Model;
using Evaluatie_2.Business.Interfaces;
using Evaluatie_2.DataAccess;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using QRCoder;

namespace Evaluatie_2.Business
{
    public class BureauLogica : IBureauLogica
    {
        private readonly DatabaseContext database;

        public BureauLogica(DatabaseContext _database)
        {
            database = _database;
        }

        public Task<Bureaus> BureauOphalen(int code)
        {
            return database.Bureaus.Include(x => x.Type).Include(x => x.Locatie).SingleOrDefaultAsync(x => x.Code == code);
            
        }

        public Task<List<Bureaus>> BureausOphalen()
        {
            return database.Bureaus.Include(x => x.Type).Include(x => x.Locatie).ToListAsync();
        }

        public async Task BureauToevoegen(Bureaus bureau)
        {
            await database.Bureaus.AddAsync(bureau);
            await database.SaveChangesAsync();
        }

        public async Task BureauVerwijderen(Bureaus bureau)
        {
            database.Bureaus.Remove(bureau);
            await database.SaveChangesAsync();
        }

        public async Task BureauWijzigen(Bureaus bureau)
        {
            await database.SaveChangesAsync();
        }

        public List<string> QrCodesOphalen()
        {
            return database.Bureaus.Select(x => x.QrCode).ToList();
        }
    }
}