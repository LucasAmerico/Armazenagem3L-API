﻿using Armazenagem3L_API.Data;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

namespace Armazenagem3L_API.Repositories.impl {
    public class MotoristaRepositoryImpl : IMotoristaRepository {
        
        private readonly DataContext _context;
        private readonly ILoggerManager _logger;

        public MotoristaRepositoryImpl(DataContext context, ILoggerManager logger) {
            _context = context;
            _logger = logger;
        }
        public Motorista FindById(int Id) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): FindById Motorista =>" + JsonSerializer.Serialize(Id));
            return _context.Motorista.AsNoTracking().OrderBy(p => p.Id).Where(m => m.Id == Id).FirstOrDefault();
        }

    }
}
