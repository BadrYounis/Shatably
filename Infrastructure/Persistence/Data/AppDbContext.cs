using Domain;
using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ServicesWork> ServicesWorks { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProjectPortfolio> ProjectPortfolios { get; set; }
    public DbSet<RatingReview> RatingReviews { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());        
    }
}
