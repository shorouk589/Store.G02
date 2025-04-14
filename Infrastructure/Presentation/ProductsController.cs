using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{

    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ProductsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {

            var result = await _serviceManager.ProductService.GetProductByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("brands")] // Get: /api/products/brandsS
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("types")] // Get: /api/products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }


    }
}
