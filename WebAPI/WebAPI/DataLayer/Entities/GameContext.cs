using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataLayer.Entities
{
    public partial class GameContext : DbContext
    {
        public GameContext()
            : base("name=GameContext")
        {
        }

        public virtual DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Score>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Score>()
                .Property(e => e.Points)
                .IsUnicode(false);
        }
    }
}
