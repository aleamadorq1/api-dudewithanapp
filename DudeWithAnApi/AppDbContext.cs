using Microsoft.EntityFrameworkCore;
using DudeWithAnApi.Models;

namespace DudeWithAnApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuotePrint> QuotePrints { get; set; }
        public DbSet<QuoteTranslation> QuoteTranslations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./DudeWithAnApi.db");
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}