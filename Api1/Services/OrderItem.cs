using System;

namespace Api1.Services
{
    /// <summary>
    /// Represents an item in an order
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or Sets ProductId
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or Sets Quantity
        /// </summary>
        public int Quantity { get; set; }
    }
} 