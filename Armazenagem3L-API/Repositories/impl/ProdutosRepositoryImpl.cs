using Armazenagem3L_API.Data;
using Armazenagem3L_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Repositories.impl
{
    public class ProdutosRepositoryImpl : IProdutosRepository
    {
        private readonly DataContext _context;

        public ProdutosRepositoryImpl(DataContext context)
        {
            _context = context;
        }

        public Produto GetProdutoById(int produtoId)
        {
            IQueryable<Produto> query = _context.Produto;

            query = query.AsNoTracking()
                        .OrderBy(p => p.Id)
                        .Where(produto => produto.Id == produtoId);

            return query.FirstOrDefault();
        }

        public void Delete(Produto produto)
        {
            _context.Remove(produto);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
