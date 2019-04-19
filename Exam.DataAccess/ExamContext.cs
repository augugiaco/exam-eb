using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exam.Infraestructure
{
    public class ExamContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }

        public ExamContext()
        {

        }

        public ExamContext(DbContextOptions<ExamContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(usr =>
            {
                usr.HasKey(x => x.IdValue);
                usr.HasIndex(x => x.Uuid).IsUnique();
            });

            modelBuilder.Entity<Location>(x =>
            {
                x.HasKey(d => d.UserId);
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}
