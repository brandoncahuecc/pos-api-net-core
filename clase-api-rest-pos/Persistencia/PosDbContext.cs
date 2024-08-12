using clase_api_rest_resources.Modelos;
using Microsoft.EntityFrameworkCore;

namespace clase_api_rest_pos.Persistencia;

public class PosDbContext : DbContext
{
    public PosDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Categoria> Categorias { get; set; }
}
