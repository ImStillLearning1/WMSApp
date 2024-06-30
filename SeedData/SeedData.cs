using Microsoft.EntityFrameworkCore;
using WMSApp.DbContexts;
using WMSApp.Models;

public static class SeedData
{
public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new WMSContext(
            serviceProvider.GetRequiredService<DbContextOptions<WMSContext>>()))
        {
            // Check if there is already seed data
            if (context.Products.Any() || context.Locations.Any() || context.Inventories.Any()
                || context.Transactions.Any() || context.Documents.Any())
            {
                return; // DB has been seeded
            }

            // Seed Products
            var products = new Product[]
            {
                new Product { SKU = "SKU001", Name = "Product A", Description = "Description for Product A"},
                new Product { SKU = "SKU002", Name = "Product B", Description = "Description for Product B"},
                // Add more products as needed
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            // Seed Locations
            var locations = new Location[]
            {
                new Location { Name = "Location 1", Description = "Description for Location 1" },
                new Location { Name = "Location 2", Description = "Description for Location 2" },
                // Add more locations as needed
            };
            context.Locations.AddRange(locations);
            context.SaveChanges();

            // Get newly inserted location and product IDs
            var locationIds = context.Locations.Select(l => l.LocationId).ToList();
            var productIds = context.Products.Select(p => p.ProductId).ToList();

            // Seed Inventories with valid LocationId and ProductId
            var inventories = new Inventory[]
            {
                new Inventory { ProductId = productIds[0], LocationId = locationIds[0], Quantity = 100 },
                new Inventory { ProductId = productIds[1], LocationId = locationIds[1], Quantity = 50 },
                // Add more inventory items as needed
            };
            context.Inventories.AddRange(inventories);
            context.SaveChanges();

            // Seed Transactions (assuming relationships and data structure)
            var transactions = new Transaction[]
            {
                new Transaction { TransactionDate = DateTime.Now, Quantity = 10, ProductId = productIds[0], TransactionType = TransactionType.Inbound },
                new Transaction { TransactionDate = DateTime.Now, Quantity = 5, ProductId = productIds[1], TransactionType = TransactionType.Outbound },
                // Add more transactions as needed
            };
            context.Transactions.AddRange(transactions);
            context.SaveChanges();

            // Seed Documents (assuming relationships and data structure)
            var documents = new Document[]
            {
                new Document { DocumentNumber = "PZ000001", DocumentType = DocumentType.Receiving, DocumentDate = DateTime.Now, CreatedBy = "Test User 2"},
                new Document { DocumentNumber = "WZ000001", DocumentType = DocumentType.Shipping, DocumentDate = DateTime.Now, CreatedBy = "Test User"},
                // Add more documents as needed
            };
            context.Documents.AddRange(documents);
            context.SaveChanges();
        }
    }
}
