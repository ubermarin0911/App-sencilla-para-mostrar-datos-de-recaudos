using Conteo_y_recaudo.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Interfaces
{
    public interface IRecaudoService
    {
        Task InsertarRecaudosAsync(ConsultaRecaudo consultaRecaudo);
        Task<IReadOnlyList<Recaudo>> GetRecaudosAsync();

    }
}