using System;
using System.Collections.Generic;
using System.Linq;
using ProductInventoryApi.Models;

namespace Api1.Services
{
    /// <summary>
    /// A fake data service to provide in-memory storage for product and inventory data
    /// </summary>
    public class FakeDataService
    {
        private readonly List<Product> _products = new List<Product>();
        private readonly List<InventoryItem> _inventoryItems = new List<InventoryItem>();

        public FakeDataService()
        {
            // Seed with some initial data
            SeedData();
        }

        private void SeedData()
        {
            // Add some sample products
            var laptop = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Laptop",
                Description = "High-performance laptop with 16GB RAM",
                Price = 999.99,
                Category = "Electronics",
                ImageUrl = "https://example.com/images/laptop.jpg",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var smartphone = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smartphone",
                Description = "Latest smartphone with 128GB storage",
                Price = 799.99,
                Category = "Electronics",
                ImageUrl = "https://example.com/images/smartphone.jpg",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var headphones = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Headphones",
                Description = "Noise-cancelling wireless headphones",
                Price = 199.99,
                Category = "Audio",
                ImageUrl = "https://example.com/images/headphones.jpg",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            _products.Add(laptop);
            _products.Add(smartphone);
            _products.Add(headphones);

            // Add inventory items for these products
            _inventoryItems.Add(new InventoryItem
            {
                ProductId = laptop.Id,
                Product = laptop,
                Quantity = 50,
                WarehouseLocation = "Building A, Section 5, Shelf 3",
                LastUpdated = DateTimeOffset.UtcNow
            });

            _inventoryItems.Add(new InventoryItem
            {
                ProductId = smartphone.Id,
                Product = smartphone,
                Quantity = 100,
                WarehouseLocation = "Building B, Section 2, Shelf 1",
                LastUpdated = DateTimeOffset.UtcNow
            });

            _inventoryItems.Add(new InventoryItem
            {
                ProductId = headphones.Id,
                Product = headphones,
                Quantity = 75,
                WarehouseLocation = "Building A, Section 3, Shelf 2",
                LastUpdated = DateTimeOffset.UtcNow
            });
        }

        #region Product Methods

        public List<Product> GetAllProducts(string category = null, int? limit = null)
        {
            var query = _products.AsQueryable();
            
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (limit.HasValue)
            {
                query = query.Take(limit.Value);
            }

            return query.ToList();
        }

        public Product GetProductById(string productId)
        {
            if (!Guid.TryParse(productId, out Guid id))
            {
                return null;
            }

            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product CreateProduct(ProductCreate productCreate)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productCreate.Name,
                Description = productCreate.Description,
                Price = productCreate.Price,
                Category = productCreate.Category,
                ImageUrl = productCreate.ImageUrl,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            _products.Add(product);
            return product;
        }

        public Product UpdateProduct(string productId, ProductUpdate productUpdate)
        {
            if (!Guid.TryParse(productId, out Guid id))
            {
                return null;
            }

            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }

            // Update properties
            if (!string.IsNullOrEmpty(productUpdate.Name))
            {
                product.Name = productUpdate.Name;
            }

            if (productUpdate.Description != null)
            {
                product.Description = productUpdate.Description;
            }

            if (productUpdate.Price.HasValue)
            {
                product.Price = productUpdate.Price.Value;
            }

            if (!string.IsNullOrEmpty(productUpdate.Category))
            {
                product.Category = productUpdate.Category;
            }

            if (productUpdate.ImageUrl != null)
            {
                product.ImageUrl = productUpdate.ImageUrl;
            }

            product.UpdatedAt = DateTimeOffset.UtcNow;
            return product;
        }

        public bool DeleteProduct(string productId)
        {
            if (!Guid.TryParse(productId, out Guid id))
            {
                return false;
            }

            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return false;
            }

            _products.Remove(product);
            
            // Also remove inventory
            var inventory = _inventoryItems.FirstOrDefault(i => i.ProductId == id);
            if (inventory != null)
            {
                _inventoryItems.Remove(inventory);
            }

            return true;
        }

        #endregion

        #region Inventory Methods

        public List<InventoryItem> GetAllInventory()
        {
            // Ensure each inventory item has its product reference
            return _inventoryItems.Select(item => 
            {
                // Make sure product reference is up to date
                item.Product = _products.FirstOrDefault(p => p.Id == item.ProductId);
                return item;
            }).ToList();
        }

        public InventoryItem GetProductInventory(string productId)
        {
            if (!Guid.TryParse(productId, out Guid id))
            {
                return null;
            }

            var inventory = _inventoryItems.FirstOrDefault(i => i.ProductId == id);
            if (inventory != null)
            {
                // Make sure product reference is up to date
                inventory.Product = _products.FirstOrDefault(p => p.Id == inventory.ProductId);
            }

            return inventory;
        }

        public InventoryItem UpdateProductInventory(string productId, InventoryUpdate inventoryUpdate)
        {
            if (!Guid.TryParse(productId, out Guid id))
            {
                return null;
            }

            // Check if product exists
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }

            var inventory = _inventoryItems.FirstOrDefault(i => i.ProductId == id);
            if (inventory == null)
            {
                // Create new inventory item
                inventory = new InventoryItem
                {
                    ProductId = id,
                    Product = product,
                    Quantity = inventoryUpdate.Quantity,
                    WarehouseLocation = inventoryUpdate.WarehouseLocation,
                    LastUpdated = DateTimeOffset.UtcNow
                };
                
                _inventoryItems.Add(inventory);
            }
            else
            {
                // Update existing inventory
                inventory.Quantity = inventoryUpdate.Quantity;
                inventory.WarehouseLocation = inventoryUpdate.WarehouseLocation;
                inventory.LastUpdated = DateTimeOffset.UtcNow;
                inventory.Product = product;
            }

            return inventory;
        }

        #endregion
    }
} 