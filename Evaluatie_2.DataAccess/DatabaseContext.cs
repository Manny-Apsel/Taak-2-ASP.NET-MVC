using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Evaluatie_2.Model;

namespace Evaluatie_2.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Bureaus> Bureaus { get; set; }
        public DbSet<BureauLocaties> BureauLocaties { get; set; }
        public DbSet<BureauTypes> BureauTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=my.djohnnie.be;Database=PermanenteEvaluatie_2_Manny;User Id=16550;Password=QMYyfax9;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bureaus>().HasKey(x => x.Code);
            modelBuilder.Entity<BureauLocaties>().HasKey(x => x.Code);
            modelBuilder.Entity<BureauTypes>().HasKey(x => x.Code);
        }
    }
}
