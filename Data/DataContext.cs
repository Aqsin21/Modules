using Microsoft.EntityFrameworkCore;
using Modules.Models;


namespace Modules.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Register> Users { get; set; }
    }
} 
