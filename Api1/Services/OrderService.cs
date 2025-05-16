using System;
using System.Collections.Generic;
using System.Linq;
using ProductInventoryApi.Models;
using Api1.Services;

namespace Api1.Services
{
    /// <summary>
    /// Service for managing customer orders
    /// </summary>
    public class OrderService
    {
        private readonly List<Order> _orders = new List<Order>();
        private readonly FakeDataService _dataService;
        private int _nextOrderNumber = 1000;

        public OrderService(FakeDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        public List<Order> GetAllOrders(string? customerEmail = null)
        {
            var query = _orders.AsQueryable();
            
            if (!string.IsNullOrEmpty(customerEmail))
            {
                query = query.Where(o => o.CustomerEmail.Equals(customerEmail, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

        /// <summary>
        /// Get order by ID
        /// </summary>
        public Order GetOrderById(string orderId)
        {
            if (!int.TryParse(orderId, out int id))
            {
                return null;
            }

            return _orders.FirstOrDefault(o => o.OrderId == id);
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        public Order CreateOrder(OrderCreate orderCreate)
        {
            // Check if products exist and have sufficient inventory
            foreach (var item in orderCreate.Items)
            {
                var inventory = _dataService.GetProductInventory(item.ProductId.ToString());
                if (inventory == null || inventory.Quantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient inventory for product ID {item.ProductId}");
                }
            }

            var order = new Order
            {
                OrderId = _nextOrderNumber++,
                CustomerName = orderCreate.CustomerName,
                CustomerEmail = orderCreate.CustomerEmail,
                ShippingAddress = orderCreate.ShippingAddress,
                Items = orderCreate.Items,
                Status = "Pending",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            }; 

            // Calculate total price
            order.TotalAmount = orderCreate.Items.Sum(item => 
            {
                var product = _dataService.GetProductById(item.ProductId.ToString());
                return product.Price * item.Quantity;
            });

            // Update inventory
            foreach (var item in orderCreate.Items)
            {
                UpdateInventoryForOrder(item.ProductId.ToString(), item.Quantity, true);
            }

            _orders.Add(order);
            return order;
        }

        /// <summary>
        /// Update order status
        /// </summary>
        public Order UpdateOrderStatus(string orderId, OrderStatusUpdate statusUpdate)
        {
            if (!int.TryParse(orderId, out int id))
            {
                return null;
            }

            var order = _orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return null;
            }

            order.Status = statusUpdate.Status;
            order.UpdatedAt = DateTimeOffset.UtcNow;
            
            return order;
        }

        /// <summary>
        /// Cancel an order and return items to inventory
        /// </summary>
        public bool CancelOrder(string orderId)
        {
            if (!int.TryParse(orderId, out int id))
            {
                return false;
            }

            var order = _orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return false;
            }

            // Only cancel if order is still pending
            if (order.Status != "Pending")
            {
                throw new InvalidOperationException("Cannot cancel an order that is not in Pending status");
            }

            // Return items to inventory
            foreach (var item in order.Items)
            {
                UpdateInventoryForOrder(item.ProductId.ToString(), item.Quantity, false);
            }

            order.Status = "Cancelled";
            order.UpdatedAt = DateTimeOffset.UtcNow;
            
            return true;
        }

        private void UpdateInventoryForOrder(string productId, int quantity, bool isDecrease)
        {
            var inventory = _dataService.GetProductInventory(productId);
            if (inventory == null)
            {
                throw new InvalidOperationException($"Inventory not found for product ID {productId}");
            }

            var update = new InventoryUpdate
            {
                Quantity = isDecrease ? 
                    inventory.Quantity - quantity : 
                    inventory.Quantity + quantity
            };

            _dataService.UpdateProductInventory(productId, update);
        }
    }
} 