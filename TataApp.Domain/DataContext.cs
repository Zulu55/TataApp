using System.Data.Entity;

namespace TataApp.Domain
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<LoginType> LoginTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
