using System.ComponentModel.DataAnnotations;

namespace Conteo_y_recaudo.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}