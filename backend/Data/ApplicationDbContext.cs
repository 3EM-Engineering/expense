using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseShare> ExpenseShares { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; } // 👈 Tabella di join esplicita

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔁 Evita ricorsioni/cicli FK tra GroupModel e User (CreatoreId)
            modelBuilder.Entity<GroupModel>()
                .HasOne(g => g.Creatore)
                .WithMany()
                .HasForeignKey(g => g.CreatoreId)
                .OnDelete(DeleteBehavior.Restrict); // ❌ niente cascata

            // 🔗 Configura tabella di join GroupMember (many-to-many)
            modelBuilder.Entity<GroupMember>()
                .HasKey(gm => new { gm.UserId, gm.GroupId });

            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.User)
                .WithMany(u => u.GroupMemberships)
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Restrict

            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.Membri)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade); // ✅ ok
        }
    }
}
