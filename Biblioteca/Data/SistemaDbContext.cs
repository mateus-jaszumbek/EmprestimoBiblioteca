using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class SistemaDbContext : DbContext
    {
        public SistemaDbContext() { }
        public SistemaDbContext(DbContextOptions<SistemaDbContext> options) : base(options) 
        {
            
        }

        public DbSet<EmprestimoModels> Emprestimos { get; set; }

    }
}
