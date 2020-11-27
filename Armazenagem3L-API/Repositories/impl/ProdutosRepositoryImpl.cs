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

        public Produto GetProdutoById(int produtoId) {
            IQueryable<Produto> query = _context.Produto;

            query = query.AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(produto => produto.Id == produtoId);

            return query.FirstOrDefault();
        }

        public Produto[] GetProdutos() {
            IQueryable<Produto> query = _context.Produto;

            query = query.AsNoTracking()
                .OrderBy(p => p.Id);

            return query.ToArray();
        }
        
        public void Add(Produto produto) { 
            _context.Add(produto);
        }

        public bool SaveChanges() {
            return (_context.SaveChanges() > 0);
        }
    }
}
