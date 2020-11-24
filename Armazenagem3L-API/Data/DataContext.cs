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
    }
}
