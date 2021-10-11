using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conteo_y_recaudo.Entities
{
    public class Recaudo : BaseEntity
    {
        public string Estacion { get; set; }
        public string Sentido { get; set; }
        public short Hora { get; set; }
        public string Categoria { get; set; }
        [Column("valor_tabulado")]
        public long ValorTabulado { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class RecaudoData
    {
        public IReadOnlyList<Recaudo> Recaudos { get; set; }
        public int TotalItems { get; set; }
    }
}