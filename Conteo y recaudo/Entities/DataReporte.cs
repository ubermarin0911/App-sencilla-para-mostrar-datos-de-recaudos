using System;
using System.Collections.Generic;

namespace Conteo_y_recaudo.Entities
{
    public class RecaudosPorFechaYEstacion
    {
        public DateTime Fecha { get; set; }
        public string Estacion { get; set; }
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
        public IReadOnlyList<RecaudosPorFechaYEstacion> RecaudosFechaEstacion { get; set; }
        public IReadOnlyList<RecaudosPorEstacion> RecaudosEstacion { get; set; }
        public int TotalCantidad { get; set; }
        public long TotalValor { get; set; }
        public int TotalItems { get; set; }
    }
}