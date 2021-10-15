using System;
using System.Collections.Generic;

namespace Conteo_y_recaudo.Entities
{
    public class ConsultaRecaudosPorFechaYEstacion : BaseEntity
    {
        public string Estacion { get; set; }
        public DateTime Fecha { get; set; }
        public int TotalCantidad { get; set; }
        public string TotalValor { get; set; }
    }

    public class DataRecaudosFechaEstacion
    {
        public string Estacion { get; set; }
        public List<FechaCantidadValor> FechaCantidadValor { get; set; }
        public RecaudosPorEstacion RecaudosEstacion { get; set; }
    }

    public class FechaCantidadValor
    {
        public DateTime Fecha { get; set; }
        public int TotalCantidad { get; set; }
        public string TotalValor { get; set; }
    }

    public class RecaudosPorEstacion
    {
        public string Estacion { get; set; }
        public int TotalCantidad { get; set; }
        public string TotalValor { get; set; }
    }

    public class DataReporte
    {
        public IReadOnlyList<DataRecaudosFechaEstacion> DataRecaudosFechaEstacion { get; set; }
        public int TotalCantidad { get; set; }
        public long TotalValor { get; set; }
        public int TotalItems { get; set; }
    }
}