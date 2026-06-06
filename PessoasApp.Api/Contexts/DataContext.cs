using Microsoft.EntityFrameworkCore;
using PessoasApp.Api.Entities;

namespace PessoasApp.Api.Contexts {
    public class DataContext : DbContext {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            optionsBuilder.UseInMemoryDatabase("BDPessoas");
        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
