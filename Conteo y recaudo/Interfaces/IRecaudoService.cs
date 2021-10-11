using Conteo_y_recaudo.Entities;
using Conteo_y_recaudo.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Interfaces
{
    public interface IRecaudoService
    {
        Task InsertarRecaudosAsync(ConsultaRecaudo consultaRecaudo);
        Task<RecaudoData> GetRecaudosAsync(RecaudoSpecParams recaudoParams);
        Task<DataReporte> GetDataReporteRecaudo(RecaudoSpecParams recaudoParams);
    }
}