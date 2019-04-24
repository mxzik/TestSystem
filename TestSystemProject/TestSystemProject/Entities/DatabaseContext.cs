namespace TestSystemProject.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Test)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Test)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Theme>()
                .HasMany(e => e.Tests)
                .WithRequired(e => e.Theme)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
