using Conteo_y_recaudo.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conteo_y_recaudo.Data
{
    public class RecaudoContext : DbContext
    {
        public RecaudoContext(DbContextOptions<RecaudoContext> options) : base(options)
        {
        }
        public DbSet<Recaudo> Recaudos { get; set; }
    }
}
