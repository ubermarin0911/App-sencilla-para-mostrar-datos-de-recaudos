using System;

namespace Conteo_y_recaudo.Entities
{
    public class Recaudo : BaseEntity
    {
        public string Estacion { get; set; }
        public string Sentido { get; set; }
        public int Hora { get; set; }
        public string Categoria { get; set; }
        public int Valor_tabulado { get; set; }
        public DateTime fecha { get; set; }
    }
}