using System;

namespace Conteo_y_recaudo.Entities
{
    public class Recaudo : BaseEntity
    {
        public string Estacion { get; set; }
        public string Sentido { get; set; }
        public short Hora { get; set; }
        public string Categoria { get; set; }
        public long Valor_tabulado { get; set; }
        public DateTime Fecha { get; set; }
    }
}