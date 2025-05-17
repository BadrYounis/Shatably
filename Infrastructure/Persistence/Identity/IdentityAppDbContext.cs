using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Identity
{
    public class IdentityAppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.OwnsOne(c => c.Address, address =>
                {
                    address.Property(a => a.city)
                           .HasColumnName("City")
                           .HasMaxLength(50);

                    address.Property(a => a.region)
                           .HasColumnName("Region")
                           .HasMaxLength(50);

                    address.Property(a => a.street)
                           .HasColumnName("Street")
                           .HasMaxLength(100);
                });
            });
        }
    }
}