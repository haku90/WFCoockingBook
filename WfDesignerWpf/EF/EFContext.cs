using Microsoft.Data.Entity;
using WfDesignerWpf.Models;

namespace WfDesignerWpf.EF
{
    public class EFContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WFDesigner;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DocumentPropertiesInfo>();
            modelBuilder.Entity<PropertyInfo>();
        }
    }
}