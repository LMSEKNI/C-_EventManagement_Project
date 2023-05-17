using Microsoft.EntityFrameworkCore;



    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Activity> Activities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.Events)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Activities)
                .WithOne(a => a.Event)
                .HasForeignKey(a => a.EventId);

            base.OnModelCreating(modelBuilder);
        }
    }

