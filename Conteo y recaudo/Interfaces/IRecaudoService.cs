using Conteo_y_recaudo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Interfaces
{
    public interface IRecaudoService
    {
        Task<Recaudo> InsertarRecaudosAsync(List<Recaudo> recaudo);
        Task<IReadOnlyList<Recaudo>> GetRecaudosAsync();
    }
}