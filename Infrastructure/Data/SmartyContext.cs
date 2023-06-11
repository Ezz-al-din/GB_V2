using System.Reflection;
using Core.Entities;
using Core.Entities.Test;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SmartyContext : DbContext
    {
        public SmartyContext(DbContextOptions<SmartyContext> options) : base(options)
        {

        }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<MarkType> MarkTypes { get; set; }
        public DbSet<MarkBrand> MarkBrands { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }

        }
    }
}