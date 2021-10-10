using Conteo_y_recaudo.Entities;
using Conteo_y_recaudo.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Services
{
    class RecaudoService : IRecaudoService
    {
        public Task<IReadOnlyList<Recaudo>> GetRecaudosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Recaudo> InsertarRecaudosAsync(List<Recaudo> recaudo)
        {
            throw new NotImplementedException();
        }
    }
}
