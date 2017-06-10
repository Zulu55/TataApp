using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TataApp.Domain
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<LoginType> LoginTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
