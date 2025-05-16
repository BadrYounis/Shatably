using Domain;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Persistence.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbInitializer(AppDbContext dbContext, RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task InitializeIdentityAsync()
        {
            // Seed Roles
            if (!_roleManager.Roles.Any())
            {
                // Admin, SuperAdmin
                await _roleManager.CreateAsync(new IdentityRole<Guid>("Worker"));
                await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
            }
            // Seed Users
            if (!_userManager.Users.Any())
            {
                var Worker = new User()
                {
                    FirstName = "Ahmed",
                    LastName = "Mohamed",
                    UserName = "AhmedMohamed",
                    Email = "Ahmed@gmail.com",
                    PhoneNumber = "01017151157"
                };
                var User = new User()
                {
                    FirstName = "Badr",
                    LastName = "Younis",
                    UserName = "BadrYounis",
                    Email = "Badr@gmail.com",
                    PhoneNumber = "01017151157"
                };
                await _userManager.CreateAsync(User, "P@ssw0rd");
                await _userManager.CreateAsync(Worker, "Passw0rd@");
                // Assign Each Role For User
                await _userManager.AddToRoleAsync(Worker, "Worker");
                await _userManager.AddToRoleAsync(User, "User");
            }
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync();
                    if (!_dbContext.DeliveryMethods.Any())
                    {
                        //D:\Team\Final ISA\Shatably\Infrastructure\Persistence\Data\DataSeeding\delivery.json
                        var methodsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\delivery.json");
                        var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(methodsData);
                        if (methods is not null && methods.Any())
                        {
                            await _dbContext.AddRangeAsync(methods);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data Seeding Error: {ex.Message}");
            }
        }


        //public async Task InitializeAsync()
        //{
        //    try
        //    {
        //        if (_dbContext.Database.GetPendingMigrations().Any())
        //        {
        //            await _dbContext.Database.MigrateAsync();
        //            var options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            options.Converters.Add(new JsonStringEnumConverter());

        //            //Clients
        //            if (!_dbContext.Clients.Any())
        //            {
        //                //D:\Graduation Project\Shatably\Infrastructure\Persistence\Data\Seeding\Clients.json
        //                var clientsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\Clients.json");
        //                var clients = JsonSerializer.Deserialize<List<Client>>(clientsData, options);
        //                if (clients is not null && clients.Any())
        //                {
        //                    await _dbContext.AddRangeAsync(clients);
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }

        //            //Workers
        //            if (!_dbContext.Workers.Any())
        //            {
        //                //D:\Graduation Project\Shatably\Infrastructure\Persistence\Data\Seeding\Workers.json
        //                options.Converters.Add(new JsonStringEnumConverter());
        //                var workersData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\Workers.json");
        //                var workers = JsonSerializer.Deserialize<List<Worker>>(workersData, options);
        //                if (workers is not null && workers.Any())
        //                {
        //                    await _dbContext.AddRangeAsync(workers);
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }

        //            //Products
        //            if (!_dbContext.Products.Any())
        //            {
        //                //D:\Graduation Project\Shatably\Infrastructure\Persistence\Data\Seeding\Products.json
        //                var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\Products.json");
        //                var products = JsonSerializer.Deserialize<List<Product>>(productsData, options);
        //                if (products is not null && products.Any())
        //                {
        //                    await _dbContext.AddRangeAsync(products);
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }

        //            //DesignTemplates
        //            if (!_dbContext.DesignTemplates.Any())
        //            {
        //                //D:\Graduation Project\Shatably\Infrastructure\Persistence\Data\Seeding\DesignTemplates.json
        //                var designTemplatesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\DesignTemplates.json");
        //                var designTemplates = JsonSerializer.Deserialize<List<DesignTemplate>>(designTemplatesData, options);
        //                if (designTemplates is not null && designTemplates.Any())
        //                {
        //                    await _dbContext.AddRangeAsync(designTemplates);
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }

        //            //ProjectPortfolios
        //            if (!_dbContext.ProjectPortfolios.Any())
        //            {
        //                //D:\Graduation Project\Shatably\Infrastructure\Persistence\Data\Seeding\ProjectPortfolios.json
        //                var projectProtofoliosData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\ProjectPortfolios.json");
        //                var projectProtofolios = JsonSerializer.Deserialize<List<ProjectPortfolio>>(projectProtofoliosData, options);
        //                if (projectProtofolios is not null && projectProtofolios.Any())
        //                {
        //                    await _dbContext.AddRangeAsync(projectProtofolios);
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }

        //            //Orders
        //            if (!_dbContext.Orders.Any())
        //            {
        //                //D:\Graduation Project\Shatably\Infrastructure\Persistence\Data\Seeding\Orders.json
        //                var ordersData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\Orders.json");
        //                var orders = JsonSerializer.Deserialize<List<Order>>(ordersData, options);
        //                if (orders is not null && orders.Any())
        //                {
        //                    await _dbContext.AddRangeAsync(orders);
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }

        //            //OrderItems
        //            if (!_dbContext.OrderItems.Any())
        //            {
        //                //D:\Graduation Project\Shatably\Infrastructure\Persistence\Data\Seeding\OrderItems.json
        //                var orderItemsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\OrderItems.json");
        //                var orderItems = JsonSerializer.Deserialize<List<OrderItem>>(orderItemsData, options);
        //                if (orderItems is not null && orderItems.Any())
        //                {
        //                    await _dbContext.AddRangeAsync(orderItems);
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }

        //            //SelectedDesigns
        //            if (!_dbContext.SelectedDesigns.Any())
        //            {
        //                //D:\Graduation Project\Shatably\Infrastructure\Persistence\Data\Seeding\SelectedDesigns.json
        //                var selectedDesignsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\SelectedDesigns.json");
        //                var selectedDesigns = JsonSerializer.Deserialize<List<SelectedDesign>>(selectedDesignsData);
        //                if (selectedDesigns is not null && selectedDesigns.Any())
        //                {
        //                    await _dbContext.AddRangeAsync(selectedDesigns);
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }

        //            //RatingReviews
        //            if (!_dbContext.RatingReviews.Any())
        //            {
        //                //D:\Graduation Project\Shatably\Infrastructure\Persistence\Data\Seeding\RatingReviews.json
        //                var ratingReviewsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\RatingReviews.json");
        //                var ratingReviews = JsonSerializer.Deserialize<List<RatingReview>>(ratingReviewsData, options);
        //                if (ratingReviews is not null && ratingReviews.Any())
        //                {
        //                    await _dbContext.AddRangeAsync(ratingReviews);
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Data Seeding Error: {ex.Message}");
        //    }
        //}
    }
}
