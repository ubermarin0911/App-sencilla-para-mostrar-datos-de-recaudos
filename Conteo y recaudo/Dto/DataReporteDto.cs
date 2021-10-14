using System;
using System.Collections.Generic;
using System.Text;

namespace Conteo_y_recaudo.Dto
{
    public class DataRecaudosFechaEstacionDto
    {
        public string Estacion { get; set; }
        public IReadOnlyList<FechaCantidadValorDto> FechaCantidadValor { get; set; }
        public RecaudosPorEstacionDto RecaudosEstacion { get; set; }
    }

    public class FechaCantidadValorDto
    {
        public string Fecha { get; set; }
        public int TotalCantidad { get; set; }
        public string TotalValor { get; set; }
    }

    public class RecaudosPorEstacionDto
    {
        public int TotalCantidad { get; set; }
        public string TotalValor { get; set; }
    }

    public class DataReporteDto
    {
        public IReadOnlyList<DataRecaudosFechaEstacionDto> DataRecaudosFechaEstacion { get; set; }
        public int TotalCantidad { get; set; }
        public long TotalValor { get; set; }

    }
}
