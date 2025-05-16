using System;
using System.Collections.Generic;

namespace Api1.Services
{
    /// <summary>
    /// Represents a customer order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or Sets OrderId
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or Sets CustomerName
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or Sets CustomerEmail
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Gets or Sets ShippingAddress
        /// </summary>
        public string ShippingAddress { get; set; }

        /// <summary>
        /// Gets or Sets Items
        /// </summary>
        public List<OrderItem> Items { get; set; }

        /// <summary>
        /// Gets or Sets TotalAmount
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets CreatedAt
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedAt
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
} 