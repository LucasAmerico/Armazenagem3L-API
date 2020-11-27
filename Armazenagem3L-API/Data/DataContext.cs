using Armazenagem3L_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Carga> Cargas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<CargaProduto> CargaProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<CargaProduto>().HasKey(AD => new { AD.CargaId, AD.ProdutoId });

            builder.Entity<Funcionario>()
                    .HasData(new List<Funcionario>(){
                    new Funcionario(1, "Lauro"),
                    });

            builder.Entity<Motorista>()
                    .HasData(new List<Motorista>(){
                    new Motorista(1, "Bino"),
                    });

            builder.Entity<Produto>()
                    .HasData(new List<Produto>(){
                    new Produto(1, "Playstation 5", 1, 1, 300),
                    new Produto(2, "Mouse", 1, 1, 300),
                    new Produto(3, "Teclkado", 1, 1, 300),
                    new Produto(4, "Monitor", 1, 1, 300),
                    });
        }
    }
}
