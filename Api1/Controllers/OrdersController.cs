using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api1.Services;

namespace Api1.Controllers
{
    /// <summary>
    /// Controller for managing orders
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        [HttpGet]
        public IActionResult GetOrders([FromQuery] string? customerEmail)
        {
            var orders = _orderService.GetAllOrders(customerEmail);
            return Ok(orders);
        }

        /// <summary>
        /// Get an order by ID
        /// </summary>
        [HttpGet("{orderId}")]
        public IActionResult GetOrderById([FromRoute] string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return BadRequest(new { Code = 400, Message = "Order ID is required" });
            }

            var order = _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound(new { Code = 404, Message = $"Order not found with ID: {orderId}" });
            }

            return Ok(order);
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderCreate orderCreate)
        {
            if (orderCreate == null)
            {
                return BadRequest(new { Code = 400, Message = "Order creation data is required" });
            }

            try
            {
                var order = _orderService.CreateOrder(orderCreate);
                return StatusCode(201, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Code = 400, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Code = 400, Message = $"Error creating order: {ex.Message}" });
            }
        }

        /// <summary>
        /// Update an order's status
        /// </summary>
        [HttpPatch("{orderId}/status")]
        public IActionResult UpdateOrderStatus([FromRoute] string orderId, [FromBody] OrderStatusUpdate statusUpdate)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return BadRequest(new { Code = 400, Message = "Order ID is required" });
            }

            if (statusUpdate == null)
            {
                return BadRequest(new { Code = 400, Message = "Status update data is required" });
            }

            var order = _orderService.UpdateOrderStatus(orderId, statusUpdate);
            if (order == null)
            {
                return NotFound(new { Code = 404, Message = $"Order not found with ID: {orderId}" });
            }

            return Ok(order);
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        [HttpPost("{orderId}/cancel")]
        public IActionResult CancelOrder([FromRoute] string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return BadRequest(new { Code = 400, Message = "Order ID is required" });
            }

            try
            {
                if (_orderService.CancelOrder(orderId))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Code = 404, Message = $"Order not found with ID: {orderId}" });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Code = 400, Message = ex.Message });
            }
        }
    }
} 