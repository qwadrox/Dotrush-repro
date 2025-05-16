using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductInventoryApi.Controllers;
using ProductInventoryApi.Models;
using Api1.Services;

namespace Api1.Controllers
{
    /// <summary>
    /// Implementation of the Inventory API
    /// </summary>
    public class InventoryApiImpl : InventoryApiController
    {
        private readonly FakeDataService _dataService;

        public InventoryApiImpl(FakeDataService dataService)
        {
            _dataService = dataService;
        }

        /// <inheritdoc />
        public override IActionResult GetInventory()
        {
            var inventory = _dataService.GetAllInventory();
            return Ok(inventory);
        }

        /// <inheritdoc />
        public override IActionResult GetProductInventory([FromRoute] string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return BadRequest(new Error
                {
                    Code = 400,
                    Message = "Product ID is required"
                });
            }

            var inventory = _dataService.GetProductInventory(productId);
            if (inventory == null)
            {
                return NotFound(new Error
                {
                    Code = 404,
                    Message = $"Product not found with ID: {productId}"
                });
            }

            return Ok(inventory);
        }

        /// <inheritdoc />
        public override IActionResult UpdateProductInventory([FromRoute] string productId, [FromBody] InventoryUpdate inventoryUpdate)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return BadRequest(new Error
                {
                    Code = 400,
                    Message = "Product ID is required"
                });
            }

            if (inventoryUpdate == null)
            {
                return BadRequest(new Error
                {
                    Code = 400,
                    Message = "Inventory update data is required"
                });
            }

            var inventory = _dataService.UpdateProductInventory(productId, inventoryUpdate);
            if (inventory == null)
            {
                return NotFound(new Error
                {
                    Code = 404,
                    Message = $"Product not found with ID: {productId}"
                });
            }

            return Ok(inventory);
        }
    }
} 