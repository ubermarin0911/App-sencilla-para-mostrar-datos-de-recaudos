using Conteo_y_recaudo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conteo_y_recaudo.Specifications
{
    public class RecaudoConFiltrosCantidadSpecification : BaseSpecification<Recaudo>
    {
        public RecaudoConFiltrosCantidadSpecification(RecaudoSpecParams recaudoParams)
        : base(x =>
           (string.IsNullOrEmpty(recaudoParams.Buscar) || x.Estacion.ToLower().Contains(recaudoParams.Buscar)) &&
           (string.IsNullOrEmpty(recaudoParams.Estacion) || x.Estacion == recaudoParams.Estacion) &&
           (string.IsNullOrEmpty(recaudoParams.Categoria) || x.Categoria == recaudoParams.Categoria) &&
           (string.IsNullOrEmpty(recaudoParams.Sentido) || x.Sentido == recaudoParams.Sentido))
       {
       }
    }
}
