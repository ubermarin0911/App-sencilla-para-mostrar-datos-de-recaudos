using Conteo_y_recaudo.Entities;

namespace Conteo_y_recaudo.Specifications
{
    public class RecaudoSpecification : BaseSpecification<Recaudo>
    {
        public RecaudoSpecification(RecaudoSpecParams recaudoParams)
        : base(x =>
           (string.IsNullOrEmpty(recaudoParams.Buscar) || x.Estacion.ToLower().Contains(recaudoParams.Buscar)) &&
           (string.IsNullOrEmpty(recaudoParams.Estacion) || x.Estacion == recaudoParams.Estacion) &&
           (string.IsNullOrEmpty(recaudoParams.Categoria) || x.Categoria == recaudoParams.Categoria) &&
           (string.IsNullOrEmpty(recaudoParams.Sentido) || x.Categoria == recaudoParams.Sentido)
           )
        {
            AddOrderByDescending(x => x.Fecha);
            ApplyPaging(recaudoParams.PageSize * (recaudoParams.PageIndex - 1),
            recaudoParams.PageSize);

            if (!string.IsNullOrEmpty(recaudoParams.Ordenar))
            {
                switch (recaudoParams.Ordenar)
                {
                    case "valorTabuladoAsc":
                        AddOrderBy(p => p.ValorTabulado);
                        break;

                    case "valorTabuladoDesc":
                        AddOrderByDescending(p => p.ValorTabulado);
                        break;

                    default:
                        AddOrderByDescending(n => n.Fecha);
                        break;
                }
            }
        }
    }
}
