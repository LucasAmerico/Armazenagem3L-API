using Armazenagem3L_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Repositories {
    public interface ICargaRepository {

        void Add(Carga carga);
        void AddCargaProdutos(CargaProduto cargaProduto);
        Carga FindById(int Id);
        bool SaveChanges();
        Carga GetLast();
        void Update(Carga carga);
        Carga[] GetCargas();
        Carga GetCargaById(int cargaId);
        Carga[] cargaByMotoristaId(int motoristaId);
        Carga cargaByIdAndMotoristaId(int cargaId, int motoristaId);


    }
}
