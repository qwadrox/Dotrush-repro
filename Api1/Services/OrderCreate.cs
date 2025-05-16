using System.Collections.Generic;

namespace Api1.Services
{
    /// <summary>
    /// Data for creating a new order
    /// </summary>
    public class OrderCreate
    {
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
    }
} 