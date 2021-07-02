using api_rest.Domain.Models;
using api_rest.Domain.Services;
using api_rest.Extensions;
using api_rest.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Controllers
{
    [Route("/api/[Controller]")]
    [Authorize()]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResources>> GetAllAsync()
        {
            var Products = await _productService.ListAsync();
            var productsResources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResources>>(Products);
            return productsResources;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostAsync([FromBody] SaveProductResources resources)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var products = _mapper.Map<SaveProductResources, Product>(resources);
            var result = await _productService.SaveAsync(products);

            if (!result.Success)
            {
                BadRequest(result.Message);
            }

            var productResourses = _mapper.Map<Product, ProductResources>(result.Product);
            return Ok(productResourses);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductResources resources)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var product = _mapper.Map<SaveProductResources, Product>(resources);
            var result = await _productService.UpdateAsync(product, id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var productResources = _mapper.Map<Product, ProductResources>(result.Product);
            return Ok(productResources);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var productResource = _mapper.Map<Product, ProductResources>(result.Product);
            return Ok(productResource);
        }
    }
}
