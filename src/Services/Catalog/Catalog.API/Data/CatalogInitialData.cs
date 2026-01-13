using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }

        //Marten UPSERT will cater for existing records
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
    {
        new Product()
            {
                Id = Guid.Parse("b3d2c4a1-1f4e-4c3a-9f1e-1a2b3c4d5e6f"),
                Name = "Laptop Pro 15",
                Category = new List<string> { "Electronics", "Computers" },
                Description = "A high-end laptop with powerful performance.",
                ImageFile = "laptop_pro_15.png",
                Price = 1999.99M
            },
        new Product()
            {
                Id = Guid.Parse("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                Name = "Smartphone X",
                Category = new List<string> { "Electronics", "Mobile Phones" },
                Description = "A sleek smartphone with the latest features.",
                ImageFile = "smartphone_x.png",
                Price = 999.99M
            },
        new Product()
            {
                Id = Guid.Parse("a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                Name = "Smartphone 11",
                Category = new List<string> { "Electronics", "Mobile Phones" },
                Description = "A sleek smartphone with the latest features and improved camera.",
                ImageFile = "smartphone_11.png",
                Price = 1199.99M
            },
        new Product()
            {
                Id = Guid.Parse("c4d5e6f7-2a3b-4c5d-8e9f-0a1b2c3d4e5f"),
                Name = "Wireless Headphones",
                Category = new List<string> { "Electronics", "Audio" },
                Description = "Noise-cancelling wireless headphones with superior sound quality.",
                ImageFile = "wireless_headphones.png",
                Price = 299.99M
            },
        new Product() 
            {
                Id = Guid.Parse("d1e2f3a4-6b7c-8d9e-0f1a-2b3c4d5e6f7a"),
                Name = "Tablet S",
                Category = new List<string> { "Electronics", "Tablets" },
                Description = "A lightweight tablet perfect for work and entertainment.",
                ImageFile = "tablet_s.png",
                Price = 599.99M
            },
        new Product()
            {
                Id = Guid.Parse("e2f3a4b5-7c8d-9e0f-1a2b-3c4d5e6f7a8b"),
                Name = "Wireless Mouse",
                Category = new List<string> { "Electronics", "Accessories" },
                Description = "Ergonomic wireless mouse with long battery life.",
                ImageFile = "wireless_mouse.png",
                Price = 49.99M
            },
        new Product()
            {
                Id = Guid.Parse("f3a4b5c6-8d9e-0f1a-2b3c-4d5e6f7a8b9c"),
                Name = "Mechanical Keyboard",
                Category = new List<string> { "Electronics", "Accessories" },
                Description = "Durable mechanical keyboard with customizable RGB lighting.",
                ImageFile = "mechanical_keyboard.png",
                Price = 129.99M
            },
        new Product()
            {
                Id = Guid.Parse("a4b5c6d7-9e0f-1a2b-3c4d-5e6f7a8b9c0d"),
                Name = "External Hard Drive 2TB",
                Category = new List<string> { "Electronics", "Storage" },
                Description = "Portable external hard drive with 2TB capacity.",
                ImageFile = "external_hard_drive_2tb.png",
                Price = 89.99M
            },
        new Product()
            {
                Id = Guid.Parse("d5e6f7a8-3b4c-5d6e-9f0a-1b2c3d4e5f6a"),
                Name = "4K Ultra HD TV",
                Category = new List<string> { "Electronics", "Televisions" },
                Description = "A stunning 4K Ultra HD television with vibrant colors.",
                ImageFile = "4k_ultra_hd_tv.png",
                Price = 1499.99M
            },
        new Product()
            {
                Id = Guid.Parse("e6f7a8b9-4c5d-6e7f-0a1b-2c3d4e5f6a7b"),
                Name = "Gaming Console Z",
                Category = new List<string> { "Electronics", "Gaming" },
                Description = "Next-gen gaming console with immersive graphics.",
                ImageFile = "gaming_console_z.png",
                Price = 499.99M
            },
        new Product()
            {
                Id = Guid.Parse("f7a8b9c0-5d6e-7f8a-1b2c-3d4e5f6a7b8c"),
                Name = "Smartwatch Series 5",
                Category = new List<string> { "Electronics", "Wearables" },
                Description = "A stylish smartwatch with health and fitness tracking.",
                ImageFile = "smartwatch_series_5.png",
                Price = 399.99M
            },
        new Product()
            {
                Id = Guid.Parse("a8b9c0d1-6e7f-8a9b-2c3d-4e5f6a7b8c9d"),
                Name = "Bluetooth Speaker",
                Category = new List<string> { "Electronics", "Audio" },
                Description = "Portable Bluetooth speaker with deep bass and clear sound.",
                ImageFile = "bluetooth_speaker.png",
                Price = 149.99M
            },
        new Product()
            {
                Id = Guid.Parse("b9c0d1e2-7f8a-9b0c-3d4e-5f6a7b8c9d0e"),
                Name = "Digital Camera Pro",
                Category = new List<string> { "Electronics", "Cameras" },
                Description = "A professional-grade digital camera for stunning photography.",
                ImageFile = "digital_camera_pro.png",
                Price = 1299.99M
            },
    };
}
