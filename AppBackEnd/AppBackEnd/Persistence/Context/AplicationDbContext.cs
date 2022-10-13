using AppBackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AppBackEnd.Persistence.Context
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options)
        {

        }
    }
}
