using Armazenagem3L_API.Data;
using Armazenagem3L_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Repositories.impl {
    public class ProdutosRepositoryImpl : IProdutosRepository {
        
        private readonly DataContext _context;

        public ProdutosRepositoryImpl(DataContext context) {
            _context = context;
        }

        public void Add(Produto produto) { 
            _context.Add(produto);
        }

        public bool SaveChanges() {
            return (_context.SaveChanges() > 0);
        }
    }
}
