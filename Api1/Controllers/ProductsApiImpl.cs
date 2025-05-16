using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductInventoryApi.Controllers;
using ProductInventoryApi.Models;
using Api1.Services;

namespace Api1.Controllers
{
    /// <summary>
    /// Implementation of the Products API
    /// </summary>
    public class ProductsApiImpl : ProductsApiController
    {
        private readonly FakeDataService _dataService;

        public ProductsApiImpl(FakeDataService dataService)
        {
            _dataService = dataService;
        }

        /// <inheritdoc />
        public override IActionResult CreateProduct([FromBody] ProductCreate productCreate)
        {
            if (productCreate == null)
            {
                return BadRequest(new Error
                {
                    Code = 400,
                    Message = "Product creation data is required"
                });
            }

            try
            {
                var product = _dataService.CreateProduct(productCreate);
                return StatusCode(201, product);
            }
            catch (Exception ex)
            {
                return BadRequest(new Error
                {
                    Code = 400,
                    Message = $"Error creating product: {ex.Message}"
                });
            }
        }

        /// <inheritdoc />
        public override IActionResult DeleteProduct([FromRoute] string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return BadRequest(new Error
                {
                    Code = 400,
                    Message = "Product ID is required"
                });
            }

            if (_dataService.DeleteProduct(productId))
            {
                return NoContent();
            }
            else
            {
                return NotFound(new Error
                {
                    Code = 404,
                    Message = $"Product not found with ID: {productId}"
                });
            }
        }

        /// <inheritdoc />
        public override IActionResult GetProductById([FromRoute] string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return BadRequest(new Error
                {
                    Code = 400,
                    Message = "Product ID is required"
                });
            }

            var product = _dataService.GetProductById(productId);
            if (product == null)
            {
                return NotFound(new Error
                {
                    Code = 404,
                    Message = $"Product not found with ID: {productId}"
                });
            }

            return Ok(product);
        }

        /// <inheritdoc />
        public override IActionResult GetProducts([FromQuery] string? category, [FromQuery] int? limit)
        {
            var products = _dataService.GetAllProducts(category, limit);
            return Ok(products);
        }

        /// <inheritdoc />
        public override IActionResult UpdateProduct([FromRoute] string productId, [FromBody] ProductUpdate productUpdate)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return BadRequest(new Error
                {
                    Code = 400,
                    Message = "Product ID is required"
                });
            }

            if (productUpdate == null)
            {
                return BadRequest(new Error
                {
                    Code = 400,
                    Message = "Product update data is required"
                });
            }

            var product = _dataService.UpdateProduct(productId, productUpdate);
            if (product == null)
            {
                return NotFound(new Error
                {
                    Code = 404,
                    Message = $"Product not found with ID: {productId}"
                });
            }

            return Ok(product);
        }
    }
} 