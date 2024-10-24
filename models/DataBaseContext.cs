using Microsoft.EntityFrameworkCore;

namespace market_api.models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Item> Items { get; set; }

        // Uncomment if needed later
        public DbSet<Invoice> Invoices { get; set; }
       
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Invoice and Item via InvoiceItem
            modelBuilder.Entity<InvoiceItem>()
                .HasKey(ii => new { ii.InvoiceId, ii.ItemId }); // Composite primary key

            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Invoice)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.InvoiceId);

            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Item)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.ItemId);

            modelBuilder.Entity<Invoice>(I => {
                I.Property(x => x.date).HasDefaultValueSql("DATE('now')");
                I.Property(x => x.Time).HasDefaultValueSql("Time('now')");
            });

            modelBuilder.Entity<InvoiceItem>()
                .Property(p => p.TotalCost)
                .HasComputedColumnSql("[Price] * [Quantity]");
        }
    }
}
